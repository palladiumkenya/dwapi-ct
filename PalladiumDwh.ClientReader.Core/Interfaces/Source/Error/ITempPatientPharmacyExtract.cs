using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source.Error
{
    public interface ITempPatientPharmacyExtractError:ITempExtractError,IPharmacy
    {


        string Emr { get; set; }
        string Project { get; set; }
    }
}