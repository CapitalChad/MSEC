using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Personify.Configuration {
	/// <summary>
	/// This is a test class for <see cref="T:PersonifyUniversalConfigurationSection"/> and is intended to contain all <see cref="T:PersonifyUniversalConfigurationSection"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class PersonifyUniversalConfigurationSectionTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyUniversalConfigurationSectionTests"/> class.
		/// </summary>
		public PersonifyUniversalConfigurationSectionTests() : base() { }

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
		[Description(".ctor() constructor for the optimal path.")]
		public void PersonifyUniversalConfigurationSection_Unit_Constructor_Optimal() {
			new PersonifyUniversalConfigurationSection();
		}

	// Property Tests
		[TestMethod()]
		[Description("OrganizationId property for the optimal path.")]
		public void PersonifyUniversalConfigurationSection_Unit_OrganizationId_Optimal() {
			String expected = "Test";
			String actual;

			PersonifyUniversalConfigurationSection target = new PersonifyUniversalConfigurationSection();
			target.OrganizationId = expected;
			actual = target.OrganizationId;

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("OrganizationId property for the default value.")]
		public void PersonifyUniversalConfigurationSection_Unit_OrganizationId_DefaultValue() {
			String expected = "MSEC";
			String actual;

			PersonifyUniversalConfigurationSection target = new PersonifyUniversalConfigurationSection();
			actual = target.OrganizationId;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("OrganizationUnitId property for the optimal path.")]
		public void PersonifyUniversalConfigurationSection_Unit_OrganizationUnitId_Optimal() {
			String expected = "Test";
			String actual;

			PersonifyUniversalConfigurationSection target = new PersonifyUniversalConfigurationSection();
			target.OrganizationUnitId = expected;
			actual = target.OrganizationUnitId;

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("OrganizationUnitId property for the default value.")]
		public void PersonifyUniversalConfigurationSection_Unit_OrganizationUnitId_DefaultValue() {
			String expected = "MSEC";
			String actual;

			PersonifyUniversalConfigurationSection target = new PersonifyUniversalConfigurationSection();
			actual = target.OrganizationUnitId;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("Password property for the optimal path.")]
		public void PersonifyUniversalConfigurationSection_Unit_Password_Optimal() {
			String expected = "Test";
			String actual;

			PersonifyUniversalConfigurationSection target = new PersonifyUniversalConfigurationSection();
			target.Password = expected;
			actual = target.Password;

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Password property for the default value.")]
		public void PersonifyUniversalConfigurationSection_Unit_Password_DefaultValue() {
			String expected = String.Empty;
			String actual;

			PersonifyUniversalConfigurationSection target = new PersonifyUniversalConfigurationSection();
			actual = target.Password;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("ServiceUrl property for the optimal path.")]
		public void PersonifyUniversalConfigurationSection_Unit_ServiceUrl_Optimal() {
			Uri expected = new Uri("http://www.google.com");
			Uri actual;

			PersonifyUniversalConfigurationSection target = new PersonifyUniversalConfigurationSection();
			target.ServiceUrl = expected;
			actual = target.ServiceUrl;

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("ServiceUrl property for the default value.")]
		public void PersonifyUniversalConfigurationSection_Unit_ServiceUrl_DefaultValue() {
			Uri expected = new Uri(String.Empty, UriKind.Relative);
			Uri actual;

			PersonifyUniversalConfigurationSection target = new PersonifyUniversalConfigurationSection();
			actual = target.ServiceUrl;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("UserName property for the optimal path.")]
		public void PersonifyUniversalConfigurationSection_Unit_UserName_Optimal() {
			String expected = "Test";
			String actual;

			PersonifyUniversalConfigurationSection target = new PersonifyUniversalConfigurationSection();
			target.UserName = expected;
			actual = target.UserName;

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("UserName property for the default value.")]
		public void PersonifyUniversalConfigurationSection_Unit_UserName_DefaultValue() {
			String expected = String.Empty;
			String actual;

			PersonifyUniversalConfigurationSection target = new PersonifyUniversalConfigurationSection();
			actual = target.UserName;

			Assert.AreEqual(expected, actual);
		}

	// Method Tests
		[TestMethod()]
		[Description("GetSection(String) method when an incorrect section type is referenced.")]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void PersonifyUniversalConfigurationSection_Integration_GetSection2_IncorrectSectionType() {
			typeof(PersonifyUniversalConfigurationSection).InvokeMethod("GetSection", "msec.personify/personifySso");
		}
		[TestMethod()]
		[Description("GetSection(String) method when an section is referenced that doesn't exist.")]
		public void PersonifyUniversalConfigurationSection_Integration_GetSection2_MissingSection() {
			typeof(PersonifyUniversalConfigurationSection).InvokeMethod("GetSection", "NotASection");
		}
	}
}