﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PalladiumDwh.DWapi.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".\\private$\\dwapi.emr")]
        public string QueueName {
            get {
                return ((string)(this["QueueName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:4777/stages/")]
        public string LiveSync {
            get {
                return ((string)(this["LiveSync"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AllowSnapshot {
            get {
                return ((bool)(this["AllowSnapshot"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1755;700-ThePalladiumGroup")]
        public string Z_Dapper_Plus_LicenseName {
            get {
                return ((string)(this["Z_Dapper_Plus_LicenseName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("218460a6-02d0-c26b-9add-e6b8d13ccbf4")]
        public string Z_Dapper_Plus_LicenseKey {
            get {
                return ((string)(this["Z_Dapper_Plus_LicenseKey"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"Manifest-10,Patient-10,PatientArt-5,PatientPharmacy-5,PatientVisits-20,PatientStatus-5,Covid-5,DefaulterTracing-1,PatientLabs-5,PatientBaselines-1,PatientAdverseEvents-1,Otz-5,Ovc-5,DepressionScreening-1,DrugAlcoholScreening-1,EnhancedAdherenceCounselling-1,GbvScreening-1,Ipt-1,AllergiesChronicIllness-1,ContactListing-1,CancerScreening-1,IITRiskScores-1,ArtFastTrack-1,CervicalCancerScreening-1,default-1")]
        public string WorkerCount {
            get {
                return ((string)(this["WorkerCount"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int WorkerComandTimeout {
            get {
                return ((int)(this["WorkerComandTimeout"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int WorkerInvisibilityTimeout {
            get {
                return ((int)(this["WorkerInvisibilityTimeout"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int WorkerBatchRetention {
            get {
                return ((int)(this["WorkerBatchRetention"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3123")]
        public int CurrentLatestVersion {
            get {
                return ((int)(this["CurrentLatestVersion"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3120")]
        public int DwapiVersionCuttoff {
            get {
                return ((int)(this["DwapiVersionCuttoff"]));
            }
        }
    }
}
