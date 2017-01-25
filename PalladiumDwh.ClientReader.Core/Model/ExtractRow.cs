using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using FastMember;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public abstract class ExtractRow : IExtractRow
    {
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
