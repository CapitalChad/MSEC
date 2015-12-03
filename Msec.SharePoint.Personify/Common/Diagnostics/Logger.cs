using System;
using System.Diagnostics;

namespace Msec.Diagnostics {
	/// <summary>
	/// Provides logging functionality for types in the <see cref="N:Msec"/> namespace.  This class may not be inherited.
	/// </summary>
	public static class Logger {
	// Fields
		/// <summary>
		/// The trace source to which to log information.  This field is read-only.
		/// </summary>
		private static readonly TraceSource _source = new TraceSource(Logger.TraceSourceName, SourceLevels.Warning);

	// Properties
		/// <summary>
		/// Gets the name of the trace source used by this type.
		/// </summary>
		public static String TraceSourceName {
			get { return "MSEC"; }
		}

	// Methods
		/// <summary>
		/// Logs information to the trace source.
		/// </summary>
		/// <param name="eventType">The type of event to log.</param>
		/// <param name="source">The source of the trace event.</param>
		/// <param name="messageOrFormat">The message or message format to log.</param>
		/// <param name="args">The optional list of arguments to provide to <paramref name="messageOrFormat"/>.</param>
		private static void Log(TraceEventType eventType, Object source, String messageOrFormat, Object[] args) {
			Debug.Assert(source != null);
			Debug.Assert(messageOrFormat != null);
			Type type = (source as Type) ?? source.GetType();

			if (args != null && args.Length > 0) {
				Logger._source.TraceEvent(eventType, type.GetHashCode(), messageOrFormat, args);
			}
			else {
				Logger._source.TraceEvent(eventType, type.GetHashCode(), messageOrFormat);
			}
		}
		/// <summary>
		/// Logs an error message.
		/// </summary>
		/// <param name="instance">The object that represents the source of the message.</param>
		/// <param name="messageOrFormat">The message or message format to log.</param>
		/// <param name="args">The optional list of arguments to provide to <paramref name="messageOrFormat"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="messageOrFormat"/> is a null reference.</exception>
		public static void LogError(this Object instance, String messageOrFormat, params Object[] args) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (messageOrFormat == null) {
				throw new ArgumentNullException("messageOrFormat");
			}
			Logger.Log(TraceEventType.Error, instance, messageOrFormat, args);
		}
		/// <summary>
		/// Logs an information message.
		/// </summary>
		/// <param name="instance">The object that represents the source of the message.</param>
		/// <param name="messageOrFormat">The message or message format to log.</param>
		/// <param name="args">The optional list of arguments to provide to <paramref name="messageOrFormat"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="messageOrFormat"/> is a null reference.</exception>
		public static void LogInformation(this Object instance, String messageOrFormat, params Object[] args) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (messageOrFormat == null) {
				throw new ArgumentNullException("messageOrFormat");
			}
			Logger.Log(TraceEventType.Information, instance, messageOrFormat, args);
		}
		/// <summary>
		/// Logs a warning message.
		/// </summary>
		/// <param name="instance">The object that represents the source of the message.</param>
		/// <param name="messageOrFormat">The message or message format to log.</param>
		/// <param name="args">The optional list of arguments to provide to <paramref name="messageOrFormat"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="messageOrFormat"/> is a null reference.</exception>
		public static void LogWarning(this Object instance, String messageOrFormat, params Object[] args) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (messageOrFormat == null) {
				throw new ArgumentNullException("messageOrFormat");
			}
			Logger.Log(TraceEventType.Warning, instance, messageOrFormat, args);
		}
		/// <summary>
		/// Logs a verbose message.
		/// </summary>
		/// <param name="instance">The object that represents the source of the message.</param>
		/// <param name="messageOrFormat">The message or message format to log.</param>
		/// <param name="args">The optional list of arguments to provide to <paramref name="messageOrFormat"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="messageOrFormat"/> is a null reference.</exception>
		public static void LogVerbose(this Object instance, String messageOrFormat, params Object[] args) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (messageOrFormat == null) {
				throw new ArgumentNullException("messageOrFormat");
			}
			Logger.Log(TraceEventType.Verbose, instance, messageOrFormat, args);
		}
	}
}
