using System;

using MemoryStream = System.IO.MemoryStream;
using BinaryFormatter = System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;

namespace Msec {
	/// <summary>
	/// Provides deep clone functionality.  This class may not be inherited.
	/// </summary>
	public static class CloneGenerator {
	// Methods
		/// <summary>
		/// Creates a deep copy of the object specified using binary serialization.
		/// </summary>
		/// <typeparam name="T">The type of object.</typeparam>
		/// <param name="instance">The object to clone.</param>
		/// <returns>The deep copy.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static T SerializeBinary<T>(T instance) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}

			using (MemoryStream stream = new MemoryStream()) {
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, instance);

				stream.Position = 0;
				return (T)formatter.Deserialize(stream);
			}
		}
	}
}
