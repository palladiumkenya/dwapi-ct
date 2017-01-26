using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Extracts
{
    public interface IPatientVisitExtractRow : IExtractRow, IVisit
    {
        string FacilityName { get; set; }
    }
}