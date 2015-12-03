using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Msec {
	/// <summary>
	/// Provides helper methods around exceptions.  This class may not be inherited.
	/// </summary>
	internal static class ExceptionHelper {
	// Methods
		/// <summary>
		/// Creates and returns an exception.
		/// </summary>
		/// <returns>The exception created.</returns>
		internal static Exception CreateException() {
			try {
				throw new InvalidOperationException("This is a created exception.");
			}
			catch (Exception ex) {
				return ex;
			}
		}
		/// <summary>
		/// Creates an exception by calling the special serialization constructor.
		/// </summary>
		/// <typeparam name="T">The type of exception to create.</typeparam>
		/// <param name="info">The serialization info object to provide to the constructor.</param>
		/// <param name="context">The streaming context for the constructor.</param>
		/// <returns>A reference to the exception created.</returns>
		internal static T CreateException<T>(SerializationInfo info, StreamingContext context) where T : Exception {
			ConstructorInfo constructor = typeof(T).GetConstructor(
				BindingFlags.CreateInstance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
				null,
				new Type[] { typeof(SerializationInfo), typeof(StreamingContext) },
				null);
			if (constructor == null) {
				throw new InvalidOperationException("The exception type specified does not implement the required .ctor(SerializationInfo, StreamingContext) constructor.");
			}
			try {
				return (T)constructor.Invoke(new Object[] { info, context });
			}
			catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
	}
}
