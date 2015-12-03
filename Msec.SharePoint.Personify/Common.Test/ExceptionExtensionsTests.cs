using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec {
	/// <summary>
	/// This is a test class for <see cref="T:ExceptionExtensions"/> and is intended to contain all <see cref="T:ExceptionExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ExceptionExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ExceptionExtensionsTests"/> class.
		/// </summary>
		public ExceptionExtensionsTests() : base() { }

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
		[Description("CanBeHandledSafely(Exception) method for the optimal true path.")]
		public void ExceptionExtensions_Unit_CanBeHandledSafely_OptimalTrue() {
			Exception instance = new Exception();
			Boolean expected = true;
			Boolean actual = ExceptionExtensions.CanBeHandledSafely(instance);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("CanBeHandledSafely(Exception) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionExtensions_Unit_CanBeHandledSafely_InstanceNull() {
			Exception instance = null;
			ExceptionExtensions.CanBeHandledSafely(instance);
		}
		[TestMethod()]
		[Description("CanBeHandledSafely(Exception) method for the optimal false path.")]
		public void ExceptionExtensions_Unit_CanBeHandledSafely_OptimalFalse() {
			Exception[] instances = new Exception[] {
				new OutOfMemoryException(),
				new AppDomainUnloadedException(),
				new BadImageFormatException(),
				new CannotUnloadAppDomainException(),
				new InvalidProgramException(),
				(Exception)Activator.CreateInstance(typeof(ThreadAbortException), true)
			};

			Boolean expected = false;
			foreach (Exception instance in instances) {
				Boolean actual = ExceptionExtensions.CanBeHandledSafely(instance);
				Assert.AreEqual(expected, actual);
			}
		}
	}
}
