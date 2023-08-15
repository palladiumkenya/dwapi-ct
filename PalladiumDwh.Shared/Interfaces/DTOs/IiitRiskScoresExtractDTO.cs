using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IIITRiskScoresExtractDTO : IExtractDTO,IIITRiskScores
    {
        Guid PatientId { get; set; }
    }
}