using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
   public interface IPatientVisitRepository : IRepository<PatientVisitExtract>, IClearPatientRecords
   {
      void Sync(Guid patientIdValue, IEnumerable<PatientVisitExtract> profilePatientVisitExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<PatientVisitExtract> extracts);
      void SyncNew(List<PatientVisitProfile> profiles, IActionRegisterRepository actionRegisterRepository);

      void SyncNewPatients(IEnumerable<PatientVisitProfile> profiles, IFacilityRepository facilityRepository,
          List<Guid> facIds,IActionRegisterRepository actionRegisterRepository);
   }
}