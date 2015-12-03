using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

// SSO Service types.
using SsoService = Msec.WebServiceInvoker.Services.PersonifySsoServiceImpl.service;

// UniversalService types.
using ApplicationCodes = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.ApplicationCodes;
using CommitteeMembers = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.CommitteeMembers;
using CustomerAddressDetails = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.CustomerAddressDetails;
using CustomerAddresses = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.CustomerAddresses;
using Customers = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.Customers;
using DataFilter = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.DataFilter;
using DataFilterOperator = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.QueryOperatorEnum;
using SortFilter = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.SortFilter;
using UniversalService = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.PersonifyWebService;
using UniversalServiceResult = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.Result;
using UniversalServiceConnectResult = Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.Result_Message;

using IMSCustomerRoleGetResult = Msec.WebServiceInvoker.Services.ImsWebServiceImpl.IMSCustomerRoleGetResult;

// PersonifyDataService types.
using PersonifyEntitiesMSEC = Msec.WebServiceInvoker.Services.PersonifyDataServiceImpl.PersonifyEntitiesMSEC;
using CustomerInfo = Msec.WebServiceInvoker.Services.PersonifyDataServiceImpl.CustomerInfo;
using CommitteeMember = Msec.WebServiceInvoker.Services.PersonifyDataServiceImpl.CommitteeMember;
using CusAddressInfo = Msec.WebServiceInvoker.Services.PersonifyDataServiceImpl.CusAddressInfo;
using ApplicationCode = Msec.WebServiceInvoker.Services.PersonifyDataServiceImpl.ApplicationCode;

namespace Msec.WebServiceInvoker {
	class Program {
		private static DateTimeOffset _executionTime = DateTimeOffset.Now;

		static void Main(String[] args) {
			List<String> failedServices = new List<String>();

			failedServices.AddRange(Program.TestSsoService());
			failedServices.AddRange(Program.TestImService());
			failedServices.AddRange(Program.TestDataService());

			if (failedServices.Count > 0) {
				Console.WriteLine("One or more services failed.");
				foreach (var failedService in failedServices)
					Console.WriteLine("\t" + failedService);
			}
			else {
				Console.WriteLine("All services performed as expected.");
			}

			Console.WriteLine();
			Console.WriteLine("Press [Enter] to exit.");
			Console.Read();
		}

		private static void LogError(String message, params Object[] args) {
			File.AppendAllText(String.Format(@".\Results-{0}.txt", Program._executionTime.ToString("yyyyMMdd-HHmmss")), String.Format(message, args) + Environment.NewLine + Environment.NewLine);
		}

		private static IEnumerable<String> TestDataService() {
			const String userName = "webuser";
			const String password = "webuser2013";
			Uri serviceRoot = new Uri("http://dev-my.msec.org/DataServices/PersonifyDataMSEC.svc");

			List<String> errors = new List<String>();

			try {
				PersonifyEntitiesMSEC service = new Services.PersonifyDataServiceImpl.PersonifyEntitiesMSEC(serviceRoot);
				service.Credentials = new NetworkCredential(userName, password);

				// MembershipProvider.FindUsersByEmail
				try {
					CustomerInfo[] customerInfos = service.CustomerInfos
						.Where(customer => customer.PrimaryEmailAddress.StartsWith("Ba"))
						.ToArray();
				}
				catch (Exception ex) {
					errors.Add("DataService.CustomerInfos threw an exception.");
					Program.LogError("DataService.CustomerInfos - Exception: {0}", ex);
				}

				// MembershipProvider.FindUsersByUserName
				try {
					CustomerInfo[] customerInfos = service.CustomerInfos
						.Where(customer => customer.MasterCustomerId.StartsWith("00000000"))
						.ToArray();
				}
				catch (Exception ex) {
					errors.Add("DataService.CustomerInfos threw an exception.");
					Program.LogError("DataService.CustomerInfos - Exception: {0}", ex);
				}

				String committeeName = null;
				try {
					CustomerInfo[] customerInfos = service.CustomerInfos
						.Where(customer => customer.PrimaryEmailAddress.StartsWith("Ba"))
						.ToArray();
					//String[] recordTypes = customers
					//    .Select(customer => customer.RecordType)
					//    .Distinct()
					//    .ToArray();
					//String[] customerClassCodes = customers
					//    .Select(customer => customer.CustomerClassCode)
					//    .Distinct()
					//    .ToArray();
					//CustomerInfo[] customerInfos = service.CustomerInfos
					//    .Where(customer => customer.RecordType == "I")
					//        //&& customer.CustomerClassCode == "ACTIVE")
					//    .ToArray();
					committeeName = customerInfos[0].MasterCustomerId;
				}
				catch (Exception ex) {
					errors.Add("DataService.CustomerInfos threw an exception.");
					Program.LogError("DataService.CustomerInfos - Exception: {0}", ex);
				}

				String memberName = null;
				if (committeeName == null) {
					errors.Add("DataService.CommitteeMembers could not be tested because CustomerInfos failed.");
					Program.LogError("DataService.CommitteeMembers could not be tested because CustomerInfos failed.");
				}
				else {
					try {
						CommitteeMember[] committeeMembers = service.CommitteeMembers
							.Where(committeeMember => committeeMember.CommitteeMasterCustomer == committeeName)
							.ToArray();
						memberName = committeeMembers[0].MemberMasterCustomer;
					}
					catch (Exception ex) {
						errors.Add("DataService.CommitteeMembers threw an exception.");
						Program.LogError("DataService.CommitteeMembers - Exception: {0}", ex);
					}
				}

				if (memberName == null) {
					errors.Add("DataService.CusAddressInfos could not be tested because CommitteeMembers failed.");
					Program.LogError("DataService.CusAddressInfos could not be tested because CommitteeMembers failed.");
				}
				else {
					try {
						CusAddressInfo[] addresses = service.CusAddressInfos
							.Where(address => address.MasterCustomerId == memberName
								&& address.AddressStatusCode == "GOOD")
							.ToArray();
					}
					catch (Exception ex) {
						errors.Add("DataService.CusAddressInfos threw an exception.");
						Program.LogError("DataService.CusAddressInfos - Exception: {0}", ex);
					}
				}

				try {
					CustomerInfo[] customers = service.CustomerInfos
						.Where(customer => customer.RecordType == "T"
							&& customer.CustomerClassCode == "A")
						.ToArray();
				}
				catch (Exception ex) {
					errors.Add("DataService.Customer threw an exception.");
					Program.LogError("DataService.Customers - Exception: {0}", ex);
				}

				try {
					ApplicationCode[] applicationCodes = service.ApplicationCodes
						.Where(applicationCode => applicationCode.ActiveFlag == true)
						.ToArray();
				}
				catch (Exception ex) {
					errors.Add("DataService.ApplicationCodes threw an exception.");
					Program.LogError("DataService.ApplicationCodes - Exception: {0}", ex);
				}
			}
			catch (Exception ex) {
				errors.Add("DataService could not initialize a proxy.");
				Program.LogError("DataService - Could not initialize a proxy: {0}", ex);
			}

			return errors;
		}

		private static IEnumerable<String> TestImService() {
			const String userName = "TIMSS";
			const String password = "10BB61615AF73164F1F9B9AC9655439C";
			//const String block = "3E918C58FB082D1B168F0D2B38830F38";
			//const String url = "http://localhost/";

			List<String> errors = new List<String>();

			using (Services.ImsWebServiceImpl.IMService service = new Services.ImsWebServiceImpl.IMService()) {
				//var result = service.IMSVendorRolesGet(userName, password);
				//var result3 = service.IMSVendorWebRolesGet(userName, password);

				String customerUserName = "mmiller@msec.org";
				String masterCustomerId = "0000212106";
				String customerToken;
				String timssCustomerIdentifier;
				using (SsoService ssoService = new SsoService()) {
					customerToken = ssoService.CustomerTokenDecrypt(
						userName,
						password,
						"3E918C58FB082D1B168F0D2B38830F38",
						"b3e2039c3a04fa63dcb9c05a5f2586c1080ffa568836e6f05ac748320710ad7746862be2f4774fcf94e3256c7cad57623828abd4643576ead459d59f6d8426f6")
							.CustomerToken;
					var timssCustomerIdentifierResult = ssoService.TIMSSCustomerIdentifierGet(userName, password, customerToken);
					timssCustomerIdentifier = timssCustomerIdentifierResult.CustomerIdentifier;
				}

				//var result1 = service.IMSCustomerRoleGet(userName, password, customerUserName);
				//var result2 = service.IMSCustomerRoleGet(userName, password, masterCustomerId);
				var result3 = service.IMSCustomerRoleGet(userName, password, customerToken);
				//var result4 = service.IMSCustomerRoleGet(userName, password, timssCustomerIdentifier);

				//var result5 = service.IMSCustomerRoleGetByTimssCustomerId(userName, password, customerUserName);
				//var result6 = service.IMSCustomerRoleGetByTimssCustomerId(userName, password, masterCustomerId);
				//var result7 = service.IMSCustomerRoleGetByTimssCustomerId(userName, password, customerToken);
				var result8 = service.IMSCustomerRoleGetByTimssCustomerId(userName, password, timssCustomerIdentifier);

				//var result9 = service.IMSCustomerWebRoleGet(userName, password, customerUserName);
				//var result10 = service.IMSCustomerWebRoleGet(userName, password, masterCustomerId);
				var result11 = service.IMSCustomerWebRoleGet(userName, password, customerToken);
				//var result12 = service.IMSCustomerWebRoleGet(userName, password, timssCustomerIdentifier);

				//IMSCustomerRoleGetResult result2;
				//Int32 id = 15833;
				//do {
				//    result2 = service.IMSCustomerRoleGetByTimssCustomerId(userName, password, id.ToString("0000000000"));
				//    id++;
				//}
				//while (result2.CustomerRoles == null || result2.CustomerRoles.Length == 0);
			}

			return errors;
		}

		private static IEnumerable<String> TestSsoService() {
			const String userName = "TIMSS";
			const String password = "10BB61615AF73164F1F9B9AC9655439C";
			const String block = "3E918C58FB082D1B168F0D2B38830F38";
			const String url = "http://localhost/";

			List<String> errors = new List<String>();

			try {
				using (SsoService service = new SsoService()) {
					var myResult = service.SSOCustomerGetByEmail(userName, password, "mseverns@msec.org");
					// VendorTokenEncrypt()
					{
						try {
							var result = service.VendorTokenEncrypt(userName, password, block, url);
							if (result.Errors != null && result.Errors.Length > 0) {
								errors.Add("SsoService.VendorTokenEncrypt returned one or more errors.");
								Program.LogError("SsoService.VendorTokenEncrypt - Service call returned one or more errors ({0})", result.Errors.Join(" | "));
							}
							if (result.VendorToken == null) {
								errors.Add("SsoService.VendorTokenEncrypt returned a NULL vendor token.");
								Program.LogError("SsoService.VendorTokenEncrypt - Service call returned a NULL vendor token.");
							}
						}
						catch (Exception ex) {
							errors.Add("SsoService.VendorTokenEncrypt threw an exception.");
							Program.LogError("SsoService.VendorTokenEncrypt - Exception: {0}", ex);
						}
					}

					String customerToken = "This doesn't matter.";
					// SSOCustomerTokenIsValid()
					try {
						var result = service.SSOCustomerTokenIsValid(userName, password, customerToken);
					}
					catch (Exception ex) {
						errors.Add("SsoService.SSOCustomerTokenIsValid threw an exception.");
						Program.LogError("SsoService.SSOCustomerTokenIsValid - Exception: {0}", ex);
					}

					// CustomerTokenDecrypt()
					try {
						var result = service.CustomerTokenDecrypt(userName, password, block, customerToken);
					}
					catch (Exception ex) {
						errors.Add("SsoService.CustomerTokenDecrypt threw an exception.");
						Program.LogError("SsoService.CustomerTokenDecrypt - Exception: {0}", ex);
					}

					// TIMSSCustomerIdentifierGet()
					try {
						var result = service.TIMSSCustomerIdentifierGet(userName, password, customerToken);
					}
					catch (Exception ex) {
						errors.Add("SsoService.TIMSSCustomerIdentifierGet threw an exception.");
						Program.LogError("SsoService.TIMSSCustomerIdentifierGet - Exception: {0}", ex);
					}
				}
			}
			catch (Exception ex) {
				errors.Add("SsoService could not initialize a proxy.");
				Program.LogError("SsoService - Could not initialize a proxy: {0}", ex);
			}

			return errors;
		}

		private static IEnumerable<String> TestUniversalService() {
			const String userName = "badges";
			const String password = "UYH81%vj";
			const String organizationId = "MSEC";
			const String unitId = "MSEC";

			List<String> errors = new List<String>();

			try {
				using (UniversalService service = new UniversalService()) {
					String token;
					try {
						UniversalServiceConnectResult connectResult = service.CONN_Connect(userName, password, organizationId, unitId);
						if (!connectResult.Success) {
							errors.Add("UniversalService.CONN_Connect returned a value indicating a failure.");
							Program.LogError("UniversalService.CONN_Connect - Return value was a failure: {0}", connectResult.Message);
							return errors;
						}
						token = connectResult.Token;
					}
					catch (Exception ex) {
						errors.Add("UniversalService.CONN_Connect threw an exception.");
						Program.LogError("UniversalService.CONN_Connect - Exception: {0}.\r\nOTHER SERVICES COULD NOT BE TESTED DUE TO THIS FAILURE.", ex);
						return errors;
					}

					String committeeName = null;
					try {
						Customers customers = new Customers();
						// From UpaSyncJob.cs line 226.
						DataFilter[] filters = new DataFilter[] {
							new DataFilter { PropertyName = "RecordType", FilterOperator = DataFilterOperator.Equals, PropertyValue = "T" },
							new DataFilter { PropertyName = "CustomerStatusCode", FilterOperator = DataFilterOperator.Equals, PropertyValue = "ACTIVE" }
						};
						SortFilter[] sorts = null;
						UniversalServiceResult result = service.CUST_GetCustomers(token, filters, sorts, ref customers);
						if (!result.IsSuccessful) {
							errors.Add("UniversalService.CUST_GetCustomers returned a value indicating a failure.");
							Program.LogError("UniversalService.CUST_GetCustomers - Service failed: {0} {1} ({2}).  {3}\r\n{4}: {5}", result.ErrorCode, result.ErrorCause, result.Version, result.ErrorSolution, result.ErrorSource, result.ErrorStackTrace);
						}
						else {
							committeeName = customers.Item[0].MasterCustomerId;
						}
					}
					catch (Exception ex) {
						errors.Add("UniversalService.CUST_GetCustomers threw an exception.");
						Program.LogError("UniversalService.CUST_GetCustomers - Exception: {0}", ex);
					}

					String memberName = null;
					if (committeeName == null) {
						errors.Add("UniversalService.COMM_GetCommitteeMember could not be tested because CUST_GetCustomers failed.");
						Program.LogError("UniversalService.COMM_GetCommitteeMember could not be tested because CUST_GetCustomers failed.");
					}
					else {
                        //try {
                        //    DateTime start = DateTime.Now;
                        //    var properties = typeof(Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.Customer).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        //    List<String> lines = new List<String>();
                        //    lines.Add(String.Join("\t", properties.Select(property => property.Name)));

                        //    Int32 emptyCount = 0;
                        //    const Int32 batchSize = 100;
                        //    Int32 batchIndex = 0;
                        //    const Int32 maxEmptyBatches = 3;
                        //    while (emptyCount < maxEmptyBatches)
                        //    {
                        //        Customers customers = new Customers();
                        //        // From UpaSyncJob.cs line 240.
                        //        Int32 emptyPlaces = (Int32)Math.Log10(batchSize);
                        //        String mask = new String('0', 8 - emptyPlaces);
                        //        DataFilter[] filters = new DataFilter[] {
                        //            new DataFilter { PropertyName = "MasterCustomerId", FilterOperator = DataFilterOperator.StartsWith, PropertyValue = batchIndex.ToString(mask) }
                        //        };
                        //        SortFilter[] sorts = null;

                        //        service.Timeout = 99999999;
                        //        UniversalServiceResult result = service.CUST_GetCustomers(token, filters, sorts, ref customers);
                        //        if (!result.IsSuccessful)
                        //        {
                        //            errors.Add("Failed!");
                        //            Program.LogError("Service failed: {0} {1} ({2}).  {3}\r\n{4}: {5}", result.ErrorCode, result.ErrorCause, result.Version, result.ErrorSolution, result.ErrorSource, result.ErrorStackTrace);
                        //            break;
                        //        }
                        //        else if (customers.Item == null || customers.Item.Length == 0)
                        //        {
                        //            emptyCount++;
                        //        }
                        //        else
                        //        {
                        //            foreach (var customer in customers.Item)
                        //                lines.Add(String.Join("\t", properties.Select(property => {
                        //                    Object value = property.GetValue(customer, null) ?? DBNull.Value;
                        //                    if (value is Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.Code)
                        //                        value = ((Msec.WebServiceInvoker.Services.UniversalWebServiceImpl.Code)value).Value;
                        //                    return value.ToString();
                        //                })));
                        //            emptyCount = 0;
                        //        }

                        //        batchIndex++;
                        //    }

                        //    TimeSpan duration = DateTime.Now - start;
                        //    File.WriteAllLines(@"C:\Temp\AllCustomers.txt", lines.ToArray());
                        //}
                        //catch (Exception ex) {
                        //    errors.Add("Failed!");
                        //    Program.LogError("Exception: {0}", ex);
                        //}
						try {
							CommitteeMembers committeeMembers = new CommitteeMembers();
							// From UpaSyncJob.cs line 240.
							DataFilter[] filters = new DataFilter[] {
								new DataFilter { PropertyName = "CommitteeMasterCustomer", FilterOperator = DataFilterOperator.Equals, PropertyValue = committeeName }
							};
							SortFilter[] sorts = null;

							service.Timeout = 99999999;
							UniversalServiceResult result = service.COMM_GetCommitteeMember(token, filters, sorts, ref committeeMembers);
							if (!result.IsSuccessful) {
								errors.Add("UniversalService.COMM_GetCommitteeMember returned a value indicating a failure.");
								Program.LogError("UniversalService.COMM_GetCommitteeMember - Service failed: {0} {1} ({2}).  {3}\r\n{4}: {5}", result.ErrorCode, result.ErrorCause, result.Version, result.ErrorSolution, result.ErrorSource, result.ErrorStackTrace);
							}
							else {
								memberName = committeeMembers.Item[0].MemberMasterCustomer;
							}
						}
						catch (Exception ex) {
							errors.Add("UniversalService.COMM_GetCommitteeMember threw an exception.");
							Program.LogError("UniversalService.COMM_GetCommitteeMember - Exception: {0}", ex);
						}
					}

					if (memberName == null) {
						errors.Add("UniversalService.CUST_GetCustomerAddressDetails could not be tested because COMM_GetCommitteeMember failed.");
						Program.LogError("UniversalService.CUST_GetCustomerAddressDetails could not be tested because COMM_GetCommitteeMember failed.");
					}
					else {
						String[] addressIds = null;
						try {
							CustomerAddressDetails customerAddressDetails = new CustomerAddressDetails();
							DataFilter[] filters = new DataFilter[] {
								new DataFilter { PropertyName = "MasterCustomerId", FilterOperator = DataFilterOperator.Equals, PropertyValue = memberName },
								new DataFilter { PropertyName = "PrioritySeq", FilterOperator = DataFilterOperator.Equals, PropertyValue = "0" },
								new DataFilter { PropertyName = "AddressStatusCode", FilterOperator = DataFilterOperator.Equals, PropertyValue = "GOOD" }
							};
							SortFilter[] sorts = null;

							service.Timeout = 99999999;
							UniversalServiceResult result = service.CUST_GetCustomerAddressDetails(token, filters, sorts, ref customerAddressDetails);
							if (!result.IsSuccessful) {
								errors.Add("UniversalService.CUST_GetCustomerAddressDetails returned a value indicating a failure.");
								Program.LogError("UniversalService.CUST_GetCustomerAddressDetails - Service failed: {0} {1} ({2}).  {3}\r\n{4}: {5}", result.ErrorCode, result.ErrorCause, result.Version, result.ErrorSolution, result.ErrorSource, result.ErrorStackTrace);
							}
							else {
								addressIds = customerAddressDetails.Item
									.Select(detail => detail.CustomerAddressId)
									.Distinct()
									.Select(id => id.ToString(CultureInfo.InvariantCulture))
									.ToArray();
							}
						}
						catch (Exception ex) {
							errors.Add("UniversalService.CUST_GetCustomerAddressDetails threw an exception.");
							Program.LogError("UniversalService.CUST_GetCustomerAddressDetails - Exception: {0}", ex);
						}

						if (addressIds == null) {
							errors.Add("UniversalService.CUST_GetCustomerAddresses could not be tested because CUST_GetCustomerAddressDetails failed.");
							Program.LogError("UniversalService.CUST_GetCustomerAddresses could not be tested because CUST_GetCustomerAddressDetails failed.");
						}
						else {
							try {
								CustomerAddresses customerAddresses = new CustomerAddresses();
								DataFilter[] filters = new DataFilter[] {
									new DataFilter { PropertyName = "CustomerAddressId", FilterOperator = DataFilterOperator.IsIn, PropertyValue = addressIds.Join(",") }
								};
								SortFilter[] sorts = null;

								service.Timeout = 99999999;
								UniversalServiceResult result = service.CUST_GetCustomerAddresses(token, filters, sorts, ref customerAddresses);
								if (!result.IsSuccessful) {
									errors.Add("UniversalService.CUST_GetCustomerAddresses returned a value indicating a failure.");
									Program.LogError("UniversalService.CUST_GetCustomerAddresses - Service failed: {0} {1} ({2}).  {3}\r\n{4}: {5}", result.ErrorCode, result.ErrorCause, result.Version, result.ErrorSolution, result.ErrorSource, result.ErrorStackTrace);
								}
							}
							catch (Exception ex) {
								errors.Add("UniversalService.CUST_GetCustomerAddresses threw an exception.");
								Program.LogError("UniversalService.CUST_GetCustomerAddresses - Exception: {0}", ex);
							}
						}
					}

					try {
						Customers customers = new Customers();
						DataFilter[] filters = new DataFilter[] {
							new DataFilter { PropertyName = "RecordType", FilterOperator = DataFilterOperator.Equals, PropertyValue = "T" },
							new DataFilter { PropertyName = "CustomerStatusCode", FilterOperator = DataFilterOperator.Equals, PropertyValue = "A" }
						};
						SortFilter[] sorts = null;

						service.Timeout = 99999999;
						UniversalServiceResult result = service.CUST_GetCustomers(token, filters, sorts, ref customers);
						if (!result.IsSuccessful) {
							errors.Add("UniversalService.CUST_GetCustomers returned a value indicating a failure.");
							Program.LogError("UniversalService.CUST_GetCustomers - Service failed: {0} {1} ({2}).  {3}\r\n{4}: {5}", result.ErrorCode, result.ErrorCause, result.Version, result.ErrorSolution, result.ErrorSource, result.ErrorStackTrace);
						}
					}
					catch (Exception ex) {
						errors.Add("UniversalService.CUST_GetCustomers threw an exception.");
						Program.LogError("UniversalService.CUST_GetCustomers - Exception: {0}", ex);
					}

					try {
						ApplicationCodes applicationCodes = new ApplicationCodes();
						DataFilter[] filters = new DataFilter[0];
						SortFilter[] sorts = null;

						service.Timeout = 99999999;
						UniversalServiceResult result = service.APPL_GetApplicationCodes(token, filters, sorts, ref applicationCodes);
						if (!result.IsSuccessful) {
							errors.Add("UniversalService.APPL_GetApplicationCodes returned a value indicating a failure.");
							Program.LogError("UniversalService.APPL_GetApplicationCodes - Service failed: {0} {1} ({2}).  {3}\r\n{4}: {5}", result.ErrorCode, result.ErrorCause, result.Version, result.ErrorSolution, result.ErrorSource, result.ErrorStackTrace);
						}
					}
					catch (Exception ex) {
						errors.Add("UniversalService.APPL_GetApplicationCodes threw an exception.");
						Program.LogError("UniversalService.APPL_GetApplicationCodes - Exception: {0}", ex);
					}

					try {
						Boolean isSuccessful = service.CONN_Disconnect(token);
						if (!isSuccessful) {
							errors.Add("UniversalService.CONN_Disconnect returned a value indicating it was not successful.");
							Program.LogError("UniversalService.CONN_Disconnect - Returned FALSE.");
						}
					}
					catch (Exception ex) {
						errors.Add("UniversalService.CONN_Disconnect threw an exception.");
						Program.LogError("UniversalService.CONN_Disconnect - Exception: {0}", ex);
					}
				}
			}
			catch (Exception ex) {
				errors.Add("UniversalService could not initialize a proxy.");
				Program.LogError("UniversalService - Could not initialize a proxy: {0}", ex);
			}

			return errors;
		}
	}
}
