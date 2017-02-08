﻿using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class SyncPatientLaboratoryExtractDbCommand : SyncCommand<TempPatientLaboratoryExtract, ClientPatientLaboratoryExtract>, ISyncPatientLaboratoryExtractCommand
    {
      public SyncPatientLaboratoryExtractDbCommand(string connectionString) : base(connectionString)
      {
      }
  }
}
