using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IProfile
    {
        FacilityDTO Facility { get; set; }
        PatientExtractDTO Demographic { get; set; }
        Facility FacilityInfo { get; set; }
        PatientExtract PatientInfo { get; set; }
        void GeneratePatientRecord();
    }
}