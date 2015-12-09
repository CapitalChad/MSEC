using System;
using System.Linq;
using Msec.Personify.Services;
using Msec.Diagnostics;

namespace Msec.Personify {
	/// <summary>
	/// Represents a user from the Personify services.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	public sealed class PersonifyUser : Object {
	// Fields
		/// <summary>
		/// The customer token for the user.  This field is read-only.
		/// </summary>
		private readonly String _decryptedCustomerToken;
		/// <summary>
		/// The user's primary e-mail address.
		/// </summary>
		private String _emailAddress;
		/// <summary>
		/// <c>true</c> if the user exists; <c>false</c> if the user does not exist; a null reference if unknown.
		/// </summary>
		private Boolean? _exists;
		/// <summary>
		/// The first name of the user.
		/// </summary>
		private String _firstName;
		/// <summary>
		/// <c>true</c> if customer data has been loaded for this instance; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isCustomerDataLoaded;
		/// <summary>
		/// The last name of the user.
		/// </summary>
		private String _lastName;
		/// <summary>
		/// The user name for the user.
		/// </summary>
		private String _masterCustomerId;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyUser"/> class.
		/// </summary>
		/// <param name="userName">The user name of the user.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="userName"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="userName"/> is empty.
		/// -or- <paramref name="userName"/> contains only white-space characters.</exception>
		public PersonifyUser(String userName)
			: base() {
			if (userName == null)
				throw new ArgumentNullException("userName");

			userName = userName.Trim();
			if (userName.Length == 0)
				throw new ArgumentException("The value specified does not have any non-white-space characters.", "userName");

			this._masterCustomerId = userName;
		}
		private PersonifyUser(String userName, String decryptedCustomerToken)
			: base() {
			this._masterCustomerId = userName;
			this._decryptedCustomerToken = decryptedCustomerToken;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifyUser"/> class.
		/// </summary>
		/// <param name="customer">Contains the customer data from which to create this instance.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="customer"/> is a null reference.</exception>
		internal PersonifyUser(CustomerData customer)
			: base() {
			if (customer == null)
				throw new ArgumentNullException("customer");

			this.LoadCustomerData(customer);
		}

	// Properties
		/// <summary>
		/// Gets the decrypted customer token for the user.
		/// </summary>
		public String CustomerToken {
			get { return this._decryptedCustomerToken; }
		}
		/// <summary>
		/// Gets the display name for the user.
		/// </summary>
		public String DisplayName {
			get {
				return this.UserName;
			}
		}
		/// <summary>
		/// Gets the e-mail address of the user.
		/// </summary>
		public String EmailAddress {
			get {
				this.EnsureCustomerData();
				return this._emailAddress;
			}
		}
		/// <summary>
		/// Gets a value indicating if this user exists.
		/// </summary>
		public Boolean Exists {
			get {
				this.EnsureExistsIsKnown();
				return this._exists.Value;
			}
		}
		public String FriendlyName {
			get {
				this.EnsureCustomerData();
				if (this._lastName != null) {
					if (this._firstName != null) {
						return this._firstName + " " + this._lastName;
					}
					return this._lastName;
				}
				if (this._firstName != null) {
					return this._firstName;
				}
				return this.UserName;
			}
		}
		public String LoginName {
			get { return "i:0#.f|msecpersonifymembershipprovider|" + this.UserName; }
		}
		/// <summary>
		/// Gets the user name for the user.
		/// </summary>
		public String UserName {
			get {
				this.EnsureExistsIsKnown();
				this.EnsureCustomerData();
				if (this._lastName != null) {
					if (this._firstName != null) {
						return this._firstName + " " + this._lastName + " (" + this._masterCustomerId + ")";
					}
					return this._lastName + " (" + this._masterCustomerId + ")";
				}
				if (this._firstName != null) {
					return this._firstName + " (" + this._masterCustomerId + ")";
				}
				return this._masterCustomerId;
			}
		}

	// Methods
		/// <summary>
		/// Ensures that any customer data is loaded.
		/// </summary>
		private void EnsureCustomerData() {
			if (!this._isCustomerDataLoaded) {
				if (this._masterCustomerId == null) {
					this.LogVerbose("PersonifyUser: Retrieving user name for customer token {0}...", this._decryptedCustomerToken);
					using (PersonifySsoService ssoService = PersonifySsoService.NewPersonifySsoService()) {
						this._masterCustomerId = ssoService.GetCustomerName(this._decryptedCustomerToken);
						this.LogVerbose("PersonifyUser: User name {0} retrieved for customer token {1}.", this._masterCustomerId, this._decryptedCustomerToken);
					}
				}

				this.LogVerbose("PersonifyUser: Retrieving customer information for user {0}...", this._masterCustomerId);
				CustomerData customer;
				using (PersonifyUniversalService service = PersonifyUniversalService.NewPersonifyUniversalService()) {
					customer = service.GetCustomerByUserName(this._masterCustomerId);
					this.LogVerbose("PersonifyUser: Customer information was {0}found for user {1}.", customer != null ? String.Empty : "not ", this._masterCustomerId);
					//Constraint constraint = new Constraint(CustomerData.UserNameKey, ConstraintOperator.Equals, this._userName);
					//customer = service.GetCustomers(constraint).FirstOrDefault();
				}
				this.LoadCustomerData(customer);
			}
		}
		/// <summary>
		/// Ensures that this instance knows whether or not it exists.
		/// </summary>
		private void EnsureExistsIsKnown() {
			this.EnsureCustomerData();
			if (!this._exists.HasValue) {
				this._exists = false;
			}
		}
		public static PersonifyUser FromDecryptedCustomerToken(String decryptedCustomerToken) {
			String masterCustomerId;
			using (PersonifySsoService ssoService = PersonifySsoService.NewPersonifySsoService()) {
				masterCustomerId = ssoService.GetCustomerName(decryptedCustomerToken);
			}
			return new PersonifyUser(masterCustomerId, decryptedCustomerToken);
		}
		/// <summary>
		/// Loads the customer data from the object specified.
		/// </summary>
		/// <param name="customer">Provides the customer data.</param>
		private void LoadCustomerData(CustomerData customer) {
			if (customer != null) {
				this._masterCustomerId = customer.UserName;
				this._emailAddress = customer.EmailAddress;
				this._firstName = customer.FirstName;
				this._lastName = customer.LastName;
				this._isCustomerDataLoaded = true;
				this._exists = true;
			}
		}
	}
}
