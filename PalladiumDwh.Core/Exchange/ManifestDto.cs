using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
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
        public Guid? Session { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Tag { get; set; }

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
            Session = manifest.Session;
            Start = manifest.Start;
            End = manifest.End;
            Tag = manifest.Tag;
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
            Session = manifest.Session;
            Start = manifest.Start;
            End = manifest.End;
            Tag = manifest.Tag;
            Metrics = manifest.Cargoes.Where(x => x.CargoType != CargoType.Patient).ToList();
        }
    }

    public class ExtractDto
    {
        public string Name { get; set; }
        public int? NoLoaded { get; set; }
        public string Version { get; set; }
        public string LogValue { get; set; }
        public DateTime? ActionDate { get; set; }
        public List<ExtractCargoDto> ExtractCargos { get; set; } = new List<ExtractCargoDto>();

        public static ExtractDto Generate(List<MetricDto> metricDtos)
        {
            var extractDto = new ExtractDto();

            var cargoes = metricDtos.Where(x =>
                    x.CargoType == CargoType.AppMetrics &&
                    x.Cargo.Contains("CareTreatment") &&
                    x.Cargo.Contains("ExtractCargos"))
                .Select(c => c.Cargo)
                .ToList();

            foreach (var cargo in cargoes)
            {
                var temp = JsonConvert.DeserializeObject<ExtractDto>(cargo);
                if (null != temp && !string.IsNullOrWhiteSpace(temp.LogValue))
                {
                    extractDto = JsonConvert.DeserializeObject<ExtractDto>(temp.LogValue);
                    if (extractDto.ExtractCargos.Any())
                        return extractDto;
                }
            }

            return extractDto;
        }
    }

    public class ExtractCargoDto
    {
        public string DocketId { get; set; }
        public string Name { get; set; }
        public int? Stats { get; set; }
    }
}
