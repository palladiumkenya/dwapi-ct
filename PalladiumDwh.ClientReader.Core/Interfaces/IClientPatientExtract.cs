using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IClientPatientExtract : IClientExtract, IPatient
    {
        string FacilityName { get; set; }
    }
}