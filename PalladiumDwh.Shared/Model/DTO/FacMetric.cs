using PalladiumDwh.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class FacMetric
    {
        public CargoType CargoType { get; set; }
        public string Metric { get; set; }
        public FacMetric()
        {

        }

        public FacMetric(CargoType cargoType, string metric)
        {
            CargoType = cargoType;
            Metric = metric;
        }
    }
}
