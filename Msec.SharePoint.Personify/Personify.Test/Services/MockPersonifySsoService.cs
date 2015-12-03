using System;
using System.Collections.Generic;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents a mock implementation for the <see cref="T:PersonifySsoService"/> class.  This class may not be inherited.
	/// </summary>
	public sealed class MockPersonifySsoService : PersonifySsoService {
	// Fields
		/// <summary>
		/// The dictionary of values keyed by decrypted value.  This field is read-only.
		/// </summary>
		private readonly IDictionary<String, String> _decryptedToEncrypted = new Dictionary<String, String>();
		/// <summary>
		/// The dictionary of values keyed by encrypted value.  This field is read-only.
		/// </summary>
		private readonly IDictionary<String, String> _encryptedToDecrypted = new Dictionary<String, String>();
		/// <summary>
		/// Used to generate random values.  This field is read-only.
		/// </summary>
		private readonly Random _random = new Random();

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifySsoService"/> class.
		/// </summary>
		public MockPersonifySsoService() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifySsoService"/> class.
		/// </summary>
		/// <param name="credentials">The vendor's credentials to use.</param>
		public MockPersonifySsoService(VendorCredentials credentials) : base(credentials) { }

	// Properties
		/// <summary>
		/// Gets or sets a value indicating if the <see cref="M:PersonifySsoService.IsCustomerTokenValid"/> method should return <c>true</c> or <c>false</c>.
		/// </summary>
		public Boolean IsCustomerTokenValidReturnValue {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating if this instance should throw exceptions when functionality is called.
		/// </summary>
		public Boolean ThrowExceptions {
			get;
			set;
		}
		/// <summary>
		/// Gets the vendor credentials used.
		/// </summary>
		public VendorCredentials VendorCredentials {
			get { return this.Credentials; }
		}
		
	// Methods
		/// <summary>
		/// Decrypts an encrypted customer token value.
		/// </summary>
		/// <param name="encryptedCustomerToken">The customer token to decrypt.</param>
		/// <returns>The decrypted value of the token.</returns>
		protected override String DecryptCustomerTokenCore(String encryptedCustomerToken) {
			if (this.ThrowExceptions) {
				throw new InvalidOperationException("This is a test.");
			}
			if (!this._encryptedToDecrypted.ContainsKey(encryptedCustomerToken)) {
				String decryptedValue = this._random.NextString();
				this._encryptedToDecrypted[encryptedCustomerToken] = decryptedValue;
				this._decryptedToEncrypted[decryptedValue] = encryptedCustomerToken;
			}
			return this._encryptedToDecrypted[encryptedCustomerToken];
		}
		/// <summary>
		/// Encrypts a vendor token using the return URL specified.
		/// </summary>
		/// <param name="returnUrl">The return URL to include in the vendor token.</param>
		/// <returns>The encrypted vendor token created.</returns>
		protected override String EncryptVendorTokenCore(String returnUrl) {
			if (this.ThrowExceptions) {
				throw new System.Configuration.ConfigurationErrorsException("This is a test.");
			}
			if (!this._decryptedToEncrypted.ContainsKey(returnUrl)) {
				String encryptedValue = this._random.NextString();
				this._encryptedToDecrypted[encryptedValue] = returnUrl;
				this._decryptedToEncrypted[returnUrl] = encryptedValue;
			}
			return this._decryptedToEncrypted[returnUrl];
		}
		/// <summary>
		/// Returns the customer name/identifier from the customer token specified.
		/// </summary>
		/// <param name="customerToken">The customer token to translate to a customer name/identifier.</param>
		/// <returns>The customer's user name.</returns>
		protected override String GetCustomerNameCore(CustomerToken customerToken) {
			if (this.ThrowExceptions) {
				throw new ApplicationException("This is a test.");
			}
			return this._random.NextString();
		}
		/// <summary>
		/// Returns a value indicating if the customer token specified is valid.
		/// </summary>
		/// <param name="encryptedCustomerToken">The customer token to validate.</param>
		/// <returns><c>true</c> if the customer token is valid; otherwise, <c>false</c>.</returns>
		protected override Boolean IsCustomerTokenValidCore(String encryptedCustomerToken) {
			if (this.ThrowExceptions) {
				throw new System.Security.SecurityException("This is a test.");
			}
			return this.IsCustomerTokenValidReturnValue;
		}

        protected override void LogoutCustomerCore(CustomerToken customerToken)
        {
        }
    }
}
