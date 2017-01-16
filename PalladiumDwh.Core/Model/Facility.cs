using System;
using System.Collections.Generic;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public class Facility : Entity
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PatientExtract> PatientExtracts { get; set; } = new List<PatientExtract>();

        public Facility()
        {
            
        }
        public Facility(int code, string name)
        {
            Code = code;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
