using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        protected TempExtract()
        {
            Id=Guid.NewGuid();
            DateExtracted=DateTime.Now;
        }

        public virtual void Load(IDataReader reader)
        {
            foreach (var p in GetType().GetProperties())
            {
                if (!Attribute.IsDefined(p, typeof(DoNotReadAttribute)))
                    p.SetValue(this, reader.Get(p.Name, p.PropertyType));
            }
        }

        public string GetAddAction()
        {
            StringBuilder scb = new StringBuilder();
            List<string> columns = new List<string>();
            foreach (var p in GetType().GetProperties())
            {
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
