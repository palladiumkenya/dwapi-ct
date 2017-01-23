
using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientBaseLinesRepository : IRepository<PatientBaselinesExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientBaselinesExtract> patientArtExtracts);
    }
}
