using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents a mock implementation of the <see cref="T:PersonifyUniversalService"/> class.  This class may not be inherited.
	/// </summary>
	public sealed class MockPersonifyUniversalService : PersonifyUniversalService {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifyUniversalService"/> class.
		/// </summary>
		public MockPersonifyUniversalService() : base() { }
		
	// Properties
		/// <summary>
		/// Gets or sets a value indicating if this instance should throw exceptions when functionality is called.
		/// </summary>
		public Boolean ThrowExceptions {
			get;
			set;
		}

	// Methods
		/// <summary>
		/// Returns the enumerable collection of committee members.
		/// </summary>
		/// <param name="constraints">Indicates the filters to apply to the query.</param>
		/// <returns>The enumerable collection of committee members found.</returns>
		protected override IEnumerable<CommitteeMemberData> GetCommitteeMembersCore(Constraint[] constraints) {
			if (this.ThrowExceptions) {
				throw new ClientServiceException();
			}

			return null;
		}
		/// <summary>
		/// Returns the enumerable collection of customer addresses.
		/// </summary>
		/// <param name="constraints">Indicates the filter to apply to the query.</param>
		/// <returns>The enumerable collection of customer addresses found.</returns>
		protected override IEnumerable<CustomerAddressData> GetCustomerAddressesCore(Constraint[] constraints) {
			if (this.ThrowExceptions) {
				throw new InvalidCastException();
			}

			return null;
		}
		/// <summary>
		/// Returns the enumerable collection of customers.
		/// </summary>
		/// <param name="constraints">Indicates the filters to apply to the query.</param>
		/// <returns>The enumerable collection of customers found.</returns>
		protected override IEnumerable<CustomerData> GetCustomersCore(Constraint[] constraints) {
			if (this.ThrowExceptions) {
				throw new System.Security.SecurityException();
			}

			return null;
		}
		/// <summary>
		/// Returns the enumerable collection of valid position codes.
		/// </summary>
		/// <returns>The enumerable collection of valid position codes.</returns>
		protected override IEnumerable<String> GetPositionCodesCore() {
			if (this.ThrowExceptions) {
				throw new InvalidOperationException();
			}

			return null;
		}
	}
}
