using System;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols.WSTrust;

namespace Msec.Personify.Services {
	/// <summary>
	/// A mock implementation of the <see cref="T:SecurityTokenService"/> class.  This class may not be inherited.
	/// </summary>
	public sealed class MockSecurityTokenService : SecurityTokenService {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockSecurityTokenService"/> class.
		/// </summary>
		public MockSecurityTokenService() : base() { }

	// Properties
		/// <summary>
		/// Gets or sets the return value for the <see cref="M:SecurityTokenService.IssueBearerSecurityToken"/> method.
		/// </summary>
		public SecurityToken IssueBearerSecurityTokenCoreReturnValue {
			get;
			set;
		}

	// Methods
		/// <summary>
		/// Issues a bearer security token for the user and URL specified.
		/// </summary>
		/// <param name="requestSecurityToken">The object that acts as the request for a security token.</param>
		/// <returns>A reference to the <see cref="T:SecurityToken"/> issued.</returns>
		protected override SecurityToken IssueBearerSecurityTokenCore(RequestSecurityToken requestSecurityToken) {
			return this.IssueBearerSecurityTokenCoreReturnValue;
		}
	}
}
