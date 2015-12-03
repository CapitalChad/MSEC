using System;
using System.Collections.Specialized;

namespace Msec.Collections {
	/// <summary>
	/// Provides extension methods for the <see cref="T:NameValueCollection"/> class.  This class may not be inherited.
	/// </summary>
	public static class NameValueCollectionExtensions {
	// Methods
		/// <summary>
		/// Removes the value specified from the name value collection and returns the value.
		/// </summary>
		/// <param name="instance">The instance from which to retrieve and remove the value.</param>
		/// <param name="name">Specifies the value to retrieve and remove.</param>
		/// <returns>The value contained, or a null reference if the value did not exist in the collection.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		public static String GetAndRemove(this NameValueCollection instance, String name) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}
			String value = instance[name];
			instance.Remove(name);
			return value;
		}
	}
}
