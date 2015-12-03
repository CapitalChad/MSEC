using System;

namespace Msec {
	/// <summary>
	/// Provides extension methods for the <see cref="T:String"/> class.  This class may not be inherited.
	/// </summary>
	public static class StringExtensions {
	// Methods
		/// <summary>
		/// Returns a formatted string using the current thread's culture.
		/// </summary>
		/// <param name="format">The <see cref="T:String"/> to use as the string format.</param>
		/// <param name="args">The array of string format arguments to provide.</param>
		/// <returns>The formatted string.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="format"/> is a null reference.</exception>
		/// <exception cref="System.FormatException">The format item in <paramref name="format"/> is invalid.
		/// -or- The number indicating an argument to format is less than zero, or greater than or equal to the number of specified objects to format.</exception>
		public static String FormatInvariant(this String format, params Object[] args) {
			return String.Format((IFormatProvider)null, format, args);
		}
		/// <summary>
		/// Concatenates a specified separator string between each element of a specified array, yielding a single concatenated string.
		/// </summary>
		/// <param name="instance">The array of elements to join.</param>
		/// <param name="separator">The separator to apply between each two elements.</param>
		/// <returns>A string consisting of the elements of value interspersed with the separator string.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static String Join(this String[] instance, String separator) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			return String.Join(separator, instance);
		}
	}
}
