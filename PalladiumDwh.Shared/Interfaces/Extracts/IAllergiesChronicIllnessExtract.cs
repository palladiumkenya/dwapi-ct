using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IAllergiesChronicIllnessExtract : IExtract,IAllergiesChronicIllness
    {
        Guid PatientId { get; set; }
    }
}
