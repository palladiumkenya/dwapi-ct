
using System;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientPharmacyRepository : IRepository<PatientPharmacyExtract>, IClearPatientRecords
    {
        
    }
}
