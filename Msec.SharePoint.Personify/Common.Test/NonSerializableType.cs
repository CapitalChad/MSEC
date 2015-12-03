using System;

namespace Msec {
	/// <summary>
	/// Represents a non-serializable object.  This class may not be inherited.
	/// </summary>
	/// <typeparam name="T">The type of value contained.</typeparam>
	internal sealed class NonSerializableType<T> : Object, IEquatable<NonSerializableType<T>>, IEquatable<T> {
	// Fields
		/// <summary>
		/// The actual value.  This field is read-only.
		/// </summary>
		private readonly T _value;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonSerializableType&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="value">The value to wrap.</param>
		public NonSerializableType(T value)
			: base() {
			this._value = value;
		}

	// Properties
		/// <summary>
		/// Gets the value.
		/// </summary>
		public T Value {
			get { return this._value; }
		}

	// Methods
		/// <summary>
		/// Returns a value indicating whether this instance is equal to the object specified.
		/// </summary>
		/// <param name="obj">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			if (obj == null) {
				return this._value == null;
			}
			else if (obj is T) {
				return this.Equals((T)obj);
			}

			NonSerializableType<T> other = obj as NonSerializableType<T>;
			return this.Equals(other);
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to the object specified.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(NonSerializableType<T> other) {
			if (other == null) {
				return false;
			}
			return Object.Equals(this._value, other._value);
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to the object specified.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(T other) {
			return Object.Equals(this._value, other);
		}
		/// <summary>
		/// Returns the hash code value for this instance.
		/// </summary>
		/// <returns>The hash code value to use for this instance.</returns>
		public override Int32 GetHashCode() {
			if (this._value == null) {
				return 0;
			}
			return this._value.GetHashCode();
		}
		/// <summary>
		/// Returns the string representation for this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override String ToString() {
			if (this._value == null) {
				return base.ToString();
			}
			return this._value.ToString();
		}

	// Operators
		/// <summary>
		/// Explicitly converts a <see cref="T:NonSerialiableType&lt;T&gt;"/> object to a <typeparamref name="T"/> object.
		/// </summary>
		/// <param name="instance">The object to convert.</param>
		/// <returns>The converted object.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static explicit operator T(NonSerializableType<T> instance) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			return instance._value;
		}
		/// <summary>
		/// Implicitly converts a <typeparamref name="T"/> object to a <see cref="T:NonSerialiableType&lt;T&gt;"/> object.
		/// </summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted object.</returns>
		public static implicit operator NonSerializableType<T>(T value) {
			return new NonSerializableType<T>(value);
		}
	}
}
