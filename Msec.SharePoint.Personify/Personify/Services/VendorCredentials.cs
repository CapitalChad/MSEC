using System;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents vendor credentials used to communicate with the Personify SSO service.  This class may not be inherited.  Instances of this class are immutable.
	/// </summary>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class VendorCredentials : Object, IEquatable<VendorCredentials> {
	// Fields
		/// <summary>
		/// The initialization block used in encryption/decryption.  This field is read-only.
		/// </summary>
		private readonly String _block;
		/// <summary>
		/// The vendor's password.  This field is read-only.
		/// </summary>
		private readonly String _password;
		/// <summary>
		/// The vendor's user name.  This field is read-only.
		/// </summary>
		private readonly String _userName;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:VendorCredentials"/> class.
		/// </summary>
		/// <param name="userName">The vendor's user name.</param>
		/// <param name="password">The vendor's password.</param>
		/// <param name="block">The initialization block used in encryption/decryption.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="userName"/> is a null reference.
		/// -or- <paramref name="password"/> is a null reference.
		/// -or- <paramref name="block"/> is a null reference.</exception>
		public VendorCredentials(String userName, String password, String block)
			: base() {
			if (userName == null) {
				throw new ArgumentNullException("userName");
			}
			if (password == null) {
				throw new ArgumentNullException("password");
			}
			if (block == null) {
				throw new ArgumentNullException("block");
			}
			this._userName = userName;
			this._password = password;
			this._block = block;
		}

	// Properties
		/// <summary>
		/// Gets the initialization block used in encryption/decryption.
		/// </summary>
		public String Block {
			get { return this._block; }
		}
		/// <summary>
		/// Gets the vendor's password.
		/// </summary>
		public String Password {
			get { return this._password; }
		}
		/// <summary>
		/// Gets the vendor's user name.
		/// </summary>
		public String UserName {
			get { return this._userName; }
		}

	// Methods
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="obj">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			VendorCredentials other = obj as VendorCredentials;
			return this.Equals(other);
		}
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(VendorCredentials other) {
			if (other == null) {
				return false;
			}
			return String.Equals(this._userName, other._userName, StringComparison.Ordinal)
				&& String.Equals(this._password, other._password, StringComparison.Ordinal)
				&& String.Equals(this._block, other._block, StringComparison.Ordinal);
		}
		/// <summary>
		/// Returns a value that can be used as a hash code for this instance.
		/// </summary>
		/// <returns>The value that can be used as a hash code for this instance.</returns>
		public override Int32 GetHashCode() {
			String combinedValue = new String[] { this._userName, this._password, this._block }.Join("\r");
			return combinedValue.GetHashCode();
		}
	}
}
