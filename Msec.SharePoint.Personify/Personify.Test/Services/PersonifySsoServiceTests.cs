using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:PersonifySsoService"/> and is intended to contain all <see cref="T:PersonifySsoService"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class PersonifySsoServiceTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifySsoServiceTests"/> class.
		/// </summary>
		public PersonifySsoServiceTests() : base() { }

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
		[Description(".ctor(VendorCredentials) constructor for the optimal path.")]
		public void PersonifySsoService_Unit_Constructor_Optimal() {
			using (new MockPersonifySsoService(null)) { }
		}

	// Property Tests
		[TestMethod()]
		[Description("Credentials property for the optimal path.")]
		public void PersonifySsoService_Unit_Credentials_Optimal() {
			VendorCredentials vendorCredentials = new VendorCredentials("MyUserName", "MyPassword", "MyBlock");
			using (MockPersonifySsoService target = new MockPersonifySsoService(vendorCredentials)) {
				VendorCredentials expected = vendorCredentials;
				VendorCredentials actual = target.VendorCredentials;
				Assert.AreEqual(expected, actual);
			}
		}

	// Method Tests
		[TestMethod()]
		[Description("DecryptCustomerToken(String) method for the optimal path.")]
		public void PersonifySsoService_Unit_DecryptCustomerToken_Optimal() {
			String customerToken = "MyToken";
			PersonifySsoService target = new MockPersonifySsoService();
			String decryptedToken = target.DecryptCustomerToken(customerToken);
			Assert.IsNotNull(decryptedToken);
			Assert.AreNotEqual(decryptedToken, customerToken);
		}
		[TestMethod()]
		[Description("DecryptCustomerToken(String) method when the 'customerToken' argument is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PersonifySsoService_Unit_DecryptCustomerToken_CustomerTokenNull() {
			String customerToken = null;
			PersonifySsoService target = new MockPersonifySsoService();
			target.DecryptCustomerToken(customerToken);
		}
		[TestMethod()]
		[Description("DecryptCustomerToken(String) method when the service has been disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void PersonifySsoService_Unit_DecryptCustomerToken_Disposed() {
			String customerToken = "MyToken";
			PersonifySsoService target = new MockPersonifySsoService();
			target.Dispose();
			target.DecryptCustomerToken(customerToken);

		}
		[TestMethod()]
		[Description("DecryptCustomerToken(String) method when an exception that is not a ServiceException is thrown.")]
		public void PersonifySsoService_Unit_DecryptCustomerToken_OtherExceptionThrown() {
			String customerToken = "MyToken";
			PersonifySsoService target = new MockPersonifySsoService() {
				ThrowExceptions = true
			};

			try {
				target.DecryptCustomerToken(customerToken);
				Assert.Fail("An exception should have been thrown.");
			}
			catch (ServiceException) {
				return;
			}
		}

		[TestMethod()]
		[Description("EncryptVendorToken(Uri) method for the optimal path.")]
		public void PersonifySsoService_Unit_EncryptVendorToken1_Optimal() {
			Uri returnUrl = new Uri("http://www.google.com/");
			PersonifySsoService target = new MockPersonifySsoService();
			String decryptedToken = target.EncryptVendorToken(returnUrl);
			Assert.IsNotNull(decryptedToken);
			Assert.AreNotEqual(decryptedToken, returnUrl);
		}
		[TestMethod()]
		[Description("EncryptVendorToken(Uri) method when the 'returnUrl' argument is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PersonifySsoService_Unit_EncryptVendorToken1_CustomerTokenNull() {
			Uri returnUrl = null;
			PersonifySsoService target = new MockPersonifySsoService();
			target.EncryptVendorToken(returnUrl);
		}
		[TestMethod()]
		[Description("EncryptVendorToken(Uri) method when the service has been disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void PersonifySsoService_Unit_EncryptVendorToken1_Disposed() {
			Uri returnUrl = new Uri("http://www.google.com/");
			PersonifySsoService target = new MockPersonifySsoService();
			target.Dispose();
			target.EncryptVendorToken(returnUrl);

		}
		[TestMethod()]
		[Description("EncryptVendorToken(Uri) method when an exception that is not a ServiceException is thrown.")]
		public void PersonifySsoService_Unit_EncryptVendorToken1_OtherExceptionThrown() {
			Uri returnUrl = new Uri("http://www.google.com/");
			PersonifySsoService target = new MockPersonifySsoService() {
				ThrowExceptions = true
			};

			try {
				target.EncryptVendorToken(returnUrl);
				Assert.Fail("An exception should have been thrown.");
			}
			catch (ServiceException) {
				return;
			}
		}

		[TestMethod()]
		[Description("EncryptVendorToken(String) method for the optimal path.")]
		public void PersonifySsoService_Unit_EncryptVendorToken2_Optimal() {
			String returnUrl = "MyToken";
			PersonifySsoService target = new MockPersonifySsoService();
			String decryptedToken = target.EncryptVendorToken(returnUrl);
			Assert.IsNotNull(decryptedToken);
			Assert.AreNotEqual(decryptedToken, returnUrl);
		}
		[TestMethod()]
		[Description("EncryptVendorToken(String) method when the 'returnUrl' argument is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PersonifySsoService_Unit_EncryptVendorToken2_CustomerTokenNull() {
			String returnUrl = null;
			PersonifySsoService target = new MockPersonifySsoService();
			target.EncryptVendorToken(returnUrl);
		}
		[TestMethod()]
		[Description("EncryptVendorToken(String) method when the service has been disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void PersonifySsoService_Unit_EncryptVendorToken2_Disposed() {
			String returnUrl = "MyToken";
			PersonifySsoService target = new MockPersonifySsoService();
			target.Dispose();
			target.EncryptVendorToken(returnUrl);

		}
		[TestMethod()]
		[Description("EncryptVendorToken(String) method when an exception that is not a ServiceException is thrown.")]
		public void PersonifySsoService_Unit_EncryptVendorToken2_OtherExceptionThrown() {
			String returnUrl = "MyToken";
			PersonifySsoService target = new MockPersonifySsoService() {
				ThrowExceptions = true
			};

			try {
				target.EncryptVendorToken(returnUrl);
				Assert.Fail("An exception should have been thrown.");
			}
			catch (ServiceException) {
				return;
			}
		}

		[TestMethod()]
		[Description("GetCustomerName(CustomerToken) method for the optimal path.")]
		public void PersonifySsoService_Unit_GetCustomerName_Optimal() {
			using (new MockPersonifySsoServiceProvider()) {
				CustomerToken customerToken = CustomerToken.Create("MyEncryptedValue");
				using (PersonifySsoService target = new MockPersonifySsoService()) {
					String userName = target.GetCustomerName(customerToken);
					Assert.IsNotNull(userName);
				}
			}
		}
		[TestMethod()]
		[Description("GetCustomerName(CustomerToken) method when the 'customerToken' argument is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PersonifySsoService_Unit_GetCustomerName_CustomerTokenNull() {
			using (new MockPersonifySsoServiceProvider()) {
				CustomerToken customerToken = null;
				using (PersonifySsoService target = new MockPersonifySsoService()) {
					target.GetCustomerName(customerToken);
				}
			}
		}
		[TestMethod()]
		[Description("GetCustomerName(CustomerToken) method when the service has been disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void PersonifySsoService_Unit_GetCustomerName_Disposed() {
			using (new MockPersonifySsoServiceProvider()) {
				CustomerToken customerToken = CustomerToken.Create("MyEncryptedValue");
				PersonifySsoService target = new MockPersonifySsoService();
				target.Dispose();
				target.GetCustomerName(customerToken);
			}
		}
		[TestMethod()]
		[Description("GetCustomerName(CustomerToken) method when an exception that is not a ServiceException is thrown.")]
		public void PersonifySsoService_Unit_GetCustomerName_OtherExceptionThrown() {
			using (new MockPersonifySsoServiceProvider()) {
				CustomerToken customerToken = CustomerToken.Create("MyEncryptedValue");
				PersonifySsoService target = new MockPersonifySsoService() { ThrowExceptions = true };
				try {
					target.GetCustomerName(customerToken);
					Assert.Fail("An exception should have been thrown.");
				}
				catch (ServiceException) {
					return;
				}
			}
		}

		[TestMethod()]
		[Description("IsCustomerTokenValid(String) method for the optimal true path.")]
		public void PersonifySsoService_Unit_IsCustomerTokenValid_OptimalTrue() {
			String encryptedCustomerToken = "MyToken";
			Boolean expected = true;
			Boolean actual;
			using (PersonifySsoService target = new MockPersonifySsoService() { IsCustomerTokenValidReturnValue = expected }) {
				actual = target.IsCustomerTokenValid(encryptedCustomerToken);
			}
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("IsCustomerTokenValid(String) method for the optimal false path.")]
		public void PersonifySsoService_Unit_IsCustomerTokenValid_OptimalFalse() {
			String encryptedCustomerToken = "MyToken";
			Boolean expected = false;
			Boolean actual;
			using (PersonifySsoService target = new MockPersonifySsoService() { IsCustomerTokenValidReturnValue = expected }) {
				actual = target.IsCustomerTokenValid(encryptedCustomerToken);
			}
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("IsCustomerTokenValid(String) method when 'encryptedCustomerToken' is a null reference.")]
		public void PersonifySsoService_Unit_IsCustomerTokenValid_EncryptedCustomerTokenNull() {
			String encryptedCustomerToken = null;
			Boolean expected = false;
			Boolean actual;
			using (PersonifySsoService target = new MockPersonifySsoService() { IsCustomerTokenValidReturnValue = true }) {
				actual = target.IsCustomerTokenValid(encryptedCustomerToken);
			}
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("IsCustomerTokenValid(String) method when the service has been disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void PersonifySsoService_Unit_IsCustomerTokenValid_Disposed() {
			String returnUrl = "MyToken";
			PersonifySsoService target = new MockPersonifySsoService();
			target.Dispose();
			target.IsCustomerTokenValid(returnUrl);

		}
		[TestMethod()]
		[Description("IsCustomerTokenValid(String) method when an exception that is not a ServiceException is thrown.")]
		public void PersonifySsoService_Unit_IsCustomerTokenValid_OtherExceptionThrown() {
			String encryptedCustomerToken = "MyToken";
			using (PersonifySsoService target = new MockPersonifySsoService() { ThrowExceptions = true }) {
				try {
					target.IsCustomerTokenValid(encryptedCustomerToken);
					Assert.Fail("An exception should have been thrown.");
				}
				catch (ServiceException) {
					return;
				}
			}
		}

		[TestMethod()]
		[Description("NewPersonifySsoService() method for the optimal path.")]
		public void PersonifySsoService_Integration_NewPersonifySsoService1_Optimal() {
			using (PersonifySsoService target = PersonifySsoService.NewPersonifySsoService()) {
				Assert.IsNotNull(target);
			}
		}

		[TestMethod()]
		[Description("NewPersonifySsoService() method for the optimal path.")]
		public void PersonifySsoService_Integration_NewPersonifySsoService2_Optimal() {
			VendorCredentials vendorCredentials = new VendorCredentials("MyUserName", "MyPassword", "MyBlock");
			using (PersonifySsoService target = PersonifySsoService.NewPersonifySsoService(vendorCredentials)) {
				Assert.IsNotNull(target);
			}
		}
		[TestMethod()]
		[Description("NewPersonifySsoService() method for the optimal path.")]
		public void PersonifySsoService_Integration_NewPersonifySsoService2_CredentialsNull() {
			VendorCredentials vendorCredentials = null;
			using (PersonifySsoService target = PersonifySsoService.NewPersonifySsoService(vendorCredentials)) {
				Assert.IsNotNull(target);
			}
		}
	}
}
