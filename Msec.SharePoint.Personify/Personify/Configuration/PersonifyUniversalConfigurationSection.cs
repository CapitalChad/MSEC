using System;
using System.Configuration;
using System.Web.Configuration;
using Msec.Diagnostics;

namespace Msec.Personify.Configuration {
	/// <summary>
	/// A custom configuration section that provides Personify Universal settings.  This class may not be inherited.
	/// </summary>
	public sealed class PersonifyUniversalConfigurationSection : ConfigurationSection {
	// Fields
		/// <summary>
		/// The path for this section in the configuration file.  This field is read-only.
		/// </summary>
		public static readonly String ConfigurationPath = "msec.personify/personifyUniversal";

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyUniversalConfigurationSection"/> class.
		/// </summary>
		public PersonifyUniversalConfigurationSection() : base() { }

	// Properties
		/// <summary>
		/// Gets or sets the password to use for the Personify Universal service.
		/// </summary>
		[ConfigurationProperty("password", DefaultValue = "")]
		public String Password {
			get { return (String)base["password"]; }
			set { base["password"] = value; }
		}
		/// <summary>
		/// Gets or sets the URL to the Personify Universal service.
		/// </summary>
		[ConfigurationProperty("serviceUrl", DefaultValue = "")]
		public Uri ServiceUrl {
			get { return (Uri)base["serviceUrl"]; }
			set { base["serviceUrl"] = value; }
		}
		/// <summary>
		/// Gets or sets the user name to use for the Personify Universal service.
		/// </summary>
		[ConfigurationProperty("userName", DefaultValue = "")]
		public String UserName {
			get { return (String)base["userName"]; }
			set { base["userName"] = value; }
		}

	// Methods
		/// <summary>
		/// Returns the configuration section values to use.
		/// </summary>
		/// <returns>A reference to the <see cref="T:PersonifyUniversalConfigurationSection"/> object to use.</returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration values are specified in the configuration file and are invalid.</exception>
		internal static PersonifyUniversalConfigurationSection GetSection() {
			return GetSection(PersonifyUniversalConfigurationSection.ConfigurationPath);
		}
		/// <summary>
		/// Returns the configuration section values to use.
		/// </summary>
		/// <param name="configurationPath">The path to the configuration section.</param>
		/// <returns>A reference to the <see cref="T:PersonifyUniversalConfigurationSection"/> object to use.</returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration values are specified in the configuration file and are invalid.</exception>
		internal static PersonifyUniversalConfigurationSection GetSection(String configurationPath) {
			Object configurationObject = WebConfigurationManager.GetSection(configurationPath);
			if (configurationObject != null) {
				PersonifyUniversalConfigurationSection validSection = configurationObject as PersonifyUniversalConfigurationSection;
				if (validSection == null) {
					throw new ConfigurationErrorsException("The configuration section at the path {0} is not of type {1}.".FormatInvariant(configurationPath, typeof(PersonifyUniversalConfigurationSection).FullName));
				}
				return validSection;
			}
			typeof(PersonifyUniversalConfigurationSection).LogInformation("No configuration section was specified in the config file.  Using a default instance.");
			return new PersonifyUniversalConfigurationSection();
		}
	}
}
