using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msec.Personify.Services;

using HttpStatusCode = System.Net.HttpStatusCode;

namespace Msec.Personify.Web {
	/// <summary>
	/// This is a test class for <see cref="T:PersonifySsoContext"/> and is intended to contain all <see cref="T:PersonifySsoContext"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class PersonifySsoContextTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifySsoContextTests"/> class.
		/// </summary>
		public PersonifySsoContextTests() : base() { }

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
		[Description(".ctor(HttpContext) constructor for the optimal path.")]
		public void PersonifySsoContext_Integration_Constructor_Optimal() {
			using (HttpContextProxy contextProxy = new HttpContextProxy()) {
				HttpContext httpContext = contextProxy.Context;
				new PersonifySsoContext(httpContext);
			}
		}
		[TestMethod()]
		[Description(".ctor(HttpContext) constructor when 'httpContext' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PersonifySsoContext_Integration_Constructor_HttpContextNull() {
			HttpContext httpContext = null;
			new PersonifySsoContext(httpContext);
		}

	// Method Tests
		[TestMethod()]
		[Description("AuthenticateRequest() method when the user has not been authenticated.")]
		public void PersonifySsoContext_Integration_AuthenticateRequest_NotAuthenticated() {
			using (MockPersonifySsoServiceProvider mockPersonifySsoServiceProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext httpContext = contextProxy.Context;
					PersonifySsoContext target = new PersonifySsoContext(httpContext);
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);

					target.AuthenticateRequest();
					Assert.AreEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreEqual(302, httpContext.Response.StatusCode);
				}
			}
		}
		[TestMethod()]
		[Description("AuthenticateRequest() method when the user is already logged in.")]
		public void PersonifySsoContext_Integration_AuthenticateRequest_AlreadyLoggedIn() {
			using (MockPersonifySsoServiceProvider mockPersonifySsoServiceProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext httpContext = contextProxy.Context;
					//new SessionTokenStorageLocation(httpContext).StoreToken(CustomerToken.Create("MyCustomerToken"));

					PersonifySsoContext target = new PersonifySsoContext(httpContext);
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);

					target.AuthenticateRequest();
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);
				}
			}
		}
		[TestMethod()]
		[Description("AuthenticateRequest() method when the user was redirected after a login.")]
		public void PersonifySsoContext_Integration_AuthenticateRequest_LoginRedirection() {
			using (MockPersonifySsoServiceProvider mockPersonifySsoServiceProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy("Default.aspx", "ct=MyEncryptedCustomerToken")) {
					HttpContext httpContext = contextProxy.Context;
					//TokenStorageLocation tokenStorageLocation = new SessionTokenStorageLocation(httpContext);
					//Assert.IsNull(tokenStorageLocation.RetrieveToken());

					PersonifySsoContext target = new PersonifySsoContext(httpContext);
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);

					target.AuthenticateRequest();
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);
					//Assert.IsNotNull(tokenStorageLocation.RetrieveToken());
				}
			}
		}
		[TestMethod()]
		[Description("AuthenticateRequest() method when the method is called multiple times.")]
		public void PersonifySsoContext_Integration_AuthenticateRequest_MultipleCalls() {
			using (MockPersonifySsoServiceProvider mockPersonifySsoServiceProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext httpContext = contextProxy.Context;
					PersonifySsoContext target = new PersonifySsoContext(httpContext);
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);

					target.AuthenticateRequest();
					Assert.AreEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreEqual(302, httpContext.Response.StatusCode);

					httpContext.Response.StatusCode = 200;
					httpContext.Response.SetFieldValue("_isRequestBeingRedirected", false);
					httpContext.Response.RedirectLocation = null;
					httpContext.Response.SetFieldValue("_redirectLocationSet", false);

					target.AuthenticateRequest();
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);
				}
			}
		}
		[TestMethod()]
		[Description("AuthenticateRequest() method when the method is called on multiple instances from the same HttpContext.")]
		public void PersonifySsoContext_Integration_AuthenticateRequest_MultipleInstancesCalledOnSameHttpContext() {
			using (MockPersonifySsoServiceProvider mockPersonifySsoServiceProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext httpContext = contextProxy.Context;
					PersonifySsoContext target = new PersonifySsoContext(httpContext);
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);

					target.AuthenticateRequest();
					Assert.AreEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreEqual(302, httpContext.Response.StatusCode);
					target = null;

					httpContext.Response.StatusCode = 200;
					httpContext.Response.SetFieldValue("_isRequestBeingRedirected", false);
					httpContext.Response.RedirectLocation = null;
					httpContext.Response.SetFieldValue("_redirectLocationSet", false);

					PersonifySsoContext newTarget = new PersonifySsoContext(httpContext);
					newTarget.AuthenticateRequest();
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);
				}
			}
		}
		[TestMethod()]
		[Description("AuthenticateRequest() method when the method is called on multiple instances from different HttpContext objects.")]
		public void PersonifySsoContext_Integration_AuthenticateRequest_MultipleInstancesCalledOnDifferentHttpContexts() {
			using (MockPersonifySsoServiceProvider mockPersonifySsoServiceProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext httpContext = contextProxy.Context;
					PersonifySsoContext target = new PersonifySsoContext(httpContext);
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);

					target.AuthenticateRequest();
					Assert.AreEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreEqual(302, httpContext.Response.StatusCode);
				}

				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext httpContext = contextProxy.Context;
					PersonifySsoContext target = new PersonifySsoContext(httpContext);
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);

					target.AuthenticateRequest();
					Assert.AreEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreEqual(302, httpContext.Response.StatusCode);
				}
			}
		}

		[TestMethod()]
		[Description("EndRequest() method when the user is unauthorized to see the current request.")]
		public void PersonifySsoContext_Integration_EndRequest_UserIsUnauthorized() {
			using (MockPersonifySsoServiceProvider mockPersonifySsoServiceProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext httpContext = contextProxy.Context;
					//new SessionTokenStorageLocation(httpContext).StoreToken(CustomerToken.Create("MyCustomerToken"));
					PersonifySsoContext target = new PersonifySsoContext(httpContext);
					target.AuthenticateRequest();
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);

					httpContext.Response.StatusCode = (Int32)HttpStatusCode.Unauthorized;
					target.EndRequest();
					Assert.AreEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreEqual(302, httpContext.Response.StatusCode);
				}
			}
		}
		[TestMethod()]
		[Description("EndRequest() method when the user is authorized to see the current request.")]
		public void PersonifySsoContext_Integration_EndRequest_UserIsAuthorized() {
			using (MockPersonifySsoServiceProvider mockPersonifySsoServiceProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext httpContext = contextProxy.Context;
					//new SessionTokenStorageLocation(httpContext).StoreToken(CustomerToken.Create("MyCustomerToken"));
					PersonifySsoContext target = new PersonifySsoContext(httpContext);
					target.AuthenticateRequest();
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);

					httpContext.Response.StatusCode = (Int32)HttpStatusCode.OK;
					target.EndRequest();
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);
				}
			}
		}
		[TestMethod()]
		[Description("EndRequest() method when the method is called without first calling AuthenticateRequest().")]
		public void PersonifySsoContext_Integration_EndRequest_AuthenticateRequestNotCalled() {
			using (MockPersonifySsoServiceProvider mockPersonifySsoServiceProvider = new MockPersonifySsoServiceProvider()) {
				using (HttpContextProxy contextProxy = new HttpContextProxy()) {
					HttpContext httpContext = contextProxy.Context;
					//new SessionTokenStorageLocation(httpContext).StoreToken(CustomerToken.Create("MyCustomerToken"));
					PersonifySsoContext target = new PersonifySsoContext(httpContext);

					target.EndRequest();
					Assert.AreNotEqual(true, httpContext.Response.IsRequestBeingRedirected);
					Assert.AreNotEqual(302, httpContext.Response.StatusCode);
				}
			}
		}
	}
}