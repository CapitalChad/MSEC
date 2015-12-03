using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Msec.Diagnostics;
using Msec.Personify.Services;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;
using PersonifyConfiguration = Msec.Personify.Configuration.PersonifyConfiguration;
using SsoService = Msec.Personify.Services.PersonifySsoServiceImpl.service;

namespace Msec.Personify {
	/// <summary>
	/// Represents an encrypted string that is used as validation of authentication via Personify SSO.  This class may not be inherited.  Instances of this class are immutable.
	/// </summary>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class CustomerToken : Object, IEquatable<CustomerToken>, IEquatable<String> {
	// Fields
		/// <summary>
		/// The encrypted value of the token.  This field is read-only.
		/// </summary>
		private readonly String _encryptedValue;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomerToken"/> class.
		/// </summary>
		/// <param name="encryptedValue">The encrypted value of the token.</param>
		private CustomerToken(String encryptedValue)
			: base() {
			Debug.Assert(encryptedValue != null);
			this._encryptedValue = encryptedValue;
		}

	// Methods
		/// <summary>
		/// Creates and returns a new instance of the <see cref="T:CustomerToken"/> class.
		/// </summary>
		/// <param name="encryptedValue">The encrypted value of the token.</param>
		/// <returns>The object created if <paramref name="encryptedValue"/> is valid.
		/// -or- A null reference if <paramref name="encryptedValue"/> is invalid.</returns>
		public static CustomerToken Create(String encryptedValue) {
			return CustomerToken.Create(encryptedValue, false);
		}
		/// <summary>
		/// Creates and returns a new instance of the <see cref="T:CustomerToken"/> class.
		/// </summary>
		/// <param name="encryptedValue">The encrypted value of the token.</param>
		/// <param name="throwOnError"><c>true</c> if a validation error should throw an exception; otherwise, <c>false</c>.</param>
		/// <returns>The object created if <paramref name="encryptedValue"/> is valid.
		/// -or- A null reference if <paramref name="encryptedValue"/> is invalid and <paramref name="throwOnError"/> is <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="encryptedValue"/> is a null reference and <paramref name="throwOnError"/> is <c>true</c>.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="encryptedValue"/> is invalid and <paramref name="throwOnError"/> is <c>true</c>.</exception>
		public static CustomerToken Create(String encryptedValue, Boolean throwOnError) {
			if (encryptedValue == null) {
				if (throwOnError) {
					throw new ArgumentNullException("encryptedValue");
				}
				return null;
			}

			Boolean isCustomerTokenValid;
			try {
				using (PersonifySsoService service = PersonifySsoService.NewPersonifySsoService()) {
					isCustomerTokenValid = service.IsCustomerTokenValid(encryptedValue);
				}
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely()) {
					throw;
				}
				typeof(CustomerToken).LogError("A customer token could not be checked for validity.  Returning false: {0}.", ex);
				isCustomerTokenValid = false;
			}
			if (!isCustomerTokenValid) {
				if (throwOnError) {
					throw new ArgumentException("The value specified is not an encrypted customer token.", "encryptedValue");
				}
				return null;
			}
			return new CustomerToken(encryptedValue);
		}
		/// <summary>
		/// Decrypts the token and returns the resulting value.
		/// </summary>
		/// <returns>The decrypted token.</returns>
		public String Decrypt() {
			return CustomerToken.Decrypt(this._encryptedValue);
		}
		/// <summary>
		/// Decrypts an encrypted customer token value and returns the resulting value.
		/// </summary>
		/// <param name="encryptedValue">The encrypted value of the token.</param>
		/// <returns>The decrypted token, or a null reference if the token could not be decrypted.</returns>
		private static String Decrypt(String encryptedValue) {
			try {
				using (PersonifySsoService service = PersonifySsoService.NewPersonifySsoService()) {
					return service.DecryptCustomerToken(encryptedValue);
				}
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely()) {
					throw;
				}
				typeof(CustomerToken).LogError("An attempt to communicate with the SSO service failed: {0}", ex);
			}
			return null;
		}
		/// <summary>
		/// Returns a value indicating if this intance is equal to the object specified.
		/// </summary>
		/// <param name="obj">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			Boolean isCustomerTokenEqual = this.Equals(obj as CustomerToken);
			if (isCustomerTokenEqual) {
				return true;
			}
			return this.Equals(obj as String);
		}
		/// <summary>
		/// Returns a value indicating if this intance is equal to the object specified.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(CustomerToken other) {
			if (other == null) {
				return false;
			}
			return String.Equals(this._encryptedValue, other._encryptedValue, StringComparison.Ordinal);
		}
		/// <summary>
		/// Returns a value indicating if this intance is equal to the object specified.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(String other) {
			Debug.Assert(this._encryptedValue != null);
			if (other == null) {
				return false;
			}
			return String.Equals(this._encryptedValue, other, StringComparison.Ordinal);
		}
		/// <summary>
		/// Returns a value that can be used as a hash code for this instance.
		/// </summary>
		/// <returns>The hash code value for this instance.</returns>
		public override Int32 GetHashCode() {
			Debug.Assert(this._encryptedValue != null);
			return this._encryptedValue.GetHashCode();
		}
		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override String ToString() {
			return this._encryptedValue;
		}

	// Operators
		/// <summary>
		/// Implicitly converts a <see cref="T:CustomerToken"/> object to a <see cref="T:String"/> object.
		/// </summary>
		/// <param name="instance">The object to convert.</param>
		/// <returns>The converted value.</returns>
		public static implicit operator String(CustomerToken instance) {
			if (instance == null) {
				return null;
			}
			return instance._encryptedValue;
		}
		/// <summary>
		/// Explicitly converts a <see cref="T:String"/> object to a <see cref="T:CustomerToken"/> object.
		/// </summary>
		/// <param name="encryptedValue">The object to convert.</param>
		/// <returns>The converted value.</returns>
		/// <exception cref="System.InvalidCastException"><paramref name="encryptedValue"/> is invalid.</exception>
		public static explicit operator CustomerToken(String encryptedValue) {
			if (encryptedValue == null) {
				return null;
			}

			CustomerToken customerToken = CustomerToken.Create(encryptedValue, false);
			if (customerToken == null) {
				throw new InvalidCastException();
			}
			return customerToken;
		}
	}
}
