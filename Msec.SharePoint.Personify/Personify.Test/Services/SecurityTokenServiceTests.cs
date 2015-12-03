using System;
using System.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:SecurityTokenService"/> and is intended to contain all <see cref="T:SecurityTokenService"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class SecurityTokenServiceTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SecurityTokenServiceTests"/> class.
		/// </summary>
		public SecurityTokenServiceTests() : base() { }

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
		public void SecurityTokenService_Unit_Constructor_Optimal() {
			new MockSecurityTokenService().Dispose();
		}

	// Method Tests
		[TestMethod()]
		[Description("IssuesBearerSecurityToken(UserNameSecurityToken, Uri) method for the optimal path.")]
		public void SecurityTokenService_Unit_IssueBearerSecurityToken_Optimal() {
			SecurityToken expected = new SamlSecurityToken(new SamlAssertion());
			using (new MockSecurityTokenServiceProvider(expected)) {
				using (SecurityTokenService target = SecurityTokenService.NewSecurityTokenService()) {
					UserNameSecurityToken onBehalfOf = new UserNameSecurityToken("MyUserName", "MyPassword");
					Uri appliesTo = new Uri("http://www.google.com");
					SecurityToken actual = target.IssueBearerSecurityToken(onBehalfOf, appliesTo);
					Assert.AreSame(expected, actual);
				}
			}
		}
		[TestMethod()]
		[Description("IssuesBearerSecurityToken(UserNameSecurityToken, Uri) method when 'onBehalfOf' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SecurityTokenService_Unit_IssueBearerSecurityToken_OnBehalfOfNull() {
			SecurityToken expected = new SamlSecurityToken(new SamlAssertion());
			using (new MockSecurityTokenServiceProvider(expected)) {
				using (SecurityTokenService target = SecurityTokenService.NewSecurityTokenService()) {
					UserNameSecurityToken onBehalfOf = null;
					Uri appliesTo = new Uri("http://www.google.com");
					target.IssueBearerSecurityToken(onBehalfOf, appliesTo);
				}
			}
		}
		[TestMethod()]
		[Description("IssuesBearerSecurityToken(UserNameSecurityToken, Uri) method when 'appliesTo' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SecurityTokenService_Unit_IssueBearerSecurityToken_AppliesToNull() {
			SecurityToken expected = new SamlSecurityToken(new SamlAssertion());
			using (new MockSecurityTokenServiceProvider(expected)) {
				using (SecurityTokenService target = SecurityTokenService.NewSecurityTokenService()) {
					UserNameSecurityToken onBehalfOf = new UserNameSecurityToken("MyUserName", "MyPassword");
					Uri appliesTo = null;
					target.IssueBearerSecurityToken(onBehalfOf, appliesTo);
				}
			}
		}
		[TestMethod()]
		[Description("IssuesBearerSecurityToken(UserNameSecurityToken, Uri) method when the service object has been disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void SecurityTokenService_Unit_IssueBearerSecurityToken_TargetDisposed() {
			SecurityToken expected = new SamlSecurityToken(new SamlAssertion());
			using (new MockSecurityTokenServiceProvider(expected)) {
				SecurityTokenService target = SecurityTokenService.NewSecurityTokenService();
				target.Dispose();

				UserNameSecurityToken onBehalfOf = new UserNameSecurityToken("MyUserName", "MyPassword");
				Uri appliesTo = new Uri("http://www.google.com");
				target.IssueBearerSecurityToken(onBehalfOf, appliesTo);
			}
		}

		[TestMethod()]
		[Description("NewSecurityTokenService() method for the optimal path.")]
		public void SecurityTokenService_Unit_NewSecurityTokenService_Optimal() {
			using (new MockSecurityTokenServiceProvider()) {
				using (SecurityTokenService securityTokenService = SecurityTokenService.NewSecurityTokenService()) {
					Assert.IsNotNull(securityTokenService);
				}
			}
		}
	}
}