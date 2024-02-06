using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IRelationshipsExtract : IExtract,IRelationships
    {
        Guid PatientId { get; set; }
    }
}
