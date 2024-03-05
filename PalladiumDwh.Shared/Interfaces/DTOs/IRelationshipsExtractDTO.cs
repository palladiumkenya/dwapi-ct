using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IRelationshipsExtractDTO : IExtractDTO,IRelationships
    {
        Guid PatientId { get; set; }
    }
}