using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web.Security;
using Msec.Diagnostics;
using Msec.Personify.Services;

using HttpContext = System.Web.HttpContext;
using SPFederationAuthenticationModule = Microsoft.SharePoint.IdentityModel.SPFederationAuthenticationModule;

namespace Msec.Personify.Web {
	/// <summary>
	/// Provides extension methods for the <see cref="T:PersonifyUser"/> class.  This class may not be inherited.
	/// </summary>
	internal static class PersonifyUserExtensions {
	// Methods
		/// <summary>
		/// Sets the user as the current user for the HTTP request.
		/// </summary>
		/// <param name="instance">The instance on which to invoke the functionality.</param>
		/// <exception cref="System.ArgumentNullException"></exception>
		/// <exception cref="System.IdentityModel.Tokens.SecurityTokenException">An error occurs while communicating with the Security Token Service to issue a bearer security token.
		/// -or- An error occurs while writing the bearer security token out as a session security token.</exception>
		public static void SetAsCurrent(this PersonifyUser instance) {
			if (instance == null)
				throw new ArgumentNullException("instance");

			HttpContext context = HttpContext.Current;
			if (context == null)
				throw new InvalidOperationException("There is no current HTTP context on which to perform this operation.");

			SecurityToken securityToken;
			try {
				typeof(PersonifyUserExtensions).LogVerbose("PersonifyUserExtensions: Requesting a SecurityToken from the STS...");
				using (SecurityTokenService service = SecurityTokenService.NewSecurityTokenService()) {
					String membershipProviderName = Membership.Providers.Cast<MembershipProvider>().OfType<PersonifyMembershipProvider>().First().Name;
					String roleProviderName = Roles.Providers.Cast<RoleProvider>().OfType<PersonifyRoleProvider>().First().Name;
					//Boolean isPersisted = true;
					//String userNameSecurityTokenId = new String[] { membershipProviderName, roleProviderName, isPersisted.ToString() }.Join(":");
					String userNameSecurityTokenId = new String[] { membershipProviderName, roleProviderName }.Join(":");
					UserNameSecurityToken onBehalfOf = new UserNameSecurityToken(instance.UserName, instance.CustomerToken, userNameSecurityTokenId);
					typeof(PersonifyUserExtensions).LogVerbose("PersonifyUserExtensions: Requesting a SecurityToken on behalf of [UserName: {0}, CustomerToken: {1}, Identifier: {2}]...", instance.UserName, instance.CustomerToken, userNameSecurityTokenId);
					securityToken = service.IssueBearerSecurityToken(onBehalfOf, new Uri(context.Request.Url.OriginalString));
					typeof(PersonifyUserExtensions).LogInformation("PersonifyUserExtensions: STS issues issued a SecurityToken successfully.");
				}
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely())
					throw;
				throw new SecurityTokenException("A bearer security token could not be retrieved from the Security Token Service.", ex);
			}

			try {
				SPFederationAuthenticationModule federationAuthenticationModule = SPFederationAuthenticationModule.Current;
				federationAuthenticationModule.SetPrincipalAndWriteSessionToken(securityToken);
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely()) {
					throw;
				}
				throw new SecurityTokenException("The security token could not be written as a session security token.", ex);
			}
		}
		/// <summary>
		/// Returns the <see cref="T:MembershipUser"/> representation of the <see cref="T:PersonifyUser"/>.
		/// </summary>
		/// <param name="instance">The <see cref="T:PersonifyUser"/> to convert.</param>
		/// <param name="providerName">The name of the provider creating the <see cref="T:MembershipUser"/>.</param>
		/// <returns>A reference to the <see cref="T:MembershipUser"/> created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static MembershipUser ToMembershipUser(this PersonifyUser instance, String providerName) {
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (String.IsNullOrEmpty(providerName) || providerName.Trim().Length == 0)
				providerName = Membership.Provider.Name;

			Boolean isApproved = true;
			Boolean isNotLoggedOut = false;
			return new MembershipUser(providerName,
				instance.DisplayName,
				instance.UserName,
				instance.EmailAddress,
				String.Empty,
				String.Empty,
				isApproved,
				isNotLoggedOut,
				DateTime.Now,
				DateTime.Now,
				DateTime.Now,
				DateTime.Now,
				DateTime.Now);
		}
		/// <summary>
		/// Returns the <see cref="T:MembershipUserCollection"/> representation of the <see cref="T:PersonifyUser"/> collection.
		/// </summary>
		/// <param name="instance">The collection of <see cref="T:PersonifyUser"/> objects to convert.</param>
		/// <param name="providerName">The name of the provider creating the <see cref="T:MembershipUserCollection"/>.</param>
		/// <returns>A reference to the <see cref="T:MembershipUserCollection"/> created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static MembershipUserCollection ToMembershipUserCollection(this IEnumerable<PersonifyUser> instance, String providerName) {
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (String.IsNullOrEmpty(providerName) || providerName.Trim().Length == 0)
				providerName = Membership.Provider.Name;

			MembershipUserCollection membershipUserCollection = new MembershipUserCollection();
			foreach (PersonifyUser user in instance)
				if (user != null)
					membershipUserCollection.Add(user.ToMembershipUser(providerName));

			return membershipUserCollection;
		}
	}
}
