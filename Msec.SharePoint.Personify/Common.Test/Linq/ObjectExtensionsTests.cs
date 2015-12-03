using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Linq {
	/// <summary>
	/// This is a test class for <see cref="T:ObjectExtensions"/> and is intended to contain all <see cref="T:ObjectExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ObjectExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ObjectExtensionsTests"/> class.
		/// </summary>
		public ObjectExtensionsTests() : base() { }

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
		[Description("Project(TInput, Func<TInput, TOutput>) method for the optimal path.")]
		public void ObjectExtensions_Unit_Project_Optimal() {
			var instance = new {
				FirstName = "Chad",
				LastName = "Greer"
			};
			var output = instance.Project(input => new { FullName = input.FirstName + " " + input.LastName });
			Assert.AreEqual(instance.FirstName + " " + instance.LastName, output.FullName);
		}
		[TestMethod()]
		[Description("Project(TInput, Func<TInput, TOutput>) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ObjectExtensions_Unit_Project_InstanceNull() {
			String instance = null;
			ObjectExtensions.Project(instance, input => new { FullName = input });
		}
		[TestMethod()]
		[Description("Project(TInput, Func<TInput, TOutput>) method when 'projection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ObjectExtensions_Unit_Project_ProjectionNull() {
			String[] instance = new String[] { "Chad", "Greer" };
			Func<String[], String> projection = null;
			ObjectExtensions.Project(instance, projection);
		}
	}
}