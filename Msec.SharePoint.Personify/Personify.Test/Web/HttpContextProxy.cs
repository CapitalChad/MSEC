using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;

using Hashtable = System.Collections.Hashtable;

namespace Msec.Personify.Web {
	/// <summary>
	/// Provides proxy access to an <see cref="T:HttpContext"/> object.  This class may not be inherited.
	/// </summary>
	public sealed class HttpContextProxy : DisposableBase {
	// Fields
		/// <summary>
		/// The HTTP context for this instance.
		/// </summary>
		private HttpContext _context;
		/// <summary>
		/// The previous HTTP context that this one replaces.
		/// </summary>
		private HttpContext _previousContext;
		/// <summary>
		/// The stream to which the response output is written.
		/// </summary>
		private MemoryStream _responseStream;
		/// <summary>
		/// The writer for the response output.
		/// </summary>
		private TextWriter _responseWriter;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:HttpContextProxy"/> class.
		/// </summary>
		public HttpContextProxy() : this("Default.aspx", null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:HttpContextProxy"/> class.
		/// </summary>
		/// <param name="filename">The file name simulated in the request.</param>
		public HttpContextProxy(String filename) : this(filename, null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:HttpContextProxy"/> class.
		/// </summary>
		/// <param name="filename">The file name simulated in the request.</param>
		/// <param name="queryString">The query string simulated in the request.</param>
		public HttpContextProxy(String filename, String queryString)
			: base() {
			MemoryStream responseStream = new MemoryStream();
			TextWriter responseWriter = new StreamWriter(responseStream);
			HttpWorkerRequest wr = new SimpleWorkerRequest("/", @"C:\inetpub\wwwroot\", filename, queryString, responseWriter);
			HttpContext context = new HttpContext(wr);
			HttpBrowserCapabilities browser = new HttpBrowserCapabilities();
			browser.SetFieldValue("_items", new Hashtable());
			context.Request.Browser = browser;

			HttpContext previousContext = HttpContext.Current;
			HttpContext.Current = context;

			this._context = context;
			this._previousContext = previousContext;
			this._responseStream = responseStream;
			this._responseWriter = responseWriter;

			IHttpSessionState container = new HttpSessionStateContainer(Guid.NewGuid().ToString(), new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 20, true, HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);
			this.SetSessionState(container);
		}

	// Properties
		/// <summary>
		/// Gets the context simulated by this instance.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException"><see cref="P:DisposableBase.IsDisposed"/> is <c>true</c>.</exception>
		public HttpContext Context {
			get {
				this.ThrowIfDisposed();
				return this._context;
			}
		}

	// Methods
		/// <summary>
		/// Disposes of any managed resources held by this instance.
		/// </summary>
		protected override void ReleaseManagedResources() {
			if (this._previousContext != null) {
				HttpContext.Current = this._previousContext;
				this._previousContext = null;
			}

			if (this._context != null) {
				this._context.Response.End();
				this._context = null;
			}

			if (this._responseWriter != null) {
				this._responseWriter.Dispose();
				this._responseWriter = null;
			}
			if (this._responseStream != null) {
				this._responseStream.Dispose();
				this._responseStream = null;
			}
		}
		/// <summary>
		/// Sets the session state container to use.
		/// </summary>
		/// <param name="container">The session state container to use.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="container"/> is a null reference.</exception>
		/// <exception cref="System.ObjectDisposedException"><see cref="P:DisposableBase.IsDisposed"/> is <c>true</c>.</exception>
		public void SetSessionState(IHttpSessionState container) {
			this.ThrowIfDisposed();
			if (container == null) {
				throw new ArgumentNullException("container");
			}
			this._context.Items["AspSession"] = typeof(HttpSessionState).CreateInstance(container);
		}
	}
}
