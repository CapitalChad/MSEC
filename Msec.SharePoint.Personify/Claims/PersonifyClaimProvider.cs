using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using Msec.Diagnostics;
using Msec.Personify.Services;

using KnownClaimTypes = Microsoft.IdentityModel.Claims.ClaimValueTypes;

namespace Msec.Personify.Claims {
	public sealed class PersonifyClaimProvider : SPClaimProvider {
		#region private sealed class ClaimInfo : Object {...}
		private sealed class ClaimInfo : Object {
			// Fields
			private static readonly ClaimInfo _displayName = new ClaimInfo("DisplayName", "http://personify.msec.org/Identity/Claims/DisplayName", KnownClaimTypes.String, user => user.FriendlyName);
			private static readonly ClaimInfo _emailAddress = new ClaimInfo("EmailAddress", "http://personify.msec.org/Identity/Claims/EmailAddress", KnownClaimTypes.String, user => user.EmailAddress);
			private static readonly ClaimInfo _masterCustomerId = new ClaimInfo("MasterCustomerId", "http://personify.msec.org/Identity/Claims/MasterCustomerId", KnownClaimTypes.String, user => user.UserName);

			// Constructors
			private ClaimInfo(String name, String identifier, String type, Func<PersonifyUser, String> selector)
				: base() {
				this.Name = name;
				this.Identifier = identifier;
				this.Type = type;
				this.Selector = selector;
			}

			// Properties
			public static IEnumerable<ClaimInfo> All {
				get {
					return new ClaimInfo[] {
						ClaimInfo._displayName,
						ClaimInfo._emailAddress,
						ClaimInfo._masterCustomerId
					};
				}
			}
			public static ClaimInfo DisplayName {
				get { return ClaimInfo._displayName; }
			}
			public static ClaimInfo EmailAddress {
				get { return ClaimInfo._emailAddress; }
			}
			public String Identifier { get; private set; }
			public static ClaimInfo MasterCustomerId {
				get { return ClaimInfo._masterCustomerId; }
			}
			public String Name { get; private set; }
			private Func<PersonifyUser, String> Selector { get; set; }
			public String Type { get; private set; }

			// Methods
			public SPClaim CreateClaimForUser(PersonifyClaimProvider provider, PersonifyUser user) {
				return provider.CreateClaim(this.Identifier, this.Selector(user), this.Type);
			}
			public static IEnumerable<SPClaim> CreateClaimsForUser(PersonifyClaimProvider provider, PersonifyUser user) {
				foreach (var claim in ClaimInfo.All)
					yield return claim.CreateClaimForUser(provider, user);
			}
		}
		#endregion

		// Constants
		internal const String ClaimProviderDisplayName = "Personify";
		internal const String ClaimProviderDescription = "Personify Claims Provider";

		// Constructors
		public PersonifyClaimProvider(String displayName) : base(displayName) { }

		// Properties
		public override String Name {
			get { return ClaimProviderDisplayName; }
		}
		public override Boolean SupportsEntityInformation {
			get { return true; }
		}
		public override Boolean SupportsHierarchy {
			get { return false; }
		}
		public override Boolean SupportsResolve {
			get { return true; }
		}
		public override Boolean SupportsSearch {
			get { return true; }
		}

		// Methods
		protected override void FillClaimTypes(List<String> claimTypes) {
			if (claimTypes == null)
				throw new ArgumentNullException("claimTypes");

			claimTypes.AddRange(ClaimInfo.All.Select(claim => claim.Identifier));
		}
		protected override void FillClaimValueTypes(List<String> claimValueTypes) {
			if (claimValueTypes == null)
				throw new ArgumentNullException("claimValueTypes");

			claimValueTypes.AddRange(ClaimInfo.All.Select(claim => claim.Type));
		}
		protected override void FillClaimsForEntity(Uri context, SPClaim entity, List<SPClaim> claims) {
			if (entity == null)
				throw new ArgumentNullException("entity");
			if (claims == null)
				throw new ArgumentNullException("claims");

			this.LogVerbose("PersonifyClaimProvider: Filling claims for entity {0}...", entity.Value);
			String userName; {
				String encodedUserName = SPUtility.FormatAccountName("i", entity.Value);
				userName = SPClaimProviderManager.Local.DecodeClaim(encodedUserName).Value;
			}
			this.LogVerbose("PersonifyClaimProvider: User name {0} extracted.", userName);

			PersonifyUser user = PersonifyUserQuery.ByUserName(userName).FirstOrDefault();
			if (user != null) {
				this.LogVerbose("PersonifyClaimProvider: User found and claims being populated.");
				claims.AddRange(ClaimInfo.CreateClaimsForUser(this, user));
			}
			else {
				this.LogVerbose("PersonifyClaimProvider: No user found.");
			}
		}
		protected override void FillEntityTypes(List<String> entityTypes) {
			entityTypes.Add("User");
		}
		protected override void FillHierarchy(Uri context, String[] entityTypes, String hierarchyNodeID, Int32 numberOfLevels, SPProviderHierarchyTree hierarchy) {
		}
		protected override void FillResolve(Uri context, String[] entityTypes, SPClaim resolveInput, List<PickerEntity> resolved) {
			if (resolveInput.ClaimType == ClaimInfo.MasterCustomerId.Identifier) {
				PersonifyUser[] users = PersonifyUserQuery.ByUserName(resolveInput.Value, 0, Int32.MaxValue).ToArray();
				this.LogVerbose("PersonifyClaimProvider: {0} user(s) found by user name for input {1}.", users.Length, resolveInput.Value);
				foreach (var user in users)
					resolved.Add(this.CreatePickerEntityForUser(user));
			}
			else if (resolveInput.ClaimType == ClaimInfo.DisplayName.Identifier) {
				PersonifyUser[] users = PersonifyUserQuery.ByDisplayName(resolveInput.Value, 0, Int32.MaxValue).ToArray();
				this.LogVerbose("PersonifyClaimProvider: {0} user(s) found by display name for input {1}.", users.Length, resolveInput.Value);
				foreach (var user in users)
					resolved.Add(this.CreatePickerEntityForUser(user));
			}
			else if (resolveInput.ClaimType == ClaimInfo.EmailAddress.Identifier) {
				PersonifyUser[] users = PersonifyUserQuery.ByEmailAddress(resolveInput.Value).ToArray();
				this.LogVerbose("PersonifyClaimProvider: {0} user(s) found by e-mail address for input {1}.", users.Length, resolveInput.Value);
				foreach (var user in users)
					resolved.Add(this.CreatePickerEntityForUser(user));
			}
		}
		protected override void FillResolve(Uri context, String[] entityTypes, String resolveInput, List<PickerEntity> resolved) {
			IDictionary<String, PersonifyUser> usersByUserName = new Dictionary<String, PersonifyUser>();

			PersonifyUser[] users = PersonifyUserQuery.ByUserName(resolveInput, 0, Int32.MaxValue).ToArray();
			this.LogVerbose("PersonifyClaimProvider: {0} user(s) found by user name for input {1}.", users.Length, resolveInput);
			foreach (var user in users)
				if (!usersByUserName.ContainsKey(user.UserName))
					usersByUserName.Add(user.UserName, user);

			users = PersonifyUserQuery.ByDisplayName(resolveInput, 0, Int32.MaxValue).ToArray();
			this.LogVerbose("PersonifyClaimProvider: {0} user(s) found by display name for input {1}.", users.Length, resolveInput);
			foreach (var user in users)
				if (!usersByUserName.ContainsKey(user.UserName))
					usersByUserName.Add(user.UserName, user);

			users = PersonifyUserQuery.ByEmailAddress(resolveInput).ToArray();
			this.LogVerbose("PersonifyClaimProvider: {0} user(s) found by e-mail address for input {1}.", users.Length, resolveInput);
			foreach (var user in users)
				if (!usersByUserName.ContainsKey(user.UserName))
					usersByUserName.Add(user.UserName, user);

			foreach (var user in usersByUserName.Values)
				resolved.Add(this.CreatePickerEntityForUser(user));
		}

		protected override void FillSchema(SPProviderSchema schema) {
			schema.AddSchemaElement(new SPSchemaElement(PeopleEditorEntityDataKeys.DisplayName, "Display Name", SPSchemaElementType.Both));
			schema.AddSchemaElement(new SPSchemaElement(PeopleEditorEntityDataKeys.Email, "Email", SPSchemaElementType.TableViewOnly));
			schema.AddSchemaElement(new SPSchemaElement(PeopleEditorEntityDataKeys.AccountName, "Account Name", SPSchemaElementType.Both));
		}
		protected override void FillSearch(Uri context, String[] entityTypes, String searchPattern, String hierarchyNodeID, Int32 maxCount, SPProviderHierarchyTree searchTree) {
			if (PersonifyClaimProvider.EntityTypesContain(entityTypes, SPClaimEntityTypes.User)) {
				IDictionary<String, PersonifyUser> usersByUserName = new Dictionary<String, PersonifyUser>();

				PersonifyUser[] users = PersonifyUserQuery.ByUserName(searchPattern, 0, Int32.MaxValue).ToArray();
				this.LogVerbose("PersonifyClaimProvider: {0} user(s) found by user name for input {1}.", users.Length, searchPattern);
				foreach (var user in users)
					if (!usersByUserName.ContainsKey(user.UserName))
						usersByUserName.Add(user.UserName, user);

				users = PersonifyUserQuery.ByDisplayName(searchPattern, 0, Int32.MaxValue).ToArray();
				this.LogVerbose("PersonifyClaimProvider: {0} user(s) found by display name for input {1}.", users.Length, searchPattern);
				foreach (var user in users)
					if (!usersByUserName.ContainsKey(user.UserName))
						usersByUserName.Add(user.UserName, user);

				users = PersonifyUserQuery.ByEmailAddress(searchPattern).ToArray();
				this.LogVerbose("PersonifyClaimProvider: {0} user(s) found by e-mail address for input {1}.", users.Length, searchPattern);
				foreach (var user in users)
					if (!usersByUserName.ContainsKey(user.UserName))
						usersByUserName.Add(user.UserName, user);

				foreach (var user in usersByUserName.Values)
					searchTree.AddEntity(this.CreatePickerEntityForUser(user));
			}
		}
		private PickerEntity CreatePickerEntityForUser(PersonifyUser user) {
			PickerEntity entity = this.CreatePickerEntity();
			entity.Claim = ClaimInfo.DisplayName.CreateClaimForUser(this, user);
			entity.Description = this.Name + ":" + user.UserName;
			entity.DisplayText = user.UserName;
			entity.EntityType = SPClaimEntityTypes.User;
			entity.IsResolved = true;
			entity.EntityGroupName = "Personify";
			entity.EntityData[PeopleEditorEntityDataKeys.DisplayName] = user.UserName;
			return entity;
		}
	}
}
