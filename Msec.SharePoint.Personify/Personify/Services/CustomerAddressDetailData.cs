using System;
using System.Collections.Generic;

using CustomerAddressDetail = Awwa.Personify.Services.UniversalWebServiceImpl.CustomerAddressDetail;

namespace Awwa.Personify.Services {
	/// <summary>
	/// Represents customre address detail data returned from the Personify services.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	public sealed class CustomerAddressDetailData : ServiceDataObject {
	// Constants
		/// <summary>
		/// The key for the company name field = "CompanyName".
		/// </summary>
		public const String CompanyNameKey = "CompanyName";
		/// <summary>
		/// The key for the priority field = "PrioritySeq".
		/// </summary>
		public const String PrioritySequenceKey = "PrioritySeq";

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomerAddressDetailData"/> class.
		/// </summary>
		/// <param name="values">The collection of fields and values from which to create this instance.</param>
		public CustomerAddressDetailData(IDictionary<String, Object> values) : base(values) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomerAddressDetailData"/> class.
		/// </summary>
		/// <param name="serviceObject">The service customer address detail object from which to create this instance.</param>
		internal CustomerAddressDetailData(CustomerAddressDetail serviceObject) : base(serviceObject) { }

	// Properties
		/// <summary>
		/// Gets the name of the company for the address, or a null reference.
		/// </summary>
		public String CompanyName {
			get { return this.GetString(CompanyNameKey); }
		}
		/// <summary>
		/// Gets the priority sequence address data.
		/// </summary>
		public Int32 PrioritySequence {
			get { return this.GetNullableInt32(PrioritySequenceKey) ?? 0; }
		}
	}
}
