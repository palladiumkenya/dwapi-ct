using System;
using Newtonsoft.Json;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Exchange
{
    public class ManifestDto
    {
        public Guid Id { get; set; }
        public int FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string Docket { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime BuildDate { get; set; }
        public int PatientCount { get; set; }
        public string Cargo { get; set; }

        public ManifestDto(MasterFacility facility, FacilityManifest manifest)
        {
            Id = manifest.Id;
            FacilityCode = manifest.SiteCode;
            FacilityName = facility.Name;
            Docket = "NDWH";
            LogDate = manifest.DateRecieved;
            BuildDate = manifest.DateRecieved;
            PatientCount = manifest.PatientCount;
            Cargo = manifest.Metrics;
        }

        public ManifestDto(int code, string fac, FacilityManifest manifest)
        {
            Id = manifest.Id;
            FacilityCode = code;
            FacilityName = fac;
            Docket = "NDWH";
            LogDate = manifest.DateRecieved;
            BuildDate = manifest.DateRecieved;
            PatientCount = manifest.PatientCount;
            Cargo = manifest.Metrics;
        }
    }
}
