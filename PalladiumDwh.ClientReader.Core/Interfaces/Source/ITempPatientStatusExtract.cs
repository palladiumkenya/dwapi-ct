using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source
{
    public interface ITempPatientStatusExtract:ITempExtract,IStatus
    {
        string FacilityName { get; set; }
        string Emr { get; set; }
        string Project { get; set; }
    }
}