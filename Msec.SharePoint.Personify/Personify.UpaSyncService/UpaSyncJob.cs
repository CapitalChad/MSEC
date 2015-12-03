using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Msec.Diagnostics;
using Msec.Personify.Services;

using SecurityException = System.Security.SecurityException;
using PropertyConstants = Microsoft.Office.Server.UserProfiles.PropertyConstants;
using UserProfile = Microsoft.Office.Server.UserProfiles.UserProfile;
using UserProfileManager = Microsoft.Office.Server.UserProfiles.UserProfileManager;
using SPServiceContext = Microsoft.SharePoint.SPServiceContext;
using SPSiteSubscriptionIdentifier = Microsoft.SharePoint.SPSiteSubscriptionIdentifier;
using SPServiceApplicationProxyGroup = Microsoft.SharePoint.Administration.SPServiceApplicationProxyGroup;
using PersonifyUniversalService = Msec.Personify.Services.PersonifyUniversalService;

namespace Msec.Personify.UpaSyncService {
	/// <summary>
	/// Represents a job that synchronizes data between Personify and the User Profile Application in SharePoint.  This class may not be inherited.
	/// </summary>
	public sealed class UpaSyncJob : DisposableBase {
	// Fields
		/// <summary>
		/// The record size of each batch.  This field is read-only.
		/// </summary>
		private readonly Int32 _batchSize;
		/// <summary>
		/// <c>true</c> if this instance is running; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isRunning;
		/// <summary>
		/// The main thread on which the job executes.
		/// </summary>
		private Thread _mainThread;
		/// <summary>
		/// Controls access to the <see cref="F:_mainThread"/> field.  This field is read-only.
		/// </summary>
		private readonly Object _mainThreadDoor = new Object();
		/// <summary>
		/// The maximum number of records to retrieve.  This field is read-only.
		/// </summary>
		private readonly Int32 _maximumRecords;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UpaSyncJob"/> class.
		/// </summary>
		public UpaSyncJob()
			: base() {
			UpaSyncConfiguration configuration = UpaSyncConfiguration.Instance;
			this._batchSize = configuration.BatchSize;
			this._maximumRecords = configuration.MaximumRecords;
		}

	// Properties
		/// <summary>
		/// Gets a value indicating if the job is running.
		/// </summary>
		public Boolean IsRunning {
			get { return this._isRunning; }
		}

	// Events
		/// <summary>
		/// Occurs when the job finishes executing.
		/// </summary>
		public event EventHandler<ExecuteFinishedEventArgs> ExecuteFinished;

	// Methods
		/// <summary>
		/// Begins execution of the job asynchronously.
		/// </summary>
		public void BeginExecute() {
			lock (this._mainThreadDoor) {
				if (!this.IsRunning) {
					this._isRunning = true;
					Thread mainThread = new Thread(this.MainLoop);
					mainThread.Start();
					this._mainThread = mainThread;
				}
			}
		}
		/// <summary>
		/// Cancels execution of the job.
		/// </summary>
		public void CancelExecute() {
			this._isRunning = false;
		}
		/// <summary>
		/// Copies all user information from the Personify Universal Service to the SharePoint User Profile Service Application.
		/// </summary>
		/// <param name="universalService">Used to retrieve the user's data from Personify.</param>
		/// <param name="userProfileManager">Used to save the user's data to SharePoint.</param>
		/// <param name="users">The sequence of users to copy.</param>
		/// <exception cref="Msec.Personify.UpaSyncService.ServiceStoppedException">This instance is not running.</exception>
		private void CopyUsersToUserProfileServiceApplication(PersonifyUniversalService universalService, UserProfileManager userProfileManager, IEnumerable<CustomerData> users) {
			Int32 totalCount = 0;
			Int32 createdProfileCount = 0;
			Int32 updatedProfileCount = 0;
			Int32 errorCount = 0;
			Int32 orphanedUserCount = 0;
			Int32 duplicateUserCount = 0;

			foreach (var user in users) {
				// Save the user's information to SharePoint's User Profile Service Application.
				try {
					String accountName = user.GetAccountName();
					String preferredName = user.GetPreferredName();
					UserProfile userProfile;
					Boolean isUpdate = userProfileManager.UserExists(accountName);
					if (isUpdate) {
						userProfile = userProfileManager.GetUserProfile(accountName);
						if ((userProfile[PropertyConstants.PreferredName].Value as String) != preferredName) {
							userProfile[PropertyConstants.PreferredName].Value = preferredName;
						}
					}
					else {
						userProfile = userProfileManager.CreateUserProfile(accountName, preferredName);
					}

					user.CopyTo(userProfile);
					userProfile.Commit();

					if (isUpdate) {
						updatedProfileCount++;
						this.LogVerbose("UpaSyncJob: User profile with account name {0} updated.", accountName);
					}
					else {
						createdProfileCount++;
						this.LogVerbose("UpaSyncJob: User profile with account name {0} created.", accountName);
					}
				}
				catch (Exception ex) {
					if (!ex.CanBeHandledSafely()) {
						throw;
					}
					errorCount++;
					this.LogError("UpaSyncJob: A user profile {0} could not be updated: {1}", user.UserName, ex);
				}
				totalCount++;
				this.EnsureIsRunning();
			}

			// Finish the job.
			this.LogInformation("UpaSyncJob: Synchronization job complete.  Of {0} total user names, {1} were created, {2} were updated, and {3} errors occurred.  In addition {4} orphaned users were found, and {5} duplicate user names exist.  For details on these values, set the logging to Verbose.",
				totalCount,
				createdProfileCount,
				updatedProfileCount,
				errorCount,
				orphanedUserCount,
				duplicateUserCount);
		}
		/// <summary>
		/// Creates a <see cref="T:UserProfileManager"/> object for the local SharePoint farm.
		/// </summary>
		/// <returns>A reference to the <see cref="T:UserProfileManager"/> created.</returns>
		/// <exception cref="System.InvalidOperationException">The local SharePoint farm can not be found.</exception>
		/// <exception cref="System.Security.SecurityException">The user for the current process does not have access to the User Profile Service Application.</exception>
		private static UserProfileManager CreateSharePointUserProfileManager() {
			UserProfileManager userProfileManager;
			try {
				SPServiceContext context = SPServiceContext.GetContext(SPServiceApplicationProxyGroup.Default, SPSiteSubscriptionIdentifier.Default);
				if (context == null) {
					throw new InvalidOperationException("The local SharePoint farm could not be found.  Make sure the services are running on the local machine.");
				}
				userProfileManager = new UserProfileManager(context);
			}
			catch (InvalidOperationException) {
				throw;
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely()) {
					throw;
				}
				throw new SecurityException("The user account under which the process is running does not have access to the User Profile Manager for the local farm.", ex);
			}
			return userProfileManager;
		}
		/// <summary>
		/// Ensures that this instance is still running.
		/// </summary>
		/// <exception cref="Msec.Personify.UpaSyncService.ServiceStoppedException">This instance is not running.</exception>
		private void EnsureIsRunning() {
			if (!this.IsRunning) {
				throw new ServiceStoppedException();
			}
		}
		/// <summary>
		/// Returns the user names for the members in the "active" committees.
		/// </summary>
		/// <param name="universalService">Used to find the committees and members.</param>
		/// <returns>The enumerable collection of user names for members in "active" committees.</returns>
		/// <exception cref="Msec.Personify.UpaSyncService.ServiceStoppedException">This instance receives a stop instruction during its operation.</exception>
		private IEnumerable<CustomerData> GetUsers(PersonifyUniversalService universalService) {
			for (Int32 i = 0; i < 9999999; i++) {
				String userNameStart = i.ToString("0000000");
				this.LogInformation("UpaSyncJob: Starting query of user names matching {0}###...", userNameStart);
				IEnumerable<CustomerData> customers = universalService.GetCustomersWhereUserNameStartsWith(userNameStart);
				foreach (var customer in customers) {
					this.LogVerbose("UpaSyncJob: Retrieved user {0} {1} ({2}).", customer.FirstName, customer.LastName, customer.UserName);
					yield return customer;
				}
			}
		}
		/// <summary>
		/// Acts as the main loop for the job.
		/// </summary>
		private void MainLoop() {
			this.LogInformation("Starting synchronization job at {0}.", DateTime.Now);
			try {
				using (PersonifyUniversalService universalService = PersonifyUniversalService.NewPersonifyUniversalService()) {
					this.EnsureIsRunning();
					UserProfileManager userProfileManager = UpaSyncJob.CreateSharePointUserProfileManager();

					this.EnsureIsRunning();
					IEnumerable<CustomerData> users = this.GetUsers(universalService);

					this.EnsureIsRunning();
					this.CopyUsersToUserProfileServiceApplication(universalService, userProfileManager, users);
				}
			}
			catch (ServiceStoppedException) {
				this.LogInformation("UpaSyncJob: Stop instruction recieved.  Exiting UPA Sync Job.");
				this.OnExecuteFinished(new ExecuteFinishedEventArgs(FinishedState.Cancelled));
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely()) {
					throw;
				}
				this._isRunning = false;
				this.LogError("UpaSyncJob: Synchronization job stopped to due error at {0}: {1}", DateTime.Now, ex);
				this.OnExecuteFinished(new ExecuteFinishedEventArgs(FinishedState.Error, ex));
			}
		}
		/// <summary>
		/// Raises the <see cref="E:ExecuteFinished"/> event.
		/// </summary>
		/// <param name="e">Provides information about how the job finished.</param>
		private void OnExecuteFinished(ExecuteFinishedEventArgs e) {
			EventHandler<ExecuteFinishedEventArgs> eventHandler = this.ExecuteFinished;
			if (eventHandler != null) {
				eventHandler(this, e);
			}
		}
	}
}
