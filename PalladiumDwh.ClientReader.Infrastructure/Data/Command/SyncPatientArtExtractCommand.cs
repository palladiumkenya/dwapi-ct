﻿using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class SyncPatientArtExtractCommand : SyncCommand<TempPatientArtExtract,ClientPatientArtExtract>, ISyncPatientArtExtractCommand
  {
      public SyncPatientArtExtractCommand(IEMRRepository emrRepository) : base(emrRepository)
      {
      }
  }
}
