using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Model.Source
{
    public abstract class TempExtract : ITempExtract
    {
        [Key]
        public Guid Id { get; set; }
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int? FacilityId { get; set; }
        public int SiteCode { get; set; }

        [SkipLoad]
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
                if (!Attribute.IsDefined(p, typeof(SkipLoadAttribute)))
                    p.SetValue(this, reader.Get(p.Name, p.PropertyType));
            }
        }
    }
}
