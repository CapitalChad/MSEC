using System;
using System.IO;

namespace Msec {
	/// <summary>
	/// Represents a write-only memory stream.  This class may not be inherited.
	/// </summary>
	public sealed class WriteOnlyStream : MemoryStream {
	// Fields
		private Boolean _isDisposed;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:WriteOnlyStream"/> class.
		/// </summary>
		public WriteOnlyStream() : base() { }

	// Properties
		public override Boolean CanRead {
			get {
				this.ThrowIfDisposed();
				return false;
			}
		}
		public override bool CanWrite {
			get {
				this.ThrowIfDisposed();
				return base.CanWrite;
			}
		}
		public override Int32 ReadTimeout {
			get { throw new NotSupportedException(); }
			set { throw new NotSupportedException(); }
		}

	// Methods
		public override IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback callback, Object state) {
			throw new NotSupportedException();
		}
		protected override void Dispose(Boolean disposing) {
			base.Dispose(disposing);
			this._isDisposed = true;
		}
		public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count) {
			throw new NotSupportedException();
		}
		public override Int32 ReadByte() {
			throw new NotSupportedException();
		}
		private void ThrowIfDisposed() {
			if (this._isDisposed) {
				throw new ObjectDisposedException(this.ToString());
			}
		}
	}
}
