using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web.Configuration;
using Msec.Diagnostics;

namespace Msec.Personify.Configuration {
	/// <summary>
	/// A custom configuration section that provides Personify IM settings.
	/// This class may not be inherited.
	/// </summary>
	public sealed class PersonifyIMConfigurationSection : ConfigurationSection {
	// Fields
		/// <summary>
		/// The path for this section in the configuration file.  This field is read-only.
		/// </summary>
		public static readonly String ConfigurationPath = "msec.personify/personifyIM";

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyIMConfigurationSection"/> class.
		/// </summary>
		public PersonifyIMConfigurationSection() : base() { }

	// Properties
		/// <summary>
		/// Gets or sets the URL to the Personify IM service.
		/// </summary>
		[ConfigurationProperty("serviceUrl", DefaultValue = "")]
		public Uri ServiceUrl {
			get { return (Uri)base["serviceUrl"]; }
			set { base["serviceUrl"] = value; }
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
		/// <returns>A reference to the <see cref="T:PersonifyIMConfigurationSection"/> object to use.</returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration values are specified in the configuration file and are invalid.</exception>
		internal static PersonifyIMConfigurationSection GetSection() {
			return GetSection(PersonifyIMConfigurationSection.ConfigurationPath);
		}
		/// <summary>
		/// Returns the configuration section values to use.
		/// </summary>
		/// <param name="configurationPath">The path to the section in the configuration file.</param>
		/// <returns>A reference to the <see cref="T:PersonifyIMConfigurationSection"/> object to use.</returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration values are specified in the configuration file and are invalid.</exception>
		private static PersonifyIMConfigurationSection GetSection(String configurationPath) {
			Object configurationObject = WebConfigurationManager.GetSection(configurationPath);
			if (configurationObject != null) {
				PersonifyIMConfigurationSection validSection = configurationObject as PersonifyIMConfigurationSection;
				if (validSection == null) {
					throw new ConfigurationErrorsException("The configuration section at the path {0} is not of type {1}.".FormatInvariant(configurationPath, typeof(PersonifyIMConfigurationSection).FullName));
				}
				return validSection;
			}
			typeof(PersonifyIMConfigurationSection).LogInformation("No configuration section was specified in the config file.  Using a default instance.");
			return new PersonifyIMConfigurationSection();
		}
	}
}
