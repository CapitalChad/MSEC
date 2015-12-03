using System;

namespace Msec.Personify {
	/// <summary>
	/// Provides extension methods for the <see cref="T:Random"/> class.  This class may not be inherited.
	/// </summary>
	public static class RandomExtensions {
	// Methods
		/// <summary>
		/// Returns the next random string.
		/// </summary>
		/// <param name="random">The random object used to create the value.</param>
		/// <returns>The random string that was generated.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="random"/> is a null reference.</exception>
		public static String NextString(this Random random) {
			if (random == null) {
				throw new ArgumentNullException("random");
			}

			Int32 length = random.Next(1, 100);
			Char[] characters = new Char[length];
			for (Int32 i = 0; i < length; i++) {
				characters[i] = random.NextAlphanumericCharacter();
			}
			String value = new String(characters);
			return value;
		}
		/// <summary>
		/// Returns the next random alpha-numeric character.
		/// </summary>
		/// <param name="random">The random object used to create the value.</param>
		/// <returns>The random alpha-numeric character that was generated.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="random"/> is a null reference.</exception>
		public static Char NextAlphanumericCharacter(this Random random) {
			if (random == null) {
				throw new ArgumentNullException("random");
			}

			Int32 value = random.Next(65, 98);
			Char character = (Char)value;
			return character;
		}
	}
}
