﻿
using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientArtExtractRepository : IRepository<PatientArtExtract>,IClearPatientRecords
    {
        void Sync(Guid patientId,IEnumerable<PatientArtExtract> extracts);
    }
}