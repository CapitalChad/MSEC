//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Msec.Diagnostics;
//using Msec.Personify.Services.UniversalWebServiceImpl;
//using System.Data;

//namespace Msec.Personify.Services {
//    /// <summary>
//    /// Provides extension methods for the <see cref="T:PersonifyWebService"/> class.  This class may not be inherited.
//    /// </summary>
//    internal static class PersonifyWebServiceExtensions {
//        // Nested Types
//        /// <summary>
//        /// Represents a method which queries the Personify web service.
//        /// </summary>
//        /// <typeparam name="T">The type of collection to populate.</typeparam>
//        /// <param name="token">The token that identifies the current service session.</param>
//        /// <param name="filters">The collection of data filters to apply to the result set.</param>
//        /// <param name="sorts">The collection of sorts to apply to the result set.</param>
//        /// <param name="collection">The collection to fill with the resut set.</param>
//        /// <returns>The result of the service method operation.</returns>
//        private delegate Result QueryServiceMethod<T>(String token, DataFilter[] filters, SortFilter[] sorts, ref T collection);

//        // Methods
//        /// <summary>
//        /// Calls a service method.
//        /// </summary>
//        /// <typeparam name="T">The type of Object to return.</typeparam>
//        /// <param name="service">The Personify web service used to make the service call.</param>
//        /// <param name="token">The token that identifies the current service session.</param>
//        /// <param name="credentials">The credentials supplied to the web service which identifies the caller.</param>
//        /// <param name="serviceMethod">The service method to invoke.</param>
//        /// <param name="filters">The collection of data filters to apply to the result set.</param>
//        /// <returns>The result of the service call.</returns>
//        private static T CallServiceMethod<T>(this PersonifyWebService service, ref String token, UniversalServiceCredentialContainer credentials, PersonifyWebServiceExtensions.QueryServiceMethod<T> serviceMethod, DataFilter[] filters)
//            where T : new() {
//            T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			
//            Result instance = serviceMethod(token, filters, null, ref result);
//            if (instance.IsLoggedOut()) {
//                Logger.LogInformation(service, "The token expired.  Retrieving another token.", new Object[0]);
//                token = service.GetConnectionToken(credentials);
//                instance = serviceMethod(token, filters, null, ref result);
//            }
//            instance.ThrowIfUnsuccessful();
			
//            return result;
//        }
//        /// <summary>
//        /// Returns the application codes in the system.
//        /// </summary>
//        /// <param name="service">The Personify web service used to make the service call.</param>
//        /// <param name="token">The token that identifies the current service session.</param>
//        /// <param name="credentials">The credentials supplied to the web service which identifies the caller.</param>
//        /// <param name="filter">The data filter to apply.</param>
//        /// <returns>The collection of application codes in the system.</returns>
//        public static IEnumerable<String> GetApplicationCodes(this PersonifyWebService service, ref String token, UniversalServiceCredentialContainer credentials, DataFilter filter) {
//            if (service == null) throw new ArgumentNullException("service");
//            if (token == null) throw new ArgumentNullException("token");
//            if (credentials == null) throw new ArgumentNullException("credentials");

//            PersonifyWebServiceExtensions.QueryServiceMethod<ApplicationCodes> serviceMethod = new PersonifyWebServiceExtensions.QueryServiceMethod<ApplicationCodes>(service.APPL_GetApplicationCodes);
//            ApplicationCodes applicationCodes = service.CallServiceMethod(ref token, credentials, serviceMethod, new DataFilter[] { filter });
//            Logger.LogVerbose(
//                service,
//                "APPL_GetApplicationCodes(token, {0} {1} {2}, (NULL), ref new ApplicationCodes()) returned ApplicationCodes count of {3}.",
//                new Object[] {
//                filter.PropertyName,
//                filter.FilterOperator,
//                filter.PropertyValue,
//                (applicationCodes.Item != null) ? (applicationCodes.Item.Length + " records") : "(NULL)" });

//            IEnumerable<String> result = applicationCodes.Item == null
//                ? new String[0]
//                : applicationCodes.Item.Select(applicationCode => applicationCode.Code);
//            return result;
//        }
//        /// <summary>
//        /// Returns the committee members from the system.
//        /// </summary>
//        /// <param name="service">The Personify web service used to make the service call.</param>
//        /// <param name="token">The token that identifies the current service session.</param>
//        /// <param name="credentials">The credentials supplied to the web service which identifies the caller.</param>
//        /// <param name="filters">The collection of data filters to apply to the result set.</param>
//        /// <returns>The collection of committee members in the database.</returns>
//        public static IEnumerable<CommitteeMember> GetCommitteeMembers(this PersonifyWebService service, ref String token, UniversalServiceCredentialContainer credentials, DataFilter[] filters)
//        {
//            if (service == null)
//                throw new ArgumentNullException("service");
//            if (token == null)
//                throw new ArgumentNullException("token");
//            if (credentials == null)
//                throw new ArgumentNullException("credentials");

//            PersonifyWebServiceExtensions.QueryServiceMethod<CommitteeMembers> serviceMethod = new PersonifyWebServiceExtensions.QueryServiceMethod<CommitteeMembers>(service.COMM_GetCommitteeMember);
//            CommitteeMembers committeeMembers = service.CallServiceMethod(ref token, credentials, serviceMethod, filters);
//            String message = "COMM_GetCommitteeMember(token, ({0}), (NULL), ref new CommitteeMembers()) returned CommitteMembers count of {1}.";
//            Object[] args = new Object[] {
//                filters
//                    .Select(filter => filter.PropertyName + " " + filter.FilterOperator.ToString() + " " + filter.PropertyValue)
//                    .ToArray()
//                    .Join(", "),
//                committeeMembers.Item != null
//                    ? committeeMembers.Item.Length + " records"
//                    : "(NULL)" };
//            Logger.LogVerbose(service, message, args);
			
//            IEnumerable<CommitteeMember> result = committeeMembers.Item == null
//                ? new CommitteeMember[0]
//                : committeeMembers.Item;
//            return result;
//        }
//        /// <summary>
//        /// Returns the customer address details from the system.
//        /// </summary>
//        /// <param name="service">The Personify web service used to make the service call.</param>
//        /// <param name="token">The token that identifies the current service session.</param>
//        /// <param name="credentials">The credentials supplied to the web service which identifies the caller.</param>
//        /// <param name="filters">The collection of data filters to apply to the result set.</param>
//        /// <returns>The collection of customer address details from the system.</returns>
//        public static IEnumerable<CustomerAddressDetail> GetCustomerAddressDetails(this PersonifyWebService service, ref String token, UniversalServiceCredentialContainer credentials, DataFilter[] filters)
//        {
//            if (service == null) throw new ArgumentNullException("service");
//            if (token == null) throw new ArgumentNullException("token");
//            if (credentials == null) throw new ArgumentNullException("credentials");

//            PersonifyWebServiceExtensions.QueryServiceMethod<CustomerAddressDetails> serviceMethod = new PersonifyWebServiceExtensions.QueryServiceMethod<CustomerAddressDetails>(service.CUST_GetCustomerAddressDetails);
//            CustomerAddressDetails customerAddressDetails = service.CallServiceMethod(ref token, credentials, serviceMethod, filters);
//            String message = "CUST_GetCustomerAddressDetails(token, ({0}), (NULL), ref new CustomerAddressDetails()) returned CustomerAddressDetails count of {1}.";
//            Object[] args = new Object[] {
//                filters
//                    .Select(filter => filter.PropertyName + " " + filter.FilterOperator.ToString() + " " + filter.PropertyValue)
//                    .ToArray()
//                    .Join(", "),
//                customerAddressDetails.Item != null
//                    ? customerAddressDetails.Item.Length + " records"
//                    : "(NULL)" };
//            Logger.LogVerbose(service, message, args);

//            IEnumerable<CustomerAddressDetail> result = customerAddressDetails.Item == null
//                ? new CustomerAddressDetail[0]
//                : customerAddressDetails.Item;
//            return result;
//        }
//        /// <summary>
//        /// Returns a collection of customer addresses from the database.
//        /// </summary>
//        /// <param name="service">The Personify web service used to make the service call.</param>
//        /// <param name="token">The token that identifies the current service session.</param>
//        /// <param name="credentials">The credentials supplied to the web service which identifies the caller.</param>
//        /// <param name="filter">The data filter to apply.</param>
//        /// <returns>The collection of customer addresses from the system.</returns>
//        public static IEnumerable<CustomerAddress> GetCustomerAddresses(this PersonifyWebService service, ref String token, UniversalServiceCredentialContainer credentials, DataFilter filter)
//        {
//            if (service == null) throw new ArgumentNullException("service");
//            if (token == null) throw new ArgumentNullException("token");
//            if (credentials == null) throw new ArgumentNullException("credentials");

//            PersonifyWebServiceExtensions.QueryServiceMethod<CustomerAddresses> serviceMethod = new PersonifyWebServiceExtensions.QueryServiceMethod<CustomerAddresses>(service.CUST_GetCustomerAddresses);
//            CustomerAddresses customerAddresses = service.CallServiceMethod(ref token, credentials, serviceMethod, new DataFilter[] { filter });
//            Logger.LogVerbose(
//                service,
//                "CUST_GetCustomerAddresses(token, {0} {1} {2}, (NULL), ref new CustomerAddresses()) returned CustomerAddresses count of {3}.",
//                new Object[] {
//                filter.PropertyName,
//                filter.FilterOperator,
//                filter.PropertyValue,
//                (customerAddresses.Item != null) ? (customerAddresses.Item.Length + " records") : "(NULL)" });

//            IEnumerable<CustomerAddress> result = customerAddresses.Item == null
//                ? new CustomerAddress[0]
//                : customerAddresses.Item;
//            return result;
//        }
//        /// <summary>
//        /// Returns a collection of customers from the system.
//        /// </summary>
//        /// <param name="service">The Personify web service used to make the service call.</param>
//        /// <param name="token">The token that identifies the current service session.</param>
//        /// <param name="credentials">The credentials supplied to the web service which identifies the caller.</param>
//        /// <param name="filters">The collection of data filters to apply to the result set.</param>
//        /// <returns>The collection of customers from the system.</returns>
//        public static IEnumerable<Customer> GetCustomers(this PersonifyWebService service, ref String token, UniversalServiceCredentialContainer credentials, DataFilter[] filters)
//        {
//            if (service == null) throw new ArgumentNullException("service");
//            if (token == null) throw new ArgumentNullException("token");
//            if (credentials == null) throw new ArgumentNullException("credentials");

//            PersonifyWebServiceExtensions.QueryServiceMethod<Customers> serviceMethod = new PersonifyWebServiceExtensions.QueryServiceMethod<Customers>(service.CUST_GetCustomers);
//            Customers customers = service.CallServiceMethod(ref token, credentials, serviceMethod, filters);
//            String message = "CUST_GetCustomers(token, ({0}), (NULL), ref new Customers()) returned Customers count of {1}.";
//            Object[] args = new Object[] {
//                filters
//                    .Select(filter => filter.PropertyName + " " + filter.FilterOperator.ToString() + " " + filter.PropertyValue)
//                    .ToArray()
//                    .Join(", "),
//                customers.Item != null
//                    ? customers.Item.Length + " records"
//                    : "(NULL)" };
//            Logger.LogVerbose(service, message, args);

//            IEnumerable<Customer> result = customers.Item == null
//                ? new Customer[0]
//                : customers.Item;
//            return result;
//        }
//        /// <summary>
//        /// Returns a connection token used to identify a connection to the web service.
//        /// </summary>
//        /// <param name="service">The Personify web service used to make the service call.</param>
//        /// <param name="credentials">The credentials supplied to the web service which identifies the caller.</param>
//        /// <returns>The connection token to use.</returns>
//        public static String GetConnectionToken(this PersonifyWebService service, UniversalServiceCredentialContainer credentials)
//        {
//            if (credentials == null) throw new ArgumentNullException("credentials");

//            Logger.LogInformation(service, "Retrieving a token from the Universal service...");

//            Result_Message resultMessage;
//            try
//            {
//                resultMessage = service.CONN_Connect(credentials.UserName, credentials.Password, credentials.OrganizationId, credentials.OrganizationUnitId);
//            }
//            catch (Exception ex)
//            {
//                if (!ExceptionExtensions.CanBeHandledSafely(ex)) {
//                    throw;
//                }
//                throw new ServerServiceException("The login call to the remote web server failed.  See the inner exception for more details.", ex);
//            }
			
//            if (!resultMessage.Success)
//            {
//                throw new ServerServiceException("The login call was unsuccessful for the following reason: {0}.".FormatInvariant(resultMessage.Message));
//            }

//            String token = resultMessage.Token;
//            if (token == null) {
//                throw new ServerServiceException("The login call returned a null session token.");
//            }

//            Logger.LogVerbose(service, "A token was issued from the Universal service successfully.");
//            return token;
//        }
//    }
//}
