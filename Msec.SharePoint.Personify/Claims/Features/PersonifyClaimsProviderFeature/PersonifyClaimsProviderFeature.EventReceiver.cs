using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration.Claims;

namespace Msec.Personify.Claims.Features.PersonifyClaimsProviderFeature {
	/// <summary>
	/// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
	/// </summary>
	/// <remarks>
	/// The GUID attached to this class may be used during packaging and should not be modified.
	/// </remarks>
	[Guid("981d4141-75d5-4cb3-8f11-bfbe9f84cc3e")]
	public class PersonifyClaimsProviderFeatureEventReceiver : SPClaimProviderFeatureReceiver {
		public override String ClaimProviderDisplayName {
			get { return PersonifyClaimProvider.ClaimProviderDisplayName; }
		}
		public override String ClaimProviderDescription {
			get { return PersonifyClaimProvider.ClaimProviderDescription; }
		}
		public override string ClaimProviderAssembly {
			get { return this.GetType().Assembly.FullName; }
		}
		public override string ClaimProviderType {
			get { return typeof(PersonifyClaimProvider).FullName; }
		}
	}
}
