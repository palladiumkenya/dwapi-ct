﻿using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface ILoadPatientStatusExtractCommand : ILoadExtractCommand<TempPatientStatusExtract>
    {
    }
}