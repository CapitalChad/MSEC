﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Msec.Personify.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://msec.ebiz.uapps.net/PersonifyIMSWebService/IMService.asmx")]
        public string Ilta_Personify_ImsWebService_IMService {
            get {
                return ((string)(this["Ilta_Personify_ImsWebService_IMService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MSEC")]
        public string Ilta_Personify_OrganizationId {
            get {
                return ((string)(this["Ilta_Personify_OrganizationId"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MSEC")]
        public string Ilta_Personify_OrganizationUnitId {
            get {
                return ((string)(this["Ilta_Personify_OrganizationUnitId"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://msec.ebiz.uapps.net/login/webservice/service.asmx")]
        public string Ilta_Personify_PersonifySso_service {
            get {
                return ((string)(this["Ilta_Personify_PersonifySso_service"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://msec.ebiz.uapps.net/PersonifyWebService/UniversalWebService/default.asmx")]
        public string Ilta_Personify_Services_UniversalWebServiceImpl_PersonifyWebService {
            get {
                return ((string)(this["Ilta_Personify_Services_UniversalWebServiceImpl_PersonifyWebService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://msec.ebiz.uapps.net/PersonifyIMSWebService/IMService.asmx")]
        public string Awwa_Personify_ImsWebService_IMService {
            get {
                return ((string)(this["Awwa_Personify_ImsWebService_IMService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://msec.ebiz.uapps.net/login/webservice/service.asmx")]
        public string Awwa_Personify_PersonifySso_service {
            get {
                return ((string)(this["Awwa_Personify_PersonifySso_service"]));
            }
        }
    }
}