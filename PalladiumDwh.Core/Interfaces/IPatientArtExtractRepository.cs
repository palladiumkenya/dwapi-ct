
using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientArtExtractRepository : IRepository<PatientArtExtract>,IClearPatientRecords
    {
        void Sync(Guid patientId,IEnumerable<PatientArtExtract> extracts);
    }
}
