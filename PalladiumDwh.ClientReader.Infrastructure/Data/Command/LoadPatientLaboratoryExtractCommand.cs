﻿using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class LoadPatientLaboratoryExtractCommand : LoadExtractCommand<TempPatientLaboratoryExtract>, ILoadPatientLaboratoryExtractCommand
  {
      public LoadPatientLaboratoryExtractCommand(IEMRRepository emrRepository, int batchSize = 1000) : base(emrRepository, batchSize)
      {
      }
  }
}
