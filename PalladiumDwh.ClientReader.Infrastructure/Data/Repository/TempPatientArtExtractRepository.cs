﻿using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientArtExtractRepository : TempExtractRepository<TempPatientArtExtractError>, ITempPatientArtExtractRepository
    {
        public TempPatientArtExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}