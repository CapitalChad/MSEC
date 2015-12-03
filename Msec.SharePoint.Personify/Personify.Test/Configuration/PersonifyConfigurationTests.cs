using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msec.Personify.Services;

using NetworkCredential = System.Net.NetworkCredential;

namespace Msec.Personify.Configuration {
	/// <summary>
	/// This is a test class for <see cref="T:PersonifyConfiguration"/> and is intended to contain all <see cref="T:PersonifyConfiguration"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class PersonifyConfigurationTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyConfigurationTests"/> class.
		/// </summary>
		public PersonifyConfigurationTests() : base() { }

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

	// Methods
		/// <summary>
		/// Creates a mock configuration provider.
		/// </summary>
		/// <param name="setSsoPropertiesAction">Used to set properties on the configuration section.</param>
		/// <param name="setUniversalPropertiesAction">Used to set properties on the configuration section.</param>
		/// <returns>The mock configuration provider created.</returns>
		private static MockPersonifyConfigurationProvider CreateMockConfiguration(Action<PersonifySsoConfigurationSection> setSsoPropertiesAction, Action<PersonifyUniversalConfigurationSection> setUniversalPropertiesAction) {
			PersonifySsoConfigurationSection ssoConfigurationSection = new PersonifySsoConfigurationSection();
			if (setSsoPropertiesAction != null) {
				setSsoPropertiesAction(ssoConfigurationSection);
			}
			PersonifyUniversalConfigurationSection universalConfigurationSection = new PersonifyUniversalConfigurationSection();
			if (setUniversalPropertiesAction != null) {
				setUniversalPropertiesAction(universalConfigurationSection);
			}
			return new MockPersonifyConfigurationProvider(ssoConfigurationSection, universalConfigurationSection);
		}

	// Property Tests
		[TestMethod()]
		[Description("Instance property for the optimal path.")]
		public void PersonifyConfiguration_Unit_Instance_Optimal() {
			PersonifyConfiguration a = PersonifyConfiguration.Instance;
			PersonifyConfiguration b = PersonifyConfiguration.Instance;
			Assert.AreSame(a, b);
		}

		[TestMethod()]
		[Description("LoginPageUrl property for the optimal path.")]
		public void PersonifyConfiguration_Unit_LoginPageUrl_Optimal() {
			Uri loginPageUrl = new Uri("http://www.google.com/");
			using (CreateMockConfiguration(ssoSection => ssoSection.LoginPageUrl = loginPageUrl, null)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				Assert.AreEqual(loginPageUrl, target.LoginPageUrl);
			}
		}

		[TestMethod()]
		[Description("OrganizationId property for the optimal path.")]
		public void PersonifyConfiguration_Unit_OrganizationId_Optimal() {
			String organizationId = "Test";
			using (CreateMockConfiguration(null, universalSection => universalSection.OrganizationId = organizationId)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				Assert.AreEqual(organizationId, target.OrganizationId);
			}
		}

		[TestMethod()]
		[Description("OrganizationUnitId property for the optimal path.")]
		public void PersonifyConfiguration_Unit_OrganizationUnitId_Optimal() {
			String organizationUnitId = "Test";
			using (CreateMockConfiguration(null, universalSection => universalSection.OrganizationUnitId = organizationUnitId)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				Assert.AreEqual(organizationUnitId, target.OrganizationUnitId);
			}
		}

		[TestMethod()]
		[Description("UniversalServiceCredentials property for the optimal path.")]
		public void PersonifyConfiguration_Unit_UniversalServiceCredentials_Optimal() {
			String password = "MyPassword";
			String userName = "MyUserName";
			Action<PersonifyUniversalConfigurationSection> setUniversalProperties = universalSection => {
				universalSection.Password = password;
				universalSection.UserName = userName;
			};
			using (CreateMockConfiguration(null, setUniversalProperties)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				NetworkCredential expected = new NetworkCredential(userName, password);
				Assert.AreEqual(expected.UserName, target.UniversalServiceCredentials.UserName);
				Assert.AreEqual(expected.Password, target.UniversalServiceCredentials.Password);
			}
		}

		[TestMethod()]
		[Description("UniversalServiceUrl property for the optimal path.")]
		public void PersonifyConfiguration_Unit_UniversalServiceUrl_Optimal() {
			Uri universalServiceUrl = new Uri("http://www.google.com/");
			using (CreateMockConfiguration(null, universalSection => universalSection.ServiceUrl = universalServiceUrl)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				Assert.AreEqual(universalServiceUrl, target.UniversalServiceUrl);
			}
		}

		[TestMethod()]
		[Description("VendorBlock property for the optimal path.")]
		public void PersonifyConfiguration_Unit_VendorBlock_Optimal() {
			String vendorBlock = "MyVendorBlock";
			using (CreateMockConfiguration(ssoSection => ssoSection.VendorBlock = vendorBlock, null)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				Assert.AreEqual(vendorBlock, target.VendorBlock);
			}
		}

		[TestMethod()]
		[Description("VendorCredentials property for the optimal path.")]
		public void PersonifyConfiguration_Unit_VendorCredentials_Optimal() {
			String vendorBlock = "MyVendorBlock";
			String vendorPassword = "MyVendorPassword";
			String vendorUserName = "MyVendorUserName";
			Action<PersonifySsoConfigurationSection> setSsoProperties = ssoSection => {
				ssoSection.VendorBlock = vendorBlock;
				ssoSection.VendorPassword = vendorPassword;
				ssoSection.VendorUserName = vendorUserName;
			};
			using (CreateMockConfiguration(setSsoProperties, null)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				VendorCredentials expected = new VendorCredentials(vendorUserName, vendorPassword, vendorBlock);
				Assert.AreEqual(expected, target.SsoVendorCredentials);
			}
		}
		[TestMethod()]
		[Description("VendorCredentials property when one the vendor block is a null reference.")]
		public void PersonifyConfiguration_Unit_VendorCredentials_BlockNull() {
			String vendorBlock = null;
			String vendorPassword = "MyVendorPassword";
			String vendorUserName = "MyVendorUserName";
			Action<PersonifySsoConfigurationSection> setSsoProperties = ssoSection => {
				ssoSection.VendorBlock = vendorBlock;
				ssoSection.VendorPassword = vendorPassword;
				ssoSection.VendorUserName = vendorUserName;
			};
			using (CreateMockConfiguration(setSsoProperties, null)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				VendorCredentials expected = null;
				Assert.AreEqual(expected, target.SsoVendorCredentials);
			}
		}
		[TestMethod()]
		[Description("VendorCredentials property when one the vendor password is a null reference.")]
		public void PersonifyConfiguration_Unit_VendorCredentials_PasswordNull() {
			String vendorBlock = "MyVendorBlock";
			String vendorPassword = null;
			String vendorUserName = "MyVendorUserName";
			Action<PersonifySsoConfigurationSection> setSsoProperties = ssoSection => {
				ssoSection.VendorBlock = vendorBlock;
				ssoSection.VendorPassword = vendorPassword;
				ssoSection.VendorUserName = vendorUserName;
			};
			using (CreateMockConfiguration(setSsoProperties, null)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				VendorCredentials expected = null;
				Assert.AreEqual(expected, target.SsoVendorCredentials);
			}
		}
		[TestMethod()]
		[Description("VendorCredentials property when one the vendor user name is a null reference.")]
		public void PersonifyConfiguration_Unit_VendorCredentials_UserNameNull() {
			String vendorBlock = "MyVendorBlock";
			String vendorPassword = "MyVendorPassword";
			String vendorUserName = null;
			Action<PersonifySsoConfigurationSection> setSsoProperties = ssoSection => {
				ssoSection.VendorBlock = vendorBlock;
				ssoSection.VendorPassword = vendorPassword;
				ssoSection.VendorUserName = vendorUserName;
			};
			using (CreateMockConfiguration(setSsoProperties, null)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				VendorCredentials expected = null;
				Assert.AreEqual(expected, target.SsoVendorCredentials);
			}
		}

		[TestMethod()]
		[Description("VendorIdentifier property for the optimal path.")]
		public void PersonifyConfiguration_Unit_VendorIdentifier_Optimal() {
			String vendorIdentifier = "MyVendorIdentifier";
			using (CreateMockConfiguration(ssoSection => ssoSection.VendorIdentifier = vendorIdentifier, null)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				Assert.AreEqual(vendorIdentifier, target.VendorIdentifier);
			}
		}

		[TestMethod()]
		[Description("VendorPassword property for the optimal path.")]
		public void PersonifyConfiguration_Unit_VendorPassword_Optimal() {
			String vendorPassword = "MyVendorPassword";
			using (CreateMockConfiguration(ssoSection => ssoSection.VendorPassword = vendorPassword, null)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				Assert.AreEqual(vendorPassword, target.VendorPassword);
			}
		}

		[TestMethod()]
		[Description("VendorUsername property for the optimal path.")]
		public void PersonifyConfiguration_Unit_VendorUsername_Optimal() {
			String vendorUserName = "MyVendorUsername";
			using (CreateMockConfiguration(ssoSection => ssoSection.VendorUserName = vendorUserName, null)) {
				PersonifyConfiguration target = PersonifyConfiguration.Instance;
				Assert.AreEqual(vendorUserName, target.VendorUserName);
			}
		}
	}
}