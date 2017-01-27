using System.Collections.Generic;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IReadExtractService
    {
        IEnumerable<Facility> GetFacilityData();
        IEnumerable<PatientExtract> GetPatientData(IList<Facility> facilities);
        IEnumerable<PatientArtExtract> GetPatientArtData(PatientExtract patient);
        IEnumerable<PatientBaselinesExtract> GetPatientBaselineData(PatientExtract patient);
        IEnumerable<PatientLaboratoryExtract> GetPatientLabData(PatientExtract patient);
        IEnumerable<PatientPharmacyExtract> GetPatientPharmData(PatientExtract patient);
        IEnumerable<PatientStatusExtract> GetPatientStatusData(PatientExtract patient);
        IEnumerable<PatientVisitExtract> GetPatientVisitData(PatientExtract patient);
    }
}