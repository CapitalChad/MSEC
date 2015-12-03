using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msec.Personify.Services;

namespace Msec.Personify {
	/// <summary>
	/// This is a test class for <see cref="T:CustomerToken"/> and is intended to contain all <see cref="T:CustomerToken"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class CustomerTokenTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomerTokenTests"/> class.
		/// </summary>
		public CustomerTokenTests() : base() { }

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

	// Method Tests
		[TestMethod()]
		[Description("Create(String) method for the optimal path.")]
		public void CustomerToken_Integration_Create1_Optimal() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Assert.IsNotNull(target);
			}
		}
		[TestMethod()]
		[Description("Create(String) method when 'encryptedValue' is a null reference.")]
		public void CustomerToken_Integration_Create1_EncryptedValueNull() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = null;
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Assert.IsNull(target);
			}
		}
		[TestMethod()]
		[Description("Create(String) method when 'encryptedValue' is invalid.")]
		public void CustomerToken_Integration_Create1_EncryptedValueInvalid() {
			using (MockPersonifySsoServiceProvider mockProvider = new MockPersonifySsoServiceProvider(false, false)) {
				String encryptedValue = "MyInvalidEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Assert.IsNull(target);
			}
		}

		[TestMethod()]
		[Description("Create(String, Boolean) method for the optimal path.")]
		public void CustomerToken_Integration_Create2_Optimal() {
			using (MockPersonifySsoServiceProvider mockProvider = new MockPersonifySsoServiceProvider(false, true)) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue, true);
				Assert.IsNotNull(target);
			}
		}
		[TestMethod()]
		[Description("Create(String, Boolean) method when 'encryptedValue' is a null reference and 'throwOnError' is false.")]
		public void CustomerToken_Integration_Create2_EncryptedValueNullAndThrowOnErrorFalse() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = null;
				CustomerToken target = CustomerToken.Create(encryptedValue, false);
				Assert.IsNull(target);
			}
		}
		[TestMethod()]
		[Description("Create(String, Boolean) method when 'encryptedValue' is a null reference and 'throwOnError' is true.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CustomerToken_Integration_Create2_EncryptedValueNullAndThrowOnErrorTrue() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = null;
				CustomerToken.Create(encryptedValue, true);
			}
		}
		[TestMethod()]
		[Description("Create(String, Boolean) method when 'encryptedValue' is invalid and 'throwOnError' is false.")]
		public void CustomerToken_Integration_Create2_EncryptedValueInvalidAndThrowOnErrorFalse() {
			using (MockPersonifySsoServiceProvider mockProvider = new MockPersonifySsoServiceProvider(false, false)) {
				String encryptedValue = "MyInvalidEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue, false);
				Assert.IsNull(target);
			}
		}
		[TestMethod()]
		[Description("Create(String, Boolean) method when 'encryptedValue' is invalid and 'throwOnError' is true.")]
		[ExpectedException(typeof(ArgumentException))]
		public void CustomerToken_Integration_Create2_EncryptedValueInvalidAndThrowOnErrorTrue() {
			using (MockPersonifySsoServiceProvider mockProvider = new MockPersonifySsoServiceProvider(false, false)) {
				String encryptedValue = "MyInvalidEncryptedValue";
				CustomerToken.Create(encryptedValue, true);
			}
		}

		[TestMethod()]
		[Description("Decrypt() method for the optimal path.")]
		public void CustomerToken_Integration_Decrypt_Optimal() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue, false);
				String decryptedValue = target.Decrypt();
				Assert.AreNotEqual(encryptedValue, decryptedValue);
			}
		}

		[TestMethod()]
		[Description("Equals(Object) method when equivalent objects are compared.")]
		public void CustomerToken_Integration_Equals1_EquivalentObjects() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Object obj = CustomerToken.Create(encryptedValue);

				Boolean expected = true;
				Boolean actual = target.Equals(obj);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(Object) method when the same instance is compared.")]
		public void CustomerToken_Integration_Equals1_SameInstance() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Object obj = target;

				Boolean expected = true;
				Boolean actual = target.Equals(obj);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(Object) method when the same encrypted value is compared.")]
		public void CustomerToken_Integration_Equals1_SameEncryptedValue() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Object obj = encryptedValue;

				Boolean expected = true;
				Boolean actual = target.Equals(obj);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(Object) method when different objects are compared.")]
		public void CustomerToken_Integration_Equals1_UnequivalentObjects() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Object obj = CustomerToken.Create(encryptedValue + encryptedValue);

				Boolean expected = false;
				Boolean actual = target.Equals(obj);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(Object) method a different encrypted value is compared.")]
		public void CustomerToken_Integration_Equals1_DifferentEncryptedValue() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Object obj = encryptedValue + encryptedValue;

				Boolean expected = false;
				Boolean actual = target.Equals(obj);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(Object) method a null reference is compared.")]
		public void CustomerToken_Integration_Equals1_Null() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Object obj = null;

				Boolean expected = false;
				Boolean actual = target.Equals(obj);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(Object) method when an object of a different type is compared.")]
		public void CustomerToken_Integration_Equals1_DifferentType() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Object obj = new Object();

				Boolean expected = false;
				Boolean actual = target.Equals(obj);
				Assert.AreEqual(expected, actual);
			}
		}

		[TestMethod()]
		[Description("Equals(CustomerToken) method when equivalent objects are compared.")]
		public void CustomerToken_Integration_Equals2_EquivalentObjects() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				CustomerToken other = CustomerToken.Create(encryptedValue);

				Boolean expected = true;
				Boolean actual = target.Equals(other);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(CustomerToken) method when the same instance is compared.")]
		public void CustomerToken_Integration_Equals2_SameInstance() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				CustomerToken other = target;

				Boolean expected = true;
				Boolean actual = target.Equals(other);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(CustomerToken) method when different objects are compared.")]
		public void CustomerToken_Integration_Equals2_UnequivalentObjects() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				CustomerToken other = CustomerToken.Create(encryptedValue + encryptedValue);

				Boolean expected = false;
				Boolean actual = target.Equals(other);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(CustomerToken) method a null reference is compared.")]
		public void CustomerToken_Integration_Equals2_Null() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				CustomerToken other = null;

				Boolean expected = false;
				Boolean actual = target.Equals(other);
				Assert.AreEqual(expected, actual);
			}
		}

		[TestMethod()]
		[Description("Equals(String) method when the same encrypted value is compared.")]
		public void CustomerToken_Integration_Equals3_SameEncryptedValue() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				String other = encryptedValue;

				Boolean expected = true;
				Boolean actual = target.Equals(other);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(String) method a different encrypted value is compared.")]
		public void CustomerToken_Integration_Equals3_DifferentEncryptedValue() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				String other = encryptedValue + encryptedValue;

				Boolean expected = false;
				Boolean actual = target.Equals(other);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Equals(String) method a null reference is compared.")]
		public void CustomerToken_Integration_Equals3_Null() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				String other = null;

				Boolean expected = false;
				Boolean actual = target.Equals(other);
				Assert.AreEqual(expected, actual);
			}
		}

	// Operator Tests
		[TestMethod()]
		[Description("GetHashCode() method when equivalent objects are compared.")]
		public void CustomerToken_Unit_GetHashCode_EquivalentObjects() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken a = CustomerToken.Create(encryptedValue);
				CustomerToken b = CustomerToken.Create(encryptedValue);

				Int32 hashCodeA = a.GetHashCode();
				Int32 hashCodeB = b.GetHashCode();
				Assert.AreEqual(hashCodeA, hashCodeB);
			}
		}
		[TestMethod()]
		[Description("GetHashCode() method when the same instance is compared.")]
		public void CustomerToken_Unit_GetHashCode_SameInstance() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken a = CustomerToken.Create(encryptedValue);
				CustomerToken b = a;

				Int32 hashCodeA = a.GetHashCode();
				Int32 hashCodeB = b.GetHashCode();
				Assert.AreEqual(hashCodeA, hashCodeB);
			}
		}
		[TestMethod()]
		[Description("GetHashCode() method when unequivalent objects are compared.")]
		public void CustomerToken_Unit_GetHashCode_UnequivalentObjects() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken a = CustomerToken.Create(encryptedValue);
				CustomerToken b = CustomerToken.Create(encryptedValue + encryptedValue);

				Int32 hashCodeA = a.GetHashCode();
				Int32 hashCodeB = b.GetHashCode();
				Assert.AreNotEqual(hashCodeA, hashCodeB);
			}
		}

		[TestMethod()]
		[Description("ToString() method for the optimal path.")]
		public void CustomerToken_Unit_ToString_Optimal() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);
				Assert.AreEqual(encryptedValue, target.ToString());
			}
		}

	// Operator Tests
		[TestMethod()]
		[Description("(String) operator for the optimal path.")]
		public void CustomerToken_Unit_StringCast_Optimal() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken target = CustomerToken.Create(encryptedValue);

				String castValue = target;
				Assert.AreEqual(encryptedValue, castValue);
			}
		}
		[TestMethod()]
		[Description("(String) operator when a null reference is cast.")]
		public void CustomerToken_Unit_StringCast_Null() {
			CustomerToken target = null;

			String castValue = target;
			Assert.IsNull(castValue);
		}

		[TestMethod()]
		[Description("(CustomerToken) operator for the optimal path.")]
		public void CustomerToken_Unit_CustomerTokenCast_Optimal() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";

				CustomerToken castValue = (CustomerToken)encryptedValue;
				Assert.AreEqual(encryptedValue, castValue.ToString());
			}
		}
		[TestMethod()]
		[Description("(CustomerToken) operator when a null reference is cast.")]
		public void CustomerToken_Unit_CustomerTokenCast_Null() {
			String encryptedValue = null;

			CustomerToken castValue = (CustomerToken)encryptedValue;
			Assert.IsNull(castValue);
		}
		[TestMethod()]
		[Description("(CustomerToken) operator when an invalid value is supplied.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void CustomerToken_Unit_CustomerTokenCast_InvalidValue() {
			using (MockPersonifySsoServiceProvider mockProvider = new MockPersonifySsoServiceProvider(false, false)) {
				String encryptedValue = "MyInvalidEncryptedValue";

				CustomerToken castValue = (CustomerToken)encryptedValue;
			}
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability functionality of the class.")]
		public void CustomerToken_Unit_Serializability_Optimal() {
			using (new MockPersonifySsoServiceProvider()) {
				String encryptedValue = "MyEncryptedValue";
				CustomerToken original = CustomerToken.Create(encryptedValue);
				CustomerToken clone = CloneGenerator.SerializeBinary(original);
				Assert.AreNotSame(original, clone);
				Assert.AreEqual(original, clone);
			}
		}
	}
}