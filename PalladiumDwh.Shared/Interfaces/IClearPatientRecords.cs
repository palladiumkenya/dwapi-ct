using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IClearPatientRecords
    {
        void Clear(Guid patientId);
    }
}