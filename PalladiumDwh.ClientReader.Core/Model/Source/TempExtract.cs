using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Model.Source
{
    public abstract class TempExtract : ITempExtract
    {
       
        
        [Key]
        [DoNotRead]
        public Guid Id { get; set; }
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int? FacilityId { get; set; }
        public int SiteCode { get; set; }

        [DoNotRead]
        public  DateTime DateExtracted { get; set; }
        [DoNotRead]
        [NotMapped]
        public bool HasError { get; set; }

        protected TempExtract()
        {
            Id=Guid.NewGuid();
            DateExtracted=DateTime.Now;
        }

        public virtual void Load(IDataReader reader)
        {
            HasError = false;
                foreach (var p in GetType().GetProperties())
                {
                    if (!Attribute.IsDefined(p, typeof(DoNotReadAttribute)))
                        p.SetValue(this, reader.Get(p.Name, p.PropertyType));
                }
            HasError = !IsValid();
        }

        public virtual bool IsValid()
        {
            return PatientPK > 0 &&
                   SiteCode > 0 &&
                   !string.IsNullOrWhiteSpace(PatientID);
        }

        public virtual string GetAddAction()
        {
            StringBuilder scb = new StringBuilder();
            List<string> columns = new List<string>();
            foreach (var p in GetType().GetProperties())
            {
                if (!Attribute.IsDefined(p, typeof(NotMappedAttribute)))
                    columns.Add(p.Name);
            }

            if (columns.Count > 1)
            {
                scb.Append($" INSERT INTO {GetType().Name} ");
                scb.Append($" ({Utility.GetColumns(columns)}) ");
                scb.Append($" VALUES({Utility.GetParameters(columns)}) ");
            }

            return scb.ToString();
        }

      
    }
}
