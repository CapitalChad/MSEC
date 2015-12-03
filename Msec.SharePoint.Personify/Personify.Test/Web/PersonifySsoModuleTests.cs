using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msec.Personify.Services;

using EventHandlerList = System.ComponentModel.EventHandlerList;
using FormsAuthenticationModule = System.Web.Security.FormsAuthenticationModule;

namespace Msec.Personify.Web {
	/// <summary>
	/// This is a test class for <see cref="T:PersonifySsoModule"/> and is intended to contain all <see cref="T:PersonifySsoModule"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class PersonifySsoModuleTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifySsoModuleTests"/> class.
		/// </summary>
		public PersonifySsoModuleTests() : base() { }

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
		public void PersonifySsoModule_Unit_Constructor_Optimal() {
			new PersonifySsoModule();
		}

	// Method Tests
		[TestMethod()]
		[Description("Tests the module assuming the current user is already logged in.")]
		public void PersonifySsoModule_Integration_Init_UserIsLoggedIn() {
			using (MockPersonifySsoServiceProvider mockSsoProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext context = contextProxy.Context;
					//new SessionTokenStorageLocation(context).StoreToken(CustomerToken.Create("MyCustomerToken"));
					//Assert.IsNotNull(new SessionTokenStorageLocation(context).RetrieveToken());

					Pair<String, IHttpModule> namedFormsAuthenticationModule = new Pair<String, IHttpModule>(
						"FormsAuthenticationModule",
						new FormsAuthenticationModule());
					using (HttpApplicationProxy applicationProxy = new HttpApplicationProxy(context, namedFormsAuthenticationModule)) {
						HttpApplication application = applicationProxy.Application;

						PersonifySsoModule target = new PersonifySsoModule();
						try {
							target.Init(application);
							applicationProxy.RaiseAuthenticateRequest();
							applicationProxy.RaiseEndRequest();
						}
						finally {
							target.Dispose();
						}
						Assert.AreNotEqual(true, context.Response.IsRequestBeingRedirected);
						Assert.AreNotEqual(302, context.Response.StatusCode);
						//Assert.IsNotNull(new SessionTokenStorageLocation(context).RetrieveToken());
					}
				}
			}
		}
		[TestMethod()]
		[Description("Tests the module assuming the current user is not authenticated yet.")]
		public void PersonifySsoModule_Integration_Init_UserIsUnathenticated() {
			using (MockPersonifySsoServiceProvider mockSsoProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext context = contextProxy.Context;

					Pair<String, IHttpModule> namedFormsAuthenticationModule = new Pair<String, IHttpModule>(
						"FormsAuthenticationModule",
						new FormsAuthenticationModule());
					using (HttpApplicationProxy applicationProxy = new HttpApplicationProxy(context, namedFormsAuthenticationModule)) {
						HttpApplication application = applicationProxy.Application;

						PersonifySsoModule target = new PersonifySsoModule();
						try {
							target.Init(application);
							applicationProxy.RaiseAuthenticateRequest();
							applicationProxy.RaiseEndRequest();
						}
						finally {
							target.Dispose();
						}
						Assert.AreEqual(true, context.Response.IsRequestBeingRedirected);
						Assert.AreEqual(302, context.Response.StatusCode);
						//Assert.IsNull(new SessionTokenStorageLocation(context).RetrieveToken());
					}
				}
			}
		}
		[TestMethod()]
		[Description("Tests the module assuming the current user was just redirected from the login page.")]
		public void PersonifySsoModule_Integration_Init_UserIsRedirectedFromLoginPage() {
			using (MockPersonifySsoServiceProvider mockSsoProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy("Default.aspx", "ct=MyCustomerToken")) {
					HttpContext context = contextProxy.Context;
					//Assert.IsNull(new SessionTokenStorageLocation(context).RetrieveToken());

					Pair<String, IHttpModule> namedFormsAuthenticationModule = new Pair<String, IHttpModule>(
						"FormsAuthenticationModule",
						new FormsAuthenticationModule());
					using (HttpApplicationProxy applicationProxy = new HttpApplicationProxy(context, namedFormsAuthenticationModule)) {
						HttpApplication application = applicationProxy.Application;

						PersonifySsoModule target = new PersonifySsoModule();
						try {
							target.Init(application);
							applicationProxy.RaiseAuthenticateRequest();
							applicationProxy.RaiseEndRequest();
						}
						finally {
							target.Dispose();
						}
						Assert.AreNotEqual(true, context.Response.IsRequestBeingRedirected);
						Assert.AreNotEqual(302, context.Response.StatusCode);
						//Assert.IsNotNull(new SessionTokenStorageLocation(context).RetrieveToken());
					}
				}
			}
		}
	}
}