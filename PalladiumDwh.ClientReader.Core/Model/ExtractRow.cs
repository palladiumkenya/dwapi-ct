using System;
using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.ExtractRows;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public abstract class ExtractRow : IExtractRow
    {
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int? FacilityId { get; set; }
        public int SiteCode { get; set; }

        [SkipLoad]
        public  DateTime DateExtracted { get; set; }

        protected ExtractRow()
        {
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
