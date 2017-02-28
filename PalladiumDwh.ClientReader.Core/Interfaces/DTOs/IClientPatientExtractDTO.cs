using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.DTOs
{
    public interface IClientPatientExtractDTO : IClientExtractDTO, IPatient
    {
        string FacilityName { get; set; }
    }
}