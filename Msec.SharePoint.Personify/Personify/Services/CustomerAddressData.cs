using System;
using System.Collections.Generic;
using System.Linq;
using Msec.Personify.Services.PersonifyUniversalServiceImpl;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents customer address data returned from the Personify services.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	public sealed class CustomerAddressData : ServiceDataObject {
	// Constants
		/// <summary>
		/// The key for the address line 1 field = "Address1".
		/// </summary>
		public const String AddressLine1Key = "Address1";
		/// <summary>
		/// The key for the address line 2 field = "Address2".
		/// </summary>
		public const String AddressLine2Key = "Address2";
		/// <summary>
		/// The key for the address line 3 field = "Address3".
		/// </summary>
		public const String AddressLine3Key = "Address3";
		/// <summary>
		/// The key for the address status code field = "AddressStatusCode".
		/// </summary>
		public const String AddressStatusKey = "AddressStatusCode";
		/// <summary>
		/// The key for the city field = "City".
		/// </summary>
		public const String CityKey = "City";
		/// <summary>
		/// The key for the company name field = "CompanyName".
		/// </summary>
		public const String CompanyNameKey = "CompanyName";
		/// <summary>
		/// The key for the id field = "CustomerAddressId".
		/// </summary>
		public const String IdKey = "CustomerAddressId";
		/// <summary>
		/// The key for the postal code field = "PostalCode".
		/// </summary>
		public const String PostalCodeKey = "PostalCode";
		/// <summary>
		/// The key for the state field = "State".
		/// </summary>
		public const String StateKey = "State";
		/// <summary>
		/// The key for the user name field = "MasterCustomerId".
		/// </summary>
		public const String UserNameKey = "MasterCustomerId";

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomerAddressData"/> class.
		/// </summary>
		/// <param name="values">The collection of fields and values from which to create this instance.</param>
		public CustomerAddressData(IDictionary<String, Object> values) : base(values) { }
		//internal CustomerAddressData(CustomerAddress customerAddressServiceObject, CustomerAddressDetail customerAddressDetailServiceObject)
		//    : base(customerAddressServiceObject) {
		//    this.AppendProperties(customerAddressDetailServiceObject);
		//}
		internal CustomerAddressData(CusAddressInfo serviceObject) : base(serviceObject) { }

	// Properties
		/// <summary>
		/// Gets the first line of the address, or a null reference.
		/// </summary>
		public String AddressLine1 {
			get { return this.GetString(AddressLine1Key); }
		}
		/// <summary>
		/// Gets the second line of the address, or a null reference.
		/// </summary>
		public String AddressLine2 {
			get { return this.GetString(AddressLine2Key); }
		}
		/// <summary>
		/// Gets the third line of the address, or a null reference.
		/// </summary>
		public String AddressLine3 {
			get { return this.GetString(AddressLine3Key); }
		}
		/// <summary>
		/// Gets the status code for the address.
		/// </summary>
		public String AddressStatus {
			get { return this.GetString(AddressStatusKey); }
		}
		/// <summary>
		/// Gets the city name of the address, or a null reference.
		/// </summary>
		public String City {
			get { return this.GetString(CityKey); }
		}
		/// <summary>
		/// Gets the name of the company for the address, or a null reference.
		/// </summary>
		public String CompanyName {
			get { return this.GetString(CompanyNameKey); }
		}
		/// <summary>
		/// Gets the value that uniquely identifies the customer address.
		/// </summary>
		public Int64? Id {
			get { return this.GetNullableInt64(IdKey); }
		}
		/// <summary>
		/// Gets the postal code of the address, or a null reference.
		/// </summary>
		public String PostalCode {
			get { return this.GetString(PostalCodeKey); }
		}
		/// <summary>
		/// Gets the state of the address, or a null reference.
		/// </summary>
		public String State {
			get { return this.GetString(StateKey); }
		}
		/// <summary>
		/// Gets the user name for the address data.
		/// </summary>
		public String UserName {
			get { return this.GetString(UserNameKey); }
		}
	}
}
