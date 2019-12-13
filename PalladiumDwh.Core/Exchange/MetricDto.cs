using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Exchange
{
    public class MetricDto
    {
        public Guid Id { get; set; }
        public int FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string Cargo { get; set; }
        public CargoType CargoType { get; set; }
        public Guid FacilityManifestId { get; set; }

        public MetricDto(MasterFacility facility, FacilityManifestCargo manifestCargo)
        {
            Id = manifestCargo.Id;
            FacilityCode = facility.Code;
            FacilityName = facility.Name;
            Cargo = manifestCargo.Items;
            CargoType = manifestCargo.CargoType;
            FacilityManifestId = manifestCargo.FacilityManifestId;
        }

        public MetricDto(int code, string fac, FacilityManifestCargo manifestCargo)
        {
            Id = manifestCargo.Id;
            FacilityCode = code;
            FacilityName = fac;
            Cargo = manifestCargo.Items;
            CargoType = manifestCargo.CargoType;
            FacilityManifestId = manifestCargo.FacilityManifestId;
        }

        public static List<MetricDto> Generate(MasterFacility masterFacility, FacilityManifest facManifest)
        {
           var  metrics=new List<MetricDto>();
           foreach (var cargo in facManifest.Cargoes)
           {
               metrics.Add(new MetricDto(masterFacility, cargo));
           }
           return metrics;
        }
    }
}
