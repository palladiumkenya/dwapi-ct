using System;
using PalladiumDwh.ClientValidator.Core.Interfaces;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientValidator.Core.Model
{
    public class Validator:IValidator
    {
        public Guid Id { get; set; }
        public string Extract { get; set; }
        public string Field { get; set; }
        public string Type { get; set; }
        public string Logic { get; set; }
        public string Summary { get; set; }       
    }
}