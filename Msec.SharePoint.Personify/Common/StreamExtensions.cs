using System;
using System.IO;

namespace Msec {
	/// <summary>
	/// Provides extension methods for the <see cref="T:Stream"/> class.  This class may not be inherited.
	/// </summary>
	public static class StreamExtensions {
	// Methods
		/// <summary>
		/// Copies the contents of a stream starting at the current position to another stream.
		/// </summary>
		/// <param name="instance">The instance from which to copy the contents.</param>
		/// <param name="stream">The stream to which to copy the contents.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="stream"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="stream"/> is not writeable.</exception>
		/// <exception cref="System.InvalidOperationException"><paramref name="instance"/> is not readable.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException"><paramref name="instance"/> has been disposed.
		/// -or- <paramref name="stream"/> has been disposed.</exception>
		/// <remarks>This method already exists in .NET 4.0.</remarks>
		public static void CopyTo(this Stream instance, Stream stream) {
			StreamExtensions.CopyTo(instance, stream, 4096);
		}
		/// <summary>
		/// Copies the contents of a stream starting at the current position to another stream.
		/// </summary>
		/// <param name="instance">The instance from which to copy the contents.</param>
		/// <param name="stream">The stream to which to copy the contents.</param>
		/// <param name="bufferSize">The size of the buffer to use.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="stream"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="bufferSize"/> is less than 1.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="stream"/> is not writeable.</exception>
		/// <exception cref="System.InvalidOperationException"><paramref name="instance"/> is not readable.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException"><paramref name="instance"/> has been disposed.
		/// -or- <paramref name="stream"/> has been disposed.</exception>
		/// <remarks>This method already exists in .NET 4.0.</remarks>
		public static void CopyTo(this Stream instance, Stream stream, Int32 bufferSize) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (stream == null) {
				throw new ArgumentNullException("stream");
			}
			if (bufferSize < 1) {
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			if (!instance.CanRead) {
				throw new InvalidOperationException("The input stream is not readable.");
			}
			if (!stream.CanWrite) {
				throw new ArgumentException("The output stream is not writeable.", "stream");
			}

			Byte[] buffer = new Byte[bufferSize];
			Int32 lengthRead;
			while (true) {
				lengthRead = instance.Read(buffer, 0, bufferSize);
				if (lengthRead == 0) {
					break;
				}
				stream.Write(buffer, 0, lengthRead);
			}
		}
	}
}
