using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model.DTO
{
    public interface IPatientPharmacyExtractDTO: IExtractDTO,IPharmacy
    {
        Guid PatientId { get; set; }
    }
}