using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public  interface ICovidExtractDTO : IExtractDTO,ICovid
    {
        Guid PatientId { get; set; }
    }
}