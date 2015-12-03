using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Msec.Collections;
using Msec.Diagnostics;
using Msec.Personify.Services;

using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;
using Regex = System.Text.RegularExpressions.Regex;
using PersonifyConfiguration = Msec.Personify.Configuration.PersonifyConfiguration;

namespace Msec.Personify {
	/// <summary>
	/// Represents a query that will search for <see cref="T:PersonifyUser"/> objects.  This class may not be inherited.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
	public sealed class PersonifyUserQuery : Object, IEnumerable<PersonifyUser> {
		#region private sealed class PagedEnumerator : DisposableBase, IEnumerator<PersonifyUser> {...}
		/// <summary>
		/// A strongly-typed enumerator for retrieving a page of <see cref="T:PersonifyUser"/> objects.  This class may not be inherited.
		/// </summary>
		private sealed class PagedEnumerator : DisposableBase, IEnumerator<PersonifyUser> {
		// Fields
			private Int32 _currentIndex = -1;
			private CustomerData[] _customers;
			private readonly Func<PersonifyUniversalService, IEnumerable<CustomerData>> _selector;
			private PersonifyUniversalService _service = PersonifyUniversalService.NewPersonifyUniversalService();
			private readonly Int32 _skip;
			private readonly Int32 _take;

		// Constructors
			/// <summary>
			/// Initializes a new instance of the <see cref="T:PersonifyUserQuery.PagedEnumerator"/> class.
			/// </summary>
			/// <param name="selector">The constraint by which to filter the results.</param>
			/// <param name="skip">The number of records to skip.</param>
			/// <param name="take">The number of records to take.</param>
			public PagedEnumerator(Func<PersonifyUniversalService, IEnumerable<CustomerData>> selector, Int32 skip, Int32 take)
				: base() {
				if (selector == null)
					throw new ArgumentNullException("selector");

				this._selector = selector;
				this._skip = skip;
				this._take = take;
			}

		// Properties
			/// <summary>
			/// Gets the element in the collection at the current position of the enumerator.
			/// </summary>
			/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MoveNext", Justification = "This term is a member name.")]
			public PersonifyUser Current {
				get {
					this.ThrowIfDisposed();
					if (this._currentIndex < 0) {
						throw new InvalidOperationException("The MoveNext() method must be called for the Current property can be called.");
					}
					CustomerData[] customers = this.Customers;
					if (this._currentIndex >= customers.Length || this._currentIndex > this.MaximumIndex) {
						throw new InvalidOperationException("The enumerator has moved beyond the length of the enumeration.");
					}
					CustomerData customer = customers[this._currentIndex];
					return new PersonifyUser(customer);
				}
			}
			/// <summary>
			/// Gets the array of customer data objects from the service through which to iterate.
			/// </summary>
			/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
			private CustomerData[] Customers {
				get {
					this.ThrowIfDisposed();
					if (this._customers == null) {
						CustomerData[] customers = this._selector(this._service)
							.Skip(this._skip)
							.Take(this._take > 0 ? this._take : Int32.MaxValue)
							.ToArray();
						this._customers = customers;
					}
					return this._customers;
				}
			}
			/// <summary>
			/// Gets the largest index to retrieve from the results.
			/// </summary>
			private Int32 MaximumIndex {
				get {
					if (this._take > 0) {
						return this._skip + this._take - 1;
					}
					return Int32.MaxValue;
				}
			}

		// Methods
			/// <summary>
			/// Advances the enumerator to the next element of the collection.
			/// </summary>
			/// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
			public Boolean MoveNext() {
				this.ThrowIfDisposed();

				if (this._currentIndex < this._skip) {
					this.LogVerbose("Setting the current index {0} to the skip value {1}.", this._currentIndex, this._skip);
					this._currentIndex = this._skip;
				}
				else {
					this.LogVerbose("Incrementing the current index {0}.", this._currentIndex);
					this._currentIndex++;
				}

				if (this._currentIndex > this.MaximumIndex) {
					this.LogVerbose("The current index {0} is greater than the maximum index {1}.  No records left.", this._currentIndex, this.MaximumIndex);
					return false;
				}

				if (this._currentIndex >= this.Customers.Length) {
					this.LogVerbose("The current index {0} is greater than or equal to the number of records {1}.  No records left.", this._currentIndex, this.Customers.Length);
					return false;
				}
				return true;
			}
			/// <summary>
			/// Releases any managed resources held by this instance.
			/// </summary>
			protected override void ReleaseManagedResources() {
				if (this._service != null) {
					this._service.Dispose();
					this._service = null;
				}
			}
			/// <summary>
			/// Sets the enumerator to its initial position, which is before the first element in the collection.
			/// </summary>
			/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
			public void Reset() {
				this.ThrowIfDisposed();
				this._currentIndex = -1;
				this._customers = null;
			}
			/// <summary>
			/// Returns a paged collection of results from this instance.
			/// </summary>
			/// <param name="skip">The number of records to skip within the enumerator.</param>
			/// <param name="take">The number of records to take from the enumerator.</param>
			/// <returns>The paged collection of users.</returns>
			/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="skip"/> is less than 0.
			/// -or- <paramref name="take"/> is less than 0.</exception>
			/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
			public PagedCollection<PersonifyUser> ToPagedCollection(Int32 skip, Int32 take) {
				this.ThrowIfDisposed();
				if (skip < 0)
					throw new ArgumentOutOfRangeException("skip");
				if (this._take < 0)
					throw new ArgumentOutOfRangeException("take");

				Int32 total = 0;
				List<PersonifyUser> users = new List<PersonifyUser>(take);

				Int32 lastObjectIndex = skip + take - 1;
				while (this.MoveNext()) {
					Int32 index = total;
					if (index >= skip && index <= lastObjectIndex)
						users.Add(this.Current);
					else
						this.LogVerbose("Skipping the index {0}.  Skip number is {1}, and last object index is {2}.", index, skip, lastObjectIndex);

					total++;
				}

				return new PagedCollection<PersonifyUser>(users, total);
			}

			#region IEnumerator Members (explicit)
			Object IEnumerator.Current {
				get { return this.Current; }
			}
			#endregion
		}
		#endregion

	// Fields
		private readonly Func<PersonifyUniversalService, IEnumerable<CustomerData>> _selector;
		private readonly Int32 _skip;
		private readonly Int32 _take;

	// Constructors
		private PersonifyUserQuery(Func<PersonifyUniversalService, IEnumerable<CustomerData>> selector) : this(selector, 0, 0) { }
		private PersonifyUserQuery(Func<PersonifyUniversalService, IEnumerable<CustomerData>> selector, Int32 skip, Int32 take)
			: base() {
			Debug.Assert(selector != null);
			Debug.Assert(skip >= 0);
			Debug.Assert(take >= 0);

			this._selector = selector;
			this._skip = skip;
			this._take = take;
		}

	// Methods
		public static PersonifyUserQuery ByDisplayName(String nameStart, Int32 skip, Int32 take) {
			return new PersonifyUserQuery(service => service.GetCustomersWhereFullNameStartsWith(nameStart), skip, take);
		}
		/// <summary>
		/// Returns a new instance of the <see cref="T:PersonifyUserQuery"/> class that will search for a single user by primary e-mail address.
		/// </summary>
		/// <param name="emailAddress">The e-mail address value for which to search.</param>
		/// <returns>A reference to the <see cref="T:PersonifyUserQuery"/> created.</returns>
		public static PersonifyUserQuery ByEmailAddress(String emailAddress) {
			return new PersonifyUserQuery(service => service.GetCustomersWhereEmailAddressEquals(emailAddress));
		}
		/// <summary>
		/// Returns a new instance of the <see cref="T:PersonifyUserQuery"/> class that will search for multiple users with an e-mail address starting with the specified value.
		/// </summary>
		/// <param name="emailAddressStart">The value representing the start of the e-mail addresses.  Any wildcard characters should already be removed.</param>
		/// <param name="skip">The number of records to skip.</param>
		/// <param name="take">The number of records to take.</param>
		/// <returns>A reference to the <see cref="T:PersonifyUserQuery"/> created.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="skip"/> is less than 0.
		/// -or- <paramref name="take"/> is less than 0.</exception>
		public static PersonifyUserQuery ByEmailAddress(String emailAddressStart, Int32 skip, Int32 take) {
			return new PersonifyUserQuery(service => service.GetCustomersWhereEmailAddressStartsWith(emailAddressStart), skip, take);
		}
		/// <summary>
		/// Returns a new instance of the <see cref="T:PersonifyUserQuery"/> class that will search for a single user by user name.
		/// </summary>
		/// <param name="userName">The user name value for which to search.</param>
		/// <returns>A reference to the <see cref="T:PersonifyUserQuery"/> created.</returns>
		public static PersonifyUserQuery ByUserName(String userName) {
			if (userName != null) {
				Regex regex = new Regex(".\\(\\d{10}\\)$");
				Boolean isMatch = regex.IsMatch(userName);
				typeof(PersonifyUserQuery).LogVerbose("Regular expression \"{0}\" tested the value \"{1}\" and returned {2}.", regex.ToString(), userName, isMatch);
				if (isMatch) {
					userName = userName.Substring(userName.Length - 11, 10);
				}
			}

			return new PersonifyUserQuery(service => service.GetCustomersWhereUserNameEquals(userName));
		}
		/// <summary>
		/// Returns a new instance of the <see cref="T:PersonifyUserQuery"/> class that will search for multiple users with a user name starting with the specified value.
		/// </summary>
		/// <param name="userNameStart">The value representing the start of the user names.  Any wildcard characters should already be removed.</param>
		/// <param name="skip">The number of records to skip.</param>
		/// <param name="take">The number of records to take.</param>
		/// <returns>A reference to the <see cref="T:PersonifyUserQuery"/> created.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="skip"/> is less than 0.
		/// -or- <paramref name="take"/> is less than 0.</exception>
		public static PersonifyUserQuery ByUserName(String userNameStart, Int32 skip, Int32 take) {
			return new PersonifyUserQuery(service => service.GetCustomersWhereUserNameStartsWith(userNameStart), skip, take);
		}
		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>An <see cref="T:IEnumerator&lt;T&gt;"/> that can be used to iterate through the collection.</returns>
		public IEnumerator<PersonifyUser> GetEnumerator() {
			return new PagedEnumerator(this._selector, this._skip, this._take);
		}
		/// <summary>
		/// Searches the user store and returns the paged collection representing the results retrieved.
		/// </summary>
		/// <returns>A <see cref="T:PagedCollection&lt;T&gt;"/> representing the search results.</returns>
		public PagedCollection<PersonifyUser> Search() {
			using (PagedEnumerator enumerator = new PagedEnumerator(this._selector, 0, 0)) {
				return enumerator.ToPagedCollection(this._skip, this._take);
			}
		}

		#region IEnumerable Members (explicit)
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
		#endregion
	}
}
