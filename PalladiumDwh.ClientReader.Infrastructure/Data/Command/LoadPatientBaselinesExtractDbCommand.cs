﻿using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class LoadPatientBaselinesExtractDbCommand : LoadExtractDbCommand<TempPatientBaselinesExtract>, ILoadPatientBaselinesExtractCommand
  {
      public LoadPatientBaselinesExtractDbCommand(IDbConnection sourceConnection, string commandText) : base(sourceConnection, commandText)
      {
      }
  }
}
