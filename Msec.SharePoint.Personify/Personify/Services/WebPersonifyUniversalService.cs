using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Msec.Linq;
using Msec.Personify.Services.PersonifyUniversalServiceImpl;

using NetworkCredential = System.Net.NetworkCredential;
using PersonifyConfiguration = Msec.Personify.Configuration.PersonifyConfiguration;

namespace Msec.Personify.Services {
	/// <summary>
	/// The web implementation of the <see cref="T:PersonifyUniversalService"/> class.  This class may not be inherited.
	/// </summary>
	public sealed class WebPersonifyUniversalService : PersonifyUniversalService {
	// Constants
		/// <summary>
		/// The maximum timeout value for a web service call = 99,999,999.
		/// </summary>
		private const Int32 MaximumTimeout = 99999999;
		/// <summary>
		/// The name of the 'position' code = "POSITION".
		/// </summary>
		private const String PositionCodeName = "POSITION";
		
	// Fields
		private PersonifyEntitiesMSEC _serviceClient;
		private readonly Uri _serviceUrl;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:WebPersonifyUniversalService"/> class using the values provided in the configuration file.
		/// </summary>
		/// <exception cref="Msec.ServiceException">An error occurs while initializing the connection to the service.</exception>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "All resources are disposed if necessary.")]
		public WebPersonifyUniversalService()
			: base() {
			var settings = PersonifyConfiguration.Instance
				.Project(configuration => new {
					ServiceUrl = configuration.UniversalServiceUrl,
					Credentials = configuration.UniversalServiceCredentials });

			this._serviceClient = new PersonifyEntitiesMSEC(settings.ServiceUrl) { Timeout = MaximumTimeout };
			this._serviceClient.Credentials = settings.Credentials;
			this._serviceUrl = settings.ServiceUrl;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:WebPersonifyUniversalService"/> class.
		/// </summary>
		/// <param name="serviceUrl">The URL of the service to use.</param>
		/// <param name="credentials">The credentials to use when logging into the service.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="serviceUrl"/> is a null reference.
		/// -or- <paramref name="credentials"/> is a null reference.</exception>
		/// <exception cref="Msec.ServiceException">An error occurs while initializing the connection to the service.</exception>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "All resources are disposed if necessary.")]
		public WebPersonifyUniversalService(Uri serviceUrl, NetworkCredential credentials)
			: base() {
			if (serviceUrl == null)
				throw new ArgumentNullException("serviceUrl");
			if (credentials == null)
				throw new ArgumentNullException("credentials");
			if (!serviceUrl.IsAbsoluteUri)
				throw new ArgumentException("The service URL must be an absolute URI.", "serviceUrl");

			this._serviceClient = new PersonifyEntitiesMSEC(serviceUrl) { Timeout = MaximumTimeout };
			this._serviceClient.Credentials = credentials;
			this._serviceUrl = serviceUrl;
		}

	// Properties
		/// <summary>
		/// Gets the URL for the service.
		/// </summary>
		public Uri ServiceUrl {
			get { return this._serviceUrl; }
		}

	// Methods
		protected override IEnumerable<CustomerAddressData> GetCustomerAddressesForUserCore(String userName) {
			CusAddressInfo[] addresses = this._serviceClient.CusAddressInfos
				.Where(record => record.MasterCustomerId == userName)
				.Where(record => record.AddressStatusCode == "GOOD")
				.ToArray();
			return addresses
				.Select(address => new CustomerAddressData(address))
				.ToArray();
		}
		protected override CustomerData GetCustomerByUserNameCore(String userName) {
			CustomerInfo[] customerInfos = this._serviceClient.CustomerInfos
				.Where(record => record.MasterCustomerId == userName)
				.ToArray();
			if (customerInfos.Length == 0)
				return null;
			return new CustomerData(customerInfos[0]);
		}
		protected override IEnumerable<CustomerData> GetCustomersWhereEmailAddressEqualsCore(String emailAddress) {
			CustomerInfo[] customerInfos = this._serviceClient.CustomerInfos
				.Where(record => record.PrimaryEmailAddress == emailAddress)
				.ToArray();
			IEnumerable<CustomerData> customerData = customerInfos
				.Select(customerInfo => new CustomerData(customerInfo));
			return customerData.ToArray();
		}
		protected override IEnumerable<CustomerData> GetCustomersWhereEmailAddressStartsWithCore(String emailAddress) {
			CustomerInfo[] customerInfos = this._serviceClient.CustomerInfos
				.Where(record => record.PrimaryEmailAddress.StartsWith(emailAddress))
				.ToArray();
			IEnumerable<CustomerData> customerData = customerInfos
				.Select(customerInfo => new CustomerData(customerInfo));
			return customerData.ToArray();
		}
		protected override IEnumerable<CustomerData> GetCustomersWhereFullNameStartsWithCore(String name) {
			String[] nameParts = name.Split(' ');
			if (nameParts.Length > 2)
				return new CustomerData[0];

			CustomerInfo[] customerInfos;
			if (nameParts.Length == 2) {
				customerInfos = this._serviceClient.CustomerInfos
					.Where(record => record.FirstName == nameParts[0] && record.LastName.StartsWith(nameParts[1]))
					.ToArray();
			}
			else {
				customerInfos = this._serviceClient.CustomerInfos
					.Where(record => record.FirstName.StartsWith(name))
					.ToArray();
			}

			IEnumerable<CustomerData> customerData = customerInfos
				.Select(customerInfo => new CustomerData(customerInfo));
			return customerData.ToArray();
		}
		protected override IEnumerable<CustomerData> GetCustomersWhereUserNameEqualsCore(String userName) {
			CustomerInfo[] customerInfos = this._serviceClient.CustomerInfos
				.Where(record => record.MasterCustomerId == userName)
				.ToArray();
			IEnumerable<CustomerData> customerData = customerInfos
				.Select(customerInfo => new CustomerData(customerInfo));
			return customerData.ToArray();
		}
		protected override IEnumerable<CustomerData> GetCustomersWhereUserNameStartsWithCore(String userName) {
			CustomerInfo[] customerInfos = this._serviceClient.CustomerInfos
				.Where(record => record.MasterCustomerId.StartsWith(userName))
				.ToArray();
			IEnumerable<CustomerData> customerData = customerInfos
				.Select(customerInfo => new CustomerData(customerInfo));
			return customerData.ToArray();
		}
		/// <summary>
		/// Returns the enumerable collection of valid position codes.
		/// </summary>
		/// <returns>The enumerable collection of valid position codes.</returns>
		protected override IEnumerable<String> GetPositionCodesCore() {
			return this._serviceClient.ApplicationCodes
				.Where(record => record.ActiveFlag != false)
				.Where(record => record.Type == PositionCodeName)
				.Select(record => record.Code)
				.ToArray();
		}
	}
}
