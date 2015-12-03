using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Security;
using Msec.Collections;
using Msec.Diagnostics;

using NameValueCollection = System.Collections.Specialized.NameValueCollection;

namespace Msec.Personify.Web {
	/// <summary>
	/// A custom role provider that uses Personify web services as a data source.  This class may not be inherited.
	/// </summary>
	public sealed class PersonifyRoleProvider : RoleProvider {
	// Fields
		/// <summary>
		/// The name of the application for which this instance has been created.
		/// </summary>
		private String _applicationName;
	
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyRoleProvider"/> class.
		/// </summary>
		public PersonifyRoleProvider() : base() { }

	// Properties
		/// <summary>
		/// Gets the name of the application to store and retrieve role information for.
		/// </summary>
		public override String ApplicationName {
			get { return this._applicationName; }
			set { throw new NotSupportedException(); }
		}

	// Methods
		/// <summary>
		/// Adds the specified user names to the specified roles for the configured applicationName.
		/// </summary>
		/// <param name="usernames">A string array of user names to be added to the specified roles.</param>
		/// <param name="roleNames">A string array of the role names to add the specified user names to.</param>
		public override void AddUsersToRoles(String[] usernames, String[] roleNames) {
			throw new NotImplementedException();
		}
		/// <summary>
		/// Adds a new role to the data source for the configured applicationName.
		/// </summary>
		/// <param name="roleName">The name of the role to create.</param>
		public override void CreateRole(String roleName) {
			throw new NotImplementedException();
		}
		/// <summary>
		/// Removes a role from the data source for the configured applicationName.
		/// </summary>
		/// <param name="roleName">The name of the role to delete.</param>
		/// <param name="throwOnPopulatedRole">If <c>true</c>, throw an exception if <paramref name="roleName"/> has one or more members and do not delete <paramref name="roleName"/>.</param>
		/// <returns><c>true</c> if the role was successfully deleted; otherwise, <c>false</c>.</returns>
		public override Boolean DeleteRole(String roleName, Boolean throwOnPopulatedRole) {
			throw new NotImplementedException();
		}
		/// <summary>
		/// Gets an array of user names in a role where the user name contains the specified user name to match.
		/// </summary>
		/// <param name="roleName">The role to search in.</param>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <returns>A string array containing the names of all the users where the user name matches <paramref name="usernameToMatch"/> and the user is a member of the specified role.</returns>
		public override String[] FindUsersInRole(String roleName, String usernameToMatch) {
			throw new NotImplementedException();
		}
		/// <summary>
		/// Gets a list of all the roles for the configured applicationName.
		/// </summary>
		/// <returns>A string array containing the names of all the roles stored in the data source for the configured applicationName.</returns>
		public override String[] GetAllRoles() {
			throw new NotImplementedException();
		}
		/// <summary>
		/// Gets a list of the roles that a specified user is in for the configured applicationName.
		/// </summary>
		/// <param name="username">The user to return a list of roles for.</param>
		/// <returns>A string array containing the names of all the roles that the specified user is in for the configured applicationName.</returns>
		public override String[] GetRolesForUser(String username) {
			this.LogVerbose("PersonifyRoleProvider: Retrieving roles for user {0}...", username ?? "(NULL)");
			if (String.IsNullOrEmpty(username)) {
				this.LogVerbose("PersonifyRoleProvider: User name is null or empty.  Returning empty array of roles.");
				return new String[0];
			}

			String[] roles = RoleCache.Instance.GetRolesForUser(username);
			if (roles == null)
				this.LogWarning("PersonifyRoleProvider: Roles have not been retrieved for user {0}.  Check that the IMService is returning roles correctly for the user.", username);
			else
				this.LogVerbose("PersonifyRoleProvider: Roles for user {0} were retrieved successfully.", username);

			return roles ?? new String[0];
		}
		/// <summary>
		/// Gets a list of users in the specified role for the configured applicationName.
		/// </summary>
		/// <param name="roleName">The name of the role to get the list of users for.</param>
		/// <returns>A string array containing the names of all the users who are members of the specified role for the configured applicationName.</returns>
		public override String[] GetUsersInRole(String roleName) {
			throw new NotImplementedException();
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
			this.LogVerbose("PersonifyRoleProvider: Initializing the provider...");
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
			this.LogVerbose("PersonifyRoleProvider: Provider initialized.");
		}
		/// <summary>
		/// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
		/// </summary>
		/// <param name="username">The user name to search for.</param>
		/// <param name="roleName">The role to search in.</param>
		/// <returns><c>true</c> if the specified user is in the specified role for the configured applicationName; otherwise, <c>false</c>.</returns>
		public override Boolean IsUserInRole(String username, String roleName) {
			throw new NotImplementedException();
		}
		/// <summary>
		/// Removes the specified user names from the specified roles for the configured applicationName.
		/// </summary>
		/// <param name="usernames">A string array of user names to be removed from the specified roles.</param>
		/// <param name="roleNames">A string array of role names to remove the specified user names from.</param>
		public override void RemoveUsersFromRoles(String[] usernames, String[] roleNames) {
			throw new NotImplementedException();
		}
		/// <summary>
		/// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
		/// </summary>
		/// <param name="roleName">The name of the role to search for in the data source.</param>
		/// <returns><c>true</c> if the role name already exists in the data source for the configured applicationName; otherwise, <c>false</c>.</returns>
		public override Boolean RoleExists(String roleName) {
			this.LogVerbose("PersonifyRoleProvider: Determining if role {0} exists...", roleName ?? "(NULL)");
			if (String.IsNullOrEmpty(roleName)) {
				this.LogVerbose("PersonifyRoleProvider: Role name is null or empty.");
				return false;
			}

			// For testing...
			//Boolean exists = Enumerable.Range(1, 9)
			//    .Select(i => "Test" + i)
			//    .Contains(roleName, StringComparer.OrdinalIgnoreCase);

			Boolean exists = true;  // Since we can't verify role names using the services, assume that any role name is valid.
			//Boolean exists = RoleCache.Instance.KnownRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase);
			this.LogVerbose("PersonifyRoleProvider: Role {0} does {1}exist.", roleName, exists ? String.Empty : "not ");
			return exists;
		}
	}
}
