using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msec.Personify.Services {
	/// <summary>
	/// This is a test class for <see cref="T:Constraint"/> and is intended to contain all <see cref="T:Constraint"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ConstraintTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ConstraintTests"/> class.
		/// </summary>
		public ConstraintTests() : base() { }

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
		[Description(".ctor(String, ConstraintOperator, String) constructor for the optimal path.")]
		public void Constraint_Unit_Constructor_Optimal() {
			String fieldName = "MyField";
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = "MyValue";
			new Constraint(fieldName, constraintOperator, value);
		}
		[TestMethod()]
		[Description(".ctor(String, ConstraintOperator, String) constructor when 'fieldName' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constraint_Unit_Constructor_FieldNameNull() {
			String fieldName = null;
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = "MyValue";
			new Constraint(fieldName, constraintOperator, value);
		}
		[TestMethod()]
		[Description(".ctor(String, ConstraintOperator, String) constructor when 'fieldName' is empty.")]
		[ExpectedException(typeof(ArgumentException))]
		public void Constraint_Unit_Constructor_FieldNameEmpty() {
			String fieldName = String.Empty;
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = "MyValue";
			new Constraint(fieldName, constraintOperator, value);
		}
		[TestMethod()]
		[Description(".ctor(String, ConstraintOperator, String) constructor when 'fieldName' is white-space.")]
		[ExpectedException(typeof(ArgumentException))]
		public void Constraint_Unit_Constructor_FieldNameWhiteSpace() {
			String fieldName = "  \t  \r  \n  ";
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = "MyValue";
			new Constraint(fieldName, constraintOperator, value);
		}
		[TestMethod()]
		[Description(".ctor(String, ConstraintOperator, String) constructor when 'constraintOperator' is not defined.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Constraint_Unit_Constructor_ConstraintOperatorNotDefined() {
			String fieldName = "MyField";
			ConstraintOperator constraintOperator = (ConstraintOperator)(-1);
			String value = "MyValue";
			new Constraint(fieldName, constraintOperator, value);
		}
		[TestMethod()]
		[Description(".ctor(String, ConstraintOperator, String) constructor when 'constraintOperator' is None.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Constraint_Unit_Constructor_ConstraintOperatorNone() {
			String fieldName = "MyField";
			ConstraintOperator constraintOperator = ConstraintOperator.None;
			String value = "MyValue";
			new Constraint(fieldName, constraintOperator, value);
		}
		[TestMethod()]
		[Description(".ctor(String, ConstraintOperator, String) constructor when 'value' is a null reference.")]
		public void Constraint_Unit_Constructor_ValueNull() {
			String fieldName = "MyField";
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = null;
			new Constraint(fieldName, constraintOperator, value);
		}
		[TestMethod()]
		[Description(".ctor(String, ConstraintOperator, String) constructor when 'value' is empty.")]
		public void Constraint_Unit_Constructor_ValueEmpty() {
			String fieldName = "MyField";
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = String.Empty;
			new Constraint(fieldName, constraintOperator, value);
		}
		[TestMethod()]
		[Description(".ctor(String, ConstraintOperator, String) constructor when 'value' is white-space.")]
		public void Constraint_Unit_Constructor_ValueWhiteSpace() {
			String fieldName = "MyField";
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = "  \t  \r  \n  ";
			new Constraint(fieldName, constraintOperator, value);
		}

	// Property Tests
		[TestMethod()]
		[Description("ConstraintOperator property for the optimal path.")]
		public void Constraint_Unit_ConstraintOperator_Optimal() {
			String fieldName = "MyField";
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = "MyValue";
			Constraint target = new Constraint(fieldName, constraintOperator, value);
			Assert.AreEqual(constraintOperator, target.ConstraintOperator);
		}

		[TestMethod()]
		[Description("FieldName property for the optimal path.")]
		public void Constraint_Unit_FieldName_Optimal() {
			String fieldName = "MyField";
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = "MyValue";
			Constraint target = new Constraint(fieldName, constraintOperator, value);
			Assert.AreEqual(fieldName, target.FieldName);
		}

		[TestMethod()]
		[Description("Value property for the optimal path.")]
		public void Constraint_Unit_Value_Optimal() {
			String fieldName = "MyField";
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = "MyValue";
			Constraint target = new Constraint(fieldName, constraintOperator, value);
			Assert.AreEqual(value, target.Value);
		}

	// Method Tests
		[TestMethod()]
		[Description("ToString() method for the optimal path.")]
		public void Constraint_Unit_ToString_Optimal() {
			ConstraintOperator[] constraintOperators = new ConstraintOperator[] {
				ConstraintOperator.Equals,
				ConstraintOperator.StartsWith,
				ConstraintOperator.NotEquals
			};

			String fieldName = "MyField";
			String value = "MyValue";
			foreach (ConstraintOperator constraintOperator in constraintOperators) {
				Constraint target = new Constraint(fieldName, constraintOperator, value);
				Assert.IsNotNull(target.ToString());

				target = new Constraint(fieldName, constraintOperator, null);
				Assert.IsNotNull(target.ToString());
			}
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class")]
		public void Constraint_Unit_Serialization_Optimal() {
			String fieldName = "MyField";
			ConstraintOperator constraintOperator = ConstraintOperator.Equals;
			String value = "MyValue";
			Constraint target = new Constraint(fieldName, constraintOperator, value);
			Constraint clone = CloneGenerator.SerializeBinary(target);

			Assert.AreNotSame(target, clone);
			Assert.AreEqual(target.ConstraintOperator, clone.ConstraintOperator);
			Assert.AreEqual(target.FieldName, clone.FieldName);
			Assert.AreEqual(target.Value, clone.Value);
		}
	}
}