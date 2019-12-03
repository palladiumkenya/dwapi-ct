using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PalladiumDwh.Shared.Model
{
    public class FacilityManifest : Entity
    {
        public int SiteCode { get; set; }
        public int PatientCount { get; set; }
        public DateTime DateRecieved { get; set; }
        public ICollection<FacilityManifestCargo> Cargoes { get; set; }=new List<FacilityManifestCargo>();
        [NotMapped]
        public string Metrics { get; private set; }

        public FacilityManifest()
        {
        }

        public FacilityManifest(int siteCode, int patientCount) : this()
        {
            SiteCode = siteCode;
            PatientCount = patientCount;
            DateRecieved =DateTime.Now;
        }

        public void AddCargo(string items)
        {
            var cargo=new FacilityManifestCargo(items,Id);
            Cargoes.Add(cargo);
        }

        public void AddMetricsCargo(string items)
        {
            var cargo = new FacilityManifestCargo(items, Id);
            Cargoes.Add(cargo);
            Metrics = items;
        }

        public static FacilityManifest Create(Manifest manifest)
        {
            var fm = new FacilityManifest(manifest.SiteCode, manifest.PatientCount);
            fm.AddCargo(manifest.Items);
            if (!string.IsNullOrWhiteSpace(manifest.Metrics))
                fm.AddMetricsCargo(manifest.Metrics);

            return fm;
        }

        public override string ToString()
        {
            return $"{SiteCode}|{PatientCount}|{DateRecieved:F}";
        }
    }
}