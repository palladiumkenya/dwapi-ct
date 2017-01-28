using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Model
{

    public abstract class ClientExtract:IClientExtract
    {
        public  virtual  int PatientPK { get; set; }
        public  virtual  int SiteCode { get; set; }

        [Column(Order = 100)]
        public virtual string Emr { get; set; }
        [Column(Order = 101)]
        public virtual string Project { get; set; }
        [DoNotRead]
        [Column(Order = 103)]
        public virtual bool Processed { get; set; }
        public virtual Guid Id { get; set; }

        protected ClientExtract()
        {
            Id = Guid.NewGuid();
        }
        public virtual string GetAddAction(string source)
        {
            StringBuilder scb = new StringBuilder();
            List<string> columns = new List<string>();
            foreach (var p in GetType().GetProperties())
            {
                if (
                    !Attribute.IsDefined(p, typeof(NotMappedAttribute)) ||
                    !Attribute.IsDefined(p, typeof(DoNotReadAttribute))
                )
                    columns.Add(p.Name);
            }

            if (columns.Count > 1)
            {
                scb.Append($" INSERT INTO {GetType().Name} ");
                scb.Append($" ({Utility.GetColumns(columns)}) ");
                scb.Append($" SELECT {Utility.GetColumns(columns)} FROM {source}");
            }

            return scb.ToString();
        }
    }
}
