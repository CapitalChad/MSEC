using System;
using System.Configuration;
using Msec.Diagnostics;

namespace Msec.Personify.UpaSyncService {
	/// <summary>
	/// A strongly-typed configuration section that contains settings for User Profile Application synchronization jobs.  This class may not be inherited.
	/// </summary>
	public sealed class UpaSyncConfigurationSection : ConfigurationSection {
	// Fields
		/// <summary>
		/// The configuration path for the section.
		/// </summary>
		public static String ConfigurationPath = "msec.personify/upaSync";

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UpaSyncConfigurationSection"/> class.
		/// </summary>
		public UpaSyncConfigurationSection() : base() { }

	// Properties
		/// <summary>
		/// Gets or sets the prefix for account names.
		/// </summary>
		[ConfigurationProperty("accountNamePrefix", DefaultValue = "i:0#.f")]
		public String AccountNamePrefix {
			get { return (String)base["accountNamePrefix"]; }
			set { base["accountNamePrefix"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the membership provider for users.
		/// </summary>
		[ConfigurationProperty("membershipProviderName", DefaultValue = "personifymembershipprovider")]
		public String MembershipProviderName {
			get { return (String)base["membershipProviderName"]; }
			set { base["membershipProviderName"] = value; }
		}
		/// <summary>
		/// Gets the element that contains the user profile property names.
		/// </summary>
		[ConfigurationProperty("profilePropertyNames")]
		public ProfilePropertyNamesConfigurationElement ProfilePropertyNames {
			get { return (ProfilePropertyNamesConfigurationElement)base["profilePropertyNames"]; }
		}
		/// <summary>
		/// Gets or sets the amount of time the thread should sleep between checks for times to run.
		/// </summary>
		[ConfigurationProperty("sleepTimeout", DefaultValue = "0:01:00.000")]
		public TimeSpan SleepTimeout {
			get { return (TimeSpan)base["sleepTimeout"]; }
			set { base["sleepTimeout"] = value; }
		}
		/// <summary>
		/// Gets or sets the time of day to run synchronization jobs.
		/// </summary>
		[ConfigurationProperty("timeOfDayToRun", DefaultValue = "1:00:00.000")]
		public TimeSpan TimeOfDayToRun {
			get { return (TimeSpan)base["timeOfDayToRun"]; }
			set { base["timeOfDayToRun"] = value; }
		}

	// Methods
		/// <summary>
		/// Returns the configuration section to use.
		/// </summary>
		/// <returns>The configuration section to use.</returns>
		internal static UpaSyncConfigurationSection GetSection() {
			UpaSyncConfigurationSection validSection;

			Object section = ConfigurationManager.GetSection(UpaSyncConfigurationSection.ConfigurationPath);
			if (section == null) {
				validSection = new UpaSyncConfigurationSection();
				validSection.LogInformation("The configuration section at path {0} does not exist.  Using the default settings.", UpaSyncConfigurationSection.ConfigurationPath);
				return validSection;
			}

			validSection = section as UpaSyncConfigurationSection;
			if (validSection == null) {
				validSection = new UpaSyncConfigurationSection();
				validSection.LogInformation("The configuration section at path {0} is not of type {1}.  Using the default settings.", UpaSyncConfigurationSection.ConfigurationPath, typeof(UpaSyncConfigurationSection).AssemblyQualifiedName);
				return validSection;
			}

			return validSection;
		}
	}
}
