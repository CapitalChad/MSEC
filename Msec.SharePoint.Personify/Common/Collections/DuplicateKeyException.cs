using System;
using System.Runtime.Serialization;

namespace Msec.Collections {
	/// <summary>
	/// Represents errors that occur because a key has a duplicate.
	/// </summary>
	[Serializable()]
	public class DuplicateKeyException : Exception {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DuplicateKeyException"/> class.
		/// </summary>
		public DuplicateKeyException() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DuplicateKeyException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public DuplicateKeyException(String message) : base(message) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DuplicateKeyException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">Contains additional information about the error.</param>
		public DuplicateKeyException(String message, Exception innerException) : base(message, innerException) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DuplicateKeyException"/> class.
		/// </summary>
		/// <param name="info">Contains information used to serialize/deserialize this instance.</param>
		/// <param name="context">Describes the source and destination of the serialization.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="info"/> is a null reference.</exception>
		/// <exception cref="System.Runtime.Serialization.SerializationException">Any errors occur during the deserialization process.</exception>
		protected DuplicateKeyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
