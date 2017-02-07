using System;
using System.Collections.Generic;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<EMR> Emrs { get; set; }=new List<EMR>();

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
    }
}