using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IReadExtracts
    {
        IEnumerable<Facility> GetFacilityData(IEnumerable<PatientExtractRow> patientExtractRows);
        IEnumerable<PatientExtract> GetPatientData(IList<Facility> facilities, IList<PatientExtractRow> patientExtractRows);
        IEnumerable<PatientArtExtract> GetPatientArtData(PatientExtract patient, IList<PatientArtExtractRow> patientExtractRows);
        IEnumerable<PatientBaselinesExtract> GetPatientBaselineData(PatientExtract patient, IList<PatientBaselinesExtractRow> patientExtractRows);
        IEnumerable<PatientLaboratoryExtract> GetPatientLabData(PatientExtract patient, IList<PatientLaboratoryExtractRow> patientExtractRows);
        IEnumerable<PatientPharmacyExtract> GetPatientPharmData(PatientExtract patient, IList<PatientPharmacyExtractRow> patientExtractRows);
        IEnumerable<PatientStatusExtract> GetPatientStatusData(PatientExtract patient, IList<PatientStatusExtractRow> patientExtractRows);
        IEnumerable<PatientVisitExtract> GetPatientVisitData(PatientExtract patient, IList<PatientVisitExtractRow> patientExtractRows);
    }
}