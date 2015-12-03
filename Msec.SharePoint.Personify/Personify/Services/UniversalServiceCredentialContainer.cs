using System;
using System.ComponentModel;
using System.Net;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents the credentials used for the Universal service for Personify.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	[ImmutableObject(true)]
	internal sealed class UniversalServiceCredentialContainer : Object {
	// Fields
		/// <summary>
		/// The identifier of the organization.  This field is read-only.
		/// </summary>
		private readonly String _organizationId;
		/// <summary>
		/// The identifier of the organization unit.  This field is read-only.
		/// </summary>
		private readonly String _organizationUnitId;
		/// <summary>
		/// The password for the service.  This field is read-only.
		/// </summary>
		private readonly String _password;
		/// <summary>
		/// The user name to use when connecting to the service.  This field is read-only.
		/// </summary>
		private readonly String _userName;
		
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UniversalServiceCredentialContainer"/> class.
		/// </summary>
		/// <param name="credentials">The network credentials specifying the user name and password.</param>
		/// <param name="organizationId">The identifier of the organization.</param>
		/// <param name="organizationUnitId">The identifier of the organization unit.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="credentials"/> is a null reference.
		/// -or- <paramref name="organizationId"/> is a null reference.
		/// -or- <paramref name="organizationUnitId"/> is a null reference.</exception>
		public UniversalServiceCredentialContainer(NetworkCredential credentials, String organizationId, String organizationUnitId)
			: base() {
			if (credentials == null) throw new ArgumentNullException("credentials");
			if (organizationId == null) throw new ArgumentNullException("organizationId");
			if (organizationUnitId == null) throw new ArgumentNullException("organizationUnitId");

			this._userName = credentials.UserName;
			this._password = credentials.Password;
			this._organizationId = organizationId;
			this._organizationUnitId = organizationUnitId;
		}
		
	// Properties
		/// <summary>
		/// Gets the identifier of the organization to use when connecting to the service.
		/// </summary>
		public String OrganizationId {
			get { return this._organizationId; }
		}
		/// <summary>
		/// Gets the identifier of the organization unit to use when connecting to the service.
		/// </summary>
		public String OrganizationUnitId {
			get { return this._organizationUnitId; }
		}
		/// <summary>
		/// Gets the password to use when connecting to the service.
		/// </summary>
		public String Password {
			get { return this._password; }
		}
		/// <summary>
		/// Gets the user name to use when connecting to the service.
		/// </summary>
		public String UserName {
			get { return this._userName; }
		}
	}
}
