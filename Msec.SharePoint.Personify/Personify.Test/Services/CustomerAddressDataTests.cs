using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:CustomerAddressData"/> and is intended to contain all <see cref="T:CustomerAddressData"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class CustomerAddressDataTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomerAddressDataTests"/> class.
		/// </summary>
		public CustomerAddressDataTests() : base() { }

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
		public void CustomerAddressData_Unit_Constructor_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CustomerAddressData.UserNameKey, "MyUserName" },
				{ CustomerAddressData.IdKey, (Int64)3 },
				{ CustomerAddressData.PostalCodeKey, "90210" }
			};
			new CustomerAddressData(values);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor when the 'values' argument is a null reference.")]
		public void CustomerAddressData_Unit_Constructor_ValuesNull() {
			IDictionary<String, Object> values = null;
			new CustomerAddressData(values);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor when the 'values' argument is empty.")]
		public void CustomerAddressData_Unit_Constructor_ValuesEmpty() {
			IDictionary<String, Object> values = new Dictionary<String, Object>();
			new CustomerAddressData(values);
		}

	// Property Tests
		[TestMethod()]
		[Description("Tests the well-known fields for the optimal path.")]
		public void CustomerAddressData_Integration_WellKnownFields_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CustomerAddressData.AddressLine1Key, "MyAddressLine1" },
				{ CustomerAddressData.AddressLine2Key, "MyAddressLine2" },
				{ CustomerAddressData.AddressLine3Key, "MyAddressLine3" },
				{ CustomerAddressData.CityKey, "MyCity" },
				{ CustomerAddressData.IdKey, (Int64)3 },
				{ CustomerAddressData.PostalCodeKey, "90210" },
				{ CustomerAddressData.StateKey, "MyState" },
				{ CustomerAddressData.UserNameKey, "MyUserName" }
			};

			CustomerAddressData target = new CustomerAddressData(values);
			foreach (String fieldName in target.FieldNames) {
				Assert.IsTrue(values.Keys.Contains(fieldName), "The field name {0} was not provided in the dictionary.", fieldName);
			}
			foreach (String fieldName in values.Keys) {
				Assert.IsTrue(target.FieldNames.Contains(fieldName), "The field name {0} does not exist in the data object.", fieldName);
			}
			Assert.AreEqual(values[CustomerAddressData.AddressLine1Key], target.AddressLine1);
			Assert.AreEqual(values[CustomerAddressData.AddressLine2Key], target.AddressLine2);
			Assert.AreEqual(values[CustomerAddressData.AddressLine3Key], target.AddressLine3);
			Assert.AreEqual(values[CustomerAddressData.CityKey], target.City);
			Assert.AreEqual(values[CustomerAddressData.IdKey], target.Id);
			Assert.AreEqual(values[CustomerAddressData.PostalCodeKey], target.PostalCode);
			Assert.AreEqual(values[CustomerAddressData.StateKey], target.State);
			Assert.AreEqual(values[CustomerAddressData.UserNameKey], target.UserName);
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class.")]
		public void CustomerAddressData_Unit_Serialization_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CustomerAddressData.UserNameKey, "MyUserName" },
				{ CustomerAddressData.IdKey, (Int64)3 },
				{ CustomerAddressData.PostalCodeKey, "90210" }
			};
			CustomerAddressData target = new CustomerAddressData(values);

			CustomerAddressData clone = CloneGenerator.SerializeBinary(target);
			Assert.AreNotSame(target, clone);
			Assert.AreEqual(target[CustomerAddressData.UserNameKey], clone[CustomerAddressData.UserNameKey]);
			Assert.AreEqual(target[CustomerAddressData.IdKey], clone[CustomerAddressData.IdKey]);
			Assert.AreEqual(target[CustomerAddressData.PostalCodeKey], clone[CustomerAddressData.PostalCodeKey]);
		}
	}
}