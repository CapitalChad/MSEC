using System;
using System.IdentityModel.Tokens;
using Msec.Diagnostics;

using EndpointAddress = System.ServiceModel.EndpointAddress;
using RequestSecurityToken = Microsoft.IdentityModel.Protocols.WSTrust.RequestSecurityToken;
using SecurityTokenElement = Microsoft.IdentityModel.Tokens.SecurityTokenElement;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents a proxy for communicating with a Security Token Service (STS).
	/// </summary>
	public abstract class SecurityTokenService : DisposableBase {
	// Constants
		/// <summary>
		/// A 'Bearer' key type = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer".
		/// </summary>
		private const String BearerKeyType = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer";
		/// <summary>
		/// An 'Issue' request type = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue".
		/// </summary>
		private const String IssueRequestType = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue";

	// Fields
#if (DEBUG)
		/// <summary>
		/// The factory method used to create instances of the actual service objects.
		/// </summary>
		/// <remarks>This field is not read-only in the DEBUG version to assist in unit testing.</remarks>
		private static Func<SecurityTokenService> _factory = WebSecurityTokenService.CreateFromLocalSharePoint;
#else
		/// <summary>
		/// The factory method used to create instances of the actual service objects.  This field is read-only.
		/// </summary>
		private static readonly Func<SecurityTokenService> _factory = WebSecurityTokenService.CreateFromLocalSharePoint;
#endif

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SecurityTokenService"/> class.
		/// </summary>
		protected SecurityTokenService() : base() { }

	// Methods
		/// <summary>
		/// Issues a bearer security token for the user and URL specified.
		/// </summary>
		/// <param name="onBehalfOf">The information about the user on whose behalf the security token will be issued.  This should contain the user's user name and password, at a minimum.  The Security Token Service uses this to determine whether or not to issue a token.</param>
		/// <param name="appliesTo">The URL to which the security token should apply.  This should be the URL of the resource being requested.  The Security Token Service uses this to determine whether or not to issue a token.</param>
		/// <returns>A reference to the <see cref="T:SecurityToken"/> issued.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="onBehalfOf"/> is a null reference.
		/// -or- <paramref name="appliesTo"/> is a null reference.</exception>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public SecurityToken IssueBearerSecurityToken(UserNameSecurityToken onBehalfOf, Uri appliesTo) {
			this.ThrowIfDisposed();
			if (onBehalfOf == null) {
				throw new ArgumentNullException("onBehalfOf");
			}
			if (appliesTo == null) {
				throw new ArgumentNullException("appliesTo");
			}

			RequestSecurityToken requestSecurityToken = new RequestSecurityToken(IssueRequestType, BearerKeyType) {
				AppliesTo = new EndpointAddress(appliesTo),
				OnBehalfOf = new SecurityTokenElement(onBehalfOf)
			};
			SecurityToken securityToken = this.IssueBearerSecurityTokenCore(requestSecurityToken);
			this.LogInformation("Bearer security token {0} issued on behalf of {1} applying to {2}.", securityToken != null ? securityToken.Id : "(NULL)", onBehalfOf.UserName, appliesTo);
			return securityToken;
		}
		/// <summary>
		/// Issues a bearer security token for the user and URL specified.
		/// </summary>
		/// <param name="requestSecurityToken">The object that acts as the request for a security token.</param>
		/// <returns>A reference to the <see cref="T:SecurityToken"/> issued.</returns>
		protected abstract SecurityToken IssueBearerSecurityTokenCore(RequestSecurityToken requestSecurityToken);
		/// <summary>
		/// Returns a new instance of the <see cref="T:SecurityTokenService"/> class.
		/// </summary>
		/// <returns>A reference to the <see cref="T:SecurityTokenService"/> created.</returns>
		public static SecurityTokenService NewSecurityTokenService() {
			return SecurityTokenService._factory();
		}
#if (DEBUG)
		/// <summary>
		/// Resets the factory method to its original state used by this type.
		/// </summary>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public static void ResetFactoryMethod() {
			SecurityTokenService._factory = WebSecurityTokenService.CreateFromLocalSharePoint;
		}
		/// <summary>
		/// Sets the factory method used to create instance of the service.
		/// </summary>
		/// <param name="factory">The factory method to use.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="factory"/> is a null reference.</exception>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public static void SetFactoryMethod(Func<SecurityTokenService> factory) {
			if (factory == null) {
				throw new ArgumentNullException("factory");
			}
			SecurityTokenService._factory = factory;
		}
#endif
	}
}
