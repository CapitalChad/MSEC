using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web.Configuration;
using Msec.Diagnostics;

namespace Msec.Personify.Configuration {
	/// <summary>
	/// A custom configuration section that provides Personify SSO settings.  This class may not be inherited.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sso", Justification = "The term 'Sso' is an abbreviation for Single Sign On.")]
	public sealed class PersonifySsoConfigurationSection : ConfigurationSection {
	// Fields
		/// <summary>
		/// The path for this section in the configuration file.  This field is read-only.
		/// </summary>
		public static readonly String ConfigurationPath = "msec.personify/personifySso";

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifySsoConfigurationSection"/> class.
		/// </summary>
		public PersonifySsoConfigurationSection() : base() { }

	// Properties
		[ConfigurationProperty("ignoredUrls")]
		[ConfigurationCollection(typeof(IgnoredUrlsElementCollection), AddItemName = "url")]
		public IgnoredUrlsElementCollection IgnoredUrls {
			get { return (IgnoredUrlsElementCollection)base["ignoredUrls"]; }
		}
		/// <summary>
		/// Gets or sets the URL to the login page.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Justification = "This term more closely matches the Personify term.")]
		[ConfigurationProperty("loginPageUrl", DefaultValue = "http://pers.msec.org/PersonifySSO/login.aspx")]
		public Uri LoginPageUrl {
			get { return (Uri)base["loginPageUrl"]; }
			set { base["loginPageUrl"] = value; }
		}
		/// <summary>
		/// Gets or sets the URL to the Personify SSO service.
		/// </summary>
		[ConfigurationProperty("serviceUrl", DefaultValue = "")]
		public Uri ServiceUrl {
			get { return (Uri)base["serviceUrl"]; }
			set { base["serviceUrl"] = value; }
		}
		/// <summary>
		/// Gets or sets the vendor's initialization block to use for encryption and decryption.
		/// </summary>
		[ConfigurationProperty("vendorBlock", IsRequired = true)]
		public String VendorBlock {
			get { return (String)base["vendorBlock"]; }
			set { base["vendorBlock"] = value; }
		}
		/// <summary>
		/// Gets or sets the vendor's identifier.
		/// </summary>
		[ConfigurationProperty("vendorIdentifier", IsRequired = true)]
		public String VendorIdentifier {
			get { return (String)base["vendorIdentifier"]; }
			set { base["vendorIdentifier"] = value; }
		}
		/// <summary>
		/// Gets or sets the vendor's password to use.
		/// </summary>
		[ConfigurationProperty("vendorPassword", IsRequired = true)]
		public String VendorPassword {
			get { return (String)base["vendorPassword"]; }
			set { base["vendorPassword"] = value; }
		}
		/// <summary>
		/// Gets or sets the vendor's user name to use.
		/// </summary>
		[ConfigurationProperty("vendorUserName", IsRequired = true)]
		public String VendorUserName {
			get { return (String)base["vendorUserName"]; }
			set { base["vendorUserName"] = value; }
		}

	// Methods
		/// <summary>
		/// Returns the configuration section values to use.
		/// </summary>
		/// <returns>A reference to the <see cref="T:PersonifySsoConfigurationSection"/> object to use.</returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration values are specified in the configuration file and are invalid.</exception>
		internal static PersonifySsoConfigurationSection GetSection() {
			return GetSection(PersonifySsoConfigurationSection.ConfigurationPath);
		}
		/// <summary>
		/// Returns the configuration section values to use.
		/// </summary>
		/// <param name="configurationPath">The path to the section in the configuration file.</param>
		/// <returns>A reference to the <see cref="T:PersonifySsoConfigurationSection"/> object to use.</returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration values are specified in the configuration file and are invalid.</exception>
		private static PersonifySsoConfigurationSection GetSection(String configurationPath) {
			Object configurationObject = WebConfigurationManager.GetSection(configurationPath);
			if (configurationObject != null) {
				PersonifySsoConfigurationSection validSection = configurationObject as PersonifySsoConfigurationSection;
				if (validSection == null) {
					throw new ConfigurationErrorsException("The configuration section at the path {0} is not of type {1}.".FormatInvariant(configurationPath, typeof(PersonifySsoConfigurationSection).FullName));
				}
				return validSection;
			}
			typeof(PersonifySsoConfigurationSection).LogInformation("No configuration section was specified in the config file.  Using a default instance.");
			return new PersonifySsoConfigurationSection();
		}
	}
}
