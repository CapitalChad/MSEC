using System;
using System.IO;

namespace Msec {
	/// <summary>
	/// Represents a read-only memory stream.  This class may not be inherited.
	/// </summary>
	public sealed class ReadOnlyStream : MemoryStream {
	// Fields
		private Boolean _isDisposed;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReadOnlyStream"/> class.
		/// </summary>
		/// <param name="buffer">The buffer to read.</param>
		public ReadOnlyStream(Byte[] buffer) : base(buffer) { }

	// Properties
		public override bool CanRead {
			get {
				this.ThrowIfDisposed();
				return base.CanRead;
			}
		}
		public override Boolean CanWrite {
			get {
				this.ThrowIfDisposed();
				return false;
			}
		}
		public override Int32 WriteTimeout {
			get { throw new NotSupportedException(); }
			set { throw new NotSupportedException(); }
		}

	// Methods
		public override IAsyncResult BeginWrite(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback callback, Object state) {
			throw new NotSupportedException();
		}
		protected override void Dispose(Boolean disposing) {
			base.Dispose(disposing);
			this._isDisposed = true;
		}
		private void ThrowIfDisposed() {
			if (this._isDisposed) {
				throw new ObjectDisposedException(this.ToString());
			}
		}
		public override void Write(Byte[] buffer, Int32 offset, Int32 count) {
			throw new NotSupportedException();
		}
		public override void WriteByte(Byte value) {
			throw new NotSupportedException();
		}
	}
}
