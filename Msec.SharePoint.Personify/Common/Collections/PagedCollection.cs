using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Msec.Collections {
	/// <summary>
	/// A read-only collection that represents a page of results.
	/// </summary>
	/// <typeparam name="T">The type of element in the collection.</typeparam>
	public class PagedCollection<T> : ReadOnlyCollection<T> {
	// Fields
		/// <summary>
		/// The total number of items.
		/// </summary>
		private readonly Int32 _totalCount;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PagedCollection&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="list">The list to wrap.</param>
		/// <param name="totalCount">The total items available including the items in this instance.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="list"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="totalCount"/> is less than 0.
		/// -or- <paramref name="totalCount"/> is less than the number of elements in <paramref name="list"/>.</exception>
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "The validation occurs in the base class.")]
		public PagedCollection(IList<T> list, Int32 totalCount)
			: base(list) {
			Debug.Assert(list != null);
			if (totalCount < 0 || totalCount < list.Count) {
				throw new ArgumentOutOfRangeException("totalCount");
			}
			this._totalCount = totalCount;
		}

	// Properties
		/// <summary>
		/// Gets the total number of items available including the items in this instance.
		/// </summary>
		public Int32 TotalCount {
			get { return this._totalCount; }
		}
	}
}
