//using System;
//using System.Reflection;
//using System.Runtime.Serialization;
//using Msec.Diagnostics;

//using BindingFlags = System.Reflection.BindingFlags;
//using Result = Msec.Personify.Services.UniversalWebServiceImpl.Result;

//namespace Msec.Personify.Services {
//    /// <summary>
//    /// Provides extension methods for the <see cref="T:Result"/> class.  This class may not be inherited.
//    /// </summary>
//    internal static class ResultExtensions {
//    // Constants
//        /// <summary>
//        /// Separates the exception type and message in the ToString() value = ": ".
//        /// </summary>
//        private const String ExceptionTypeAndMessageSeparator = ": ";

//    // Methods
//        /// <summary>
//        /// Returns the type and message from the ToString() representation of an exception.
//        /// </summary>
//        /// <param name="toString">The ToString() value.</param>
//        /// <param name="stackTraceString">The stack trace string.</param>
//        /// <returns>The type and message of the exception.</returns>
//        private static Pair<Type, String> GetTypeAndMessage(String toString, String stackTraceString) {
//            String typeName = typeof(Exception).FullName;
//            String message = toString ?? String.Empty;
//            if (toString != null) {
//                String typeAndMessage = toString;
//                if (stackTraceString != null && typeAndMessage.Contains(stackTraceString)) {
//                    typeAndMessage = typeAndMessage.Replace(stackTraceString, String.Empty);
//                }

//                typeName = typeof(ApplicationException).FullName;
//                message = typeAndMessage;
//                if (typeAndMessage.Contains(ExceptionTypeAndMessageSeparator)) {
//                    typeName = typeAndMessage.Substring(0, typeAndMessage.IndexOf(ExceptionTypeAndMessageSeparator, StringComparison.Ordinal));
//                    message = typeAndMessage.Substring(typeName.Length + ExceptionTypeAndMessageSeparator.Length);
//                }
//            }

//            Type type = Type.GetType(typeName, false, false);
//            if (type == null || !typeof(Exception).IsAssignableFrom(type)) {
//                type = typeof(ApplicationException);
//            }
//            return new Pair<Type, String>(type, message);
//        }
//        /// <summary>
//        /// Returns a value indicating if the result communicates that the caller has been logged out.
//        /// </summary>
//        /// <param name="instance">The result object to check.</param>
//        /// <returns><c>true</c> if the result indicates that the caller has been logged out; otherwise, <c>false</c>.</returns>
//        /// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
//        public static Boolean IsLoggedOut(this Result instance) {
//            if (instance == null) {
//                throw new ArgumentNullException("instance");
//            }

//            return !instance.IsSuccessful
//                && instance.ErrorCause != null
//                && instance.ErrorCause.StartsWith("System.Exception: Invalid Token, please login again.", StringComparison.Ordinal);
//        }
//        /// <summary>
//        /// Throws an exception if the result represents an unsuccessful call.
//        /// </summary>
//        /// <param name="instance">The result to check.</param>
//        /// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
//        /// <exception cref="System.Exception">The result is unsuccessful and contains exception information.</exception>
//        public static void ThrowIfUnsuccessful(this Result instance) {
//            if (instance == null) {
//                throw new ArgumentNullException("instance");
//            }

//            if (!instance.IsSuccessful) {
//                String toString = instance.ErrorCause;
//                if (toString == null) {
//                    throw new ServerServiceException("An unknown error occurred.");
//                }
//                String stackTraceString = instance.ErrorStackTrace;
//                Pair<Type, String> typeAndMessage = ResultExtensions.GetTypeAndMessage(toString, stackTraceString);
//                Type type = typeAndMessage.First;
//                String message = typeAndMessage.Second;
				
//                Exception exception;
//                try {
//                    PropertyInfo hResultProperty = typeof(Exception).GetProperty("HResult", BindingFlags.Instance | BindingFlags.Public);
//                    Int32 hResult = (Int32)hResultProperty.GetValue(Activator.CreateInstance(type), null);

//                    SerializationInfo info = new SerializationInfo(type, new FormatterConverter());
//                    info.AddValue("ClassName", type.FullName);
//                    info.AddValue("Message", message);
//                    info.AddValue("InnerException", null, typeof(Exception));
//                    info.AddValue("HelpURL", null, typeof(String));
//                    info.AddValue("StackTraceString", stackTraceString);
//                    info.AddValue("RemoteStackTraceString", null, typeof(String));
//                    info.AddValue("RemoteStackIndex", 0);
//                    info.AddValue("ExceptionMethod", null, typeof(String));
//                    info.AddValue("HResult", hResult);
//                    info.AddValue("Source", null, typeof(String));
//                    StreamingContext context = new StreamingContext(StreamingContextStates.CrossAppDomain | StreamingContextStates.CrossMachine | StreamingContextStates.CrossProcess);

//                    ConstructorInfo constructor = type.GetConstructor(
//                        BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
//                        null,
//                        new Type[] { typeof(SerializationInfo), typeof(StreamingContext) },
//                        null);
//                    exception = (Exception)constructor.Invoke(new Object[] { info, context });
//                }
//                catch (Exception ex) {
//                    if (!ex.CanBeHandledSafely()) {
//                        throw;
//                    }
//                    instance.LogError("An exception could not be created: {0}", ex);
//                    exception = new ServerServiceException(message);
//                }
//                throw exception;
//            }
//        }
//    }
//}
