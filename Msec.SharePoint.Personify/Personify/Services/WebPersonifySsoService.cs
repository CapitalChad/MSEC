using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Msec.Diagnostics;
using Msec.Personify.Services.PersonifySsoServiceImpl;

using PersonifyConfiguration = Msec.Personify.Configuration.PersonifyConfiguration;

namespace Msec.Personify.Services {
	/// <summary>
	/// The web-enabled implementation of the <see cref="T:PersonifySsoService"/> class.  This class may not be inherited.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sso", Justification = "The term 'Sso' is an abbreviation for Single Sign On.")]
	public sealed class WebPersonifySsoService : PersonifySsoService {
	// Fields
		/// <summary>
		/// The web service proxy to use.
		/// </summary>
		private service _service;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:WebPersonifySsoService"/> class.
		/// </summary>
		/// <param name="credentials">The vendor's credentials to use for service calls.</param>
		public WebPersonifySsoService(VendorCredentials credentials)
			: base(credentials) {
			this._service = new service() {
				Url = PersonifyConfiguration.Instance.SsoServiceUrl.ToString()
			};
		}

	// Methods
		/// <summary>
		/// Decrypts an encrypted customer token value.
		/// </summary>
		/// <param name="encryptedCustomerToken">The customer token to decrypt.</param>
		/// <returns>The decrypted value of the token.</returns>
		protected override String DecryptCustomerTokenCore(String encryptedCustomerToken) {
			Debug.Assert(this._service != null);
			Debug.Assert(encryptedCustomerToken != null);

			SSOCustomerTokenIsValidResult result = WebPersonifySsoService.ExecuteServiceCall(
				() => this._service.SSOCustomerTokenIsValid(this.Credentials.UserName, this.Credentials.Password, encryptedCustomerToken),
				callResult => callResult.Errors);
			Boolean isValid = result.Valid;
			if (isValid) {
				this.LogVerbose("WebPersonifySsoService: Customer token {0} is valid and returned decrypted value {1}.", encryptedCustomerToken, result.NewCustomerToken);
				return result.NewCustomerToken;
			}

			try {
				CustomerTokenDecryptResult decryptResult = WebPersonifySsoService.ExecuteServiceCall(
					() => this._service.CustomerTokenDecrypt(this.Credentials.UserName, this.Credentials.Password, this.Credentials.Block, encryptedCustomerToken),
					callResult => callResult.Errors);
				String decryptedCustomerToken = decryptResult.CustomerToken;
				if (decryptedCustomerToken != null) {
					this.LogInformation("SSOCustomerTokenIsValid returned false, but DecryptCustomerTokenCore returned a decrypted token.  Returning true.");
					return decryptedCustomerToken;
				}
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely()) {
					throw;
				}
				this.LogError("Error occurred calling DecryptCustomerTokenCore(): {0}", ex);
			}

			this.LogVerbose("WebPersonifySsoService: Customer token {0} is not valid.", encryptedCustomerToken);
			return null;
		}
		/// <summary>
		/// Encrypts a vendor token using the return URL specified.
		/// </summary>
		/// <param name="returnUrl">The return URL to include in the vendor token.</param>
		/// <returns>The encrypted vendor token created.</returns>
		protected override String EncryptVendorTokenCore(String returnUrl) {
			Debug.Assert(this._service != null);
			Debug.Assert(returnUrl != null);

			this.LogVerbose("Calling VendorTokenEncrypt(userName, password, block, {0}).", returnUrl);
			VendorTokenEncryptResult result = WebPersonifySsoService.ExecuteServiceCall(
				() => this._service.VendorTokenEncrypt(this.Credentials.UserName, this.Credentials.Password, this.Credentials.Block, returnUrl),
				callResult => callResult.Errors);
			String vendorToken = result.VendorToken;
			this.LogVerbose("VendorTokenEncrypt(userName, password, block, {0}) returned vendor token {1}.", returnUrl, vendorToken);
			return vendorToken;
		}
		/// <summary>
		/// Executes a service call.
		/// </summary>
		/// <typeparam name="T">The type of result to return.</typeparam>
		/// <param name="func">Represents the service call.</param>
		/// <param name="errorsFunc">A function that returns a list of errors from a servie call result.</param>
		/// <returns>The result of the service call.</returns>
		/// <exception cref="Msec.ServiceException">An error occurs during the service call.</exception>
		private static T ExecuteServiceCall<T>(Func<T> func, Func<T, String[]> errorsFunc) {
			T result;
			try {
				result = func();
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely()) {
					throw;
				}
				throw new ServerServiceException("An error occurred while attempting to communicate with the service.", ex);
			}

			String[] errors = errorsFunc(result);
			if (errors != null && errors.Length > 0) {
				ServerServiceException serverServiceException = new ServerServiceException(errors[0]);
				foreach (Int32 i in Enumerable.Range(0, errors.Length)) {
					serverServiceException.Data["Error" + i] = errors[i];
				}
				throw serverServiceException;
			}
			return result;
		}
		/// <summary>
		/// Returns the customer name/identifier from the customer token specified.
		/// </summary>
		/// <param name="decryptedCustomerToken">The customer token to translate to a customer name/identifier.</param>
		/// <returns>The customer's user name.</returns>
		protected override String GetCustomerNameCore(String decryptedCustomerToken) {
			Debug.Assert(this._service != null);
			if (decryptedCustomerToken == null) {
				throw new ArgumentNullException("decryptedCustomerToken");
			}

			TIMSSCustomerIdentifierGetResult timssResult = WebPersonifySsoService.ExecuteServiceCall(
				() => this._service.TIMSSCustomerIdentifierGet(this.Credentials.UserName, this.Credentials.Password, decryptedCustomerToken),
				callResult => callResult.Errors);
			this.LogVerbose("TIMSSCustomerIdentifierGet(userName, password, {0}) returned user name {1}.", decryptedCustomerToken, timssResult.CustomerIdentifier);
			String[] parts = timssResult.CustomerIdentifier.Split('|');
			return parts[0];
		}
		/// <summary>
		/// Logs out a customer.
		/// </summary>
		/// <param name="customerToken">The token for the customer to logout.</param>
		protected override void LogoutCustomerCore(CustomerToken customerToken) {
			Debug.Assert(customerToken != null);

			SSOCustomerLogoutResult result = WebPersonifySsoService.ExecuteServiceCall(
				() => this._service.SSOCustomerLogout(this.Credentials.UserName, this.Credentials.Password, customerToken),
				callResult => callResult.Errors);
			this.LogVerbose("SSOCustomerLogout(userName, password, customerToken) returned {0} errors.", (result.Errors ?? new String[0]).Length);
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
	}
}
