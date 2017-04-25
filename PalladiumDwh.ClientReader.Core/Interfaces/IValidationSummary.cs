using System;

namespace PalladiumDwh.ClientValidator.Core.Interfaces
{
    public interface IValidationError
    {
        Guid Id { get; set; }
        Guid ValidatorId { get; set; }
        Guid RecordId { get; set; }                
        DateTime DateGenerated { get; set; }
    }
}