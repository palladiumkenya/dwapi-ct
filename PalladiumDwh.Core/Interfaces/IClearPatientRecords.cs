using System;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IClearPatientRecords
    {
        void Clear(Guid patientId);
    }
}