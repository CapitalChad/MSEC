using System;
using System.Diagnostics.CodeAnalysis;

namespace Msec {
	/// <summary>
	/// Acts as a base class for object that have disposable resources.
	/// </summary>
	[Serializable()]
	public abstract class DisposableBase : Object, IDisposable {
	// Fields
		/// <summary>
		/// <c>true</c> if this instance has been disposed; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isDisposed;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DisposableBase"/> class.
		/// </summary>
		protected DisposableBase() : base() { }
		/// <summary>
		/// Finalizes an instance of the <see cref="T:DisposableBase"/> class.
		/// </summary>
		~DisposableBase() {
			this.Dispose(false);
		}

	// Properties
		/// <summary>
		/// Gets a value indicating if this instance has been disposed.
		/// </summary>
		public Boolean IsDisposed {
			get { return this._isDisposed; }
		}

	// Methods
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "GC.SuppressFinalize(System.Object) is called appropriately.")]
		public void Dispose() {
			this.Dispose(true);
		}
		/// <summary>
		/// Disposes any resources held by this instance.
		/// </summary>
		/// <param name="isDisposing"><c>true</c> if this method is called from <see cref="M:IDisposable.Dispose"/>; otherwise, <c>false</c>.</param>
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "GC.SuppressFinalize(System.Object) is called appropriately.")]
		private void Dispose(Boolean isDisposing) {
			if (!this._isDisposed) {
				if (isDisposing) {
					this.ReleaseManagedResources();
					GC.SuppressFinalize(this);
				}
				this.ReleaseUnmanagedResources();
				this._isDisposed = true;
			}
		}
		/// <summary>
		/// When overriden in a derived class, releases any managed resources held by this instance.
		/// </summary>
		protected virtual void ReleaseManagedResources() { }
		/// <summary>
		/// When overriden in a derived class, releases any unmanaged resources held by this instance.
		/// </summary>
		protected virtual void ReleaseUnmanagedResources() { }
		/// <summary>
		/// Throws an <see cref="T:ObjectDisposedException"/> if this instance has been disposed.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException"><see cref="P:IsDisposed"/> is <c>true</c>.</exception>
		protected void ThrowIfDisposed() {
			if (this._isDisposed) {
				throw new ObjectDisposedException(this.ToString());
			}
		}
	}
}
