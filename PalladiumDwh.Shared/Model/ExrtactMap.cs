using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumDwh.Shared.Model
{
    public class ExMap
    {
        public int Ordinal { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }

        public ExMap(int ordinal, string name, Type type)
        {
            Ordinal = ordinal;
            Name = name;
            Type = type;
        }
    }
}
