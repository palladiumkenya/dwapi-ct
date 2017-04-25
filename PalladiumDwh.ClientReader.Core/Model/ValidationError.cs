using System;
using PalladiumDwh.ClientValidator.Core.Interfaces;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class ValidationError : IValidationError
    {
        public Guid Id { get; set; }
        public Guid ValidatorId { get; set; }
        public Guid RecordId { get; set; }
        public DateTime DateGenerated { get; set; }

        public ValidationError()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}
