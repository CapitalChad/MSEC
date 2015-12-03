using System;

namespace Msec {
	/// <summary>
	/// A mock object to test the <see cref="T:DisposableBase"/> class.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	internal sealed class MockDisposableBase : DisposableBase {
	// Fields
		/// <summary>
		/// Called in the <see cref="M:DisposableBase.ReleaseManagedResources"/> method.  This field is read-only.
		/// </summary>
		private readonly Action _releaseManagedResourcesAction;
		/// <summary>
		/// Called in the <see cref="M:DisposableBase.ReleaseUnmanagedResources"/> method.  This field is read-only.
		/// </summary>
		private readonly Action _releaseUnmanagedResourcesAction;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockDisposableBase"/> class.
		/// </summary>
		public MockDisposableBase() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockDisposableBase"/> class.
		/// </summary>
		/// <param name="releaseManagedResourcesAction">The action to execute when <see cref="M:DisposableBase.ReleaseManagedResources"/> is called, or a null reference.</param>
		/// <param name="releaseUnmanagedResourcesAction">The action to execute when <see cref="M:DisposableBase.ReleaseUnmanagedResources"/> is called, or a null reference.</param>
		public MockDisposableBase(Action releaseManagedResourcesAction, Action releaseUnmanagedResourcesAction)
			: base() {
			this._releaseManagedResourcesAction = releaseManagedResourcesAction;
			this._releaseUnmanagedResourcesAction = releaseUnmanagedResourcesAction;
		}

	// Methods
		/// <summary>
		/// Releases any managed resources held by this instance.
		/// </summary>
		protected override void ReleaseManagedResources() {
			base.ReleaseManagedResources();
			if (this._releaseManagedResourcesAction != null) {
				this._releaseManagedResourcesAction();
			}
		}
		/// <summary>
		/// Releases any unmanaged resources held by this instance.
		/// </summary>
		protected override void ReleaseUnmanagedResources() {
			base.ReleaseUnmanagedResources();
			if (this._releaseUnmanagedResourcesAction != null) {
				this._releaseUnmanagedResourcesAction();
			}
		}
		/// <summary>
		/// A test method that will throw an exception if this instance is disposed.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public void TestMethod() {
			this.ThrowIfDisposed();
		}
	}
}
