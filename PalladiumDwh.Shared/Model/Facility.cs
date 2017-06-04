using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model
{
    public class Facility : Entity
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }

        public virtual ICollection<PatientExtract> PatientExtracts { get; set; } = new List<PatientExtract>();

        public Facility()
        {
            Created = DateTime.Now;
        }

        public Facility(int code, string name, string emr, string project)
            : this()
        {
            Code = code;
            Name = name;
            Emr = emr;
            Project = project;
        }

        public string GetStatus()
        {
            return $"{Name} ({Code}) | Patients:{PatientExtracts.Count}";
        }
        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
