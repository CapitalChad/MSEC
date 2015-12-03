using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel.Configuration;

using ConfigurationFile = System.Configuration.Configuration;
using Path = System.IO.Path;
using BindingFlags = System.Reflection.BindingFlags;
using EndpointAddress = System.ServiceModel.EndpointAddress;
using Binding = System.ServiceModel.Channels.Binding;
using TrustVersion = System.ServiceModel.Security.TrustVersion;
using WSTrustChannel = Microsoft.IdentityModel.Protocols.WSTrust.WSTrustChannel;
using WSTrustChannelFactory = Microsoft.IdentityModel.Protocols.WSTrust.WSTrustChannelFactory;
using SPFarm = Microsoft.SharePoint.Administration.SPFarm;
using SPUtility = Microsoft.SharePoint.Utilities.SPUtility;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents a factory object that creates WS-Trust channels for issuing security tokens from a Security Token Service (STS) in the local SharePoint farm.  This class may not be inherited.
	/// </summary>
	/// <remarks>
	/// This class implements a modified Singleton pattern.
	/// Note: much of the code in this class is hard-coded.  This code was ported from the Microsoft.SharePoint assemblies which also contains hard-coded rules.
	/// </remarks>
	public sealed class SharePointSecurityTokenServiceTrustChannelFactory : DisposableBase {
	// Fields
		/// <summary>
		/// Controls access to the <see cref="F:_instance"/> field.  This field is read-only.
		/// </summary>
		private static readonly Object _door = new Object();
		/// <summary>
		/// The sole instance of the <see cref="T:SharePointSecurityTokenServiceTrustChannelFactory"/> class.
		/// </summary>
		private static SharePointSecurityTokenServiceTrustChannelFactory _instance;
		/// <summary>
		/// The factory object that creates WS-Trust channels to the STS in the local SharePoint farm.
		/// </summary>
		private WSTrustChannelFactory _wsTrustChannelFactory;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SharePointSecurityTokenServiceTrustChannelFactory"/> class.
		/// </summary>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The object is contained in a field and disposed using the IDisposable pattern.")]
		private SharePointSecurityTokenServiceTrustChannelFactory()
			: base() {
			Binding binding = SharePointSecurityTokenServiceTrustChannelFactory.CreateBindingFromConfiguration();
			EndpointAddress remoteAddress = SharePointSecurityTokenServiceTrustChannelFactory.CreateEndpointAddressFromLocalFarm();
			this._wsTrustChannelFactory = new WSTrustChannelFactory(binding, remoteAddress) {
				TrustVersion = TrustVersion.WSTrust13
			};
		}

	// Properties
		/// <summary>
		/// Gets the sole instance of the <see cref="T:SharePointSecurityTokenServiceTrustChannelFactory"/> class.
		/// </summary>
		public static SharePointSecurityTokenServiceTrustChannelFactory Instance {
			get {
				if (SharePointSecurityTokenServiceTrustChannelFactory._instance == null) {
					lock (SharePointSecurityTokenServiceTrustChannelFactory._door) {
						if (SharePointSecurityTokenServiceTrustChannelFactory._instance == null) {
							SharePointSecurityTokenServiceTrustChannelFactory._instance = new SharePointSecurityTokenServiceTrustChannelFactory();
						}
					}
				}
				return SharePointSecurityTokenServiceTrustChannelFactory._instance;
			}
		}

	// Methods
		/// <summary>
		/// Creates a <see cref="T:Binding"/> that used in creating WS-Trust channels.
		/// </summary>
		/// <returns>A reference to the <see cref="T:Binding"/> created.</returns>
		/// <remarks>This code was ported from the SharePoint assemblies using ILSpy.</remarks>
		private static Binding CreateBindingFromConfiguration() {
			// Open up the "C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\14\WebClients\SecurityToken\client.config" file.
			ExeConfigurationFileMap configurationFileMap = new ExeConfigurationFileMap() {
				ExeConfigFilename = Path.Combine(SPUtility.GetGenericSetupPath(@"WebClients\SecurityToken"), "client.config")
			};
			ConfigurationFile configuration = ConfigurationManager.OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None);
			
			// Get the strongly-typed configuration values from the file.
			ServiceModelSectionGroup sectionGroup = ServiceModelSectionGroup.GetSectionGroup(configuration);
			ChannelEndpointElement endpointAddressElement = sectionGroup.Client.Endpoints["contractType:Microsoft.IdentityModel.Protocols.WSTrust.WSTrustServiceContract;name:SecurityTokenService"];
			BindingCollectionElement bindingCollectionElement = sectionGroup.Bindings.BindingCollections
				.First(instance => instance.BindingName == endpointAddressElement.Binding);
			IBindingConfigurationElement bindingConfigurationElement = bindingCollectionElement.ConfiguredBindings
				.First(instance => instance.Name == endpointAddressElement.BindingConfiguration);

			// Create the binding, apply the configuration, and return the binding.
			Binding binding = (Binding)Activator.CreateInstance(bindingCollectionElement.BindingType);
			bindingConfigurationElement.ApplyConfiguration(binding);
			return binding;
		}
		/// <summary>
		/// Creates a WS-Trust channel that can be used to issue security tokens.  This is a direct connection to SharePoint's STS.
		/// </summary>
		/// <returns>A reference to the <see cref="T:WSTrustChannel"/> created.</returns>
		public WSTrustChannel CreateChannel() {
			this.ThrowIfDisposed();
			return this._wsTrustChannelFactory.CreateChannel() as WSTrustChannel;
		}
		/// <summary>
		/// Creates an <see cref="T:EndpointAddress"/> that is used in creating WS-Trust channels.
		/// </summary>
		/// <returns>A reference to the <see cref="T:EndpointAddress"/> created.</returns>
		/// <remarks>This code was ported from the SharePoint assemblies using ILSpy.</remarks>
		private static EndpointAddress CreateEndpointAddressFromLocalFarm() {
			// Get the SPIisWebServiceSettings object from the local farm.  This is an internal type, so we need to use reflection.
			SPFarm localFarm = SPFarm.Local;
			Type spIisWebServiceSettingsType = Type.GetType("Microsoft.SharePoint.Administration.SPIisWebServiceSettings, Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c");
			Object webServiceSettings = localFarm.GetObject("SharePoint Web Services", localFarm.Id, spIisWebServiceSettingsType);

			// Try to get the port for the STS.  Use 32483 as the default.
			Int32 port = 32843;
			if (webServiceSettings != null) {
				port = (Int32)spIisWebServiceSettingsType.GetProperty("HttpPort", BindingFlags.Instance | BindingFlags.Public)
					.GetValue(webServiceSettings, null);
			}

			// Return the endpoint address for SharePoint's STS.
			return new EndpointAddress(new Uri("http://localhost:" + port + "/SecurityTokenServiceApplication/securitytoken.svc"));
		}
		/// <summary>
		/// Releases any managed resources held by this instance.
		/// </summary>
		protected override void ReleaseManagedResources() {
			if (this._wsTrustChannelFactory != null) {
				((IDisposable)this._wsTrustChannelFactory).Dispose();
				this._wsTrustChannelFactory = null;
			}
		}
	}
}
