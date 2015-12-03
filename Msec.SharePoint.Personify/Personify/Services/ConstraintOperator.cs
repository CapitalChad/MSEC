using System;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents an operator used in a constraint.
	/// </summary>
	public enum ConstraintOperator {
		/// <summary>
		/// No operator specified.
		/// </summary>
		None = 0,
		/// <summary>
		/// Represents an "equals" operator.
		/// </summary>
		Equals,
		/// <summary>
		/// Represents a "not equals" operator.
		/// </summary>
		NotEquals,
		/// <summary>
		/// Represents a "starts with" operator.
		/// </summary>
		StartsWith,
		/// <summary>
		/// Represents a "greater than" operator.
		/// </summary>
		GreaterThan,
		/// <summary>
		/// Represents a "greater than or equal to" operator.
		/// </summary>
		GreaterThanOrEqualTo,
		/// <summary>
		/// Represents a "less than" operator.
		/// </summary>
		LessThan,
		/// <summary>
		/// Represents a "less than or equal to" operator.
		/// </summary>
		LessThanOrEqualTo
	}
}
