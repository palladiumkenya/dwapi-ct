using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IIITRiskScoresExtract : IExtract,IIITRiskScores
    {
        Guid PatientId { get; set; }
    }
}
