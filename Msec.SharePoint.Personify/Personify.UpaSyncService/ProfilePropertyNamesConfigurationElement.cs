using System;
using System.Configuration;

using PropertyConstants = Microsoft.Office.Server.UserProfiles.PropertyConstants;

namespace Msec.Personify.UpaSyncService {
	/// <summary>
	/// A strongly-typed configuration element that provides user profile property names.  This class may not be inherited.
	/// </summary>
	public sealed class ProfilePropertyNamesConfigurationElement : ConfigurationElement {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ProfilePropertyNamesConfigurationElement"/> class.
		/// </summary>
		public ProfilePropertyNamesConfigurationElement() : base() { }

	// Properties
		/// <summary>
		/// Gets or sets the name of the "address line 1" user profile property.
		/// </summary>
		[ConfigurationProperty("addressLine1", DefaultValue = "AddressLine1")]
		public String AddressLine1 {
			get { return (String)base["addressLine1"]; }
			set { base["addressLine1"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "address line 2" user profile property.
		/// </summary>
		[ConfigurationProperty("addressLine2", DefaultValue = "AddressLine2")]
		public String AddressLine2 {
			get { return (String)base["addressLine2"]; }
			set { base["addressLine2"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "address line 3" user profile property.
		/// </summary>
		[ConfigurationProperty("addressLine3", DefaultValue = "AddressLine3")]
		public String AddressLine3 {
			get { return (String)base["addressLine3"]; }
			set { base["addressLine3"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "city" user profile property.
		/// </summary>
		[ConfigurationProperty("city", DefaultValue = "City")]
		public String City {
			get { return (String)base["city"]; }
			set { base["city"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "company" user profile property.
		/// </summary>
		[ConfigurationProperty("company", DefaultValue = "Company")]
		public String Company {
			get { return (String)base["company"]; }
			set { base["company"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "first name" user profile property.
		/// </summary>
		[ConfigurationProperty("firstName", DefaultValue = PropertyConstants.FirstName)]
		public String FirstName {
			get { return (String)base["firstName"]; }
			set { base["firstName"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "home phone" user profile property.
		/// </summary>
		[ConfigurationProperty("homePhone", DefaultValue = PropertyConstants.HomePhone)]
		public String HomePhone {
			get { return (String)base["homePhone"]; }
			set { base["homePhone"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "job title" user profile property.
		/// </summary>
		[ConfigurationProperty("jobTitle", DefaultValue = PropertyConstants.JobTitle)]
		public String JobTitle {
			get { return (String)base["jobTitle"]; }
			set { base["jobTitle"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "last name" user profile property.
		/// </summary>
		[ConfigurationProperty("lastName", DefaultValue = PropertyConstants.LastName)]
		public String LastName {
			get { return (String)base["lastName"]; }
			set { base["lastName"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "middle name" user profile property.
		/// </summary>
		[ConfigurationProperty("middleName", DefaultValue = "MiddleName")]
		public String MiddleName {
			get { return (String)base["middleName"]; }
			set { base["middleName"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "postalCode" user profile property.
		/// </summary>
		[ConfigurationProperty("postalCode", DefaultValue = "PostalCode")]
		public String PostalCode {
			get { return (String)base["postalCode"]; }
			set { base["postalCode"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "prefix" user profile property.
		/// </summary>
		[ConfigurationProperty("prefix", DefaultValue = "Prefix")]
		public String Prefix {
			get { return (String)base["prefix"]; }
			set { base["prefix"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "state" user profile property.
		/// </summary>
		[ConfigurationProperty("state", DefaultValue = "State")]
		public String State {
			get { return (String)base["state"]; }
			set { base["state"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "suffix" user profile property.
		/// </summary>
		[ConfigurationProperty("suffix", DefaultValue = "Suffix")]
		public String Suffix {
			get { return (String)base["suffix"]; }
			set { base["suffix"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "work e-mail" user profile property.
		/// </summary>
		[ConfigurationProperty("workEmail", DefaultValue = PropertyConstants.WorkEmail)]
		public String WorkEmail {
			get { return (String)base["workEmail"]; }
			set { base["workEmail"] = value; }
		}
		/// <summary>
		/// Gets or sets the name of the "work phone" user profile property.
		/// </summary>
		[ConfigurationProperty("workPhone", DefaultValue = PropertyConstants.WorkPhone)]
		public String WorkPhone {
			get { return (String)base["workPhone"]; }
			set { base["workPhone"] = value; }
		}
	}
}
