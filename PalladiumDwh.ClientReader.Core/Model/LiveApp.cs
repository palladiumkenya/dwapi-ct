using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Model
{
   public class LiveApp:Entity
    {
        public string Product { get; set; }
        public string Activation { get; set; }

        public override string Emr { get; set; }        
        public override string Project { get; set; }
        public override bool Voided { get; set; }
        public override bool Processed { get; set; }
    }
}
