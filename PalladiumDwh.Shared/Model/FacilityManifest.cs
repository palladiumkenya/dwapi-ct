using System;
using System.Collections.Generic;

namespace PalladiumDwh.Shared.Model
{
    public class FacilityManifest : Entity
    {
        public int SiteCode { get; set; }
        public int PatientCount { get; set; }
        public DateTime DateRecieved { get; set; }
        public ICollection<FacilityManifestCargo> Cargoes { get; set; }=new List<FacilityManifestCargo>();

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

        public static FacilityManifest Create(Manifest manifest)
        {
            var fm = new FacilityManifest(manifest.SiteCode, manifest.PatientCount);
            fm.AddCargo(manifest.Items);
            return fm;
        }

        public override string ToString()
        {
            return $"{SiteCode}|{PatientCount}|{DateRecieved:F}";
        }
    }
}