using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msec.Collections;

namespace Msec {
	/// <summary>
	/// This is a test class for <see cref="T:UriExtensions"/> and is intended to contain all <see cref="T:UriExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class UriExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UriExtensionsTests"/> class.
		/// </summary>
		public UriExtensionsTests() : base() { }

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
		[Description("Append(Uri, String) method for the optimal path.")]
		public void UriExtensions_Unit_Append_Optimal() {
			Uri instance = new Uri("http://www.google.com/MyGoogleFolder");
			String relativePath = "folder1/folder2";
			Uri actual = UriExtensions.Append(instance, relativePath);
			Assert.AreEqual(new Uri("http://www.google.com/MyGoogleFolder/folder1/folder2"), actual);
		}
		[TestMethod()]
		[Description("Append(Uri, String) method when there is a trailing slash on 'instance'.")]
		public void UriExtensions_Unit_Append_TrailingSlash() {
			Uri instance = new Uri("http://www.google.com/MyGoogleFolder/");
			String relativePath = "folder1/folder2";
			Uri actual = UriExtensions.Append(instance, relativePath);
			Assert.AreEqual(new Uri("http://www.google.com/MyGoogleFolder/folder1/folder2"), actual);
		}
		[TestMethod()]
		[Description("Append(Uri, String) method when there is a leading slash on 'relativePath'.")]
		public void UriExtensions_Unit_Append_LeadingSlash() {
			Uri instance = new Uri("http://www.google.com");
			String relativePath = "/folder1/folder2";
			Uri actual = UriExtensions.Append(instance, relativePath);
			Assert.AreEqual(new Uri("http://www.google.com/folder1/folder2"), actual);
		}
		[TestMethod()]
		[Description("Append(Uri, String) method when there is a trailing slash on 'instance' and a leading slash on 'relativePath'.")]
		public void UriExtensions_Unit_Append_TwoSlashes() {
			Uri instance = new Uri("http://www.google.com/");
			String relativePath = "/folder1/folder2";
			Uri actual = UriExtensions.Append(instance, relativePath);
			Assert.AreEqual(new Uri("http://www.google.com/folder1/folder2"), actual);
		}
		[TestMethod()]
		[Description("Append(Uri, String) method when there is query information.")]
		public void UriExtensions_Unit_Append_WithQuery() {
			Uri instance = new Uri("http://www.google.com/?key=value");
			String relativePath = "folder1/folder2";
			Uri actual = UriExtensions.Append(instance, relativePath);
			Assert.AreEqual(new Uri("http://www.google.com/folder1/folder2?key=value"), actual);
		}
		[TestMethod()]
		[Description("Append(Uri, String) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_Append_InstanceNull() {
			Uri instance = null;
			String relativePath = "folder1/folder2";
			UriExtensions.Append(instance, relativePath);
		}
		[TestMethod()]
		[Description("Append(Uri, String) method when 'relativePath' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_Append_RelativePathNull() {
			Uri instance = new Uri("http://www.google.com");
			String relativePath = null;
			UriExtensions.Append(instance, relativePath);
		}
		[TestMethod()]
		[Description("Append(Uri, String) method when 'relativePath' is invalid.")]
		[ExpectedException(typeof(UriFormatException))]
		public void UriExtensions_Unit_Append_RelativePathInvalid() {
			Uri instance = new Uri("http://www.google.com");
			String relativePath = "!@#$%^&*()-_=+,. <>?;:'\"\\" + new String('a', 65500);
			UriExtensions.Append(instance, relativePath);
		}

		[TestMethod()]
		[Description("AppendQuery(Uri, String, String) method for the optimal path.")]
		public void UriExtensions_Unit_AppendQuery1_OptimalPath() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "key2";
			String value = "value2";
			Uri actual = UriExtensions.AppendQuery(instance, key, value);
			Assert.AreEqual(new Uri("http://www.google.com/?key1=value1&key2=value2"), actual);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_AppendQuery1_InstanceNull() {
			Uri instance = null;
			String key = "key2";
			String value = "value2";
			UriExtensions.AppendQuery(instance, key, value);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String) method when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_AppendQuery1_KeyNull() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = null;
			String value = "value2";
			UriExtensions.AppendQuery(instance, key, value);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String) method when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_AppendQuery1_ValueNull() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "key2";
			String value = null;
			UriExtensions.AppendQuery(instance, key, value);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String) method when 'key' is invalid.")]
		[ExpectedException(typeof(UriFormatException))]
		public void UriExtensions_Unit_AppendQuery1_KeyInvalid() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "!@#$%^&*()-_=+,. <>?;:'\"\\" + new String('a', 65500);
			String value = "value2";
			UriExtensions.AppendQuery(instance, key, value);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String) method when 'value' is invalid.")]
		[ExpectedException(typeof(UriFormatException))]
		public void UriExtensions_Unit_AppendQuery1_ValueInvalid() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "key2";
			String value = "!@#$%^&*()-_=+,. <>?;:'\"\\" + new String('a', 65500);
			UriExtensions.AppendQuery(instance, key, value);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String) method when 'key' already exists.")]
		[ExpectedException(typeof(DuplicateKeyException))]
		public void UriExtensions_Unit_AppendQuery1_KeyExists() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "key1";
			String value = "value2";
			UriExtensions.AppendQuery(instance, key, value);
		}

		[TestMethod()]
		[Description("AppendQuery(Uri, String, String, Boolean) method for the optimal path.")]
		public void UriExtensions_Unit_AppendQuery2_OptimalPath() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "key2";
			String value = "value2";
			Boolean overwrite = true;
			Uri actual = UriExtensions.AppendQuery(instance, key, value, overwrite);
			Assert.AreEqual(new Uri("http://www.google.com/?key1=value1&key2=value2"), actual);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String, Boolean) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_AppendQuery2_InstanceNull() {
			Uri instance = null;
			String key = "key2";
			String value = "value2";
			Boolean overwrite = true;
			UriExtensions.AppendQuery(instance, key, value, overwrite);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String, Boolean) method when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_AppendQuery2_KeyNull() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = null;
			String value = "value2";
			Boolean overwrite = true;
			UriExtensions.AppendQuery(instance, key, value, overwrite);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String, Boolean) method when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_AppendQuery2_ValueNull() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "key2";
			String value = null;
			Boolean overwrite = true;
			UriExtensions.AppendQuery(instance, key, value, overwrite);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String, Boolean) method when 'key' is invalid.")]
		[ExpectedException(typeof(UriFormatException))]
		public void UriExtensions_Unit_AppendQuery2_KeyInvalid() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "!@#$%^&*()-_=+,. <>?;:'\"\\" + new String('a', 65500);
			String value = "value2";
			Boolean overwrite = true;
			UriExtensions.AppendQuery(instance, key, value, overwrite);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String, Boolean) method when 'value' is invalid.")]
		[ExpectedException(typeof(UriFormatException))]
		public void UriExtensions_Unit_AppendQuery2_ValueInvalid() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "key2";
			String value = "!@#$%^&*()-_=+,. <>?;:'\"\\" + new String('a', 65500);
			Boolean overwrite = true;
			UriExtensions.AppendQuery(instance, key, value, overwrite);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String, Boolean) method for the optimal path.")]
		public void UriExtensions_Unit_AppendQuery2_OverwiteKey() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "key1";
			String value = "value2";
			Boolean overwrite = true;
			Uri actual = UriExtensions.AppendQuery(instance, key, value, overwrite);
			Assert.AreEqual(new Uri("http://www.google.com/?key1=value2"), actual);
		}
		[TestMethod()]
		[Description("AppendQuery(Uri, String, String, Boolean) method when 'key' already exists.")]
		[ExpectedException(typeof(DuplicateKeyException))]
		public void UriExtensions_Unit_AppendQuery2_DoNotOverwiteKey() {
			Uri instance = new Uri("http://www.google.com/?key1=value1");
			String key = "key1";
			String value = "value2";
			Boolean overwrite = false;
			UriExtensions.AppendQuery(instance, key, value, overwrite);
		}

		[TestMethod()]
		[Description("ContainsQueryKey(Uri, String) method when a key exists.")]
		public void UriExtensions_Unit_ContainsQueryKey_KeyExists() {
			Uri instance = new Uri("http://www.google.com/?key=value");
			String key = "key";
			Boolean actual = UriExtensions.ContainsQueryKey(instance, key);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("ContainsQueryKey(Uri, String) method when a key does not exist.")]
		public void UriExtensions_Unit_ContainsQueryKey_KeyDoesNotExist() {
			Uri instance = new Uri("http://www.google.com/?key=value");
			String key = "key1";
			Boolean actual = UriExtensions.ContainsQueryKey(instance, key);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("ContainsQueryKey(Uri, String) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_ContainsQueryKey_InstanceNull() {
			Uri instance = null;
			String key = "key";
			UriExtensions.ContainsQueryKey(instance, key);
		}
		[TestMethod()]
		[Description("ContainsQueryKey(Uri, String) method when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_ContainsQueryKey_KeyNull() {
			Uri instance = new Uri("http://www.google.com/?key=value");
			String key = null;
			UriExtensions.ContainsQueryKey(instance, key);
		}
		[TestMethod()]
		[Description("ContainsQueryKey(Uri, String) method when a key does not exist but its value exists as a value.")]
		public void UriExtensions_Unit_ContainsQueryKey_KeyExistsAsValue() {
			Uri instance = new Uri("http://www.google.com/?key=value");
			String key = "value";
			Boolean actual = UriExtensions.ContainsQueryKey(instance, key);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("RemoveQuery(Uri, String) method for the optimal path.")]
		public void UriExtensions_Unit_RemoveQuery_Optimal() {
			Uri instance = new Uri("http://www.google.com/somewhere?id=3");
			String key = "id";
			Uri uri = UriExtensions.RemoveQuery(instance, key);
			Assert.AreEqual(new Uri("http://www.google.com/somewhere"), uri);
		}
		[TestMethod()]
		[Description("RemoveQuery(Uri, String) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_RemoveQuery_InstanceNull() {
			Uri instance = null;
			String key = "id";
			UriExtensions.RemoveQuery(instance, key);
		}
		[TestMethod()]
		[Description("RemoveQuery(Uri, String) method when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UriExtensions_Unit_RemoveQuery_KeyNull() {
			Uri instance = new Uri("http://www.google.com/somewhere?id=3");
			String key = null;
			UriExtensions.RemoveQuery(instance, key);
		}
		[TestMethod()]
		[Description("RemoveQuery(Uri, String) method when 'key' is not in the query string.")]
		public void UriExtensions_Unit_RemoveQuery_KeyIsNotInInstance() {
			Uri instance = new Uri("http://www.google.com/somewhere?id=3");
			String key = "somewhere";
			Uri uri = UriExtensions.RemoveQuery(instance, key);
			Assert.AreEqual(instance, uri);
		}
	}
}
