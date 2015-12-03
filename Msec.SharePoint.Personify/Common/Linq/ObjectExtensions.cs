using System;

namespace Msec.Linq {
	/// <summary>
	/// Provides extension methods for the <see cref="T:Object"/> class.  This class may not be inherited.
	/// </summary>
	public static class ObjectExtensions {
	// Methods
		/// <summary>
		/// Projects an object forward onto a new object type.
		/// </summary>
		/// <typeparam name="TInput">The type of object to be projected.</typeparam>
		/// <typeparam name="TOutput">The type of object that will be the projection.</typeparam>
		/// <param name="instance">The object to be projected.</param>
		/// <param name="projection">The projection function.</param>
		/// <returns>The projection created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="projection"/> is a null reference.</exception>
		public static TOutput Project<TInput, TOutput>(this TInput instance, Func<TInput, TOutput> projection) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (projection == null) {
				throw new ArgumentNullException("projection");
			}
			return projection(instance);
		}
	}
}
