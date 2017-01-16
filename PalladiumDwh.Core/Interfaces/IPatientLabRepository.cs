﻿using System;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientLabRepository : IRepository<PatientLaboratoryExtract>, IClearPatientRecords
    {
        
    }
}
