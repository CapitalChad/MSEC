using System;
using System.Diagnostics.CodeAnalysis;

namespace Msec.Personify.UpaSyncService {
	/// <summary>
	/// Contains the configuration settings for the UPA Sync functionality.  This class may not be inherited.  This class implements the singleton pattern.
	/// </summary>
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "The type implements the Singleton pattern, and it would be inappropriate to dipose of a singleton object.")]
	public sealed class UpaSyncConfiguration : Object {
		#region private sealed class Nested : Object {...}
		/// <summary>
		/// This class is used to make the pattern fully lazy.
		/// </summary>
		private sealed class Nested : Object {
		// Fields
			/// <summary>
			/// The sole use of the Nested class is to provide the lazy, thread-safe instance of the <see cref="T:UpaSyncConfiguration"/> object.
			/// </summary>
			internal static readonly UpaSyncConfiguration Instance = new UpaSyncConfiguration();

		// Constructors
			/// <summary>
			/// Initializes a new instance of the <see cref="T:Nested"/> class.
			/// </summary>
			private Nested() : base() { }
			/// <summary>
			/// Required in order to mark the type with 'beforefieldinit'.
			/// </summary>
			[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "The static constructor is needed to mark the type with 'beforefieldinit'.")]
			static Nested() { }
		}
		#endregion

	// Fields
#if (DEBUG)
		/// <summary>
		/// The UPA synchronization configuration section from which to load values to this instance.
		/// </summary>
		/// <remarks>This field is not read-only in the DEBUG version to assist in unit testing.</remarks>
		private LazyLoader<UpaSyncConfigurationSection> _upaSyncConfigurationSection = new LazyLoader<UpaSyncConfigurationSection>(UpaSyncConfigurationSection.GetSection);
#else
		/// <summary>
		/// The UPA synchronization configuration section from which to load values to this instance.  This field is read-only.
		/// </summary>
		private readonly LazyLoader<UpaSyncConfigurationSection> _upaSyncConfigurationSection = new LazyLoader<UpaSyncConfigurationSection>(UpaSyncConfigurationSection.GetSection);
#endif

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UpaSyncConfiguration"/> class.
		/// </summary>
		private UpaSyncConfiguration() : base() { }

	// Properties
		/// <summary>
		/// Gets the prefix for account names.
		/// </summary>
		public String AccountNamePrefix {
			get { return this._upaSyncConfigurationSection.Object.AccountNamePrefix; }
		}
		/// <summary>
		/// Gets the name of the "address line 1" user profile property.
		/// </summary>
		public String AddressLine1PropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.AddressLine1; }
		}
		/// <summary>
		/// Gets the name of the "address line 2" user profile property.
		/// </summary>
		public String AddressLine2PropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.AddressLine2; }
		}
		/// <summary>
		/// Gets the name of the "address line 3" user profile property.
		/// </summary>
		public String AddressLine3PropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.AddressLine3; }
		}
		/// <summary>
		/// Gets the name of the "city" user profile property.
		/// </summary>
		public String CityPropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.City; }
		}
		/// <summary>
		/// Gets the name of the "company" user profile property.
		/// </summary>
		public String CompanyPropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.Company; }
		}
		/// <summary>
		/// Gets the name of the "first name" user profile property.
		/// </summary>
		public String FirstNamePropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.FirstName; }
		}
		/// <summary>
		/// Gets the name of the "home phone" user profile property.
		/// </summary>
		public String HomePhonePropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.HomePhone; }
		}
		/// <summary>
		/// Gets the sole instance of the <see cref="T:UpaSyncConfiguration"/> class.
		/// </summary>
		public static UpaSyncConfiguration Instance {
			get { return UpaSyncConfiguration.Nested.Instance; }
		}
		/// <summary>
		/// Gets the name of the "job title" user profile property.
		/// </summary>
		public String JobTitlePropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.JobTitle; }
		}
		/// <summary>
		/// Gets the name of the "last name" user profile property.
		/// </summary>
		public String LastNamePropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.LastName; }
		}
		/// <summary>
		/// Gets the name of the membership provider for users.
		/// </summary>
		public String MembershipProviderName {
			get { return this._upaSyncConfigurationSection.Object.MembershipProviderName; }
		}
		/// <summary>
		/// Gets the name of the "middle name" user profile property.
		/// </summary>
		public String MiddleNamePropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.MiddleName; }
		}
		/// <summary>
		/// Gets the name of the "postalCode" user profile property.
		/// </summary>
		public String PostalCodePropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.PostalCode; }
		}
		/// <summary>
		/// Gets the name of the "prefix" user profile property.
		/// </summary>
		public String PrefixPropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.Prefix; }
		}
		/// <summary>
		/// Gets the amount of time the thread should sleep between checks for times to run.
		/// </summary>
		public TimeSpan SleepTimeout {
			get { return this._upaSyncConfigurationSection.Object.SleepTimeout; }
		}
		/// <summary>
		/// Gets the name of the "state" user profile property.
		/// </summary>
		public String StatePropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.State; }
		}
		/// <summary>
		/// Gets the name of the "suffix" user profile property.
		/// </summary>
		public String SuffixPropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.Suffix; }
		}
		/// <summary>
		/// Gets the time of day to run synchronization jobs.
		/// </summary>
		public TimeSpan TimeOfDayToRun {
			get { return this._upaSyncConfigurationSection.Object.TimeOfDayToRun; }
		}
		/// <summary>
		/// Gets the name of the "work e-mail" user profile property.
		/// </summary>
		public String WorkEmailPropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.WorkEmail; }
		}
		/// <summary>
		/// Gets the name of the "work phone" user profile property.
		/// </summary>
		public String WorkPhonePropertyName {
			get { return this._upaSyncConfigurationSection.Object.ProfilePropertyNames.WorkPhone; }
		}

	// Methods
		/// <summary>
		/// Formats a user name into an account name and returns the value.
		/// </summary>
		/// <param name="userName">The user name to format.</param>
		/// <returns>The account name represented by the user name.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="userName"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="userName"/> has a length of 0.</exception>
		public String GetAccountName(String userName) {
			if (userName == null) {
				throw new ArgumentNullException("userName");
			}
			if (userName.Length == 0) {
				throw new ArgumentException("The user name may not be empty.", "userName");
			}

			String[] parts = new String[] {
				this.AccountNamePrefix,
				this.MembershipProviderName,
				userName
			};
			String accountName = parts.Join("|");
			return accountName;
		}
#if DEBUG
		/// <summary>
		/// Reset the configuration section to its original state.
		/// </summary>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public void ResetUpaSyncConfigurationSection() {
			this._upaSyncConfigurationSection = new LazyLoader<UpaSyncConfigurationSection>(UpaSyncConfigurationSection.GetSection);
			this._upaSyncConfigurationSection.Load();
		}
		/// <summary>
		/// Sets the configuration section to use.
		/// </summary>
		/// <param name="configurationSection">The configuration section to use.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="configurationSection"/> is a null reference.</exception>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public void SetConfigurationSection(UpaSyncConfigurationSection configurationSection) {
			if (configurationSection == null) {
				throw new ArgumentNullException("configurationSection");
			}
			this._upaSyncConfigurationSection = new LazyLoader<UpaSyncConfigurationSection>(() => configurationSection);
			this._upaSyncConfigurationSection.Load();
		}
#endif
	}
}
