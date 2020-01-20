using System;
using System.Collections.Generic;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IExtractProfile<T> :IProfile
    {
        //Guid ProfileId { get; }
        List<T> Extracts { get; set; }
        bool IsValid();
        bool HasData();
        void GeneratePatientRecord();
        void GenerateRecords(Guid patientId);
    }
}
