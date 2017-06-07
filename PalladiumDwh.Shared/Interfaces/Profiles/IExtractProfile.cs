using System;
using System.Collections.Generic;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IExtractProfile<T> :IProfile
    {
        List<T> Extracts { get; set; }
        bool IsValid();
        bool HasData();
        void GenerateRecords(Guid patientId);
    }
}