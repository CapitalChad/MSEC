using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec {
	/// <summary>
	/// This is a test class for the Pair classes and is intended to contain all Pair Unit Tests.
	///</summary>
	[TestClass()]
	public class PairTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PairTests"/> class.
		/// </summary>
		public PairTests() : base() { }

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
		[Description("Tests the .ctor() constructor for the optimal path.")]
		public void Pair1_Unit_Constructor1_Optimal() {
			String expected = default(String);
			Pair<String> target = new Pair<String>();
			Assert.AreEqual(expected, target.First);
			Assert.AreEqual(expected, target.Second);
		}
		[TestMethod()]
		[Description("Tests the .ctor(T, T) constructor for the optimal path.")]
		public void Pair1_Unit_Constructor2_Optimal() {
			String first = "First";
			String second = "Second";
			Pair<String> target = new Pair<String>(first, second);
			Assert.AreEqual(first, target.First);
			Assert.AreEqual(second, target.Second);
		}

		[TestMethod()]
		[Description("Tests the .ctor() constructor for the optimal path.")]
		public void Pair2_Unit_Constructor1_Optimal() {
			String expectedFirst = default(String);
			Int32 expectedSecond = default(Int32);
			Pair<String, Int32> target = new Pair<String, Int32>();
			Assert.AreEqual(expectedFirst, target.First);
			Assert.AreEqual(expectedSecond, target.Second);
		}
		[TestMethod()]
		[Description("Tests the .ctor(TFirst, TSecond) constructor for the optimal path.")]
		public void Pair2_Unit_Constructor2_Optimal() {
			String first = "First";
			Int32 second = 2;
			Pair<String, Int32> target = new Pair<String, Int32>(first, second);
			Assert.AreEqual(first, target.First);
			Assert.AreEqual(second, target.Second);
		}
	}
}