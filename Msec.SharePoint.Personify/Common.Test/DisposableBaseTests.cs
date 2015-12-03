using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec {
	/// <summary>
	/// This is a test class for <see cref="T:DisposableBase"/> and is intended to contain all <see cref="T:DisposableBase"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class DisposableBaseTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DisposableBaseTests"/> class.
		/// </summary>
		public DisposableBaseTests() : base() { }

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

	// Fields
		private static Boolean _releaseManagedResourcesCalled;
		private static Boolean _releaseUnmanagedResourcesCalled;

	// Properties
		/// <summary>
		/// Gets or sets a value indicating if the <see cref="M:ReleaseManagedResources"/> method has been called.
		/// </summary>
		private static Boolean ReleaseManagedResourcesCalled {
			get { return DisposableBaseTests._releaseManagedResourcesCalled; }
			set { DisposableBaseTests._releaseManagedResourcesCalled = value; }
		}
		/// <summary>
		/// Gets or sets a value indicating if the <see cref="M:ReleaseUnmanagedResources"/> method has been called.
		/// </summary>
		private static Boolean ReleaseUnmanagedResourcesCalled {
			get { return DisposableBaseTests._releaseUnmanagedResourcesCalled; }
			set { DisposableBaseTests._releaseUnmanagedResourcesCalled = value; }
		}

	// Methods
		/// <summary>
		/// Tracks when a disposable object releases managed resources.
		/// </summary>
		private static void ReleaseManagedResources() {
			DisposableBaseTests.ReleaseManagedResourcesCalled = true;
		}
		/// <summary>
		/// Tracks when a disposable object releases unmanaged resources.
		/// </summary>
		private static void ReleaseUnmanagedResources() {
			DisposableBaseTests.ReleaseUnmanagedResourcesCalled = true;
		}

	// Constructor Tests
		[TestMethod()]
		[Description(".ctor() constructor for the optimal path.")]
		public void DisposableBase_Unit_Constructor_Optimal() {
			new MockDisposableBase();
		}

	// Methods tests
		[TestMethod()]
		[Description("Dispose() method for the optimal path.")]
		public void DisposableBase_Unit_Dispose_Optimal() {
			DisposableBaseTests.ReleaseManagedResourcesCalled = false;
			DisposableBaseTests.ReleaseUnmanagedResourcesCalled = false;
			DisposableBase target = new MockDisposableBase(DisposableBaseTests.ReleaseManagedResources, DisposableBaseTests.ReleaseUnmanagedResources);
			Assert.AreEqual(false, target.IsDisposed);
			Assert.AreEqual(false, DisposableBaseTests.ReleaseManagedResourcesCalled);
			Assert.AreEqual(false, DisposableBaseTests.ReleaseUnmanagedResourcesCalled);

			target.Dispose();
			Assert.AreEqual(true, target.IsDisposed);
			Assert.AreEqual(true, DisposableBaseTests.ReleaseManagedResourcesCalled);
			Assert.AreEqual(true, DisposableBaseTests.ReleaseUnmanagedResourcesCalled);
		}
		[TestMethod()]
		[Description("Dispose() method when it is called multiple times.")]
		public void DisposableBase_Unit_Dispose_CallTwice() {
			DisposableBase target = new MockDisposableBase();
			target.Dispose();
			target.Dispose();
		}
		[TestMethod()]
		[Description("ThrowIfDisposed() method when the object is not disposed.")]
		public void DisposableBase_Unit_ThrowIfDisposed_NotDisposed() {
			MockDisposableBase target = new MockDisposableBase();
			target.TestMethod();
		}
		[TestMethod()]
		[Description("ThrowIfDisposed() method when the object is disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void DisposableBase_Unit_ThrowIfDisposed_Disposed() {
			MockDisposableBase target = new MockDisposableBase();
			target.Dispose();
			target.TestMethod();
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class for the optimal path.")]
		public void DisposableBase_Integration_Serialization_Optimal() {
			DisposableBaseTests.ReleaseManagedResourcesCalled = false;
			DisposableBaseTests.ReleaseUnmanagedResourcesCalled = false;
			DisposableBase original = new MockDisposableBase(DisposableBaseTests.ReleaseManagedResources, DisposableBaseTests.ReleaseUnmanagedResources);

			DisposableBase clone = CloneGenerator.SerializeBinary(original);
			Assert.AreEqual(original.IsDisposed, clone.IsDisposed);
			Assert.AreEqual(false, DisposableBaseTests.ReleaseManagedResourcesCalled);
			Assert.AreEqual(false, DisposableBaseTests.ReleaseUnmanagedResourcesCalled);

			clone.Dispose();
			Assert.AreEqual(true, DisposableBaseTests.ReleaseManagedResourcesCalled);
			Assert.AreEqual(true, DisposableBaseTests.ReleaseUnmanagedResourcesCalled);
		}
		[TestMethod()]
		[Description("Serializability of the class when the object has been disposed.")]
		public void DisposableBase_Integration_Serialization_IsDisposed() {
			DisposableBase original = new MockDisposableBase();
			original.Dispose();

			DisposableBase clone = CloneGenerator.SerializeBinary(original);
			Assert.AreEqual(true, clone.IsDisposed);
		}
	}
}