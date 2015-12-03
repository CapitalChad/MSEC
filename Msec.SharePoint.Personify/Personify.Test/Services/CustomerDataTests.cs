using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msec.Collections;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:CustomerData"/> and is intended to contain all <see cref="T:CustomerData"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class CustomerDataTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomerDataTests"/> class.
		/// </summary>
		public CustomerDataTests() : base() { }

		/// <summary>
		/// Gets or sets the test context which provides information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get { return this._testContextInstance; }
			set { this._testContextInstance = value; }
		}
		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion
		#endregion

	// Constructor Tests
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor for the optimal path.")]
		public void CustomerData_Unit_Constructor_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CustomerData.UserNameKey, "MyUserName" },
				{ CustomerData.EmailAddressKey, "MyEmailAddress@somewhere.com" },
				{ CustomerData.FirstNameKey, "Myfirst" },
				{ CustomerData.LastNameKey, "Mylast" }
			};
			new CustomerData(values);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor when 'values' is a null reference.")]
		public void CustomerData_Unit_Constructor_ValuesNull() {
			IDictionary<String, Object> values = null;
			new CustomerData(values);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor when 'values' is empty.")]
		public void CustomerData_Unit_Constructor_ValuesEmpty() {
			IDictionary<String, Object> values = new Dictionary<String, Object>();
			new CustomerData(values);
		}

	// Property Tests
		[TestMethod()]
		[Description("Addresses property for the optimal path.")]
		public void CustomerData_Integration_Addresses_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CustomerData.UserNameKey, "MyUserName" },
				{ CustomerData.EmailAddressKey, "MyEmailAddress@somewhere.com" },
				{ CustomerData.FirstNameKey, "Myfirst" },
				{ CustomerData.LastNameKey, "Mylast" }
			};
			CustomerData target = new CustomerData(values);

			using (new MockPersonifyUniversalServiceProvider()) {
				CustomerAddressData[] first = target.PrimaryAddresses.ToArray();
				CustomerAddressData[] second = target.PrimaryAddresses.ToArray();
				CollectionAssert.AreEqual(first, second);
			}
		}

		[TestMethod()]
		[Description("Tests the well-known fields for the optimal path.")]
		public void CustomerData_Integration_WellKnownFields_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CustomerData.UserNameKey, "MyUserName" },
				{ CustomerData.EmailAddressKey, "MyEmailAddress@somewhere.com" },
				{ CustomerData.FirstNameKey, "Myfirst" },
				{ CustomerData.LastNameKey, "Mylast" },
				{ CustomerData.HomePhoneKey, "123 456 7890" },
				{ CustomerData.JobTitleKey, "MyJob" },
				{ CustomerData.MemberSinceDateKey, DateTime.Now },
				{ CustomerData.WorkPhoneKey, "212 555 1212" },
				{ CustomerData.MiddleNameKey, "MyMiddleName" },
				{ CustomerData.PrefixKey, "MyPrefix" },
				{ CustomerData.SuffixKey, "MySuffix" }
			};

			CustomerData target = new CustomerData(values);
			foreach (String fieldName in target.FieldNames) {
				Assert.IsTrue(values.Keys.Contains(fieldName), "The field name {0} was not provided in the dictionary.", fieldName);
			}
			foreach (String fieldName in values.Keys) {
				Assert.IsTrue(target.FieldNames.Contains(fieldName), "The field name {0} does not exist in the data object.", fieldName);
			}
			Assert.AreEqual(values[CustomerData.CompanyNameKey], target.CompanyName);
			Assert.AreEqual(values[CustomerData.EmailAddressKey], target.EmailAddress);
			Assert.AreEqual(values[CustomerData.FirstNameKey], target.FirstName);
			Assert.AreEqual(values[CustomerData.HomePhoneKey], target.HomePhone);
			Assert.AreEqual(values[CustomerData.JobTitleKey], target.JobTitle);
			Assert.AreEqual(values[CustomerData.LastNameKey], target.LastName);
			Assert.AreEqual(values[CustomerData.MemberSinceDateKey], target.MemberSinceDate);
			Assert.AreEqual(values[CustomerData.UserNameKey], target.UserName);
			Assert.AreEqual(values[CustomerData.WorkPhoneKey], target.WorkPhone);
			Assert.AreEqual(values[CustomerData.MiddleNameKey], target.MiddleName);
			Assert.AreEqual(values[CustomerData.PrefixKey], target.Prefix);
			Assert.AreEqual(values[CustomerData.SuffixKey], target.Suffix);
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class.")]
		public void CustomerData_Unit_Serialization_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CustomerData.UserNameKey, "MyUserName" },
				{ CustomerData.EmailAddressKey, "MyEmailAddress@somewhere.com" },
				{ CustomerData.FirstNameKey, "Myfirst" },
				{ CustomerData.LastNameKey, "Mylast" }
			};
			CustomerData target = new CustomerData(values);

			CustomerData clone = CloneGenerator.SerializeBinary(target);
			Assert.AreNotSame(target, clone);
			Assert.AreEqual(target[CustomerData.UserNameKey], clone[CustomerData.UserNameKey]);
			Assert.AreEqual(target[CustomerData.EmailAddressKey], clone[CustomerData.EmailAddressKey]);
			Assert.AreEqual(target[CustomerData.FirstNameKey], clone[CustomerData.FirstNameKey]);
			Assert.AreEqual(target[CustomerData.LastNameKey], clone[CustomerData.LastNameKey]);
		}
	}
}
