using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Diagnostics {
	/// <summary>
	/// This is a test class for <see cref="T:Logger"/> and is intended to contain all <see cref="T:Logger"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class LoggerTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:LoggerTests"/> class.
		/// </summary>
		public LoggerTests() : base() { }

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

	// Method Tests
		[TestMethod()]
		[Description("LogError(Object, String, Object[]) method for the optimal path.")]
		public void Logger_Unit_LogError_Optimal() {
			Object instance = new Object();
			String messageOrFormat = "This is a test: {0}.";
			Object[] args = new Object[] { "Testing" };
			Logger.LogError(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogError(Object, String, Object[]) method when 'args' is a null reference.")]
		public void Logger_Unit_LogError_ArgsNull() {
			Object instance = new Object();
			String messageOrFormat = "This is a test.";
			Object[] args = null;
			Logger.LogError(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogError(Object, String, Object[]) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Logger_Unit_LogError_InstanceNull() {
			Object instance = null;
			String messageOrFormat = "This is a test: {0}.";
			Object[] args = new Object[] { "Testing" };
			Logger.LogError(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogError(Object, String, Object[]) method when 'messageOrFormat' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Logger_Unit_LogError_MessageOrFormatNull() {
			Object instance = new Object();
			String messageOrFormat = null;
			Object[] args = new Object[] { "Testing" };
			Logger.LogError(instance, messageOrFormat, args);
		}

		[TestMethod()]
		[Description("LogInformation(Object, String, Object[]) method for the optimal path.")]
		public void Logger_Unit_LogInformation_Optimal() {
			Object instance = new Object();
			String messageOrFormat = "This is a test: {0}.";
			Object[] args = new Object[] { "Testing" };
			Logger.LogInformation(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogInformation(Object, String, Object[]) method when 'args' is a null reference.")]
		public void Logger_Unit_LogInformation_ArgsNull() {
			Object instance = new Object();
			String messageOrFormat = "This is a test.";
			Object[] args = null;
			Logger.LogInformation(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogInformation(Object, String, Object[]) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Logger_Unit_LogInformation_InstanceNull() {
			Object instance = null;
			String messageOrFormat = "This is a test: {0}.";
			Object[] args = new Object[] { "Testing" };
			Logger.LogInformation(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogInformation(Object, String, Object[]) method when 'messageOrFormat' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Logger_Unit_LogInformation_MessageOrFormatNull() {
			Object instance = new Object();
			String messageOrFormat = null;
			Object[] args = new Object[] { "Testing" };
			Logger.LogInformation(instance, messageOrFormat, args);
		}

		[TestMethod()]
		[Description("LogWarning(Object, String, Object[]) method for the optimal path.")]
		public void Logger_Unit_LogWarning_Optimal() {
			Object instance = new Object();
			String messageOrFormat = "This is a test: {0}.";
			Object[] args = new Object[] { "Testing" };
			Logger.LogWarning(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogWarning(Object, String, Object[]) method when 'args' is a null reference.")]
		public void Logger_Unit_LogWarning_ArgsNull() {
			Object instance = new Object();
			String messageOrFormat = "This is a test.";
			Object[] args = null;
			Logger.LogWarning(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogWarning(Object, String, Object[]) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Logger_Unit_LogWarning_InstanceNull() {
			Object instance = null;
			String messageOrFormat = "This is a test: {0}.";
			Object[] args = new Object[] { "Testing" };
			Logger.LogWarning(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogWarning(Object, String, Object[]) method when 'messageOrFormat' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Logger_Unit_LogWarning_MessageOrFormatNull() {
			Object instance = new Object();
			String messageOrFormat = null;
			Object[] args = new Object[] { "Testing" };
			Logger.LogWarning(instance, messageOrFormat, args);
		}

		[TestMethod()]
		[Description("LogVerbose(Object, String, Object[]) method for the optimal path.")]
		public void Logger_Unit_LogVerbose_Optimal() {
			Object instance = new Object();
			String messageOrFormat = "This is a test: {0}.";
			Object[] args = new Object[] { "Testing" };
			Logger.LogVerbose(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogVerbose(Object, String, Object[]) method when 'args' is a null reference.")]
		public void Logger_Unit_LogVerbose_ArgsNull() {
			Object instance = new Object();
			String messageOrFormat = "This is a test.";
			Object[] args = null;
			Logger.LogVerbose(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogVerbose(Object, String, Object[]) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Logger_Unit_LogVerbose_InstanceNull() {
			Object instance = null;
			String messageOrFormat = "This is a test: {0}.";
			Object[] args = new Object[] { "Testing" };
			Logger.LogVerbose(instance, messageOrFormat, args);
		}
		[TestMethod()]
		[Description("LogVerbose(Object, String, Object[]) method when 'messageOrFormat' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Logger_Unit_LogVerbose_MessageOrFormatNull() {
			Object instance = new Object();
			String messageOrFormat = null;
			Object[] args = new Object[] { "Testing" };
			Logger.LogVerbose(instance, messageOrFormat, args);
		}
	}
}