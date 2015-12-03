using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msec.Personify.Configuration;
using Msec.Personify.Services.UniversalWebServiceImpl;

using NetworkCredential = System.Net.NetworkCredential;
using System.Configuration;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:WebPersonifyUniversalService"/> and is intended to contain all <see cref="T:WebPersonifyUniversalService"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class WebPersonifyUniversalServiceTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:WebPersonifyUniversalServiceTests"/> class.
		/// </summary>
		public WebPersonifyUniversalServiceTests() : base() { }

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
		/// Creates a configuration section that uses valid credential and service configuration settings.
		/// </summary>
		/// <returns>The configuration section created.</returns>
		private static PersonifyUniversalConfigurationSection CreateConfigurationSection() {
			PersonifyUniversalConfigurationSection configurationSection = (PersonifyUniversalConfigurationSection)ConfigurationManager.GetSection("msec.personify/personifyUniversal");
			return configurationSection;
		}
		/// <summary>
		/// Creates a mock configuration provider that uses valid credential and service configuration settings.
		/// </summary>
		/// <returns>The mock configuration provider created.</returns>
		private static MockPersonifyConfigurationProvider CreateMockConfiguration() {
			PersonifyUniversalConfigurationSection configurationSection = WebPersonifyUniversalServiceTests.CreateConfigurationSection();
			return new MockPersonifyConfigurationProvider(configurationSection);
		}

	// Constructor Tests
		[TestMethod()]
		[Description(".ctor() constructor for the optimal path.")]
		public void WebPersonifyUniversalService_System_Constructor1_Optimal() {
			using (MockPersonifyConfigurationProvider mockConfiguration = CreateMockConfiguration()) {
				WebPersonifyUniversalService target = null;
				try {
					target = new WebPersonifyUniversalService();
					Assert.AreEqual(true, target.IsLoggedIn);
					Assert.AreEqual(mockConfiguration.UniversalSection.ServiceUrl, target.ServiceUrl);
				}
				finally {
					if (target != null) {
						target.Dispose();
					}
				}
				Assert.AreEqual(false, target.IsLoggedIn);
			}
		}

		[TestMethod()]
		[Description(".ctor(Uri, NetworkCredential, String, String) constructor for the optimal path.")]
		public void WebPersonifyUniversalService_System_Constructor2_Optimal() {
			PersonifyUniversalConfigurationSection configurationSection = WebPersonifyUniversalServiceTests.CreateConfigurationSection();
			Uri serviceUrl = configurationSection.ServiceUrl;
			NetworkCredential credentials = new NetworkCredential(configurationSection.UserName, configurationSection.Password);
			String organizationId = configurationSection.OrganizationId;
			String organizationUnitId = configurationSection.OrganizationUnitId;

			WebPersonifyUniversalService target = null;
			try {
				target = new WebPersonifyUniversalService(serviceUrl, credentials, organizationId, organizationUnitId);
				Assert.AreEqual(true, target.IsLoggedIn);
				Assert.AreEqual(serviceUrl, target.ServiceUrl);
			}
			finally {
				if (target != null) {
					target.Dispose();
				}
			}
			Assert.AreEqual(false, target.IsLoggedIn);
		}
		[TestMethod()]
		[Description(".ctor(Uri, NetworkCredential, String, String) constructor when the 'serviceUrl' argument is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void WebPersonifyUniversalService_Unit_Constructor2_ServiceUrlNull() {
			PersonifyUniversalConfigurationSection configurationSection = WebPersonifyUniversalServiceTests.CreateConfigurationSection();
			Uri serviceUrl = null;
			NetworkCredential credentials = new NetworkCredential(configurationSection.UserName, configurationSection.Password);
			String organizationId = configurationSection.OrganizationId;
			String organizationUnitId = configurationSection.OrganizationUnitId;

			using (WebPersonifyUniversalService target = new WebPersonifyUniversalService(serviceUrl, credentials, organizationId, organizationUnitId)) {
			}
		}
		[TestMethod()]
		[Description(".ctor(Uri, NetworkCredential, String, String) constructor when the 'credentials' argument is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void WebPersonifyUniversalService_Unit_Constructor2_CredentialsNull() {
			PersonifyUniversalConfigurationSection configurationSection = WebPersonifyUniversalServiceTests.CreateConfigurationSection();
			Uri serviceUrl = configurationSection.ServiceUrl;
			NetworkCredential credentials = null;
			String organizationId = configurationSection.OrganizationId;
			String organizationUnitId = configurationSection.OrganizationUnitId;

			using (WebPersonifyUniversalService target = new WebPersonifyUniversalService(serviceUrl, credentials, organizationId, organizationUnitId)) {
			}
		}
		[TestMethod()]
		[Description(".ctor(Uri, NetworkCredential, String, String) constructor when the 'organizationId' argument is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void WebPersonifyUniversalService_Unit_Constructor2_OrganizationIdNull() {
			PersonifyUniversalConfigurationSection configurationSection = WebPersonifyUniversalServiceTests.CreateConfigurationSection();
			Uri serviceUrl = configurationSection.ServiceUrl;
			NetworkCredential credentials = new NetworkCredential(configurationSection.UserName, configurationSection.Password);
			String organizationId = null;
			String organizationUnitId = configurationSection.OrganizationUnitId;

			using (WebPersonifyUniversalService target = new WebPersonifyUniversalService(serviceUrl, credentials, organizationId, organizationUnitId)) {
			}
		}
		[TestMethod()]
		[Description(".ctor(Uri, NetworkCredential, String, String) constructor when the 'organizationUnitId' argument is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void WebPersonifyUniversalService_Unit_Constructor2_OrganizationUnitIdNull() {
			PersonifyUniversalConfigurationSection configurationSection = WebPersonifyUniversalServiceTests.CreateConfigurationSection();
			Uri serviceUrl = configurationSection.ServiceUrl;
			NetworkCredential credentials = new NetworkCredential(configurationSection.UserName, configurationSection.Password);
			String organizationId = configurationSection.OrganizationId;
			String organizationUnitId = null;

			using (WebPersonifyUniversalService target = new WebPersonifyUniversalService(serviceUrl, credentials, organizationId, organizationUnitId)) {
			}
		}

	// Method Tests
		[TestMethod()]
		[Description("GetCommitteeMembersCore(Constraint) method for the optimal path.")]
		public void WebPersonifyUniversalService_System_GetCommitteeMembersCore_Optimal() {
			String userName = "00002166";

			IEnumerable<CommitteeMemberData> committeeMemberDatas;
			using (WebPersonifyUniversalServiceTests.CreateMockConfiguration()) {
				using (WebPersonifyUniversalService target = new WebPersonifyUniversalService()) {
					Constraint constraint = new Constraint(CommitteeMemberData.CustomerUserNameKey, ConstraintOperator.Equals, userName);
					committeeMemberDatas = target.GetCommitteeMembers(constraint);
				}
			}

			Assert.IsNotNull(committeeMemberDatas);
			Assert.AreNotEqual(0, committeeMemberDatas.Count());
		}
		[TestMethod()]
		[Description("GetCustomersCore(Constraint) method for the optimal path.")]
		public void WebPersonifyUniversalService_System_GetCustomersCore_Optimal() {
			String userNameStartsWith = "000128";

			IEnumerable<CustomerData> customerDatas;
			using (WebPersonifyUniversalServiceTests.CreateMockConfiguration()) {
				using (WebPersonifyUniversalService target = new WebPersonifyUniversalService()) {
					Constraint constraint = new Constraint(CustomerData.UserNameKey, ConstraintOperator.StartsWith, userNameStartsWith);
					customerDatas = target.GetCustomers(constraint);
				}
			}

			Assert.IsNotNull(customerDatas);
			Assert.AreNotEqual(0, customerDatas.Count());
		}
	}
}
