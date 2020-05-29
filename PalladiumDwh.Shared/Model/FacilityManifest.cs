using PalladiumDwh.Shared.Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PalladiumDwh.Shared.Enum;

namespace PalladiumDwh.Shared.Model
{
    public class FacilityManifest : Entity
    {
        public int SiteCode { get; set; }
        public int PatientCount { get; set; }
        public DateTime DateRecieved { get; set; }
        public string Name { get; set; }
        public Guid? EmrId { get; set; }
        public string EmrName { get; set; }
        public EmrSetup EmrSetup { get; set; }
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
            var cargo=new FacilityManifestCargo(items,Id,Enum.CargoType.Patient);
            Cargoes.Add(cargo);
        }

        public void AddMetricsCargo(string items)
        {
            var cargo = new FacilityManifestCargo(items, Id);
            Cargoes.Add(cargo);
            Metrics = items;
        }

        public void AddMetricsCargo(FacMetric metric)
        {
            var cargo = new FacilityManifestCargo(metric.Metric, Id,metric.CargoType);
            Cargoes.Add(cargo);
        }

        public static FacilityManifest Create(Manifest manifest)
        {
            var fm = new FacilityManifest(manifest.SiteCode, manifest.PatientCount);
            
            fm.EmrId = manifest.EmrId;
            fm.EmrName = manifest.EmrName;
            fm.EmrSetup = manifest.EmrSetup;
            fm.Name = manifest.Name;

            fm.AddCargo(manifest.Items);

            if (manifest.FacMetrics.Any())
            {
                foreach (var m in manifest.FacMetrics)
                {
                    fm.AddMetricsCargo(m);
                }
            }
            return fm;
        }

        public override string ToString()
        {
            return $"{SiteCode}|{PatientCount}|{DateRecieved:F}";
        }
    }
}