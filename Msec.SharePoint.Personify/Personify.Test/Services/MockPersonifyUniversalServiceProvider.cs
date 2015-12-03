using System;

namespace Msec.Personify.Services {
	/// <summary>
	/// Ensures that the Personify Universal service used is the Mock implementation.  This class may not be inherited.
	/// </summary>
	internal sealed class MockPersonifyUniversalServiceProvider : Object, IDisposable {
	// Fields
		/// <summary>
		/// <c>true</c> if this instance has been disposed; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isDisposed;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifyUniversalServiceProvider"/> class.
		/// </summary>
		public MockPersonifyUniversalServiceProvider() : this(false) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifyUniversalServiceProvider"/> class.
		/// </summary>
		/// <param name="throwExceptions"><c>true</c> to throw exceptions instead of executing functionality; otherwise, <c>false</c>.</param>
		public MockPersonifyUniversalServiceProvider(Boolean throwExceptions)
			: base() {
			PersonifyUniversalService.SetFactoryMethod(() => new MockPersonifyUniversalService() { ThrowExceptions = throwExceptions });
		}
		/// <summary>
		/// Finalizes an instance of the <see cref="T:MockPersonifyUniversalServiceProvider"/> class.
		/// </summary>
		~MockPersonifyUniversalServiceProvider() {
			this.Dispose();
		}

	// Methods
		/// <summary>
		/// Disposes of any resources held by this instance.
		/// </summary>
		public void Dispose() {
			if (!this._isDisposed) {
				PersonifyUniversalService.ResetFactoryMethod();
				this._isDisposed = true;
			}
		}
	}
}
