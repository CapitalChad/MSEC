using System;
using System.Runtime.Serialization;

namespace Msec.Personify.UpaSyncService {
	/// <summary>
	/// Represents an error that occurs because a service has stopped.
	/// </summary>
	[Serializable()]
	public class ServiceStoppedException : Exception {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServiceStoppedException"/> class.
		/// </summary>
		public ServiceStoppedException() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServiceStoppedException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ServiceStoppedException(String message) : base(message) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServiceStoppedException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">Contains additional information about the error.</param>
		public ServiceStoppedException(String message, Exception innerException) : base(message, innerException) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServiceStoppedException"/> class.
		/// </summary>
		/// <param name="info">Contains information used to serialize/deserialize this instance.</param>
		/// <param name="context">Describes the source and destination of the serialization.</param>
		/// <exception cref="ArgumentNullException"><paramref name="info"/> is a null reference.</exception>
		/// <exception cref="SerializationException">Any errors occur during the deserialization process.</exception>
		protected ServiceStoppedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
