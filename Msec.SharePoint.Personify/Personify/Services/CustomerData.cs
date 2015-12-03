using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Msec.Collections;
using Msec.Diagnostics;
using Msec.Personify.Services.PersonifyUniversalServiceImpl;

using PropertyInfo = System.Reflection.PropertyInfo;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents customer data returned from the Personify services.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	public sealed class CustomerData : ServiceDataObject {
	// Constants
		/// <summary>
		/// The key for the company name field = "LastName".
		/// </summary>
		public const String CompanyNameKey = LastNameKey;
		/// <summary>
		/// The key for the e-mail address field = "PrimaryEmailAddress".
		/// </summary>
		public const String EmailAddressKey = "PrimaryEmailAddress";
		/// <summary>
		/// The key for the first name field = "FirstName".
		/// </summary>
		public const String FirstNameKey = "FirstName";
		/// <summary>
		/// The key for the home phone field = "HomePhone".
		/// </summary>
		public const String HomePhoneKey = "HomePhone";
		/// <summary>
		/// The key for the job title field = "PrimaryJobTitle".
		/// </summary>
		public const String JobTitleKey = "PrimaryJobTitle";
		/// <summary>
		/// The key for the last name field = "LastName".
		/// </summary>
		public const String LastNameKey = "LastName";
		/// <summary>
		/// The key for the member since date field = "UserDefinedMemberSinceDate".
		/// </summary>
		public const String MemberSinceDateKey = "UserDefinedMemberSinceDate";
		/// <summary>
		/// The key for the middle name field = "MiddleName".
		/// </summary>
		public const String MiddleNameKey = "MiddleName";
		/// <summary>
		/// The key for the prefix field = "NamePrefix".
		/// </summary>
		public const String PrefixKey = "NamePrefix";
		/// <summary>
		/// The key for th eprimary phone field = "PrimaryPhone".
		/// </summary>
		public const String PrimaryPhoneKey = "PrimaryPhone";
		/// <summary>
		/// The key for the suffix field = "NameSuffix".
		/// </summary>
		public const String SuffixKey = "NameSuffix";
		/// <summary>
		/// The key for the user name field = "MasterCustomerId".
		/// </summary>
		public const String UserNameKey = "MasterCustomerId";
		/// <summary>
		/// The key for the work phone field = "WorkPhone".
		/// </summary>
		public const String WorkPhoneKey = "WorkPhone";

	// Fields
		/// <summary>
		/// The enumerable collection of primary addresses in this instance.
		/// </summary>
		private CustomerAddressData _primaryAddress;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomerData"/> class.
		/// </summary>
		/// <param name="values">The collection of fields and values from which to create this instance.</param>
		public CustomerData(IDictionary<String, Object> values) : base(values) { }
		//internal CustomerData(Customer customer) : base(customer) { }
		internal CustomerData(CustomerInfo customer) : base(customer) { }

	// Properties
		/// <summary>
		/// Gets the company name if there is one.  This is the same as the last name.
		/// </summary>
		public String CompanyName {
			get { return this.GetString(CompanyNameKey); }
		}
		/// <summary>
		/// Gets the e-mail address.
		/// </summary>
		public String EmailAddress {
			get { return this.GetString(EmailAddressKey); }
		}
		/// <summary>
		/// Gets the first name.
		/// </summary>
		public String FirstName {
			get { return this.GetString(FirstNameKey); }
		}
		/// <summary>
		/// Gets the home phone number.
		/// </summary>
		public String HomePhone {
			get { return this.GetString(HomePhoneKey); }
		}
		/// <summary>
		/// Gets the job title.
		/// </summary>
		public String JobTitle {
			get { return this.GetString(JobTitleKey); }
		}
		/// <summary>
		/// Gets the last name.  This is the same as the company name.
		/// </summary>
		public String LastName {
			get { return this.GetString(LastNameKey); }
		}
		/// <summary>
		/// Gets the date and time since the customer has been a member.
		/// </summary>
		public DateTime? MemberSinceDate {
			get { return this.GetNullableDateTime(MemberSinceDateKey); }
		}
		/// <summary>
		/// Gets the middle name.
		/// </summary>
		public String MiddleName {
			get { return this.GetString(MiddleNameKey); }
		}
		/// <summary>
		/// Gets the prefix of the customer's name.
		/// </summary>
		public String Prefix {
			get { return this.GetString(PrefixKey); }
		}
		/// <summary>
		/// Gets the enumerable collection of primary addresses in this instance.
		/// </summary>
		public CustomerAddressData PrimaryAddress {
			get {
				this.EnsurePrimaryAddress();
				return this._primaryAddress;
			}
		}
		/// <summary>
		/// Gets the primary phone number.
		/// </summary>
		public String PrimaryPhone {
			get { return this.GetString(PrimaryPhoneKey); }
		}
		/// <summary>
		/// Gets the suffix of the customer's name.
		/// </summary>
		public String Suffix {
			get { return this.GetString(SuffixKey); }
		}
		/// <summary>
		/// Gets the user name.  This is also the master customer identifier.
		/// </summary>
		public String UserName {
			get { return this.GetString(UserNameKey); }
		}
		/// <summary>
		/// Gets the work phone number.
		/// </summary>
		public String WorkPhone {
			get { return this.GetString(WorkPhoneKey); }
		}

	// Methods
		private void EnsurePrimaryAddress() {
			if (this._primaryAddress == null) {
				using (PersonifyUniversalService universalService = PersonifyUniversalService.NewPersonifyUniversalService()) {
					CustomerAddressData[] addresses = universalService.GetCustomerAddressesForUser(this.UserName).ToArray();
					this._primaryAddress = addresses.FirstOrDefault();
					//Constraint userNameConstraint = new Constraint(CustomerAddressData.UserNameKey, ConstraintOperator.Equals, this.UserName);
					//Constraint prioritySequenceConstraint = new Constraint(CustomerAddressData.PrioritySequenceKey, ConstraintOperator.Equals, "0");
					//Constraint addressStatusCodeConstraint = new Constraint(CustomerAddressData.AddressStatusKey, ConstraintOperator.Equals, "GOOD");
					//this._primaryAddresses = universalService.GetCustomerAddresses(userNameConstraint, prioritySequenceConstraint, addressStatusCodeConstraint).ToArray();
				}
			}
		}
	}
}
