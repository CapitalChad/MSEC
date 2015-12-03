using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Msec {
	/// <summary>
	/// Lazily loads an object.  This class may not be inherited.  Instances of this class are thread-safe.
	/// </summary>
	/// <typeparam name="T">The type of object to load.</typeparam>
	/// <remarks>This class already exists in .NET 4.0 as System.Lazy&lt;T&gt;.</remarks>
	[Serializable()]
	public sealed class LazyLoader<T> : Object, IDisposable {
	// Fields
		/// <summary>
		/// The function that will load the object.  This field is read-only.
		/// </summary>
		private readonly Func<T> _initializer;
		/// <summary>
		/// <c>true</c> if this instance has been initialized; otherwise, <c>false</c>.
		/// </summary>
		[NonSerialized()]
		private Boolean _isInitialized;
		/// <summary>
		/// Controls access to the <see cref="F:_object"/> field.  This field is read-only.
		/// </summary>
		private readonly Object _door = new Object();
		/// <summary>
		/// The object that is loaded.  Only access this field through the <see cref="P:Object"/> property.
		/// </summary>
		[NonSerialized()]
		private T _object;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LazyLoader&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="initializer">The function that will load the object.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="initializer"/> is a null reference.</exception>
		public LazyLoader(Func<T> initializer)
			: base() {
			if (initializer == null) {
				throw new ArgumentNullException("initializer");
			}
			this._initializer = initializer;
		}
		/// <summary>
		/// Finalizes an instance of the <see cref="T:LazyLoader&lt;T&gt;"/> class.
		/// </summary>
		~LazyLoader() {
			this.Dispose();
		}

	// Properties
		/// <summary>
		/// Gets a value indicating if this instance has been loaded.
		/// </summary>
		public Boolean IsInitialized {
			get { return this._isInitialized; }
		}
		/// <summary>
		/// Gets the object.  If the object is not already loaded, it will be loaded when this property is called.
		/// </summary>
		public T Object {
			get {
				this.EnsureInitialized();
				return this._object;
			}
		}

	// Methods
		/// <summary>
		/// Disposes any resources held by this instance.
		/// </summary>
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This type may be disposed multiple times.")]
		public void Dispose() {
			if (!this._isInitialized) {
				return;
			}

			lock (this._door) {
				if (!this._isInitialized) {
					return;
				}

				IDisposable disposableObject = this._object as IDisposable;
				if (disposableObject != null) {
					disposableObject.Dispose();
				}
				this._object = default(T);
				this._isInitialized = false;
			}
		}
		/// <summary>
		/// Ensures that the object is loaded.
		/// </summary>
		private void EnsureInitialized() {
			if (this._isInitialized) {
				return;
			}

			lock (this._door) {
				if (this._isInitialized) {
					return;
				}

				this._object = this._initializer();
				this._isInitialized = true;
			}
		}
		/// <summary>
		/// Forces this instance to load the object.  If the object is already loaded, nothing happens.
		/// </summary>
		public void Load() {
			this.EnsureInitialized();
		}
		/// <summary>
		/// Handles the special OnSerializing event.
		/// </summary>
		/// <param name="context">Describes the source and destination of the serialization process.</param>
		[OnSerializing()]
		private void OnSerializing(StreamingContext context) {
			this.Dispose();
		}
	}
}
