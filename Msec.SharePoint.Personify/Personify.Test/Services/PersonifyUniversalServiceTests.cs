using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:PersonifyUniversalService"/> and is intended to contain all <see cref="T:PersonifyUniversalService"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class PersonifyUniversalServiceTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyUniversalServiceTests"/> class.
		/// </summary>
		public PersonifyUniversalServiceTests() : base() { }

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

	// Constructors
		[TestMethod()]
		[Description(".ctor() constructor for the optimal path.")]
		public void PersonifyUniversalService_Unit_Constructor_Optimal() {
			using (new MockPersonifyUniversalService()) { }
		}
		
	// Method Tests
		[TestMethod()]
		[Description("GetCommitteeMembers(Constraint) method for the optimal path.")]
		public void PersonifyUniversalService_Unit_GetCommitteeMembers_Optimal() {
			IEnumerable<CommitteeMemberData> results;
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				Constraint constraint = new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue");
				results = target.GetCommitteeMembers(constraint);
			}
			Assert.IsNotNull(results);
		}
		[TestMethod()]
		[Description("GetCommitteeMembers(Constraint) method when 'constraint' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PersonifyUniversalService_Unit_GetCommitteeMembers_ConstraintNull() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				target.GetCommitteeMembers((Constraint)null);
			}
		}
		[TestMethod()]
		[Description("GetCommitteeMembers(Constraint) method when the object is disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void PersonifyUniversalService_Unit_GetCommitteeMembers_Disposed() {
			PersonifyUniversalService target = new MockPersonifyUniversalService();
			target.Dispose();
			Constraint constraint = new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue");
			target.GetCommitteeMembers(constraint);
		}
		[TestMethod()]
		[Description("GetCommitteeMembers(Constraint) method when an exception that is not a ServiceException is thrown.")]
		public void PersonifyUniversalService_Unit_GetCommitteeMembers_OtherExceptionThrown() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService() { ThrowExceptions = true }) {
				Constraint constraint = new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue");
				try {
					target.GetCommitteeMembers(constraint);
					Assert.Fail("An exception should have been thrown.");
				}
				catch (ServiceException) {
					return;
				}
			}
		}

		[TestMethod()]
		[Description("GetCustomerAddresses(Constraint) method for the optimal path.")]
		public void PersonifyUniversalService_Unit_GetCustomerAddresses_Optimal() {
			IEnumerable<CustomerAddressData> results;
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				Constraint constraint = new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue");
				results = target.GetCustomerAddresses(constraint);
			}
			Assert.IsNotNull(results);
		}
		[TestMethod()]
		[Description("GetCustomerAddresses(Constraint) method when 'constraint' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PersonifyUniversalService_Unit_GetCustomerAddresses_ConstraintNull() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				target.GetCustomerAddresses((Constraint)null);
			}
		}
		[TestMethod()]
		[Description("GetCustomerAddresses(Constraint) method when the object is disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void PersonifyUniversalService_Unit_GetCustomerAddresses_Disposed() {
			PersonifyUniversalService target = new MockPersonifyUniversalService();
			target.Dispose();
			Constraint constraint = new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue");
			target.GetCustomerAddresses(constraint);
		}
		[TestMethod()]
		[Description("GetCustomerAddresses(Constraint) method when an exception that is not a ServiceException is thrown.")]
		public void PersonifyUniversalService_Unit_GetCustomerAddresses_OtherExceptionThrown() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService() { ThrowExceptions = true }) {
				Constraint constraint = new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue");
				try {
					target.GetCustomerAddresses(constraint);
					Assert.Fail("An exception should have been thrown.");
				}
				catch (ServiceException) {
					return;
				}
			}
		}

		[TestMethod()]
		[Description("GetCustomers(Constraint) method for the optimal path.")]
		public void PersonifyUniversalService_Unit_GetCustomers1_Optimal() {
			IEnumerable<CustomerData> results;
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				Constraint constraint = new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue");
				results = target.GetCustomers(constraint);
			}
			Assert.IsNotNull(results);
		}
		[TestMethod()]
		[Description("GetCustomers(Constraint) method when 'constraint' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PersonifyUniversalService_Unit_GetCustomers1_ConstraintNull() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				target.GetCustomers((Constraint)null);
			}
		}
		[TestMethod()]
		[Description("GetCustomers(Constraint) method when the object is disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void PersonifyUniversalService_Unit_GetCustomers1_Disposed() {
			PersonifyUniversalService target = new MockPersonifyUniversalService();
			target.Dispose();
			Constraint constraint = new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue");
			target.GetCustomers(constraint);
		}
		[TestMethod()]
		[Description("GetCustomers(Constraint) method when an exception that is not a ServiceException is thrown.")]
		public void PersonifyUniversalService_Unit_GetCustomers1_OtherExceptionThrown() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService() { ThrowExceptions = true }) {
				Constraint constraint = new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue");
				try {
					target.GetCustomers(constraint);
					Assert.Fail("An exception should have been thrown.");
				}
				catch (ServiceException) {
					return;
				}
			}
		}

		[TestMethod()]
		[Description("GetCustomers(Constraint[]) method for the optimal path.")]
		public void PersonifyUniversalService_Unit_GetCustomers2_Optimal() {
			IEnumerable<CustomerData> results;
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				Constraint[] constraints = new Constraint[] {
					new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue"),
					new Constraint("MyOtherFieldName", ConstraintOperator.StartsWith, "MyOtherValue")
				};
				results = target.GetCustomers(constraints);
			}
			Assert.IsNotNull(results);
		}
		[TestMethod()]
		[Description("GetCustomers(Constraint[]) method when 'constraints' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PersonifyUniversalService_Unit_GetCustomers2_ConstraintNull() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				target.GetCustomers((Constraint[])null);
			}
		}
		[TestMethod()]
		[Description("GetCustomers(Constraint[]) method when 'constraints' is empty.")]
		[ExpectedException(typeof(ArgumentException))]
		public void PersonifyUniversalService_Unit_GetCustomers2_ConstraintEmpty() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				target.GetCustomers(new Constraint[0]);
			}
		}
		[TestMethod()]
		[Description("GetCustomers(Constraint[]) method when 'constraints' contains an element that is a null reference.")]
		[ExpectedException(typeof(ArgumentException))]
		public void PersonifyUniversalService_Unit_GetCustomers2_ConstraintContainsNull() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				target.GetCustomers(new Constraint[] { null });
			}
		}
		[TestMethod()]
		[Description("GetCustomers(Constraint[]) method when the object is disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void PersonifyUniversalService_Unit_GetCustomers2_Disposed() {
			PersonifyUniversalService target = new MockPersonifyUniversalService();
			target.Dispose();
			Constraint[] constraints = new Constraint[] {
				new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue"),
				new Constraint("MyOtherFieldName", ConstraintOperator.StartsWith, "MyOtherValue")
			};
			target.GetCustomers(constraints);
		}
		[TestMethod()]
		[Description("GetCustomers(Constraint[]) method when an exception that is not a ServiceException is thrown.")]
		public void PersonifyUniversalService_Unit_GetCustomers2_OtherExceptionThrown() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService() { ThrowExceptions = true }) {
				Constraint[] constraints = new Constraint[] {
					new Constraint("MyFieldName", ConstraintOperator.Equals, "MyValue"),
					new Constraint("MyOtherFieldName", ConstraintOperator.StartsWith, "MyOtherValue")
				};
				try {
					target.GetCustomers(constraints);
					Assert.Fail("An exception should have been thrown.");
				}
				catch (ServiceException) {
					return;
				}
			}
		}

		[TestMethod()]
		[Description("GetPositionCodes() method for the optimal path.")]
		public void PersonifyUniversalService_Unit_GetPositionCodes_Optimal() {
			IEnumerable<String> results;
			using (PersonifyUniversalService target = new MockPersonifyUniversalService()) {
				results = target.GetPositionCodes();
			}
			Assert.IsNotNull(results);
		}
		[TestMethod()]
		[Description("GetPositionCodes() method when the object is disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void PersonifyUniversalService_Unit_GetPositionCodes_Disposed() {
			PersonifyUniversalService target = new MockPersonifyUniversalService();
			target.Dispose();
			target.GetPositionCodes();
		}
		[TestMethod()]
		[Description("GetPositionCodes() method when an exception that is not a ServiceException is thrown.")]
		public void PersonifyUniversalService_Unit_GetPositionCodes_OtherExceptionThrown() {
			using (PersonifyUniversalService target = new MockPersonifyUniversalService() { ThrowExceptions = true }) {
				try {
					target.GetPositionCodes();
					Assert.Fail("An exception should have been thrown.");
				}
				catch (ServiceException) {
					return;
				}
			}
		}

		[TestMethod()]
		[Description("NewPersonifyUniversalService() method for the optimal path.")]
		public void PersonifyUniversalService_Unit_NewPersonifyUniversalService_Optimal() {
			using (new MockPersonifyUniversalServiceProvider()) {
				using (PersonifyUniversalService target = PersonifyUniversalService.NewPersonifyUniversalService()) {
					Assert.IsNotNull(target);
				}
			}
		}
	}
}