using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IPatientPharmacyExtractDTO: IExtractDTO,IPharmacy
    {
        Guid PatientId { get; set; }
    }
}