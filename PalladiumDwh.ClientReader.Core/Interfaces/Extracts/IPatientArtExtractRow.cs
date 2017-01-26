using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Extracts
{
    public interface IPatientArtExtractRow: IExtractRow,IArt
    {
         string FacilityName { get; set; }

    }
}