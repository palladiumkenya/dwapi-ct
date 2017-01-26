using System.Collections.Generic;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model
{
    public class Facility : Entity
    {
        public int Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PatientExtract> PatientExtracts { get; set; } = new List<PatientExtract>();

        public Facility()
        {
            
        }
        public Facility(int code, string name,string emr, string project)
        {
            Code = code;
            Name = name;
            Emr = emr;
            Project = project;
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
