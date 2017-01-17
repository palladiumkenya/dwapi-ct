
using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientArtExtractRepository : IRepository<PatientArtExtract>,IClearPatientRecords
    {
        void Sync(Guid patientId,IEnumerable<PatientArtExtract> extracts);
    }
}
