using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Personify.Configuration {
	/// <summary>
	/// This is a test class for <see cref="T:PersonifySsoConfigurationSection"/> and is intended to contain all <see cref="T:PersonifySsoConfigurationSection"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class PersonifySsoConfigurationSectionTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifySsoConfigurationSectionTests"/> class.
		/// </summary>
		public PersonifySsoConfigurationSectionTests() : base() { }

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
		public void PersonifySsoConfigurationSection_Unit_Constructor_Optimal() {
			new PersonifySsoConfigurationSection();
		}

	// Property Tests
		[TestMethod()]
		[Description("LoginPageUrl property for the optimal path.")]
		public void PersonifySsoConfigurationSection_Unit_LoginPageUrl_Optimal() {
			PersonifySsoConfigurationSection target = new PersonifySsoConfigurationSection();
			Uri value = new Uri("http://www.google.com/");
			target.LoginPageUrl = value;
			Uri actualValue = target.LoginPageUrl;
			Assert.AreEqual(value, actualValue);
		}
		[TestMethod()]
		[Description("LoginPageUrl property for the default value.")]
		public void PersonifySsoConfigurationSection_Unit_LoginPageUrl_DefaultValue() {
			PersonifySsoConfigurationSection target = new PersonifySsoConfigurationSection();
			Uri actualValue = target.LoginPageUrl;
			Assert.AreEqual(new Uri("http://pers.msec.org/PersonifySSO/login.aspx"), actualValue);
		}

		[TestMethod()]
		[Description("VendorBlock property for the optimal path.")]
		public void PersonifySsoConfigurationSection_Unit_VendorBlock_Optimal() {
			PersonifySsoConfigurationSection target = new PersonifySsoConfigurationSection();
			String value = "MyVendorBlock";
			target.VendorBlock = value;
			String actualValue = target.VendorBlock;
			Assert.AreEqual(value, actualValue);
		}
		[TestMethod()]
		[Description("VendorBlock property for the default value.")]
		public void PersonifySsoConfigurationSection_Unit_VendorBlock_DefaultValue() {
			PersonifySsoConfigurationSection target = new PersonifySsoConfigurationSection();
			String actualValue = target.VendorBlock;
			Assert.AreEqual(String.Empty, actualValue);
		}

		[TestMethod()]
		[Description("VendorIdentifier property for the optimal path.")]
		public void PersonifySsoConfigurationSection_Unit_VendorIdentifier_Optimal() {
			PersonifySsoConfigurationSection target = new PersonifySsoConfigurationSection();
			String value = "MyVendorIdentifier";
			target.VendorIdentifier = value;
			String actualValue = target.VendorIdentifier;
			Assert.AreEqual(value, actualValue);
		}
		[TestMethod()]
		[Description("VendorIdentifier property for the default value.")]
		public void PersonifySsoConfigurationSection_Unit_VendorIdentifier_DefaultValue() {
			PersonifySsoConfigurationSection target = new PersonifySsoConfigurationSection();
			String actualValue = target.VendorIdentifier;
			Assert.AreEqual(String.Empty, actualValue);
		}

		[TestMethod()]
		[Description("VendorPassword property for the optimal path.")]
		public void PersonifySsoConfigurationSection_Unit_VendorPassword_Optimal() {
			PersonifySsoConfigurationSection target = new PersonifySsoConfigurationSection();
			String value = "MyVendorPassword";
			target.VendorPassword = value;
			String actualValue = target.VendorPassword;
			Assert.AreEqual(value, actualValue);
		}
		[TestMethod()]
		[Description("VendorPassword property for the default value.")]
		public void PersonifySsoConfigurationSection_Unit_VendorPassword_DefaultValue() {
			PersonifySsoConfigurationSection target = new PersonifySsoConfigurationSection();
			String actualValue = target.VendorPassword;
			Assert.AreEqual(String.Empty, actualValue);
		}

		[TestMethod()]
		[Description("VendorUsername property for the optimal path.")]
		public void PersonifySsoConfigurationSection_Unit_VendorUsername_Optimal() {
			PersonifySsoConfigurationSection target = new PersonifySsoConfigurationSection();
			String value = "MyVendorUsername";
			target.VendorUserName = value;
			String actualValue = target.VendorUserName;
			Assert.AreEqual(value, actualValue);
		}
		[TestMethod()]
		[Description("VendorUsername property for the default value.")]
		public void PersonifySsoConfigurationSection_Unit_VendorUsername_DefaultValue() {
			PersonifySsoConfigurationSection target = new PersonifySsoConfigurationSection();
			String actualValue = target.VendorUserName;
			Assert.AreEqual(String.Empty, actualValue);
		}

	// Method Tests
		[TestMethod()]
		[Description("GetSection(String) method when an incorrect section type is referenced.")]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void PersonifySsoConfigurationSection_Integration_GetSection2_IncorrectSectionType() {
			typeof(PersonifySsoConfigurationSection).InvokeMethod("GetSection", "msec.personify/personifyUniversal");
		}
		[TestMethod()]
		[Description("GetSection(String) method when an section is referenced that doesn't exist.")]
		public void PersonifySsoConfigurationSection_Integration_GetSection2_MissingSection() {
			typeof(PersonifySsoConfigurationSection).InvokeMethod("GetSection", "NotASection");
		}
	}
}