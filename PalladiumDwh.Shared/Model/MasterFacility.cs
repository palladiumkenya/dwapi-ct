using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model
{
    public class MasterFacility
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }
        public string Name { get; set; }
        public string County { get; set; }

        public MasterFacility()
        {
        }
        public MasterFacility(int code, string name,string county)
        {
            Code = code;
            Name = name;
            County = county;
        }
        public override string ToString()
        {
            return $"{Code} - {Name} ({County})";
        }
    }
}
