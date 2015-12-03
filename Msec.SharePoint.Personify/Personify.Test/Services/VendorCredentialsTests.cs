using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:VendorCredentials"/> and is intended to contain all <see cref="T:VendorCredentials"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class VendorCredentialsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:VendorCredentialsTests"/> class.
		/// </summary>
		public VendorCredentialsTests() : base() { }

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
		[Description(".ctor(String, String, String) constructor for the optimal path.")]
		public void VendorCredentials_Unit_Constructor_Optimal() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			new VendorCredentials(username, password, block);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String) constructor when 'username' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void VendorCredentials_Unit_Constructor_UsernameNull() {
			String username = null;
			String password = "Password";
			String block = "Block";
			new VendorCredentials(username, password, block);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String) constructor when 'password' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void VendorCredentials_Unit_Constructor_PasswordNull() {
			String username = "Username";
			String password = null;
			String block = "Block";
			new VendorCredentials(username, password, block);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String) constructor when 'block' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void VendorCredentials_Unit_Constructor_BlockNull() {
			String username = "Username";
			String password = "Password";
			String block = null;
			new VendorCredentials(username, password, block);
		}

	// Property Tests
		[TestMethod()]
		[Description("All properties for their immutability.")]
		public void VendorCredentials_Unit_AllProperties_Immutability() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			Assert.AreEqual(username, target.UserName);
			Assert.AreEqual(password, target.Password);
			Assert.AreEqual(block, target.Block);
		}

	// Method Tests
		[TestMethod()]
		[Description("Equals(Object) method when an equivalent object is compared.")]
		public void VendorCredentials_Unit_Equals1_EquivalentObject() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			Object obj = new VendorCredentials(username, password, block);
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when the same instance is compared.")]
		public void VendorCredentials_Unit_Equals1_SameInstance() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			Object obj = target;
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a null reference.")]
		public void VendorCredentials_Unit_Equals1_ObjNull() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			Object obj = null;
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when the Username properties are different.")]
		public void VendorCredentials_Unit_Equals1_DifferentUsername() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			Object obj = new VendorCredentials(username.ToUpperInvariant(), password, block);
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when the Password properties are different.")]
		public void VendorCredentials_Unit_Equals1_DifferentPassword() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			Object obj = new VendorCredentials(username, password.ToUpperInvariant(), block);
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when the Block properties are different.")]
		public void VendorCredentials_Unit_Equals1_DifferentBlock() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			Object obj = new VendorCredentials(username, password, block.ToUpperInvariant());
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is of a different type.")]
		public void VendorCredentials_Unit_Equals1_DifferentType() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			Object obj = new Object();
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Equals(VendorCredentials) method when an equivalent object is compared.")]
		public void VendorCredentials_Unit_Equals2_EquivalentObject() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			VendorCredentials other = new VendorCredentials(username, password, block);
			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(VendorCredentials) method when the same instance is compared.")]
		public void VendorCredentials_Unit_Equals2_SameInstance() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			VendorCredentials other = target;
			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(VendorCredentials) method when 'other' is a null reference.")]
		public void VendorCredentials_Unit_Equals2_ObjNull() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			VendorCredentials other = null;
			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(VendorCredentials) method when the Username properties are different.")]
		public void VendorCredentials_Unit_Equals2_DifferentUsername() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			VendorCredentials other = new VendorCredentials(username.ToUpperInvariant(), password, block);
			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(VendorCredentials) method when the Password properties are different.")]
		public void VendorCredentials_Unit_Equals2_DifferentPassword() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			VendorCredentials other = new VendorCredentials(username, password.ToUpperInvariant(), block);
			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(VendorCredentials) method when the Block properties are different.")]
		public void VendorCredentials_Unit_Equals2_DifferentBlock() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials target = new VendorCredentials(username, password, block);
			VendorCredentials other = new VendorCredentials(username, password, block.ToUpperInvariant());
			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("GetHashCode() method when two equivalent objects are compared.")]
		public void VendorCredentials_Unit_GetHashCode_EquivalentObject() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials a = new VendorCredentials(username, password, block);
			VendorCredentials b = new VendorCredentials(username, password, block);
			Int32 hashCodeA = a.GetHashCode();
			Int32 hashCodeB = b.GetHashCode();
			Assert.AreEqual(hashCodeA, hashCodeB);
		}
		[TestMethod()]
		[Description("GetHashCode() method when the same instance is compared.")]
		public void VendorCredentials_Unit_GetHashCode_SameInstance() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials a = new VendorCredentials(username, password, block);
			VendorCredentials b = a;
			Int32 hashCodeA = a.GetHashCode();
			Int32 hashCodeB = b.GetHashCode();
			Assert.AreEqual(hashCodeA, hashCodeB);
		}
		[TestMethod()]
		[Description("GetHashCode() method when two objects have different Username property values.")]
		public void VendorCredentials_Unit_GetHashCode_DifferentUsername() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials a = new VendorCredentials(username, password, block);
			VendorCredentials b = new VendorCredentials(username.ToUpperInvariant(), password, block);
			Int32 hashCodeA = a.GetHashCode();
			Int32 hashCodeB = b.GetHashCode();
			Assert.AreNotEqual(hashCodeA, hashCodeB);
		}
		[TestMethod()]
		[Description("GetHashCode() method when two objects have different Password property values.")]
		public void VendorCredentials_Unit_GetHashCode_DifferentPassword() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials a = new VendorCredentials(username, password, block);
			VendorCredentials b = new VendorCredentials(username, password.ToUpperInvariant(), block);
			Int32 hashCodeA = a.GetHashCode();
			Int32 hashCodeB = b.GetHashCode();
			Assert.AreNotEqual(hashCodeA, hashCodeB);
		}
		[TestMethod()]
		[Description("GetHashCode() method when two objects have different Block property values.")]
		public void VendorCredentials_Unit_GetHashCode_DifferentBlock() {
			String username = "Username";
			String password = "Password";
			String block = "Block";
			VendorCredentials a = new VendorCredentials(username, password, block);
			VendorCredentials b = new VendorCredentials(username, password, block.ToUpperInvariant());
			Int32 hashCodeA = a.GetHashCode();
			Int32 hashCodeB = b.GetHashCode();
			Assert.AreNotEqual(hashCodeA, hashCodeB);
		}
	}
}
