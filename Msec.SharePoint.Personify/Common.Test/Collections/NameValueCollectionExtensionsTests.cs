using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NameValueCollection = System.Collections.Specialized.NameValueCollection;

namespace Msec.Collections {
	/// <summary>
	/// This is a test class for <see cref="T:NameValueCollectionExtensions"/> and is intended to contain all <see cref="T:NameValueCollectionExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class NameValueCollectionExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NameValueCollectionExtensionsTests"/> class.
		/// </summary>
		public NameValueCollectionExtensionsTests() : base() { }

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
		[Description("GetAndRemove(NameValueCollection, String) method when 'name' exists.")]
		public void NameValueCollectionExtensions_Unit_GetAndRemove_NameExists() {
			String name = "Name";
			String value = "Value";
			NameValueCollection instance = new NameValueCollection() {
				{ name, value }
			};
			CollectionAssert.Contains(instance.AllKeys, name);

			String actualValue = NameValueCollectionExtensions.GetAndRemove(instance, name);
			Assert.AreEqual(value, actualValue);
			CollectionAssert.DoesNotContain(instance.AllKeys, name);
		}
		[TestMethod()]
		[Description("GetAndRemove(NameValueCollection, String) method when 'name' does not exist.")]
		public void NameValueCollectionExtensions_Unit_GetAndRemove_NameDoesNotExist() {
			String name = "Name";
			String name1 = "Name1";
			String value = "Value";
			NameValueCollection instance = new NameValueCollection() {
				{ name1, value }
			};
			CollectionAssert.DoesNotContain(instance.AllKeys, name);

			String actualValue = NameValueCollectionExtensions.GetAndRemove(instance, name);
			Assert.IsNull(actualValue);
			CollectionAssert.DoesNotContain(instance.AllKeys, name);
			CollectionAssert.Contains(instance.AllKeys, name1);
		}
		[TestMethod()]
		[Description("GetAndRemove(NameValueCollection, String) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetAndRemove_InstanceNull() {
			String name = "Name";

			NameValueCollectionExtensions.GetAndRemove(null, name);
		}
		[TestMethod()]
		[Description("GetAndRemove(NameValueCollection, String) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetAndRemove_NameNull() {
			String name = "Name";
			String value = "Value";
			NameValueCollection instance = new NameValueCollection() {
				{ name, value }
			};

			NameValueCollectionExtensions.GetAndRemove(instance, null);
		}
	}
}
