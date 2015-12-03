using System;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;

namespace Msec {
	/// <summary>
	/// Represents a strongly-typed pair of values.
	/// </summary>
	/// <typeparam name="TFirst">The type of the first object.</typeparam>
	/// <typeparam name="TSecond">The type of the second object.</typeparam>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class Pair<TFirst, TSecond> : Object {
	// Fields
		/// <summary>
		/// The first object.  This field is read-only.
		/// </summary>
		private readonly TFirst _first;
		/// <summary>
		/// The second object.  This field is read-only.
		/// </summary>
		private readonly TSecond _second;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Pair&lt;TFirst,TSecond&gt;"/> class.
		/// </summary>
		public Pair() : this(default(TFirst), default(TSecond)) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="Pair&lt;TFirst,TSecond&gt;"/> class.
		/// </summary>
		/// <param name="first">The first object.</param>
		/// <param name="second">The second object.</param>
		public Pair(TFirst first, TSecond second)
			: base() {
			this._first = first;
			this._second = second;
		}

	// Properties
		/// <summary>
		/// Gets the first object.
		/// </summary>
		public TFirst First {
			get { return this._first; }
		}
		/// <summary>
		/// Gets the second object.
		/// </summary>
		public TSecond Second {
			get { return this._second; }
		}
	}

	/// <summary>
	/// Represents a strongly-typed pair of values.
	/// </summary>
	/// <typeparam name="T">The type of objects contained.</typeparam>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class Pair<T> : Object {
	// Fields
		/// <summary>
		/// The first object.  This field is read-only.
		/// </summary>
		private readonly T _first;
		/// <summary>
		/// The second object.  This field is read-only.
		/// </summary>
		private readonly T _second;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Pair&lt;T&gt;"/> class.
		/// </summary>
		public Pair() : this(default(T), default(T)) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="Pair&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="first">The first object.</param>
		/// <param name="second">The second object.</param>
		public Pair(T first, T second)
			: base() {
			this._first = first;
			this._second = second;
		}

	// Properties
		/// <summary>
		/// Gets the first object.
		/// </summary>
		public T First {
			get { return this._first; }
		}
		/// <summary>
		/// Gets the second object.
		/// </summary>
		public T Second {
			get { return this._second; }
		}
	}
}
