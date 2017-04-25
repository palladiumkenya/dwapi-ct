using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.ClientValidator.Core.Interfaces;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientValidator.Core.Model
{
    public class ValidationSummary : IValidationSummary
    {
        public Guid Id { get; set; }
        public Guid ValidatorId { get; set; }
        public Guid RecordId { get; set; }
        public DateTime DateGenerated { get; set; }
    }
}
