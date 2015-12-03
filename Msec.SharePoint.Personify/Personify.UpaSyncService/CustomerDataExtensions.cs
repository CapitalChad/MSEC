using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Msec.Diagnostics;
using Msec.Personify.Services;

using PropertyConstants = Microsoft.Office.Server.UserProfiles.PropertyConstants;
using UserProfile = Microsoft.Office.Server.UserProfiles.UserProfile;
using UserProfileValueCollection = Microsoft.Office.Server.UserProfiles.UserProfileValueCollection;

namespace Msec.Personify.UpaSyncService {
	/// <summary>
	/// Provides extension methods for the <see cref="T:CustomerData"/> class.  This class may not be inherited.
	/// </summary>
	public static class CustomerDataExtensions {
	// Methods
		/// <summary>
		/// Copies all relevant fields from the customer to the user profile properties.
		/// </summary>
		/// <param name="instance">The object on which to invoke the functionality.</param>
		/// <param name="userProfile">The destination of the copy functionality.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="userProfile"/> is a null reference.</exception>
		public static void CopyTo(this CustomerData instance, UserProfile userProfile) {
			if (instance == null)
				throw new ArgumentNullException("instance");
			if (userProfile == null)
				throw new ArgumentNullException("userProfile");

			UpaSyncConfiguration configuration = UpaSyncConfiguration.Instance;
			CustomerDataExtensions.CopyTo(instance.EmailAddress, userProfile[configuration.WorkEmailPropertyName]);
			CustomerDataExtensions.CopyTo(instance.FirstName, userProfile[configuration.FirstNamePropertyName]);
			CustomerDataExtensions.CopyTo(instance.HomePhone, userProfile[configuration.HomePhonePropertyName]);
			CustomerDataExtensions.CopyTo(instance.JobTitle, userProfile[configuration.JobTitlePropertyName]);
			CustomerDataExtensions.CopyTo(instance.LastName, userProfile[configuration.LastNamePropertyName]);
			CustomerDataExtensions.CopyTo(instance.PrimaryPhone, userProfile[configuration.WorkPhonePropertyName]);
			CustomerDataExtensions.CopyTo(instance.Prefix, userProfile[configuration.PrefixPropertyName]);
			CustomerDataExtensions.CopyTo(instance.Suffix, userProfile[configuration.SuffixPropertyName]);
			CustomerDataExtensions.CopyTo(instance.MiddleName, userProfile[configuration.MiddleNamePropertyName]);
		}
		/// <summary>
		/// Copies the value specified to the property specified.
		/// </summary>
		/// <param name="value">The value to copy.</param>
		/// <param name="property">The property to which to copy the value.</param>
		private static void CopyTo(Object value, UserProfileValueCollection property) {
			Debug.Assert(property != null);
			if (property.Value != value) {
				property.Value = value;
			}
		}
		/// <summary>
		/// Returns the account name for the customer.
		/// </summary>
		/// <param name="instance">The object on which to invoke the functionality.</param>
		/// <returns>The account name for the customer.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static String GetAccountName(this CustomerData instance) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			String userName = instance.UserName;
			String accountName = UpaSyncConfiguration.Instance.GetAccountName(userName);
			return accountName;
		}
		/// <summary>
		/// Gets the preferred display name for the customer.
		/// </summary>
		/// <param name="instance">The object on which to invoke the functionality.</param>
		/// <returns>The preferred display name for the customer.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static String GetPreferredName(this CustomerData instance) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			String lastName = instance.LastName;
			String firstName = instance.FirstName;
			if (!String.IsNullOrEmpty(firstName)) {
				if (!String.IsNullOrEmpty(lastName)) {
					return firstName + " " + lastName;
				}
				return firstName;
			}
			else if (!String.IsNullOrEmpty(lastName)) {
				return lastName;
			}
			return instance.UserName;
		}
	}
}
