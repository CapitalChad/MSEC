using System;

namespace Msec.Personify.Configuration {
	/// <summary>
	/// Ensures that the <see cref="T:PersonifyConfiguration"/> object uses a mock implementation.  This class may not be inherited.
	/// </summary>
	internal sealed class MockPersonifyConfigurationProvider : Object, IDisposable {
	// Fields
		/// <summary>
		/// <c>true</c> if this instance has been disposed; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isDisposed;
		/// <summary>
		/// The SSO configuration section being used.  This field is read-only.
		/// </summary>
		private readonly PersonifySsoConfigurationSection _ssoSection;
		/// <summary>
		/// The Universal configuration section being used.  This field is read-only.
		/// </summary>
		private readonly PersonifyUniversalConfigurationSection _universalSection;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifyConfigurationProvider"/> class.
		/// </summary>
		/// <param name="personifySsoConfigurationSection">The SSO configuration section to emulate.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="personifySsoConfigurationSection"/> is a null reference.</exception>
		public MockPersonifyConfigurationProvider(PersonifySsoConfigurationSection personifySsoConfigurationSection) : this(personifySsoConfigurationSection, null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifyConfigurationProvider"/> class.
		/// </summary>
		/// <param name="personifyUniversalConfigurationSection">The Universal configuration section to emulate.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="personifyUniversalConfigurationSection"/> is a null reference.</exception>
		public MockPersonifyConfigurationProvider(PersonifyUniversalConfigurationSection personifyUniversalConfigurationSection) : this(null, personifyUniversalConfigurationSection) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockPersonifyConfigurationProvider"/> class.
		/// </summary>
		/// <param name="personifySsoConfigurationSection">The SSO configuration section to emulate.</param>
		/// <param name="personifyUniversalConfigurationSection">The Universal configuration section to emulate.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="personifySsoConfigurationSection"/> is a null reference, and <paramref name="personifyUniversalConfigurationSection"/> is a null reference.</exception>
		public MockPersonifyConfigurationProvider(PersonifySsoConfigurationSection personifySsoConfigurationSection, PersonifyUniversalConfigurationSection personifyUniversalConfigurationSection)
			: base() {
			if (personifySsoConfigurationSection == null && personifyUniversalConfigurationSection == null) {
				throw new ArgumentNullException("personifySsoConfigurationSection");
			}

			PersonifyConfiguration configuration = PersonifyConfiguration.Instance;
			if (personifySsoConfigurationSection != null) {
				configuration.SetConfigurationSection(personifySsoConfigurationSection);
			}
			if (personifyUniversalConfigurationSection != null) {
				configuration.SetConfigurationSection(personifyUniversalConfigurationSection);
			}

			this._ssoSection = personifySsoConfigurationSection;
			this._universalSection = personifyUniversalConfigurationSection;
		}
		/// <summary>
		/// Finalizes an instance of the <see cref="T:MockPersonifyConfigurationProvider"/> class.
		/// </summary>
		~MockPersonifyConfigurationProvider() {
			this.Dispose();
		}

	// Properties
		/// <summary>
		/// Gets the SSO configuration section being used, or a null reference.
		/// </summary>
		public PersonifySsoConfigurationSection SsoSection {
			get { return this._ssoSection; }
		}
		/// <summary>
		/// Gets the Universal configuration section being used, or a null reference.
		/// </summary>
		public PersonifyUniversalConfigurationSection UniversalSection {
			get { return this._universalSection; }
		}

	// Methods
		/// <summary>
		/// Disposes of any resources held by this instance.
		/// </summary>
		public void Dispose() {
			if (!this._isDisposed) {
				PersonifyConfiguration configuration = PersonifyConfiguration.Instance;
				configuration.ResetSsoConfigurationSection();
				configuration.ResetUniversalConfigurationSection();
				this._isDisposed = true;
			}
		}
	}
}
