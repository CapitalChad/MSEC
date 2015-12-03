using System;
using System.Runtime.Serialization;

namespace Msec {
	/// <summary>
	/// Represents an error that occurs server-side during a service call.
	/// </summary>
	[Serializable()]
	public class ServerServiceException : ServiceException {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServerServiceException"/> class.
		/// </summary>
		public ServerServiceException() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServerServiceException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ServerServiceException(String message) : base(message) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServerServiceException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">Contains additional information about the error.</param>
		public ServerServiceException(String message, Exception innerException) : base(message, innerException) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServerServiceException"/> class.
		/// </summary>
		/// <param name="info">Contains information used to serialize/deserialize this instance.</param>
		/// <param name="context">Describes the source and destination of the serialization.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="info"/> is a null reference.</exception>
		/// <exception cref="System.Runtime.Serialization.SerializationException">An error occurs during the deserialization process.</exception>
		protected ServerServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}