using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public  interface ICovidExtract : IExtract,ICovid
    {
        Guid PatientId { get; set; }
    }
}