using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model
{
    public interface IPatientPharmacyExtract: IExtract,IPharmacy
    {
        Guid PatientId { get; set; }
    }
}