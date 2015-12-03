using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Msec.Linq;
using Msec.Personify.Services;

using NetworkCredential = System.Net.NetworkCredential;

namespace Msec.Personify.Configuration {
	/// <summary>
	/// Represents the configuration for the types in the <see cref="N:Msec.Personify"/> namespace.  This class may not be inherited.  This class implements the singleton pattern.
	/// </summary>
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Since this is a singleton, Dispose() would muddle the implementation.")]
	public sealed class PersonifyConfiguration : Object {
		#region private sealed class Nested : Object {...}
		/// <summary>
		/// This class is used to make the pattern fully lazy.  This class may not be inherited.
		/// </summary>
		private sealed class Nested : Object {
		// Fields
			/// <summary>
			/// The sole use of the Nested class is to provide the lazy, thread-safe instance of the <see cref="T:PersonifyConfiguration"/> object.  This field is read-only.
			/// </summary>
			internal static readonly PersonifyConfiguration Instance = new PersonifyConfiguration();

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
		private IEnumerable<String> _ignoredUrls;
#if DEBUG
		private LazyLoader<PersonifyIMConfigurationSection> _personifyIMConfigurationSection = new LazyLoader<PersonifyIMConfigurationSection>(PersonifyIMConfigurationSection.GetSection);
		/// <summary>
		/// The SSO configuration section from which to load the values for this instance.
		/// </summary>
		/// <remarks>This field is not read-only in the DEBUG version to assist in unit testing.</remarks>
		private LazyLoader<PersonifySsoConfigurationSection> _personifySsoConfigurationSection = new LazyLoader<PersonifySsoConfigurationSection>(PersonifySsoConfigurationSection.GetSection);
		/// <summary>
		/// The Universal configuration section from which to load the values for this instance.
		/// </summary>
		/// <remarks>This field is not read-only in the DEBUG version to assist in unit testing.</remarks>
		private LazyLoader<PersonifyUniversalConfigurationSection> _personifyUniversalConfigurationSection = new LazyLoader<PersonifyUniversalConfigurationSection>(PersonifyUniversalConfigurationSection.GetSection);
#else
		private readonly LazyLoader<PersonifyIMConfigurationSection> _personifyIMConfigurationSection = new LazyLoader<PersonifyIMConfigurationSection>(PersonifyIMConfigurationSection.GetSection);
		/// <summary>
		/// The SSO configuration section from which to load the values for this instance.  This field is read-only.
		/// </summary>
		private readonly LazyLoader<PersonifySsoConfigurationSection> _personifySsoConfigurationSection = new LazyLoader<PersonifySsoConfigurationSection>(PersonifySsoConfigurationSection.GetSection);
		/// <summary>
		/// The Universal configuration section from which to load the values for this instance.  This field is read-only.
		/// </summary>
		private readonly LazyLoader<PersonifyUniversalConfigurationSection> _personifyUniversalConfigurationSection = new LazyLoader<PersonifyUniversalConfigurationSection>(PersonifyUniversalConfigurationSection.GetSection);
#endif

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyConfiguration"/> class.
		/// </summary>
		private PersonifyConfiguration() : base() { }

	// Properties
		public IEnumerable<String> IgnoredUrls {
			get {
				if (this._ignoredUrls == null) {
					PersonifySsoConfigurationSection ssoConfiguration = this._personifySsoConfigurationSection.Object;
					if (ssoConfiguration.IgnoredUrls != null)
						this._ignoredUrls = this._personifySsoConfigurationSection.Object.IgnoredUrls
							.Cast<IgnoredUrlElement>()
							.Select(element => element.Path)
							.ToArray();
					else
						this._ignoredUrls = new String[0];
				}
				return this._ignoredUrls;
			}
		}
		/// <summary>
		/// Gets the base URL for the IM service.
		/// </summary>
		public Uri IMServiceUrl {
			get { return this._personifyIMConfigurationSection.Object.ServiceUrl; }
		}
		/// <summary>
		/// Gets the credentials to use for the IM service.
		/// </summary>
		public NetworkCredential IMCredentials {
			get {
				String userName = this._personifyIMConfigurationSection.Object.VendorUserName;
				String password = this._personifyIMConfigurationSection.Object.VendorPassword;
				if (userName == null || password == null)
					return null;
				return new NetworkCredential(userName, password);
			}
		}
		/// <summary>
		/// Gets the sole instance of the <see cref="T:PersonifyConfiguration"/> class.
		/// </summary>
		public static PersonifyConfiguration Instance {
			get { return PersonifyConfiguration.Nested.Instance; }
		}
		/// <summary>
		/// Gets the URL to the login page.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Justification = "This matches the Personify term.")]
		public Uri LoginPageUrl {
			get { return this._personifySsoConfigurationSection.Object.LoginPageUrl; }
		}
		/// <summary>
		/// Gets the URL of the Personify SSO service.
		/// </summary>
		public Uri SsoServiceUrl {
			get { return this._personifySsoConfigurationSection.Object.ServiceUrl; }
		}
		/// <summary>
		/// Gets the vendor credentials to use when connecting to the Personify SSO service.
		/// </summary>
		public VendorCredentials SsoVendorCredentials {
			get {
				String userName = this.VendorUserName;
				String password = this.VendorPassword;
				String block = this.VendorBlock;
				if (userName == null || password == null || block == null) {
					return null;
				}
				return new VendorCredentials(userName, password, block);
			}
		}
		/// <summary>
		/// Gets the credentials use when communicating with the Personify Universal service.
		/// </summary>
		public NetworkCredential UniversalServiceCredentials {
			get {
				return this._personifyUniversalConfigurationSection.Object
					.Project(section => new NetworkCredential(section.UserName, section.Password));
			}
		}
		/// <summary>
		/// Gets the URL of the Personify Universal service.
		/// </summary>
		public Uri UniversalServiceUrl {
			get { return this._personifyUniversalConfigurationSection.Object.ServiceUrl; }
		}
		/// <summary>
		/// Gets the vendor's initialization block to use for encryption and decryption.
		/// </summary>
		public String VendorBlock {
			get { return this._personifySsoConfigurationSection.Object.VendorBlock; }
		}
		/// <summary>
		/// Gets the vendor's identifier.
		/// </summary>
		public String VendorIdentifier {
			get { return this._personifySsoConfigurationSection.Object.VendorIdentifier; }
		}
		/// <summary>
		/// Gets the vendor's password to use.
		/// </summary>
		public String VendorPassword {
			get { return this._personifySsoConfigurationSection.Object.VendorPassword; }
		}
		/// <summary>
		/// Gets the vendor's user name to use.
		/// </summary>
		public String VendorUserName {
			get { return this._personifySsoConfigurationSection.Object.VendorUserName; }
		}

	// Methods
#if DEBUG
		/// <summary>
		/// Reset the configuration section to its original state.
		/// </summary>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sso", Justification = "The term 'Sso' is an abbreviation for Single Sign On.")]
		public void ResetSsoConfigurationSection() {
			this._personifySsoConfigurationSection = new LazyLoader<PersonifySsoConfigurationSection>(PersonifySsoConfigurationSection.GetSection);
			this._personifySsoConfigurationSection.Load();
		}
		/// <summary>
		/// Reset the configuration section to its original state.
		/// </summary>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public void ResetUniversalConfigurationSection() {
			this._personifyUniversalConfigurationSection = new LazyLoader<PersonifyUniversalConfigurationSection>(PersonifyUniversalConfigurationSection.GetSection);
			this._personifyUniversalConfigurationSection.Load();
		}
		/// <summary>
		/// Sets the configuration section to use.
		/// </summary>
		/// <param name="configurationSection">The configuration section to use.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="configurationSection"/> is a null reference.</exception>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public void SetConfigurationSection(PersonifySsoConfigurationSection configurationSection) {
			this._personifySsoConfigurationSection = new LazyLoader<PersonifySsoConfigurationSection>(() => configurationSection);
			this._personifySsoConfigurationSection.Load();
		}
		/// <summary>
		/// Sets the configuration section to use.
		/// </summary>
		/// <param name="configurationSection">The configuration section to use.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="configurationSection"/> is a null reference.</exception>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public void SetConfigurationSection(PersonifyUniversalConfigurationSection configurationSection) {
			this._personifyUniversalConfigurationSection = new LazyLoader<PersonifyUniversalConfigurationSection>(() => configurationSection);
			this._personifyUniversalConfigurationSection.Load();
		}
#endif
	}
}
