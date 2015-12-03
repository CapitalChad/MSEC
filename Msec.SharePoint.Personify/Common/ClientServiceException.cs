using System;
using System.Runtime.Serialization;

namespace Msec {
	/// <summary>
	/// Represents an error that occurs client-side during a service call.
	/// </summary>
	[Serializable()]
	public class ClientServiceException : ServiceException {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ClientServiceException"/> class.
		/// </summary>
		public ClientServiceException() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ClientServiceException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ClientServiceException(String message) : base(message) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ClientServiceException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">Contains additional information about the error.</param>
		public ClientServiceException(String message, Exception innerException) : base(message, innerException) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ClientServiceException"/> class.
		/// </summary>
		/// <param name="info">Contains information used to serialize/deserialize this instance.</param>
		/// <param name="context">Describes the source and destination of the serialization.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="info"/> is a null reference.</exception>
		/// <exception cref="System.Runtime.Serialization.SerializationException">An error occurs during the deserialization process.</exception>
		protected ClientServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}