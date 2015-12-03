using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Security;
using Msec.Collections;
using Msec.Diagnostics;

using NameValueCollection = System.Collections.Specialized.NameValueCollection;
using ConfigurationErrorsException = System.Configuration.ConfigurationErrorsException;

namespace Msec.Personify.Web {
	/// <summary>
	/// A custom membership provider that uses Personify web services as a data source.  This class may not be inherited.
	/// </summary>
	public sealed class PersonifyMembershipProvider : MembershipProvider {
	// Fields
		/// <summary>
		/// The name of the application for which this instance has been created.
		/// </summary>
		private String _applicationName;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyMembershipProvider"/> class.
		/// </summary>
		public PersonifyMembershipProvider() : base() { }

	// Properties
		/// <summary>
		/// Gets the name of the application using the custom membership provider.
		/// </summary>
		/// <exception cref="System.NotSupportedException">An attempt is made to call the setter.</exception>
		public override String ApplicationName {
			get { return this._applicationName; }
			set { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets a value indicating whether the membership provider is configured to allow users to reset their passwords.
		/// </summary>
		public override Boolean EnablePasswordReset {
			get { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets a value indicating whether the membership provider is configured to allow users to retrieve their passwords.
		/// </summary>
		public override Boolean EnablePasswordRetrieval {
			get { throw new NotSupportedException(); }
		}
		/// <summary>
		///  Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
		/// </summary>
		public override Int32 MaxInvalidPasswordAttempts {
			get { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets the minimum number of special characters that must be present in a valid password.
		/// </summary>
		public override Int32 MinRequiredNonAlphanumericCharacters {
			get { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets the minimum length required for a password.
		/// </summary>
		public override Int32 MinRequiredPasswordLength {
			get { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
		/// </summary>
		public override Int32 PasswordAttemptWindow {
			get { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets a value indicating the format for storing passwords in the membership data store.
		/// </summary>
		public override MembershipPasswordFormat PasswordFormat {
			get { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets the regular expression used to evaluate a password.
		/// </summary>
		public override String PasswordStrengthRegularExpression {
			get { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
		/// </summary>
		public override Boolean RequiresQuestionAndAnswer {
			get { throw new NotSupportedException(); }
		}
		/// <summary>
		///  Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
		/// </summary>
		public override Boolean RequiresUniqueEmail {
			get { throw new NotSupportedException(); }
		}

	// Methods
		/// <summary>
		/// Processes a request to update the password for a membership user.
		/// </summary>
		/// <param name="username">The user to update the password for.</param>
		/// <param name="oldPassword">The current password for the specified user.</param>
		/// <param name="newPassword">The new password for the specified user.</param>
		/// <returns><c>true</c> if the password was updated successfully; otherwise, <c>false</c>.</returns>
		public override Boolean ChangePassword(String username, String oldPassword, String newPassword) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Processes a request to update the password question and answer for a membership user.
		/// </summary>
		/// <param name="username">The user to change the password question and answer for.</param>
		/// <param name="password">The password for the specified user.</param>
		/// <param name="newPasswordQuestion">The new password question for the specified user.</param>
		/// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
		/// <returns><c>true</c> if the password question and answer are updated successfully; otherwise, <c>false</c>.</returns>
		public override Boolean ChangePasswordQuestionAndAnswer(String username, String password, String newPasswordQuestion, String newPasswordAnswer) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Adds a new membership user to the data source.
		/// </summary>
		/// <param name="username">The user name for the new user.</param>
		/// <param name="password">The password for the new user.</param>
		/// <param name="email">The e-mail address for the new user.</param>
		/// <param name="passwordQuestion">The password question for the new user.</param>
		/// <param name="passwordAnswer">The password answer for the new user.</param>
		/// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
		/// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
		/// <param name="status">Indicates whether the user was created successfully.</param>
		/// <returns>A <see cref="T:MembershipUser"/> object populated with the information for the newly created user.</returns>
		public override MembershipUser CreateUser(String username, String password, String email, String passwordQuestion, String passwordAnswer, Boolean isApproved, Object providerUserKey, out MembershipCreateStatus status) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Removes a user from the membership data source.
		/// </summary>
		/// <param name="username">The name of the user to delete.</param>
		/// <param name="deleteAllRelatedData"><c>true</c> to delete data related to the user from the database; <c>false</c> to leave data related to the user in the database.</param>
		/// <returns><c>true</c> if the user was successfully deleted; otherwise, <c>false</c>.</returns>
		public override Boolean DeleteUser(String username, Boolean deleteAllRelatedData) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
		/// </summary>
		/// <param name="emailToMatch">The e-mail address to search for.</param>
		/// <param name="pageIndex">The 0-based index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">The total number of matched users.</param>
		/// <returns>A <see cref="T:MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/> <see cref="T:MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.</returns>
		public override MembershipUserCollection FindUsersByEmail(String emailToMatch, Int32 pageIndex, Int32 pageSize, out Int32 totalRecords) {
			this.LogVerbose("PersonifyMembershipProvider: Finding users by email {0} for page {1} with {2} records per page...", emailToMatch ?? "(NULL)", pageIndex, pageSize);
			if (pageIndex < 0)
				throw new ArgumentOutOfRangeException("pageIndex");
			if (pageSize < 1)
				throw new ArgumentOutOfRangeException("pageSize");

			if (emailToMatch == null) {
				this.LogVerbose("PersonifyMembershipProvider: Email address specified is null.  Returning 0 records.");
				totalRecords = 0;
				return new MembershipUserCollection();
			}

			try {
				Int32 take = pageSize;
				Int32 skip = pageIndex * pageSize;
				PersonifyUserQuery query = PersonifyUserQuery.ByEmailAddress(emailToMatch, skip, take);
				PagedCollection<PersonifyUser> users = query.Search();

				this.LogInformation("PersonifyMembershipProvider: FindUsersByEmail({0}, {1}, {2}, 0) return {3} records and {4} total records.", emailToMatch, pageIndex, pageSize, users.Count, users.TotalCount);
				totalRecords = users.TotalCount;
				return users.ToMembershipUserCollection(this.Name);
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely())
					throw;
				this.LogWarning("PersonifyMembershipProvider: FindUsersByName(String, Int32, Int32, &Int32) caused an unexpected error: {0}", ex);
				totalRecords = 0;
				return new MembershipUserCollection();
			}
		}
		/// <summary>
		/// Gets a collection of membership users where the user name contains the specified user name to match.
		/// </summary>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <param name="pageIndex">The 0-based index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">The total number of matched users.</param>
		/// <returns>A <see cref="T:MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/> <see cref="T:MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.</returns>
		public override MembershipUserCollection FindUsersByName(String usernameToMatch, Int32 pageIndex, Int32 pageSize, out Int32 totalRecords) {
			this.LogVerbose("PersonifyMembershipProvider: Finding users by user name {0} for page {1} with {2} records per page...", usernameToMatch ?? "(NULL)", pageIndex, pageSize);
			if (pageIndex < 0)
				throw new ArgumentOutOfRangeException("pageIndex");
			if (pageSize < 1)
				throw new ArgumentOutOfRangeException("pageSize");

			if (usernameToMatch == null) {
				this.LogVerbose("PersonifyMembershipProvider: User name specified is null.  Returning 0 records.");
				totalRecords = 0;
				return new MembershipUserCollection();
			}

			try {
				Int32 take = pageSize;
				Int32 skip = pageIndex * pageSize;
				PersonifyUserQuery query = PersonifyUserQuery.ByUserName(usernameToMatch, skip, take);
				PagedCollection<PersonifyUser> users = query.Search();

				this.LogInformation("PersonifyMembershipProvider: FindUsersByName({0}, {1}, {2}, 0) return {3} records and {4} total records.", usernameToMatch, pageIndex, pageSize, users.Count, users.TotalCount);
				totalRecords = users.TotalCount;
				return users.ToMembershipUserCollection(this.Name);
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely())
					throw;
				this.LogWarning("PersonifyMembershipProvider: FindUsersByName(String, Int32, Int32, &Int32) caused an unexpected error: {0}", ex);
				totalRecords = 0;
				return new MembershipUserCollection();
			}
		}
		/// <summary>
		/// Gets a collection of all the users in the data source in pages of data.
		/// </summary>
		/// <param name="pageIndex">The 0-based index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">The total number of matched users.</param>
		/// <returns>A <see cref="T:MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/> <see cref="T:MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.</returns>
		public override MembershipUserCollection GetAllUsers(Int32 pageIndex, Int32 pageSize, out Int32 totalRecords) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Gets the number of users currently accessing the application.
		/// </summary>
		/// <returns>The number of users currently accessing the application.</returns>
		public override Int32 GetNumberOfUsersOnline() {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Gets the password for the specified user name from the data source.
		/// </summary>
		/// <param name="username">The user to retrieve the password for.</param>
		/// <param name="answer">The password answer for the user.</param>
		/// <returns>The password for the specified user name.</returns>
		public override String GetPassword(String username, String answer) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Gets user information from the data source based on the unique identifier for the membership user.  Provides an option to update the last-activity date/time stamp for the user.
		/// </summary>
		/// <param name="username">The name of the user to get information for.</param>
		/// <param name="userIsOnline"><c>true</c> to update the last-activity date/time stamp for the user; <c>false</c> to return user information without updating the last-activity date/time stamp for the user.</param>
		/// <returns>A <see cref="T:MembershipUser"/> object populated with the specified user's information from the data source.</returns>
		public override MembershipUser GetUser(String username, Boolean userIsOnline) {
			this.LogVerbose("PersonifyMembershipProvider: Retrieving membership user by their user name {0}...", username ?? "(NULL)");
			if (username != null) {
				try {
					PersonifyUserQuery query = PersonifyUserQuery.ByUserName(username);
					PersonifyUser user = query.FirstOrDefault();

					this.LogInformation("PersonifyMembershipProvider: GetUser({0}, {1}) returned {2}.", username, userIsOnline, user != null ? user.UserName : "(NULL)");
					if (user != null)
						return user.ToMembershipUser(this.Name);
				}
				catch (Exception ex) {
					if (!ex.CanBeHandledSafely())
						throw;
					this.LogError("PersonifyMembershipProvider: GetUser(String, Boolean) caused an unexpected error: {0}", ex);
				}
			}

			this.LogVerbose("PersonifyMembershipProvider: No user was found for user name {0}.", username);
			return null;
		}
		/// <summary>
		/// Gets user information from the data source based on the unique identifier for the membership user.  Provides an option to update the last-activity date/time stamp for the user.
		/// </summary>
		/// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
		/// <param name="userIsOnline"><c>true</c> to update the last-activity date/time stamp for the user; <c>false</c> to return user information without updating the last-activity date/time stamp for the user.</param>
		/// <returns>A <see cref="T:MembershipUser"/> object populated with the specified user's information from the data source.</returns>
		public override MembershipUser GetUser(Object providerUserKey, Boolean userIsOnline) {
			return this.GetUser(providerUserKey as String, userIsOnline);
		}
		/// <summary>
		/// Gets the user name associated with the specified e-mail address.
		/// </summary>
		/// <param name="email">The e-mail address to search for.</param>
		/// <returns>The user name associated with the specified e-mail address.  If no match is found, return null.</returns>
		public override String GetUserNameByEmail(String email) {
			this.LogVerbose("PersonifyMembershipProvider: Retrieving a user name by the email address {0}...", email);
			if (email != null) {
				try {
					PersonifyUserQuery query = PersonifyUserQuery.ByEmailAddress(email);
					PersonifyUser user = query.FirstOrDefault();

					this.LogInformation("PersonifyMembershipProvider: GetUserNameByEmail({0}) returned {1}.", email, user != null ? user.UserName : "(NULL)");
					if (user != null)
						return user.UserName;
				}
				catch (Exception ex) {
					if (!ex.CanBeHandledSafely())
						throw;
					this.LogError("PersonifyMembershipProvider: GetUser caused an unexpected error: {0}", ex);
				}
			}

			this.LogVerbose("PersonifyMembershipProvider: No user name was found for email address {0}.", email);
			return null;
		}
		/// <summary>
		/// Initializes the provider.
		/// </summary>
		/// <param name="name">The friendly name of the provider.</param>
		/// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="name"/> is a null reference.
		/// -or- <paramref name="config"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="name"/> has a length of 0.</exception>
		/// <exception cref="System.InvalidOperationException">An attempt is made to call <see cref="M:ProviderBase.Initialize"/> on a provider after the provider has already been initialized.</exception>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">The configuration values provided in <paramref name="config"/> are invalid.</exception>
		public override void Initialize(String name, NameValueCollection config) {
			this.LogVerbose("PersonifyMembershipProvider: Initializing the provider...");
			// Validation and base implementation.
			if (name == null)
				throw new ArgumentNullException("name");
			if (config == null)
				throw new ArgumentNullException("config");
			base.Initialize(name, config);

			String applicationName = config.GetAndRemove("applicationName");
			if (String.IsNullOrEmpty(applicationName))
				applicationName = "Mountain States Employers Council";
			this._applicationName = applicationName;
			this.LogVerbose("PersonifyMembershipProvider: Provider initialized.");
		}
		/// <summary>
		/// Resets a user's password to a new, automatically generated password.
		/// </summary>
		/// <param name="username">The user to reset the password for.</param>
		/// <param name="answer">The password answer for the specified user.</param>
		/// <returns>The new password for the specified user.</returns>
		public override String ResetPassword(String username, String answer) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Clears a lock so that the membership user can be validated.
		/// </summary>
		/// <param name="userName">The membership user whose lock status you want to clear.</param>
		/// <returns><c>true</c> if the membership user was successfully unlocked; otherwise, <c>false</c>.</returns>
		public override Boolean UnlockUser(String userName) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Updates information about a user in the data source.
		/// </summary>
		/// <param name="user">A <see cref="T:MembershipUser"/> object that represents the user to update and the updated information for the user.</param>
		public override void UpdateUser(MembershipUser user) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Verifies that the specified user name and password exist in the data source.
		/// </summary>
		/// <param name="username">The name of the user to validate.</param>
		/// <param name="password">The password for the specified user.</param>
		/// <returns><c>true</c> if the specified username and password are valid; otherwise, <c>false</c>.</returns>
		public override Boolean ValidateUser(String username, String password) {
			this.LogVerbose("PersonifyMembershipProvider: Validating user {0}...", username);

			if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password)) {
				String encryptedCustomerToken = password;
				CustomerToken customerToken = CustomerToken.Create(encryptedCustomerToken);
				if (customerToken != null) {
					PersonifyUser user = new PersonifyUser(customerToken);
					this.LogInformation("PersonifyMembershipProvider: User name {0} with customer token {1} has been validated.", username, encryptedCustomerToken);
					Boolean isValid = username == user.UserName;
					if (isValid)
						RoleCache.Instance.RefreshRoles(username, customerToken);
					return isValid;
				}
				this.LogInformation("PersonifyMembershipProvider: User name {0} with customer token {1} is not valid.", username, password);
			}

			this.LogVerbose("PersonifyMembershipProvider: User {0} is not validated.", username);
			return false;
		}
	}
}
