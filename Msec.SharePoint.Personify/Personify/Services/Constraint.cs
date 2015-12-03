//using System;
//using Msec.Diagnostics;
//using Msec.Personify.Services.UniversalWebServiceImpl;

//using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;

//namespace Msec.Personify.Services {
//    /// <summary>
//    /// Represents a filtering constraint for a query.  This class may not be inherited.
//    /// </summary>
//    [Serializable()]
//    [ImmutableObject(true)]
//    public sealed class Constraint : Object {
//    // Fields
//        /// <summary>
//        /// The operator for the constraint.  This field is read-only.
//        /// </summary>
//        private readonly ConstraintOperator _constraintOperator;
//        /// <summary>
//        /// The name of the field.  This field is read-only.
//        /// </summary>
//        private readonly String _fieldName;
//        /// <summary>
//        /// The value to compare.  This field is read-only.
//        /// </summary>
//        private readonly String _value;

//    // Constructors
//        /// <summary>
//        /// Initializes a new instance of the <see cref="T:Constraint"/> class.
//        /// </summary>
//        /// <param name="fieldName">The name of the field on which to constrain the query.</param>
//        /// <param name="constraintOperator">The operator to use when constraining the query.</param>
//        /// <param name="value">The value to use on the constraint.</param>
//        /// <exception cref="System.ArgumentNullException"><paramref name="fieldName"/> is a null reference.</exception>
//        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="constraintOperator"/> is not defined by <see cref="T:ConstraintOperator"/>.
//        /// -or- <paramref name="constraintOperator"/> is equal to <see cref="F:ConstraintOperator.None"/>.</exception>
//        /// <exception cref="System.ArgumentException"><paramref name="fieldName"/> is empty.
//        /// -or- <paramref name="fieldName"/> contains only white-space characters.</exception>
//        public Constraint(String fieldName, ConstraintOperator constraintOperator, String value)
//            : base() {
//            if (fieldName == null) {
//                throw new ArgumentNullException("fieldName");
//            }
//            if (fieldName.Trim().Length == 0) {
//                throw new ArgumentException("A field name may not be empty or contain only white-space characters.", "fieldName");
//            }
//            switch (constraintOperator) {
//                case ConstraintOperator.Equals:
//                case ConstraintOperator.NotEquals:
//                case ConstraintOperator.StartsWith:
//                case ConstraintOperator.GreaterThan:
//                case ConstraintOperator.GreaterThanOrEqualTo:
//                case ConstraintOperator.LessThan:
//                case ConstraintOperator.LessThanOrEqualTo:
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException("constraintOperator");
//            }

//            this._fieldName = fieldName;
//            this._constraintOperator = constraintOperator;
//            this._value = value;
//        }

//    // Properties
//        /// <summary>
//        /// Gets the operator on which to apply the constraint.
//        /// </summary>
//        public ConstraintOperator ConstraintOperator {
//            get { return this._constraintOperator; }
//        }
//        /// <summary>
//        /// Gets the name of the field.
//        /// </summary>
//        public String FieldName {
//            get { return this._fieldName; }
//        }
//        /// <summary>
//        /// Gets the <see cref="T:QueryOperatorEnum"/> for the operator in this instance.
//        /// </summary>
//        private QueryOperatorEnum QueryOperatorEnum {
//            get {
//                switch (this._constraintOperator) {
//                    case ConstraintOperator.Equals:
//                        return QueryOperatorEnum.Equals;
//                    case ConstraintOperator.NotEquals:
//                        return QueryOperatorEnum.NotEqualTo;
//                    case ConstraintOperator.StartsWith:
//                        return QueryOperatorEnum.StartsWith;
//                    case ConstraintOperator.GreaterThan:
//                        return QueryOperatorEnum.GreaterThan;
//                    case ConstraintOperator.GreaterThanOrEqualTo:
//                        return QueryOperatorEnum.GreaterThanOrEqual;
//                    case ConstraintOperator.LessThan:
//                        return QueryOperatorEnum.LessThan;
//                    case ConstraintOperator.LessThanOrEqualTo:
//                        return QueryOperatorEnum.LessThanOrEqual;
//                    default:
//                        this.LogWarning("A constraint operator {0} was not recognized.  Returning a NotNull operation.", this._constraintOperator);
//                        return QueryOperatorEnum.NotNull;
//                }
//            }
//        }
//        /// <summary>
//        /// Gets the value to use in the constraint.
//        /// </summary>
//        public String Value {
//            get { return this._value; }
//        }

//    // Methods
//        /// <summary>
//        /// Returns the <see cref="T:DataFilter"/> representation of this instance.
//        /// </summary>
//        /// <returns>A reference to the <see cref="T:DataFilter"/> created.</returns>
//        internal DataFilter ToDataFilter() {
//            return new DataFilter() {
//                PropertyName = this._fieldName,
//                FilterOperator = this.QueryOperatorEnum,
//                PropertyValue = this._value
//            };
//        }
//        /// <summary>
//        /// Returns the string representation of this instance.
//        /// </summary>
//        /// <returns>The string representation of this instance.</returns>
//        public override String ToString() {
//            String predicate = null;
//            switch (this._constraintOperator) {
//                case ConstraintOperator.Equals:
//                    predicate = "= " + (this._value ?? "(NULL)");
//                    break;
//                case ConstraintOperator.NotEquals:
//                    predicate = "<> " + (this._value ?? "(NULL)");
//                    break;
//                case ConstraintOperator.StartsWith:
//                    predicate = "Like '" + this._value + "%'";
//                    break;
//                case ConstraintOperator.GreaterThan:
//                    predicate = "> " + (this._value ?? "(NULL)");
//                    break;
//                case ConstraintOperator.GreaterThanOrEqualTo:
//                    predicate = ">= " + (this._value ?? "(NULL)");
//                    break;
//                case ConstraintOperator.LessThan:
//                    predicate = "< " + (this._value ?? "(NULL)");
//                    break;
//                case ConstraintOperator.LessThanOrEqualTo:
//                    predicate = "<= " + (this._value ?? "(NULL)");
//                    break;
//                case ConstraintOperator.None:
//                default:
//                    return "(Invalid)";
//            }
//            return this._fieldName + " " + predicate;
//        }
//    }
//}
