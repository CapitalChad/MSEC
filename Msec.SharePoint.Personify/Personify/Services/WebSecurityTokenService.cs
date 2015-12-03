using System;
using System.Diagnostics;
using Msec.Diagnostics;

using SecurityToken = System.IdentityModel.Tokens.SecurityToken;
using CommunicationState = System.ServiceModel.CommunicationState;
using RequestSecurityToken = Microsoft.IdentityModel.Protocols.WSTrust.RequestSecurityToken;
using WSTrustChannel = Microsoft.IdentityModel.Protocols.WSTrust.WSTrustChannel;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents a web-based client for a Security Token Service (STS).  This class may not be inherited.
	/// </summary>
	public sealed class WebSecurityTokenService : SecurityTokenService {
	// Fields
		/// <summary>
		/// The WS-Trust channel that represents a connection to the Security Token Service.
		/// </summary>
		private WSTrustChannel _trustChannel;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:WebSecurityTokenService"/> class.
		/// </summary>
		/// <param name="trustChannelFactory">The factory object that creates WS-Trust channels.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="trustChannelFactory"/> is a null reference.</exception>
		private WebSecurityTokenService(SharePointSecurityTokenServiceTrustChannelFactory trustChannelFactory)
			: base() {
				if (trustChannelFactory == null) {
				throw new ArgumentNullException("trustChannelFactory");
			}
			this._trustChannel = trustChannelFactory.CreateChannel();
		}

	// Methods
		/// <summary>
		/// Creates a connection to the local SharePoint farm's Security Token Service.
		/// </summary>
		/// <returns>A reference to the <see cref="T:SecurityTokenService"/> created.</returns>
		public static SecurityTokenService CreateFromLocalSharePoint() {
			return new WebSecurityTokenService(SharePointSecurityTokenServiceTrustChannelFactory.Instance);
		}
		/// <summary>
		/// Issues a bearer security token for the user and URL specified.
		/// </summary>
		/// <param name="requestSecurityToken">The object that acts as the request for a security token.</param>
		/// <returns>A reference to the <see cref="T:SecurityToken"/> issued.</returns>
		protected override SecurityToken IssueBearerSecurityTokenCore(RequestSecurityToken requestSecurityToken) {
			Debug.Assert(this._trustChannel != null);
			if (requestSecurityToken == null) {
				throw new ArgumentNullException("requestSecurityToken");
			}
			return this._trustChannel.Issue(requestSecurityToken);
		}
		/// <summary>
		/// Releases any managed resources held by this instance.
		/// </summary>
		protected override void ReleaseManagedResources() {
			if (this._trustChannel != null) {
				if (this._trustChannel.State == CommunicationState.Opened) {
					this._trustChannel.Close();
				}
				this._trustChannel = null;
			}
		}
	}
}
