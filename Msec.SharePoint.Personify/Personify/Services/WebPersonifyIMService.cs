using System;
using System.Diagnostics;
using System.Linq;
using Msec.Diagnostics;
using Msec.Personify.Configuration;
using Msec.Personify.Services.ImsWebServiceImpl;
using System.Net;

namespace Msec.Personify.Services {
	/// <summary>
	/// The web implementation of the <see cref="PersonifyIMService"/> class.
	/// This class may not be inherited.
	/// </summary>
	public sealed class WebPersonifyIMService : PersonifyIMService {
	// Fields
		/// <summary>
		/// The web service proxy to use.
		/// </summary>
		private IMService _service;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:WebPersonifyIMService"/> class.
		/// </summary>
		/// <param name="credentials">The vendor's credentials to use for service calls.</param>
		public WebPersonifyIMService(NetworkCredential credentials)
			: base(credentials) {
			this._service = new IMService() {
				Url = PersonifyConfiguration.Instance.IMServiceUrl.ToString()
			};
		}

	// Methods
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
		/// Returns the roles for a customer token.
		/// </summary>
		/// <param name="decryptedCustomerToken">Identifies the customer.</param>
		/// <returns>The roles for the customer.</returns>
		protected override String[] GetCustomerRolesCore(String decryptedCustomerToken) {
			Debug.Assert(this._service != null);
			if (decryptedCustomerToken == null)
				throw new ArgumentNullException("decryptedCustomerToken");

			IMSCustomerRoleGetResult result = WebPersonifyIMService.ExecuteServiceCall(
				() => this._service.IMSCustomerRoleGet(this.Credentials.UserName, this.Credentials.Password, decryptedCustomerToken),
				callResult => callResult.Errors);
			this.LogVerbose("IMSCustomerRoleGet(userName, password, {0}) returned roles {1}.", decryptedCustomerToken, result.CustomerRoles != null ? String.Join(", ", result.CustomerRoles.Select(role => role.Value).ToArray()) : "(NULL)");
			if (result.CustomerRoles == null)
				return new String[0];
			return result.CustomerRoles
				.Select(role => role.Value)
				.ToArray();
		}
		/// <summary>
		/// Returns the roles for a customer.
		/// </summary>
		/// <param name="masterCustomerId">Identifies the customer.</param>
		/// <returns>The roles for the customer.</returns>
		protected override String[] GetCustomerRolesByMasterCustomerIdCore(String masterCustomerId) {
			Debug.Assert(this._service != null);
			if (masterCustomerId == null)
				throw new ArgumentNullException("masterCustomerId");

			IMSCustomerRoleGetResult result = WebPersonifyIMService.ExecuteServiceCall(
				() => this._service.IMSCustomerRoleGetByTimssCustomerId(this.Credentials.UserName, this.Credentials.Password, masterCustomerId + "|0"),
				callResult => callResult.Errors);
			this.LogVerbose("IMSCustomerRoleGetByTimssCustomerId(userName, password, {0}) return roles {1}.", masterCustomerId, result.CustomerRoles != null ? String.Join(", ", result.CustomerRoles.Select(role => role.Value).ToArray()) : "(NULL)");
			if (result.CustomerRoles == null)
				return new String[0];
			return result.CustomerRoles
				.Select(role => role.Value)
				.ToArray();
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
