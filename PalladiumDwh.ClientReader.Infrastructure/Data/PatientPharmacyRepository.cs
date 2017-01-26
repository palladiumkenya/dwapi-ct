﻿using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class PatientPharmacyRepository : GenericRepository<PatientPharmacyExtract>,IPatientPharmacyRepository
    {
        private readonly DwapiRemoteContext _context;
        public PatientPharmacyRepository(DwapiRemoteContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
