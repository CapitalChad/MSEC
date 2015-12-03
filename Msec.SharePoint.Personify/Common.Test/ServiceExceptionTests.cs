using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec {
	/// <summary>
	/// This is a test class for <see cref="T:ServiceException"/> and is intended to contain all <see cref="T:ServiceException"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ServiceExceptionTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServiceExceptionTests"/> class.
		/// </summary>
		public ServiceExceptionTests() : base() { }

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
		public void ServiceException_Unit_Constructor1_Optimal() {
			new ServiceException();
		}
		[TestMethod()]
		[Description(".ctor(String) constructor for the optimal path.")]
		public void ServiceException_Unit_Constructor2_Optimal() {
			String message = "This is a test.";
			new ServiceException(message);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when message is a null reference.")]
		public void ServiceException_Unit_Constructor2_MessageNull() {
			String message = null;
			new ServiceException(message);
		}
		[TestMethod()]
		[Description(".ctor(String, Exception) constructor for the optimal path.")]
		public void ServiceException_Unit_Constructor3_Optimal() {
			String message = "This is a test.";
			Exception innerException = ExceptionHelper.CreateException();
			new ServiceException(message, innerException);
		}
		[TestMethod()]
		[Description(".ctor(String, Exception) constructor when message is a null reference.")]
		public void ServiceException_Unit_Constructor3_MessageNull() {
			String message = null;
			Exception innerException = ExceptionHelper.CreateException();
			new ServiceException(message, innerException);
		}
		[TestMethod()]
		[Description(".ctor(String, Exception) constructor when innerException is a null reference.")]
		public void ServiceException_Unit_Constructor3_InnerExceptionNull() {
			String message = "This is a test.";
			Exception innerException = null;
			new ServiceException(message, innerException);
		}
		[TestMethod()]
		[Description("Tests the .ctor(SerializationInfo, StreamingContext) constructor when info is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ServiceException_Unit_Constructor4_InfoNull() {
			SerializationInfo info = null;
			StreamingContext context = new StreamingContext();
			ExceptionHelper.CreateException<ServiceException>(info, context);
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class.")]
		public void ServiceException_Integration_Serialization_Optimal() {
			String message = "This is a test.";
			Exception innerException = ExceptionHelper.CreateException();
			ServiceException original = new ServiceException(message, innerException);

			ServiceException clone = CloneGenerator.SerializeBinary(original);
			Assert.AreEqual(original.Message, clone.Message);
			Assert.AreEqual(original.HelpLink, clone.HelpLink);
			Assert.AreEqual(original.Source, clone.Source);
			Assert.AreEqual(original.StackTrace, clone.StackTrace);
			Assert.AreEqual(original.InnerException.Message, clone.InnerException.Message);
			Assert.AreEqual(original.InnerException.HelpLink, clone.InnerException.HelpLink);
			Assert.AreEqual(original.InnerException.Source, clone.InnerException.Source);
			Assert.AreEqual(original.InnerException.StackTrace, clone.InnerException.StackTrace);
		}
	}
}