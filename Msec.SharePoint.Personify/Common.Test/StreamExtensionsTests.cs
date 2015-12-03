using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Encoding = System.Text.Encoding;

namespace Msec {
	/// <summary>
	/// This is a test class for <see cref="T:StreamExtensions"/> and is intended to contain all <see cref="T:StreamExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class StreamExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:StreamExtensionsTests"/> class.
		/// </summary>
		public StreamExtensionsTests() : base() { }

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

	// Constants
		/// <summary>
		/// The contents of the test streams = "This is a test.".
		/// </summary>
		private const String StreamContents = "This is a test.";

	// Method Tests
		[TestMethod()]
		[Description("CopyTo(Stream, Stream) method for the optimal path.")]
		public void StreamExtensions_Unit_CopyTo1_Optimal() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = new WriteOnlyStream();
			StreamExtensions.CopyTo(instance, stream);

			Assert.AreEqual(StreamContents, Encoding.Unicode.GetString(((MemoryStream)stream).ToArray()));
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream) method when instance is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StreamExtensions_Unit_CopyTo1_InstanceNull() {
			Stream instance = null;
			Stream stream = new WriteOnlyStream();
			StreamExtensions.CopyTo(instance, stream);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream) method when stream is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StreamExtensions_Unit_CopyTo1_StreamNull() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = null;
			StreamExtensions.CopyTo(instance, stream);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream) method when instance is not readable.")]
		[ExpectedException(typeof(InvalidOperationException))]
		public void StreamExtensions_Unit_CopyTo1_InstanceNotReadable() {
			Stream instance = new WriteOnlyStream();
			Stream stream = new WriteOnlyStream();
			StreamExtensions.CopyTo(instance, stream);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream) method when stream is not writeable.")]
		[ExpectedException(typeof(ArgumentException))]
		public void StreamExtensions_Unit_CopyTo1_StreamNotWriteable() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			StreamExtensions.CopyTo(instance, stream);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream) method when instance is disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void StreamExtensions_Unit_CopyTo1_InstanceDisposed() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = new WriteOnlyStream();
			instance.Dispose();
			StreamExtensions.CopyTo(instance, stream);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream) method when stream is disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void StreamExtensions_Unit_CopyTo1_StreamDisposed() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = new WriteOnlyStream();
			stream.Dispose();
			StreamExtensions.CopyTo(instance, stream);
		}

		[TestMethod()]
		[Description("CopyTo(Stream, Stream, Int32) method for the optimal path.")]
		public void StreamExtensions_Unit_CopyTo2_Optimal() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = new WriteOnlyStream();
			Int32 bufferSize = 100;
			StreamExtensions.CopyTo(instance, stream, bufferSize);

			Assert.AreEqual(StreamContents, Encoding.Unicode.GetString(((MemoryStream)stream).ToArray()));
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream, Int32) method when instance is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StreamExtensions_Unit_CopyTo2_InstanceNull() {
			Stream instance = null;
			Stream stream = new WriteOnlyStream();
			Int32 bufferSize = 100;
			StreamExtensions.CopyTo(instance, stream, bufferSize);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream, Int32) method when stream is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StreamExtensions_Unit_CopyTo2_StreamNull() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = null;
			Int32 bufferSize = 100;
			StreamExtensions.CopyTo(instance, stream, bufferSize);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream, Int32) method when bufferSize is less than 1.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void StreamExtensions_Unit_CopyTo2_BufferSizeLessThan1() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = new WriteOnlyStream();
			Int32 bufferSize = 0;
			StreamExtensions.CopyTo(instance, stream, bufferSize);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream, Int32) method when instance is not readable.")]
		[ExpectedException(typeof(InvalidOperationException))]
		public void StreamExtensions_Unit_CopyTo2_InstanceNotReadable() {
			Stream instance = new WriteOnlyStream();
			Stream stream = new WriteOnlyStream();
			Int32 bufferSize = 100;
			StreamExtensions.CopyTo(instance, stream, bufferSize);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream, Int32) method when stream is not writeable.")]
		[ExpectedException(typeof(ArgumentException))]
		public void StreamExtensions_Unit_CopyTo2_StreamNotWriteable() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Int32 bufferSize = 100;
			StreamExtensions.CopyTo(instance, stream, bufferSize);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream, Int32) method when instance is disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void StreamExtensions_Unit_CopyTo2_InstanceDisposed() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = new WriteOnlyStream();
			Int32 bufferSize = 100;
			instance.Dispose();
			StreamExtensions.CopyTo(instance, stream, bufferSize);
		}
		[TestMethod()]
		[Description("CopyTo(Stream, Stream, Int32) method when stream is disposed.")]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void StreamExtensions_Unit_CopyTo2_StreamDisposed() {
			Stream instance = new ReadOnlyStream(Encoding.Unicode.GetBytes(StreamContents));
			Stream stream = new WriteOnlyStream();
			Int32 bufferSize = 100;
			stream.Dispose();
			StreamExtensions.CopyTo(instance, stream, bufferSize);
		}
	}
}