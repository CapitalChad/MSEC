using System;

namespace Msec.Personify.Services {
	/// <summary>
	/// Ensures that the Personify SSO service used is the Mock implementation.  This class may not be inherited.
	/// </summary>
	internal sealed class MockPersonifySsoServiceProvider : Object, IDisposable {
	// Fields
		/// <summary>
		/// <c>true</c> if this instance has been disposed; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isDisposed;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifySsoServiceProvider"/> class.
		/// </summary>
		public MockPersonifySsoServiceProvider() : this(false, true) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifySsoServiceProvider"/> class.
		/// </summary>
		/// <param name="throwExceptions"><c>true</c> to throw exceptions instead of executing functionality; otherwise, <c>false</c>.</param>
		public MockPersonifySsoServiceProvider(Boolean throwExceptions) : this(false, true) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifySsoServiceProvider"/> class.
		/// </summary>
		/// <param name="throwExceptions"><c>true</c> to throw exceptions instead of executing functionality; otherwise, <c>false</c>.</param>
		/// <param name="isCustomerTokenValid">The return value of the <see cref="M:PersonifySsoService.IsCustomerTokenValid"/> method.</param>
		public MockPersonifySsoServiceProvider(Boolean throwExceptions, Boolean isCustomerTokenValid)
			: base() {
			PersonifySsoService.SetFactoryMethod(credentials => new MockPersonifySsoService(credentials) { ThrowExceptions = throwExceptions, IsCustomerTokenValidReturnValue = isCustomerTokenValid });
		}
		/// <summary>
		/// Finalizes an instance of the <see cref="T:MockPersonifySsoServiceProvider"/> class.
		/// </summary>
		~MockPersonifySsoServiceProvider() {
			this.Dispose();
		}

	// Methods
		/// <summary>
		/// Disposes of any resources held by this instance.
		/// </summary>
		public void Dispose() {
			if (!this._isDisposed) {
				PersonifySsoService.ResetFactoryMethod();
				this._isDisposed = true;
			}
		}
	}
}
