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
        public CargoType CargoType { get; }
        public string Metric { get; }
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
