using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Msec.Diagnostics;
using Msec.Personify.Services;

namespace Msec.Personify.Web {
	internal sealed class RoleCache : Object {
		#region private sealed class Nested : Object {...}
		/// <summary>
		/// This class is used to make the pattern fully lazy.  This class may not be inherited.
		/// </summary>
		private sealed class Nested : Object {
			// Fields
			/// <summary>
			/// The sole use of the Nested class is to provide the lazy, thread-safe instance of the <see cref="T:RoleCache"/> object.  This field is read-only.
			/// </summary>
			internal static readonly RoleCache Instance = new RoleCache();

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
		private String[] _knownRoles;
		private readonly IDictionary<String, String[]> _rolesByUserName = new Dictionary<String, String[]>(StringComparer.OrdinalIgnoreCase);

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyConfiguration"/> class.
		/// </summary>
		private RoleCache() : base() { }

	// Properties
		/// <summary>
		/// Gets the sole instance of the <see cref="T:PersonifyConfiguration"/> class.
		/// </summary>
		public static RoleCache Instance {
			get { return RoleCache.Nested.Instance; }
		}
		/// <summary>
		/// Gets the roles that are known to the system at this time.
		/// </summary>
		public String[] KnownRoles {
			get {
				this.LogVerbose("RoleCache: Retrieving all known roles in the system...");
				if (this._knownRoles == null)
					this._knownRoles = this._rolesByUserName.Values
						.SelectMany(group => group.Select(role => role))
						.Distinct()
						.ToArray();

				this.LogVerbose("RoleCache: Known roles include {0}.", String.Join(", ", this._knownRoles));
				return (String[])this._knownRoles.Clone();
			}
		}

	// Methods
		/// <summary>
		/// Returns the roles for a user.
		/// </summary>
		/// <param name="userName">Identifies the user.</param>
		/// <returns>The roles for the user specified.</returns>
		public String[] GetRolesForUser(String userName) {
			this.LogVerbose("RoleCache: Retrieving roles for user {0}...", userName);

			if (!this._rolesByUserName.ContainsKey(userName)) {
				this.LogVerbose("RoleCache: No roles have been retrieved for the user {0}.", userName);
				return new String[0];
			}

			return this._rolesByUserName[userName];
			//try {
			//    using (PersonifyIMService imService = PersonifyIMService.NewPersonifyIMService()) {
			//        String[] roles = imService.GetCustomerRolesByMasterCustomerId(userName);
			//        this._rolesByUserName[userName] = roles;
			//        this.LogVerbose("RoleCache: User {0} has roles {1}.", userName, String.Join(", ", roles));
			//        this.ResetKnownRoles();
			//        return roles;
			//    }
			//}
			//catch (Exception ex) {
			//    if (!ex.CanBeHandledSafely())
			//        throw;

			//    this.LogError("RoleCache: Roles could not be retrieved for user {0}: {1}", userName, ex);
			//    return null;
			//}
		}
		/// <summary>
		/// Refreshes the roles for a user.
		/// </summary>
		/// <param name="userName">Identifies the user.</param>
		/// <param name="decryptedCustomerToken">Identifies the user.</param>
		public void RefreshRoles(String userName, String decryptedCustomerToken) {
			this.LogVerbose("RoleCache: Refreshing roles for user {0} and token {1}...", userName, decryptedCustomerToken);
			try {
				using (PersonifyIMService imService = PersonifyIMService.NewPersonifyIMService()) {
					String[] roles = imService.GetCustomerRoles(decryptedCustomerToken);
					this._rolesByUserName[userName] = roles;
					this.ResetKnownRoles();
					this.LogVerbose("RoleCache: User {0} has roles {1}.", userName, String.Join(", ", roles));
				}
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely())
					throw;

				this.LogError("RoleCache: Roles could not be refreshed for user {0}: {1}", userName, ex);
			}
		}
		private void ResetKnownRoles() {
			this._knownRoles = null;
		}
	}
}
