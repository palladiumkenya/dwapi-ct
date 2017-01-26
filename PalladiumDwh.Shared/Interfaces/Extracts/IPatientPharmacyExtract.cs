using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IPatientPharmacyExtract: IExtract,IPharmacy
    {
        Guid PatientId { get; set; }
    }
}