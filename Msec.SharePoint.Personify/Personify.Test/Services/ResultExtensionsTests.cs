using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Result = Msec.Personify.Services.UniversalWebServiceImpl.Result;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:ResultExtensions"/> and is intended to contain all <see cref="T:ResultExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ResultExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ResultExtensionsTests"/> class.
		/// </summary>
		public ResultExtensionsTests() : base() { }

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
		/// A helper method that invokes the IsLoggedOut(Result) method.
		/// </summary>
		/// <param name="instance">The result instance.</param>
		/// <returns>The result of the call.</returns>
		private static Boolean IsLoggedOut(Result instance) {
			Type type = Type.GetType("Msec.Personify.Services.ResultExtensions, Msec.Personify");
			return (Boolean)type.InvokeMethod("IsLoggedOut", new Type[] { typeof(Result) }, new Object[] { instance });
		}
		/// <summary>
		/// A helper method that invokes the ThrowIfUnsuccessful(Result) method.
		/// </summary>
		/// <param name="instance">The result instance.</param>
		private static void ThrowIfUnsuccessful(Result instance) {
			Type type = Type.GetType("Msec.Personify.Services.ResultExtensions, Msec.Personify");
			type.InvokeMethod("ThrowIfUnsuccessful", new Type[] { typeof(Result) }, new Object[] { instance });
		}

	// Method Tests
		[TestMethod()]
		[Description("IsLoggedOut(Result) method for the optimal true path.")]
		public void ResultExtensions_Unit_IsLoggedOut_OptimalTrue() {
			Result instance = new Result() {
				IsSuccessful = false,
				ErrorCause = "System.Exception: Invalid Token, please login again."
			};
			Boolean result = ResultExtensionsTests.IsLoggedOut(instance);
			Assert.AreEqual(true, result);
		}
		[TestMethod()]
		[Description("IsLoggedOut(Result) method for the optimal false path.")]
		public void ResultExtensions_Unit_IsLoggedOut_OptimalFalse1() {
			Result instance = new Result() {
				IsSuccessful = true,
				ErrorCause = "System.Exception: Invalid Token, please login again."
			};
			Boolean result = ResultExtensionsTests.IsLoggedOut(instance);
			Assert.AreEqual(false, result);
		}
		[TestMethod()]
		[Description("IsLoggedOut(Result) method for the optimal false path.")]
		public void ResultExtensions_Unit_IsLoggedOut_OptimalFalse2() {
			Result instance = new Result() {
				IsSuccessful = false
			};
			Boolean result = ResultExtensionsTests.IsLoggedOut(instance);
			Assert.AreEqual(false, result);
		}
		[TestMethod()]
		[Description("IsLoggedOut(Result) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ResultExtensions_Unit_IsLoggedOut_InstanceNull() {
			Result instance = null;
			ResultExtensionsTests.IsLoggedOut(instance);
		}

		[TestMethod()]
		[Description("ThrowIfUnsuccessful(Result) method for the optimal path when no exception is expected.")]
		public void ResultExtensions_Unit_ThrowIfUnsuccessful_OptimalNoException() {
			Result instance = new Result() {
				IsSuccessful = true
			};
			ResultExtensionsTests.ThrowIfUnsuccessful(instance);
		}
		[TestMethod()]
		[Description("ThrowIfUnsuccessful(Result) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ResultExtensions_Unit_ThrowIfUnsuccessful_InstanceNull() {
			Result instance = null;
			ResultExtensionsTests.ThrowIfUnsuccessful(instance);
		}
		[TestMethod()]
		[Description("ThrowIfUnsuccessful(Result) method when an InvalidOperationException is specified.")]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ResultExtensions_Unit_ThrowIfUnsuccessful_OptimalInvalidOperationException() {
			Result instance = new Result() {
				IsSuccessful = false,
				ErrorCause = "System.InvalidOperationException: This is a test."
			};
			ResultExtensionsTests.ThrowIfUnsuccessful(instance);
		}
		[TestMethod()]
		[Description("ThrowIfUnsuccessful(Result) method when no exception type is specified.")]
		[ExpectedException(typeof(ServerServiceException))]
		public void ResultExtensions_Unit_ThrowIfUnsuccessful_OptimalNoExceptionSpecified() {
			Result instance = new Result() {
				IsSuccessful = false
			};
			ResultExtensionsTests.ThrowIfUnsuccessful(instance);
		}
	}
}