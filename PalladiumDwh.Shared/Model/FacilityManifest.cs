﻿using PalladiumDwh.Shared.Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Extentions;

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
        public UploadMode UploadMode { get; set; }
        public Guid? Session { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Tag { get; set; }
        public ICollection<FacilityManifestCargo> Cargoes { get; set; }=new List<FacilityManifestCargo>();
        [NotMapped]
        public string Metrics { get; private set; }

        public FacilityManifest()
        {
        }

        public FacilityManifest(int siteCode, int patientCount, Guid? id) : this()
        {
            SiteCode = siteCode;
            PatientCount = patientCount;
            DateRecieved = DateTime.Now;
            if (null != id)
            {
                Id = id.IsNullOrEmpty() ? Guid.NewGuid() : id.Value;
            }
            else
            {
                Id = Guid.NewGuid();
            }
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
            var fm = new FacilityManifest(manifest.SiteCode, manifest.PatientCount, manifest.Id);

            fm.EmrId = manifest.EmrId;
            fm.EmrName = manifest.EmrName;
            fm.EmrSetup = manifest.EmrSetup;
            fm.Name = manifest.Name;
            fm.UploadMode = manifest.UploadMode;
            fm.Session = manifest.Session;
            fm.Start = manifest.Start;
            fm.Tag = manifest.Tag;

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

        public void EndSession(DateTime end)
        {
            End = end;
        }

    }
}
