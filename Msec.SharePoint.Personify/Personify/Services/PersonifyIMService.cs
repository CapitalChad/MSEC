using System;
using Msec.Personify.Configuration;
using System.Diagnostics;
using System.Net;
using System.Linq;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents the base class for objects the interface with the IM service.
	/// </summary>
	public abstract class PersonifyIMService : DisposableBase {
	// Fields
		/// <summary>
		/// The vendor's credentials to use for the service calls.
		/// This field is read-only.
		/// </summary>
		private readonly NetworkCredential _credentials;
#if (DEBUG)
		/// <summary>
		/// The factory method used to create instances of the actual service objects.
		/// </summary>
		/// <remarks>This field is not read-only in the DEBUG version to assist in unit testing.</remarks>
		private static Func<NetworkCredential, PersonifyIMService> _factory = PersonifyIMService.CreatePersonifyIMService;
#else
		/// <summary>
		/// The factory method used to create instances of the actual service objects.  This field is read-only.
		/// </summary>
		private static readonly Func<NetworkCredential, PersonifyIMService> _factory = PersonifyIMService.CreatePersonifyIMService;
#endif

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyIMService"/> class.
		/// </summary>
		protected PersonifyIMService() : this(null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyIMService"/> class.
		/// </summary>
		/// <param name="credentials">The vendor's credentials to use for the service calls.</param>
		protected PersonifyIMService(NetworkCredential credentials)
			: base() {
			this._credentials = credentials ?? PersonifyConfiguration.Instance.IMCredentials;
		}

	// Properties
		/// <summary>
		/// Gets the vendor's credentials to use for service calls.
		/// </summary>
		protected NetworkCredential Credentials {
			get { return this._credentials; }
		}

	// Methods
		/// <summary>
		/// Acts as the default factory method.
		/// </summary>
		/// <param name="credentials">The vendor's credentials to use for the service calls.</param>
		/// <returns>The service object created.</returns>
		private static PersonifyIMService CreatePersonifyIMService(NetworkCredential credentials) {
			return new WebPersonifyIMService(credentials);
		}
		/// <summary>
		/// Executes a function and returns the result from it.
		/// </summary>
		/// <typeparam name="T">The type of value to return.</typeparam>
		/// <param name="func">The function to execute.</param>
		/// <returns>The return value from the function.</returns>
		/// <exception cref="Msec.ServiceException">An error occurs while communicating with the service.</exception>
		private static T Execute<T>(Func<T> func) {
			Debug.Assert(func != null);
			try {
				return func();
			}
			catch (ServiceException) {
				throw;
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely()) {
					throw;
				}
				throw new ClientServiceException("An unexpected error occurred while communicating with the service.  See the inner exception for more details.", ex);
			}
		}
		/// <summary>
		/// Returns the roles for a customer token.
		/// </summary>
		/// <param name="customerToken">Identifies the customer.</param>
		/// <returns>The roles for the customer.</returns>
		public String[] GetCustomerRoles(CustomerToken customerToken) {
			this.ThrowIfDisposed();
			if (customerToken == null)
				throw new ArgumentNullException("customerToken");

			return PersonifyIMService.Execute(() => this.GetCustomerRolesCore(customerToken));
		}
		/// <summary>
		/// Returns the roles for a customer token.
		/// </summary>
		/// <param name="customerToken">Identifies the customer.</param>
		/// <returns>The roles for the customer.</returns>
		protected abstract String[] GetCustomerRolesCore(CustomerToken customerToken);
		/// <summary>
		/// Returns the roles for a customer.
		/// </summary>
		/// <param name="masterCustomerId">Identifies the customer.</param>
		/// <returns>The roles for the customer.</returns>
		public String[] GetCustomerRolesByMasterCustomerId(String masterCustomerId) {
			this.ThrowIfDisposed();
			if (masterCustomerId == null)
				throw new ArgumentNullException("masterCustomerId");

			// For testing...
			//return Enumerable.Range(1, 9)
			//    .Select(i => "Test" + i)
			//    .ToArray();

			return PersonifyIMService.Execute(() => this.GetCustomerRolesByMasterCustomerIdCore(masterCustomerId));
		}
		/// <summary>
		/// Returns the roles for a customer.
		/// </summary>
		/// <param name="masterCustomerId">Identifies the customer.</param>
		/// <returns>The roles for the customer.</returns>
		protected abstract String[] GetCustomerRolesByMasterCustomerIdCore(String masterCustomerId);
		/// <summary>
		/// Returns a new instance of the <see cref="T:PersonifyIMService"/> class.
		/// </summary>
		/// <returns>The object created.</returns>
		public static PersonifyIMService NewPersonifyIMService() {
			return PersonifyIMService.NewPersonifyIMService(null);
		}
		/// <summary>
		/// Returns a new instance of the <see cref="T:PersonifySsoService"/> class.
		/// </summary>
		/// <param name="credentials">The vendor's credentials to use for service calls.</param>
		/// <returns>The object created.</returns>
		public static PersonifyIMService NewPersonifyIMService(NetworkCredential credentials) {
			return PersonifyIMService._factory(credentials);
		}
	}
}
