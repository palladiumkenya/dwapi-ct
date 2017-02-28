using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.DTO;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.DTOs
{
    public interface IClientPatientBaselinesExtractDTO: IClientExtractDTO,IBaseline
    {
        IEnumerable<ClientPatientBaselinesExtractDTO> GeneratePatientBaselinesExtractDtOs(
            IEnumerable<ClientPatientBaselinesExtract> extracts);
    }
}