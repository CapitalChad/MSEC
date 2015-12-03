using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec {
	/// <summary>
	/// This is a test class for <see cref="T:LazyLoader&lt;T&gt;"/> and is intended to contain all <see cref="T:LazyLoader&lt;T&gt;"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class LazyLoaderTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:LazyLoaderTests"/> class.
		/// </summary>
		public LazyLoaderTests() : base() { }

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
		[Description(".ctor(Func<T>) constructor for the optimal path.")]
		public void LazyLoader_Unit_Constructor_Optimal() {
			new LazyLoader<String>(() => "Value");
		}
		[TestMethod()]
		[Description(".ctor(Func<T>) constructor when the initializer is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void LazyLoader_Unit_Constructor_InitializerNull() {
			new LazyLoader<String>(null);
		}
		[TestMethod()]
		[Description(".ctor(Func<T>) constructor when the initializer throws an exception.")]
		public void LazyLoader_Unit_Constructor_InitializerThrows() {
			Func<String> initializer = () => {
				throw new Exception("Oops!");
			};
			new LazyLoader<String>(initializer);
		}

	// Property Tests
		[TestMethod()]
		[Description("Object property for the optimal path.")]
		public void LazyLoader_Unit_Object_Optimal() {
			const String value = "This is a test.";
			Func<String> initializer = () => value;
			LazyLoader<String> target = new LazyLoader<String>(initializer);
			Assert.AreEqual(false, target.IsInitialized);

			String actualValue = target.Object;
			Assert.AreEqual(value, actualValue);
			Assert.AreEqual(true, target.IsInitialized);
		}

	// Method Tests
		[TestMethod()]
		[Description("Dispose() method for the optimal path.")]
		public void LazyLoader_Unit_Dispose_Optimal() {
			Boolean releaseManagedResourcesCalled = false;
			Action releaseManagedResourcesAction = () => releaseManagedResourcesCalled = true;
			Boolean releaseUnmanagedResourcesCalled = false;
			Action releaseUnmanagedResourcesAction = () => releaseUnmanagedResourcesCalled = true;
			Func<Object> initializer = () => new MockDisposableBase(releaseManagedResourcesAction, releaseUnmanagedResourcesAction);
			LazyLoader<Object> target = new LazyLoader<Object>(initializer);
			target.Load();
			Assert.AreEqual(false, releaseManagedResourcesCalled);
			Assert.AreEqual(false, releaseUnmanagedResourcesCalled);

			target.Dispose();
			Assert.AreEqual(true, releaseManagedResourcesCalled);
			Assert.AreEqual(true, releaseUnmanagedResourcesCalled);
			Assert.AreEqual(false, target.IsInitialized);
		}
		[TestMethod()]
		[Description("Dispose() method when the object contained is not disposable.")]
		public void LazyLoader_Unit_Dispose_NotDisposable() {
			LazyLoader<String> target = new LazyLoader<String>(() => "This is a test.");
			target.Load();

			target.Dispose();
			Assert.AreEqual(false, target.IsInitialized);
		}
		[TestMethod()]
		[Description("Load() method for the optimal path.")]
		public void LazyLoader_Unit_Load_Optimal() {
			const String Value = "This is a test.";
			LazyLoader<String> target = new LazyLoader<String>(() => Value);
			Assert.AreEqual(false, target.IsInitialized);
			
			target.Load();
			Assert.AreEqual(true, target.IsInitialized);
			Assert.AreEqual(Value, target.Object);
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class for the optimal path.")]
		public void LazyLoader_Integration_Serialization_Optimal() {
			const String Value = "This is a test.";
			LazyLoader<String> original = new LazyLoader<String>(() => Value);
			original.Load();
			
			LazyLoader<String> clone = CloneGenerator.SerializeBinary(original);
			Assert.AreEqual(false, clone.IsInitialized);
			Assert.AreEqual(Value, clone.Object);
		}
		[TestMethod()]
		[Description("Serializability of the class when a non-serializable value is provided.")]
		public void LazyLoader_Integration_Serialization_NonSerializableValue() {
			const String Value = "This is a test.";
			LazyLoader<NonSerializableType<String>> original = new LazyLoader<NonSerializableType<String>>(() => Value);
			original.Load();
			
			LazyLoader<NonSerializableType<String>> clone = CloneGenerator.SerializeBinary(original);
			Assert.AreEqual(false, clone.IsInitialized);
			Assert.AreEqual(Value, clone.Object);
		}
	}
}