using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using PersonifyConfiguration = Msec.Personify.Configuration.PersonifyConfiguration;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents a proxy for the Personify SSO service.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sso", Justification = "The term 'Sso' is an abbreviation for Single Sign On.")]
	public abstract class PersonifySsoService : DisposableBase {
	// Fields
		/// <summary>
		/// The vendor's credentials to use for the service calls.  This field is read-only.
		/// </summary>
		private readonly VendorCredentials _credentials;
#if (DEBUG)
		/// <summary>
		/// The factory method used to create instances of the actual service objects.
		/// </summary>
		/// <remarks>This field is not read-only in the DEBUG version to assist in unit testing.</remarks>
		private static Func<VendorCredentials, PersonifySsoService> _factory = PersonifySsoService.CreatePersonifySsoService;
#else
		/// <summary>
		/// The factory method used to create instances of the actual service objects.  This field is read-only.
		/// </summary>
		private static readonly Func<VendorCredentials, PersonifySsoService> _factory = PersonifySsoService.CreatePersonifySsoService;
#endif

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifySsoService"/> class.
		/// </summary>
		protected PersonifySsoService() : this(null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifySsoService"/> class.
		/// </summary>
		/// <param name="credentials">The vendor's credentials to use for the service calls.</param>
		protected PersonifySsoService(VendorCredentials credentials)
			: base() {
			this._credentials = credentials ?? PersonifyConfiguration.Instance.SsoVendorCredentials;
		}

	// Properties
		/// <summary>
		/// Gets the vendor's credentials to use for service calls.
		/// </summary>
		protected VendorCredentials Credentials {
			get { return this._credentials; }
		}

	// Methods
		/// <summary>
		/// Acts as the default factory method.
		/// </summary>
		/// <param name="credentials">The vendor's credentials to use for the service calls.</param>
		/// <returns>The service object created.</returns>
		private static PersonifySsoService CreatePersonifySsoService(VendorCredentials credentials) {
			return new WebPersonifySsoService(credentials);
		}
		/// <summary>
		/// Decrypts an encrypted customer token value.
		/// </summary>
		/// <param name="encryptedCustomerToken">The customer token to decrypt.</param>
		/// <returns>The decrypted value of the token.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="encryptedCustomerToken"/> is a null reference.</exception>
		/// <exception cref="Msec.ServiceException">An error occurs while communicating with the service.</exception>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public String DecryptCustomerToken(String encryptedCustomerToken) {
			this.ThrowIfDisposed();
			if (encryptedCustomerToken == null) {
				throw new ArgumentNullException("encryptedCustomerToken");
			}

			return PersonifySsoService.Execute(() => this.DecryptCustomerTokenCore(encryptedCustomerToken));
		}
		/// <summary>
		/// Decrypts an encrypted customer token value.
		/// </summary>
		/// <param name="encryptedCustomerToken">The customer token to decrypt.</param>
		/// <returns>The decrypted value of the token.</returns>
		protected abstract String DecryptCustomerTokenCore(String encryptedCustomerToken);
		/// <summary>
		/// Encrypts a vendor token using the return URL specified.
		/// </summary>
		/// <param name="returnUrl">The return URL to include in the vendor token.</param>
		/// <returns>The encrypted vendor token created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="returnUrl"/> is a null reference.</exception>
		/// <exception cref="Msec.ServiceException">An error occurs while communicating with the service.</exception>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public String EncryptVendorToken(Uri returnUrl) {
			this.ThrowIfDisposed();
			if (returnUrl == null) {
				throw new ArgumentNullException("returnUrl");
			}

			return PersonifySsoService.Execute(() => this.EncryptVendorTokenCore(returnUrl.ToString()));
		}
		/// <summary>
		/// Encrypts a vendor token using the return URL specified.
		/// </summary>
		/// <param name="returnUrl">The return URL to include in the vendor token.</param>
		/// <returns>The encrypted vendor token created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="returnUrl"/> is a null reference.</exception>
		/// <exception cref="Msec.ServiceException">An error occurs while communicating with the service.</exception>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		[SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads", Justification = "The implementation calls for the URL to be a string.")]
		public String EncryptVendorToken(String returnUrl) {
			this.ThrowIfDisposed();
			if (returnUrl == null) {
				throw new ArgumentNullException("returnUrl");
			}

			return PersonifySsoService.Execute(() => this.EncryptVendorTokenCore(returnUrl));
		}
		/// <summary>
		/// Encrypts a vendor token using the return URL specified.
		/// </summary>
		/// <param name="returnUrl">The return URL to include in the vendor token.</param>
		/// <returns>The encrypted vendor token created.</returns>
		[SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "The implementation calls for the URL to be a string.")]
		protected abstract String EncryptVendorTokenCore(String returnUrl);
		/// <summary>
		/// Executes a function.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <exception cref="Msec.ServiceException">An error occurs while communicating with the service.</exception>
		private static void Execute(Action action) {
			Debug.Assert(action != null);
			try {
				action();
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
		/// Returns the customer name/identifier from the customer token specified.
		/// </summary>
		/// <param name="customerToken">The customer token to translate to a customer name/identifier.</param>
		/// <returns>The customer's user name.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="customerToken"/> is a null reference.</exception>
		/// <exception cref="Msec.ServiceException">An error occurs while communicating with the service.</exception>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public String GetCustomerName(CustomerToken customerToken) {
			this.ThrowIfDisposed();
			if (customerToken == null) {
				throw new ArgumentNullException("customerToken");
			}

			return PersonifySsoService.Execute(() => this.GetCustomerNameCore(customerToken));
		}
		/// <summary>
		/// Returns the customer name/identifier from the customer token specified.
		/// </summary>
		/// <param name="customerToken">The customer token to translate to a customer name/identifier.</param>
		/// <returns>The customer's user name.</returns>
		protected abstract String GetCustomerNameCore(CustomerToken customerToken);
		/// <summary>
		/// Returns a value indicating if the customer token specified is valid.
		/// </summary>
		/// <param name="encryptedCustomerToken">The customer token to validate.</param>
		/// <returns><c>true</c> if the customer token is valid; otherwise, <c>false</c>.</returns>
		/// <exception cref="Msec.ServiceException">An error occurs while communicating with the service.</exception>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public Boolean IsCustomerTokenValid(String encryptedCustomerToken) {
			this.ThrowIfDisposed();
			if (encryptedCustomerToken == null) {
				return false;
			}
			return PersonifySsoService.Execute(() => this.IsCustomerTokenValidCore(encryptedCustomerToken));
		}
		/// <summary>
		/// Returns a value indicating if the customer token specified is valid.
		/// </summary>
		/// <param name="encryptedCustomerToken">The customer token to validate.</param>
		/// <returns><c>true</c> if the customer token is valid; otherwise, <c>false</c>.</returns>
		protected abstract Boolean IsCustomerTokenValidCore(String encryptedCustomerToken);
		/// <summary>
		/// Logs out a customer.
		/// </summary>
		/// <param name="customerToken">The token for the customer to logout.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="customerToken"/> is a null reference.</exception>
		/// <exception cref="Msec.ServiceException">An error occurs while communicating with the service.</exception>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public void LogoutCustomer(CustomerToken customerToken) {
			this.ThrowIfDisposed();
			if (customerToken == null)
				throw new ArgumentNullException("customerToken");

			PersonifySsoService.Execute(() => this.LogoutCustomerCore(customerToken));
		}
		/// <summary>
		/// Logs out a customer.
		/// </summary>
		/// <param name="customerToken">The token for the customer to logout.</param>
		protected abstract void LogoutCustomerCore(CustomerToken customerToken);
		/// <summary>
		/// Returns a new instance of the <see cref="T:PersonifySsoService"/> class.
		/// </summary>
		/// <returns>The object created.</returns>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sso", Justification = "The term 'Sso' is an abbreviation for Single Sign On.")]
		public static PersonifySsoService NewPersonifySsoService() {
			return PersonifySsoService.NewPersonifySsoService(null);
		}
		/// <summary>
		/// Returns a new instance of the <see cref="T:PersonifySsoService"/> class.
		/// </summary>
		/// <param name="credentials">The vendor's credentials to use for service calls.</param>
		/// <returns>The object created.</returns>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sso", Justification = "The term 'Sso' is an abbreviation for Single Sign On.")]
		public static PersonifySsoService NewPersonifySsoService(VendorCredentials credentials) {
			return PersonifySsoService._factory(credentials);
		}
#if (DEBUG)
		/// <summary>
		/// Resets the factory method to its original state used by this type.
		/// </summary>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public static void ResetFactoryMethod() {
			PersonifySsoService._factory = PersonifySsoService.CreatePersonifySsoService;
		}
		/// <summary>
		/// Sets the factory method used to create instance of the service.
		/// </summary>
		/// <param name="factory">The factory method to use.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="factory"/> is a null reference.</exception>
		/// <remarks>This method only exists in the DEBUG version to assist in unit testing.</remarks>
		public static void SetFactoryMethod(Func<VendorCredentials, PersonifySsoService> factory) {
			if (factory == null) {
				throw new ArgumentNullException("factory");
			}
			PersonifySsoService._factory = factory;
		}
#endif
	}
}
