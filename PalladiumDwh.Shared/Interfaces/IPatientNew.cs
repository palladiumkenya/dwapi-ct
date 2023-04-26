using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IPatientNew
    {
        string Pkv { get; set; }
        string Occupation { get; set; }
        string NUPI { get; set; }
        DateTime? Date_Created { get; set; } 
        DateTime? Date_Last_Modified { get; set; } 
    }
}
