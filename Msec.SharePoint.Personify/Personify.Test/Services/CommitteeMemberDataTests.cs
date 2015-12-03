using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BindingFlags = System.Reflection.BindingFlags;
using CommitteeMember = Msec.Personify.Services.UniversalWebServiceImpl.CommitteeMember;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:CommitteeMemberData"/> and is intended to contain all <see cref="T:CommitteeMemberData"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class CommitteeMemberDataTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CommitteeMemberDataTests"/> class.
		/// </summary>
		public CommitteeMemberDataTests() : base() { }

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
		public void CommitteeMemberData_Unit_Constructor_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CommitteeMemberData.CommitteeUserNameKey, "MyCommitteeUserName" },
				{ CommitteeMemberData.CustomerUserNameKey, "MyCustomerUserName" },
				{ CommitteeMemberData.PositionCodeKey, "PC" }
			};
			new CommitteeMemberData(values);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor when the 'values' argument is a null reference.")]
		public void CommitteeMemberData_Unit_Constructor_ValuesNull() {
			IDictionary<String, Object> values = null;
			new CommitteeMemberData(values);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor when the 'values' argument is empty.")]
		public void CommitteeMemberData_Unit_Constructor_ValuesEmpty() {
			IDictionary<String, Object> values = new Dictionary<String, Object>();
			new CommitteeMemberData(values);
		}

	// Property Tests
		[TestMethod()]
		[Description("Tests the well-known fields for the optimal path.")]
		public void CommitteeMemberData_Integration_WellKnownFields_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CommitteeMemberData.CommitteeUserNameKey, "MyCommitteeUserName" },
				{ CommitteeMemberData.CustomerUserNameKey, "MyCustomerUserName" },
				{ CommitteeMemberData.PositionCodeKey, "PC" }
			};

			CommitteeMemberData target = new CommitteeMemberData(values);
			foreach (String fieldName in target.FieldNames) {
				Assert.IsTrue(values.Keys.Contains(fieldName), "The field name {0} was not provided in the dictionary.", fieldName);
			}
			foreach (String fieldName in values.Keys) {
				Assert.IsTrue(target.FieldNames.Contains(fieldName), "The field name {0} does not exist in the data object.", fieldName);
			}
			Assert.AreEqual(values[CommitteeMemberData.CommitteeUserNameKey], target.CommitteeUserName);
			Assert.AreEqual(values[CommitteeMemberData.CustomerUserNameKey], target.CustomerUserName);
			Assert.AreEqual(values[CommitteeMemberData.PositionCodeKey], target.PositionCode);
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class.")]
		public void CommitteeMemberData_Unit_Serialization_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ CommitteeMemberData.CommitteeUserNameKey, "MyCommitteeUserName" },
				{ CommitteeMemberData.CustomerUserNameKey, "MyCustomerUserName" },
				{ CommitteeMemberData.PositionCodeKey, "PC" }
			};
			CommitteeMemberData target = new CommitteeMemberData(values);

			CommitteeMemberData clone = CloneGenerator.SerializeBinary(target);
			Assert.AreNotSame(target, clone);
			Assert.AreEqual(target[CommitteeMemberData.CustomerUserNameKey], clone[CommitteeMemberData.CustomerUserNameKey]);
			Assert.AreEqual(target[CommitteeMemberData.CommitteeUserNameKey], clone[CommitteeMemberData.CommitteeUserNameKey]);
			Assert.AreEqual(target[CommitteeMemberData.PositionCodeKey], clone[CommitteeMemberData.PositionCodeKey]);
		}
	}
}