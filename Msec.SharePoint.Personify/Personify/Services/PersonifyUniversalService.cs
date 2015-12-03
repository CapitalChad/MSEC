using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Msec.Collections;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents a proxy for the Personify Universal service.
	/// </summary>
	public abstract class PersonifyUniversalService : DisposableBase {
	// fields
#if (DEBUG)
		/// <summary>
		/// The factory method used to create instances of the actual service objects.
		/// </summary>
		/// <remarks>This field is not read-only in the DEBUG version to assist in unit testing.</remarks>
		private static Func<PersonifyUniversalService> _factory = PersonifyUniversalService.CreatePersonifyUniversalService;
#else
		/// <summary>
		/// The factory method used to create instances of the actual service objects.  This field is read-only.
		/// </summary>
		private static readonly Func<PersonifyUniversalService> _factory = PersonifyUniversalService.CreatePersonifyUniversalService;
#endif

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyUniversalService"/> class.
		/// </summary>
		protected PersonifyUniversalService() : base() { }

	// Methods
		/// <summary>
		/// Acts as the default factory method.
		/// </summary>
		/// <returns>The service object created.</returns>
		private static PersonifyUniversalService CreatePersonifyUniversalService() {
			return new WebPersonifyUniversalService();
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
		public IEnumerable<CustomerAddressData> GetCustomerAddressesForUser(String userName) {
			this.ThrowIfDisposed();
			if (userName == null)
				return new CustomerAddressData[0];

			return PersonifyUniversalService.Execute(() => this.GetCustomerAddressesForUserCore(userName));
		}
		protected abstract IEnumerable<CustomerAddressData> GetCustomerAddressesForUserCore(String userName);
		public CustomerData GetCustomerByUserName(String userName) {
			this.ThrowIfDisposed();
			if (userName == null)
				return null;

			return PersonifyUniversalService.Execute(() => this.GetCustomerByUserNameCore(userName));
		}
		protected abstract CustomerData GetCustomerByUserNameCore(String userName);
		public IEnumerable<CustomerData> GetCustomersWhereFullNameStartsWith(String name) {
			this.ThrowIfDisposed();
			if (name == null)
				return new CustomerData[0];

			return PersonifyUniversalService.Execute(() => this.GetCustomersWhereFullNameStartsWithCore(name));
		}
		protected abstract IEnumerable<CustomerData> GetCustomersWhereFullNameStartsWithCore(String name);
		public IEnumerable<CustomerData> GetCustomersWhereEmailAddressEquals(String emailAddress) {
			this.ThrowIfDisposed();
			if (emailAddress == null)
				return new CustomerData[0];

			return PersonifyUniversalService.Execute(() => this.GetCustomersWhereEmailAddressEqualsCore(emailAddress));
		}
		protected abstract IEnumerable<CustomerData> GetCustomersWhereEmailAddressEqualsCore(String emailAddress);
		public IEnumerable<CustomerData> GetCustomersWhereEmailAddressStartsWith(String emailAddress) {
			this.ThrowIfDisposed();
			if (emailAddress == null)
				return new CustomerData[0];

			return PersonifyUniversalService.Execute(() => this.GetCustomersWhereEmailAddressStartsWithCore(emailAddress));
		}
		protected abstract IEnumerable<CustomerData> GetCustomersWhereEmailAddressStartsWithCore(String emailAddress);
		public IEnumerable<CustomerData> GetCustomersWhereUserNameEquals(String userName) {
			this.ThrowIfDisposed();
			if (userName == null)
				return new CustomerData[0];

			return PersonifyUniversalService.Execute(() => this.GetCustomersWhereUserNameEqualsCore(userName));
		}
		protected abstract IEnumerable<CustomerData> GetCustomersWhereUserNameEqualsCore(String userName);
		public IEnumerable<CustomerData> GetCustomersWhereUserNameStartsWith(String userName) {
			this.ThrowIfDisposed();
			if (userName == null)
				return new CustomerData[0];

			return PersonifyUniversalService.Execute(() => this.GetCustomersWhereUserNameStartsWithCore(userName));
		}
		protected abstract IEnumerable<CustomerData> GetCustomersWhereUserNameStartsWithCore(String userName);
		/// <summary>
		/// Returns the enumerable collection of valid position codes.
		/// </summary>
		/// <returns>The enumerable collection of valid position codes.</returns>
		/// <exception cref="Msec.ServiceException">An error occurs while attempting to communicate with the service.</exception>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This method represents a service call.")]
		public IEnumerable<String> GetPositionCodes() {
			this.ThrowIfDisposed();
			return PersonifyUniversalService.Execute(() => this.GetPositionCodesCore()) ?? new String[0];
		}
		/// <summary>
		/// Returns the enumerable collection of valid position codes.
		/// </summary>
		/// <returns>The enumerable collection of valid position codes.</returns>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This method represents a service call.")]
		protected abstract IEnumerable<String> GetPositionCodesCore();
		/// <summary>
		/// Returns a new instance of the <see cref="T:PersonifyUniversalService"/> class.
		/// </summary>
		/// <returns>The object created.</returns>
		public static PersonifyUniversalService NewPersonifyUniversalService() {
			return PersonifyUniversalService._factory();
		}
#if (DEBUG)
		/// <summary>
		/// Resets the factory method to its original state used by this type.
		/// </summary>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public static void ResetFactoryMethod() {
			PersonifyUniversalService._factory = PersonifyUniversalService.CreatePersonifyUniversalService;
		}
		/// <summary>
		/// Sets the factory method used to create instance of the service.
		/// </summary>
		/// <param name="factory">The factory method to use.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="factory"/> is a null reference.</exception>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public static void SetFactoryMethod(Func<PersonifyUniversalService> factory) {
			if (factory == null) {
				throw new ArgumentNullException("factory");
			}
			PersonifyUniversalService._factory = factory;
		}
#endif
	}
}
