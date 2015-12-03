using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Awwa.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:CustomerAddressDetailData"/> and is intended to contain all <see cref="T:CustomerAddressDetailData"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class CustomerAddressDetailDataTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomerAddressDetailDataTests"/> class.
		/// </summary>
		public CustomerAddressDetailDataTests() : base() { }

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
		public void CustomerAddressDetailData_Unit_Constructor_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CustomerAddressDetailData.CompanyNameKey, "MyUserName" }
			};
			new CustomerAddressDetailData(values);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor when the 'values' argument is a null reference.")]
		public void CustomerAddressDetailData_Unit_Constructor_ValuesNull() {
			IDictionary<String, Object> values = null;
			new CustomerAddressDetailData(values);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor when the 'values' argument is empty.")]
		public void CustomerAddressDetailData_Unit_Constructor_ValuesEmpty() {
			IDictionary<String, Object> values = new Dictionary<String, Object>();
			new CustomerAddressDetailData(values);
		}

	// Property Tests
		[TestMethod()]
		[Description("Tests the well-known fields for the optimal path.")]
		public void CustomerAddressDetailData_Integration_WellKnownFields_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CustomerAddressDetailData.CompanyNameKey, "MyUserName" }
			};

			CustomerAddressDetailData target = new CustomerAddressDetailData(values);
			foreach (String fieldName in target.FieldNames) {
				Assert.IsTrue(values.Keys.Contains(fieldName), "The field name {0} was not provided in the dictionary.", fieldName);
			}
			foreach (String fieldName in values.Keys) {
				Assert.IsTrue(target.FieldNames.Contains(fieldName), "The field name {0} does not exist in the data object.", fieldName);
			}
			Assert.AreEqual(values[CustomerAddressDetailData.CompanyNameKey], target.CompanyName);
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class.")]
		public void CustomerAddressDetailData_Unit_Serialization_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CustomerAddressDetailData.CompanyNameKey, "MyUserName" }
			};
			CustomerAddressDetailData target = new CustomerAddressDetailData(values);

			CustomerAddressDetailData clone = CloneGenerator.SerializeBinary(target);
			Assert.AreNotSame(target, clone);
			Assert.AreEqual(target[CustomerAddressDetailData.CompanyNameKey], clone[CustomerAddressDetailData.CompanyNameKey]);
		}
	}
}