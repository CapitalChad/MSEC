using System;

using SecurityToken = System.IdentityModel.Tokens.SecurityToken;

namespace Msec.Personify.Services {
	/// <summary>
	/// Ensures that the Security Token service used is the Mock implementation.  This class may not be inherited.
	/// </summary>
	internal sealed class MockSecurityTokenServiceProvider : Object, IDisposable {
	// Fields
		/// <summary>
		/// <c>true</c> if this instance has been disposed; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isDisposed;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockSecurityTokenServiceProvider"/> class.
		/// </summary>
		public MockSecurityTokenServiceProvider() : this(null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockSecurityTokenServiceProvider"/> class.
		/// </summary>
		/// <param name="issueBearerSecurityTokenCoreReturnValue">The return value for the <see cref="M:SecurityTokenService.IssueBearerSecurityToken"/> method.</param>
		public MockSecurityTokenServiceProvider(SecurityToken issueBearerSecurityTokenCoreReturnValue)
			: base() {
			SecurityTokenService.SetFactoryMethod(() => new MockSecurityTokenService() { IssueBearerSecurityTokenCoreReturnValue = issueBearerSecurityTokenCoreReturnValue });
		}
		/// <summary>
		/// Finalizes an instance of the <see cref="T:MockSecurityTokenServiceProvider"/> class.
		/// </summary>
		~MockSecurityTokenServiceProvider() {
			this.Dispose();
		}

	// Methods
		/// <summary>
		/// Disposes of any resources held by this instance.
		/// </summary>
		public void Dispose() {
			if (!this._isDisposed) {
				SecurityTokenService.ResetFactoryMethod();
				this._isDisposed = true;
			}
		}
	}
}
