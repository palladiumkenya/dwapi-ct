using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Exchange
{
    public class IndicatorDto
    {
        public Guid Id { get; set; }
        public int FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Stage { get; set; }
        public DateTime? IndicatorDate { get; set; }
        public Guid FacilityManifestId { get; set; }

        public IndicatorDto(Guid id, int facilityCode, string facilityName, Guid facilityManifestId)
        {
            Id = id;
            FacilityCode = facilityCode;
            FacilityName = facilityName;
            FacilityManifestId = facilityManifestId;
            Stage = "EMR";
        }

        public static List<IndicatorDto> Generate(List<MetricDto> metrics)
        {
            var indicators = new List<IndicatorDto>();
            foreach (var m in metrics)
            {
                var idn = new IndicatorDto(m.Id, m.FacilityCode, m.FacilityName, m.FacilityManifestId);
                var cargo = JsonConvert.DeserializeObject<IndicatorItemDto>(m.Cargo);
                idn.Name = cargo.Indicator;
                idn.Value = cargo.IndicatorValue;
                idn.IndicatorDate = cargo.IndicatorDate;
                indicators.Add(idn);
            }
            return indicators;
        }
    }

    public class IndicatorItemDto
    {
        public string Indicator { get; set; }
        public string IndicatorValue { get; set; }
        public DateTime? IndicatorDate { get; set; }
    }
}