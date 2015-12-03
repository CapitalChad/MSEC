using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using Msec.Diagnostics;
using Msec.Personify.Services;

using FederatedAuthentication = Microsoft.IdentityModel.Web.FederatedAuthentication;
using HttpStatusCode = System.Net.HttpStatusCode;
using PersonifyConfiguration = Msec.Personify.Configuration.PersonifyConfiguration;
using SessionSecurityToken = Microsoft.IdentityModel.Tokens.SessionSecurityToken;
using SPSessionAuthenticationModule = Microsoft.SharePoint.IdentityModel.SPSessionAuthenticationModule;
using Microsoft.SharePoint;

namespace Msec.Personify.Web {
	/// <summary>
	/// The context object that performs the Personify SSO functionality for an HTTP request.
	/// This class may not be inherited.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sso", Justification = "The term 'Sso' is an abbreviation for Single Sign On.")]
	public sealed class PersonifySsoContext : Object {
	// Fields
		/// <summary>
		/// The HTTP context to handle.
		/// This field is read-only.
		/// </summary>
		private readonly HttpContext _httpContext;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifySsoContext"/> class.
		/// </summary>
		/// <param name="httpContext">The HTTP context to handle.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="httpContext"/> is a null reference.</exception>
		public PersonifySsoContext(HttpContext httpContext)
			: base() {
			if (httpContext == null)
				throw new ArgumentNullException("httpContext");

			this._httpContext = httpContext;
		}

	// Properties
		/// <summary>
		/// Gets or sets a value indicating if the AuthenticateRequest event has already been handled for the HTTP context.
		/// </summary>
		private Boolean AuthenticateRequestCalled {
			get {
				Debug.Assert(this._httpContext != null);
				Debug.Assert(this._httpContext.Items != null);
				Object value = this._httpContext.Items[typeof(PersonifySsoContext).FullName + ".AuthenticateRequestCalled"];
				if (value is Boolean) {
					Boolean authenticateRequestedCalled = (Boolean)value;
					return authenticateRequestedCalled;
				}
				return false;
			}
			set {
				Debug.Assert(this._httpContext != null);
				Debug.Assert(this._httpContext.Items != null);
				this._httpContext.Items[typeof(PersonifySsoContext).FullName + ".AuthenticateRequestCalled"] = value;
			}
		}
		/// <summary>
		/// Gets the <see cref="T:SessionSecurityToken"/> for the current request, or a null reference.
		/// </summary>
		private static SessionSecurityToken CurrentSessionSecurityToken {
			get {
				SPSessionAuthenticationModule sessionAuthenticationModule = null;
				try {
					sessionAuthenticationModule = FederatedAuthentication.SessionAuthenticationModule as SPSessionAuthenticationModule;
				}
				catch (NullReferenceException) { }
				if (sessionAuthenticationModule == null) {
					throw new InvalidOperationException("The {0} HTTP module must be added in the web.config file.".FormatInvariant(typeof(SPSessionAuthenticationModule).AssemblyQualifiedName));
				}
				SessionSecurityToken sessionSecurityToken;
				sessionAuthenticationModule.TryReadSessionTokenFromCookie(out sessionSecurityToken);
				return sessionSecurityToken;
			}
		}
		private Boolean IsAuthenticationRequired {
			get {
				String requestUrl = this._httpContext.Request.Url.AbsolutePath;
				String matchingIgnoredUrl = PersonifyConfiguration.Instance.IgnoredUrls
					.Where(ignoredUrl => {
						String absoluteUrl = VirtualPathUtility.ToAbsolute(ignoredUrl);
						Boolean matches = requestUrl.StartsWith(absoluteUrl, StringComparison.OrdinalIgnoreCase);
						this.LogVerbose("URL {0} {1} ignored URL {2} as absolute URL {3}.", requestUrl, matches ? "matches" : "does not match", ignoredUrl, absoluteUrl);
						return matches;
					})
					.FirstOrDefault();

				if (matchingIgnoredUrl != null) {
					this.LogVerbose("PersonifySsoContext: Requested URL ({0}) matches ignored URL ({1}).", requestUrl, matchingIgnoredUrl);
					return false;
				}

				this.LogVerbose("PersonifySsoContext: Requested URL ({0}) does not match any ignored URL.", requestUrl);
				return true;
			}
		}
		/// <summary>
		/// Gets a value indicating if the response for the current request is returning an Unauthorized response (401).
		/// </summary>
		private Boolean IsUnauthorizedRequest {
			get {
				Debug.Assert(this._httpContext != null);
				Debug.Assert(this._httpContext.Response != null);
				Boolean isUnauthorizedRequest = this._httpContext.Response.StatusCode == (Int32)HttpStatusCode.Unauthorized;
				return isUnauthorizedRequest;
			}
		}

	// Methods
		/// <summary>
		/// Authenticates the HTTP request.
		/// </summary>
		public void AuthenticateRequest() {
			this.LogVerbose("PersonifySsoContext: Authenticating request...");

			if (!this.IsAuthenticationRequired) {
				this.LogVerbose("PersonifySsoContext: No authentication is required.");
				return;
			}

			if (this.AuthenticateRequestCalled) {
				this.LogVerbose("PersonifySsoContext: Request was already authenticated.");
				return;
			}

			this.AuthenticateRequestCalled = true;
			SessionSecurityToken sessionSecurityToken = PersonifySsoContext.CurrentSessionSecurityToken;
			if (sessionSecurityToken != null) {
				this.LogInformation("PersonifySsoContext: A session security token was found for the current user.  User is authenticated.");
				return;
			}

			if (!this._httpContext.Request.QueryString.AllKeys.Contains("ct")) {
				this.LogVerbose("PersonifySsoContext: The user has not been authenticated by the SSO login page.");
				this.RedirectToLoginPage();
				return;
			}

			CustomerToken customerToken = CustomerToken.Create(this._httpContext.Request.QueryString["ct"]);
			if (customerToken == null) {
				this.LogVerbose("PersonifySsoContext: The customer token received by the SSO login page is invalid.");
				return;
			}

			PersonifyUser user = new PersonifyUser(customerToken);
			user.SetAsCurrent();
			this.LogInformation("PersonifySsoContext: A session security token was created for the current user based on the query string.  User is authenticated.");

			try {
				SPSite currentSite = SPContext.Current.Site;
				SPWeb currentWeb = SPContext.Current.Web;
				SPSecurity.RunWithElevatedPrivileges(delegate() {
					using (SPSite elevatedSite = new SPSite(currentSite.ID)) {
						elevatedSite.AllowUnsafeUpdates = true;
						foreach (SPWeb elevatedWeb in elevatedSite.AllWebs) {
							elevatedWeb.AllowUnsafeUpdates = true;
							SPUser elevatedUser;
							try {
								elevatedUser = elevatedWeb.AllUsers[user.LoginName];
								if (elevatedUser.Name == user.FriendlyName)
									continue;
								elevatedUser.Name = user.FriendlyName;
								elevatedUser.Update();
							}
							catch (SPException) {
								elevatedWeb.AllUsers.Add(user.LoginName, String.Empty, user.FriendlyName, String.Empty);
							}
							elevatedWeb.Update();
						}
					}
				});
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely())
					throw;

				this.LogError("PersonifySsoContext: Error updating friendly name of user {0}: {1}", user.LoginName, ex);
			}
		}
		/// <summary>
		/// Performs specific actions at the end of the HTTP request.
		/// </summary>
		public void EndRequest() {
			if (this.IsAuthenticationRequired && this.AuthenticateRequestCalled && this.IsUnauthorizedRequest) {
				this.LogInformation("PersonifySsoContext: User is authenticated, but the request returned a 401 HTTP status code.  Redirecting to SSO login page.");
				this.RedirectToLoginPage();
			}
		}
		/// <summary>
		/// Redirects the user to the login page.
		/// </summary>
		private void RedirectToLoginPage() {
			this.RedirectToLoginPage(this._httpContext.Request.Url.OriginalString);
		}
		/// <summary>
		/// Redirects the user to the login page.
		/// </summary>
		/// <param name="requestedUrl">The URL requested by the user.</param>
		public void RedirectToLoginPage(String requestedUrl) {
			this.LogVerbose("PersonifySsoContext: Redirecting to the SSO login page...");
			Debug.Assert(this._httpContext != null);
			Debug.Assert(this._httpContext.Request != null);
			Debug.Assert(this._httpContext.Response != null);

			if (requestedUrl == null)
				throw new ArgumentNullException("url");

			PersonifyConfiguration configuration = PersonifyConfiguration.Instance;
			String vendorToken = null;
			using (PersonifySsoService ssoService = PersonifySsoService.NewPersonifySsoService()) {
				vendorToken = ssoService.EncryptVendorToken(requestedUrl);
			}
			Uri loginPageUrl = configuration.LoginPageUrl
				.RemoveQuery("ct")
				.AppendQuery("vi", configuration.VendorIdentifier)
				.AppendQuery("vt", vendorToken);
			this.LogInformation("Redirecting to {0}.", loginPageUrl);
			this._httpContext.Response.Redirect(loginPageUrl.ToString(), false);
			this.LogVerbose("PersonifySsoContext: Redirected to URL {0}.", loginPageUrl);
		}
	}
}
