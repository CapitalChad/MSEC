using System;
using System.ComponentModel;
using System.Web;

namespace Msec.Personify.Web {
	/// <summary>
	/// Represents a proxy that creates and emulates an <see cref="T:HttpApplication"/> object.  This class may not be inherited.
	/// </summary>
	public sealed class HttpApplicationProxy : DisposableBase {
	// Fields
		/// <summary>
		/// The application object emulated.
		/// </summary>
		private HttpApplication _application;
		/// <summary>
		/// The list of events for the application.
		/// </summary>
		private EventHandlerList _events;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:HttpApplicationProxy"/> class.
		/// </summary>
		/// <param name="context">The HTTP context for the application.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="context"/> is a null reference.</exception>
		public HttpApplicationProxy(HttpContext context) : this(context, new Pair<String, IHttpModule>[0]) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:HttpApplicationProxy"/> class.
		/// </summary>
		/// <param name="context">The HTTP context for the application.</param>
		/// <param name="namedHttpModules">The optional array of named HTTP modules to add to the application.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="context"/> is a null reference.</exception>
		public HttpApplicationProxy(HttpContext context, params Pair<String, IHttpModule>[] namedHttpModules)
			: base() {
			if (context == null) {
				throw new ArgumentNullException("context");
			}

			HttpApplication application = new HttpApplication();

			// Add the HTTP modules.
			if (namedHttpModules != null && namedHttpModules.Length > 0) {
				HttpModuleCollection httpModuleCollection = (HttpModuleCollection)typeof(HttpModuleCollection).CreateInstance();
				foreach (Pair<String, IHttpModule> namedHttpModule in namedHttpModules) {
					if (namedHttpModule != null && namedHttpModule.First != null && namedHttpModule.Second != null) {
						httpModuleCollection.InvokeMethod("AddModule", namedHttpModule.First, namedHttpModule.Second);
					}
				}
				application.SetFieldValue("_moduleCollection", httpModuleCollection);
			}

			// Tie the HTTP context to the HTTP application.
			application.SetFieldValue("_context", context);
			application.SetFieldValue("_stepManager", typeof(HttpApplication).FindNestedType("ApplicationStepManager").CreateInstance(application));
			context.ApplicationInstance = application;

			// Save the state.
			this._application = application;
		}

	// Properties
		/// <summary>
		/// Gets the actual application object emulated.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException"><see cref="P:DisposableBase.IsDisposed"/> is <c>true</c>.</exception>
		public HttpApplication Application {
			get {
				this.ThrowIfDisposed();
				return this._application;
			}
		}
		/// <summary>
		/// Gets the list of events for the application.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException"><see cref="P:DisposableBase.IsDisposed"/> is <c>true</c>.</exception>
		private EventHandlerList Events {
			get {
				this.ThrowIfDisposed();
				if (this._events == null) {
					this._events = (EventHandlerList)this._application.GetPropertyValue("Events");
				}
				return this._events;
			}
		}

	// Methods
		/// <summary>
		/// Raises the <see cref="E:HttpApplication.AuthenticateRequest"/> event.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException"><see cref="P:DisposableBase.IsDisposed"/> is <c>true</c>.</exception>
		public void RaiseAuthenticateRequest() {
			this.ThrowIfDisposed();
			EventHandler authenticateRequest = (EventHandler)this.Events[typeof(HttpApplication).GetFieldValue("EventAuthenticateRequest")];
			if (authenticateRequest != null) {
				authenticateRequest(this._application, EventArgs.Empty);
			}
		}
		/// <summary>
		/// Raises the <see cref="E:HttpApplication.EndRequest"/> event.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException"><see cref="P:DisposableBase.IsDisposed"/> is <c>true</c>.</exception>
		public void RaiseEndRequest() {
			this.ThrowIfDisposed();
			EventHandler endRequest = (EventHandler)this.Events[typeof(HttpApplication).GetFieldValue("EventEndRequest")];
			if (endRequest != null) {
				endRequest(this._application, EventArgs.Empty);
			}
		}
		/// <summary>
		/// Releases any resources held by this instance.
		/// </summary>
		protected override void ReleaseManagedResources() {
			if (this._events != null) {
				this._events.Dispose();
				this._events = null;
			}
			if (this._application != null) {
				this._application.Dispose();
				this._application = null;
			}
		}
	}
}
