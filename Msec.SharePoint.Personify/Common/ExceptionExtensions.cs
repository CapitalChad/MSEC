using System;

namespace Msec {
	/// <summary>
	/// Provides extension methods for the <see cref="T:Exception"/> class.  This class may not be inherited.
	/// </summary>
	public static class ExceptionExtensions {
	// Methods
		/// <summary>
		/// Returns a value indicating if the exception can be handled safely within an application.
		/// </summary>
		/// <param name="instance">The exception to check.</param>
		/// <returns><c>true</c> if the exception can be handled safely; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static Boolean CanBeHandledSafely(this Exception instance) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}

			if (instance is OutOfMemoryException ||
				instance is AppDomainUnloadedException ||
				instance is BadImageFormatException ||
				instance is CannotUnloadAppDomainException ||
				instance is ExecutionEngineException ||
				instance is InvalidProgramException ||
				instance is System.Threading.ThreadAbortException) {
				return false;
			}
			return true;
		}
	}
}
