using System;
using System.Collections.Generic;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents a mock implementation of the <see cref="T:ServiceDataObject"/> class.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	public sealed class MockServiceDataObject : ServiceDataObject {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockServiceDataObject"/> class.
		/// </summary>
		/// <param name="values">The values from which to create this instance.</param>
		public MockServiceDataObject(IDictionary<String, Object> values) : base(values) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockServiceDataObject"/> class.
		/// </summary>
		/// <param name="serviceObject">The service object from which to create this instance.</param>
		public MockServiceDataObject(Object serviceObject) : base(serviceObject) { }
	}
}
