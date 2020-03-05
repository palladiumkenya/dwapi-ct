using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Enum;
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

        public List<FacilityManifestCargo> Metrics { get; set; } = new List<FacilityManifestCargo>();

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
            Metrics = manifest.Cargoes.Where(x => x.CargoType != CargoType.Patient).ToList();
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
            Metrics = manifest.Cargoes.Where(x => x.CargoType != CargoType.Patient).ToList();
        }
    }
}
