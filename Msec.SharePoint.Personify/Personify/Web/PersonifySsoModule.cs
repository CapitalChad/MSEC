using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Security;

namespace Msec.Personify.Web {
	/// <summary>
	/// An HTTP module that allows an application to interface with the Personify SSO login page.
	/// This class may not be inherited.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sso", Justification = "The term 'Sso' is an abbreviation for Single Sign On.")]
	public sealed class PersonifySsoModule : Object, IHttpModule {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PersonifySsoModule"/> class.
		/// </summary>
		public PersonifySsoModule() : base() { }

	// Methods
		/// <summary>
		/// Disposes of any resources held by this instance.
		/// </summary>
		public void Dispose() {
			/* Nothing to dispose.  Remove this comment if a disposable field is created. */
		}
		/// <summary>
		/// Handles the <see cref="E:HttpApplication.AuthenticateRequest"/> event.
		/// </summary>
		/// <param name="sender">The <see cref="T:HttpApplication"/> sending the event.</param>
		/// <param name="e">Provides information about the event.</param>
		private static void HttpApplication_AuthenticateRequest(Object sender, EventArgs e) {
			HttpApplication application = sender as HttpApplication;
			Debug.Assert(application != null);
			Debug.Assert(application.Context != null);

			PersonifySsoContext context = new PersonifySsoContext(application.Context);
			context.AuthenticateRequest();
		}
		/// <summary>
		/// Handles the <see cref="E:HttpApplication.EndRequest"/> event.
		/// </summary>
		/// <param name="sender">The <see cref="T:HttpApplication"/> sending the event.</param>
		/// <param name="e">Provides information about the event.</param>
		private static void HttpApplication_EndRequest(Object sender, EventArgs e) {
			HttpApplication application = sender as HttpApplication;
			Debug.Assert(application != null);
			Debug.Assert(application.Context != null);

			PersonifySsoContext context = new PersonifySsoContext(application.Context);
			context.EndRequest();
		}
		/// <summary>
		/// Initializes the HTTP module.
		/// </summary>
		/// <param name="context">The application context from which to initialize this instance.</param>
		public void Init(HttpApplication context) {
			if (context == null)
				throw new ArgumentNullException("context");

			context.AuthenticateRequest += PersonifySsoModule.HttpApplication_AuthenticateRequest;
			context.EndRequest += PersonifySsoModule.HttpApplication_EndRequest;
		}
	}
}
