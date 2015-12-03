using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Collections {
	/// <summary>
	/// This is a test class for <see cref="T:PagedCollection&lt;T&gt;"/> and is intended to contain all <see cref="T:PagedCollection&lt;T&gt;"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class PagedCollectionTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PagedCollectionTests"/> class.
		/// </summary>
		public PagedCollectionTests() : base() { }

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
		[Description(".ctor(IList<T>, Int32) constructor for the optimal path.")]
		public void PagedCollection_Unit_Constructor_Optimal() {
			String[] list = new String[] { "One", "Two", "Three" };
			Int32 totalCount = 128;
			PagedCollection<String> target = new PagedCollection<String>(list, totalCount);
			Assert.AreEqual(totalCount, target.TotalCount);
			CollectionAssert.AreEquivalent(list, target);
		}
		[TestMethod()]
		[Description(".ctor(IList<T>, Int32) constructor when 'list' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PagedCollection_Unit_Constructor_ListNull() {
			String[] list = null;
			Int32 totalCount = 128;
			new PagedCollection<String>(list, totalCount);
		}
		[TestMethod()]
		[Description(".ctor(IList<T>, Int32) constructor when 'totalCount' is less than 0.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void PagedCollection_Unit_Constructor_TotalCountLessThan0() {
			String[] list = new String[] { "One", "Two", "Three" };
			Int32 totalCount = -1;
			new PagedCollection<String>(list, totalCount);
		}
		[TestMethod()]
		[Description(".ctor(IList<T>, Int32) constructor when 'totalCount' is less than the number of elements in 'list'.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void PagedCollection_Unit_Constructor_TotalCountLessThanListCount() {
			String[] list = new String[] { "One", "Two", "Three" };
			Int32 totalCount = list.Length - 1;
			new PagedCollection<String>(list, totalCount);
		}
	}
}