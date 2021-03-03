using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IAllergiesChronicIllnessExtractDTO : IExtractDTO,IAllergiesChronicIllness
    {
        Guid PatientId { get; set; }
    }
}