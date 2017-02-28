using System;
using System.Collections.Generic;
using System.Linq;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EMR> Emrs { get; set; }=new List<EMR>();

        public Project()
        {
            Id = Guid.NewGuid();
        }

        public Project(string code,string name) : this()
        {
            Code = code;
            Name = name;
        }

        public void AddEMR(EMR emr)
        {
            emr.ProjectId = Id;
            Emrs.Add(emr);
        }
        public void AddEMR(List<EMR> emrs)
        {
            foreach (var e in emrs)
            {
               AddEMR(e);
            }
        }

        public EMR GetDefaultEmr()
        {
            return Emrs.FirstOrDefault(x => x.IsDefault);
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}