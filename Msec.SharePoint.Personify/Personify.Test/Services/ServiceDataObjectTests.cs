using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:ServiceDataObject"/> and is intended to contain all <see cref="T:ServiceDataObject"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ServiceDataObjectTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServiceDataObjectTests"/> class.
		/// </summary>
		public ServiceDataObjectTests() : base() { }

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
		[Description(".ctor(IDictionary<String, Object>) constructor for the optimal path.")]
		public void ServiceDataObject_Unit_Constructor1_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" }
			};
			new MockServiceDataObject(values);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor when 'values' is a null reference.")]
		public void ServiceDataObject_Unit_Constructor1_ValuesNull() {
			IDictionary<String, Object> values = null;
			new MockServiceDataObject(values);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<String, Object>) constructor when 'values' is empty.")]
		public void ServiceDataObject_Unit_Constructor1_ValuesEmpty() {
			IDictionary<String, Object> values = new Dictionary<String, Object>();
			new MockServiceDataObject(values);
		}

		[TestMethod()]
		[Description(".ctor(Object) constructor for the optimal path.")]
		public void ServiceDataObject_Unit_Constructor2_Optimal() {
			MockServiceObject serviceObject = new MockServiceObject() {
				MyBoolean = true,
				MyCode = new Msec.Personify.Services.UniversalWebServiceImpl.Code() { Value = "Test" },
				MyDateTime = DateTime.Now,
				MyDouble = 123,
				MyInt32 = 456,
				MyInt64 = 789,
				MyString = "Another test",
				MyStringArray = new String[] { "one", "two", "three" }
			};
			ServiceDataObject target = new MockServiceDataObject(serviceObject);
			Assert.AreEqual(serviceObject.MyBoolean, target["MyBoolean"]);
			Assert.AreEqual(serviceObject.MyCode.Value, target["MyCode"]);
			Assert.AreEqual(serviceObject.MyDateTime, target["MyDateTime"]);
			Assert.AreEqual(serviceObject.MyDouble, target["MyDouble"]);
			Assert.AreEqual(serviceObject.MyInt32, target["MyInt32"]);
			Assert.AreEqual(serviceObject.MyInt64, target["MyInt64"]);
			Assert.AreEqual(serviceObject.MyString, target["MyString"]);
			Assert.AreEqual(null, target["MyStringArray"]);
		}
		[TestMethod()]
		[Description(".ctor(Object) constructor when 'serviceObject' is a null reference.")]
		public void ServiceDataObject_Unit_Constructor2_ServiceObjectNull() {
			Object serviceObject = null;
			new MockServiceDataObject(serviceObject);
		}

	// Property Tests
		[TestMethod()]
		[Description("this[String] property for the optimal path by field name.")]
		public void ServiceDataObject_Unit_this_OptimalByFieldName() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = values.Keys.First();
			Object expected = values[fieldName];

			Object actual = target[fieldName];
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("this[String] property when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ServiceDataObject_Unit_this_ValueNull() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = null;
			Object actual = target[fieldName];
		}
		[TestMethod()]
		[Description("this[String] property when 'value' doesn't exist in the object.")]
		public void ServiceDataObject_Unit_this_FieldNotInObject() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = "Key5";

			Object actual = target[fieldName];
			Assert.IsNull(actual);
		}
		[TestMethod()]
		[Description("this[String] property when the field name specified does not exist.")]
		public void ServiceDataObject_Unit_this_FieldDoesNotExist() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = "SomeBogusName";
			
			Object actual = target[fieldName];
			Assert.IsNull(actual);
		}

	// Method Tests
		[TestMethod()]
		[Description("GetNullableDateTime(String) method for the optimal path by field name.")]
		public void ServiceDataObject_Unit_GetNullableDateTime_OptimalByFieldName() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = values.Keys.Last();
			DateTime? expected = (DateTime)values[fieldName];

			DateTime? actual = target.GetNullableDateTime(fieldName);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetNullableDateTime(String) method when 'fieldName' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ServiceDataObject_Unit_GetNullableDateTime_ValueNull() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = null;
			target.GetNullableDateTime(fieldName);
		}
		[TestMethod()]
		[Description("GetNullableDateTime(String) method when 'fieldName' doesn't exist in the object.")]
		public void ServiceDataObject_Unit_GetNullableDateTime_FieldNotInObject() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = "Key5";
			DateTime? expected = null;

			DateTime? actual = target.GetNullableDateTime(fieldName);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetNullableDateTime(String) method when the field name specified does not exist.")]
		public void ServiceDataObject_Unit_GetNullableDateTime_FieldDoesNotExist() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = "SomeBogusName";
			DateTime? actual = target.GetNullableDateTime(fieldName);
			Assert.IsNull(actual);
		}
		[TestMethod()]
		[Description("GetNullableDateTime(String) method when the value for the field is of a different type.")]
		public void ServiceDataObject_Unit_GetNullableDateTime_FieldTypeDifferent() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = values.Keys.First();
			DateTime? expected = null;

			DateTime? actual = target.GetNullableDateTime(fieldName);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("GetNullableInt64(String) method for the optimal path by field name.")]
		public void ServiceDataObject_Unit_GetNullableInt64_OptimalByFieldName() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", (Int64)3 }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = values.Keys.Last();
			Int64? expected = (Int64)values[fieldName];

			Int64? actual = target.GetNullableInt64(fieldName);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetNullableInt64(String) method when 'fieldName' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ServiceDataObject_Unit_GetNullableInt64_ValueNull() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", (Int64)3 }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = null;
			target.GetNullableInt64(fieldName);
		}
		[TestMethod()]
		[Description("GetNullableInt64(String) method when 'fieldName' doesn't exist in the object.")]
		public void ServiceDataObject_Unit_GetNullableInt64_FieldNotInObject() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = "Key5";
			DateTime? expected = null;

			Int64? actual = target.GetNullableInt64(fieldName);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetNullableInt64(String) method when the field name specified does not exist.")]
		public void ServiceDataObject_Unit_GetNullableInt64_FieldDoesNotExist() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", (Int64)3 }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = "SomeBogusName";
			Int64? actual = target.GetNullableInt64(fieldName);
			Assert.IsNull(actual);
		}
		[TestMethod()]
		[Description("GetNullableInt64(String) method when the value for the field is of a different type.")]
		public void ServiceDataObject_Unit_GetNullableInt64_FieldTypeDifferent() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", (Int64)3 }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = values.Keys.First();
			Int64? expected = null;

			Int64? actual = target.GetNullableInt64(fieldName);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("GetString(String) method for the optimal path by field name.")]
		public void ServiceDataObject_Unit_GetString_OptimalByFieldName() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = values.Keys.First();
			String expected = (String)values[fieldName];

			String actual = target.GetString(fieldName);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetString(String) method when 'fieldName' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ServiceDataObject_Unit_GetString_ValueNull() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = null;
			target.GetString(fieldName);
		}
		[TestMethod()]
		[Description("GetString(String) method when 'fieldName' doesn't exist in the object.")]
		public void ServiceDataObject_Unit_GetString_FieldNotInObject() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = "Key1";
			String expected = null;

			String actual = target.GetString(fieldName);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetString(String) method when the field name specified does not exist.")]
		public void ServiceDataObject_Unit_GetString_FieldDoesNotExist() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = "SomeBogusName";
			String actual = target.GetString(fieldName);
			Assert.IsNull(actual);
		}
		[TestMethod()]
		[Description("GetString(String) method when the value for the field is of a different type.")]
		public void ServiceDataObject_Unit_GetString_FieldTypeDifferent() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = values.Keys.Last();
			String expected = null;

			String actual = target.GetString(fieldName);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("GetValue(String) method for the optimal path by field name.")]
		public void ServiceDataObject_Unit_GetValue_OptimalByFieldName() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = values.Keys.First();
			Object expected = values[fieldName];

			Object actual = target.GetValue(fieldName);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetValue(String) method when 'fieldName' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ServiceDataObject_Unit_GetValue_ValueNull() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = null;
			target.GetValue(fieldName);
		}
		[TestMethod()]
		[Description("GetValue(String) method when 'fieldName' doesn't exist in the object.")]
		public void ServiceDataObject_Unit_GetValue_FieldNotInObject() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = "Key6";

			Object actual = target.GetValue(fieldName);
			Assert.IsNull(actual);
		}
		[TestMethod()]
		[Description("GetValue(String) method when the field name specified does not exist.")]
		public void ServiceDataObject_Unit_GetValue_FieldDoesNotExist() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			String fieldName = "SomeBogusName";
			Object actual = target.GetValue(fieldName);
			Assert.IsNull(actual);
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class.")]
		public void ServiceDataObject_Unit_Serialization_Optimal() {
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" },
				{ "Key4", "Value4" },
				{ "Key5", DateTime.Now }
			};
			ServiceDataObject target = new MockServiceDataObject(values);

			ServiceDataObject clone = CloneGenerator.SerializeBinary(target);
			Assert.AreNotSame(target, clone);
			foreach (String fieldName in target.FieldNames) {
				Assert.IsTrue(clone.FieldNames.Contains(fieldName), "The cloned object does not contain the field {0}.", fieldName);
				Assert.AreEqual(target[fieldName], clone[fieldName]);
			}
		}
	}
}
