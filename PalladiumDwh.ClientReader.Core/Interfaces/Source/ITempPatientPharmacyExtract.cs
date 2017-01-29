using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source
{
    public interface ITempPatientPharmacyExtract:ITempExtract,IPharmacy
    {


        string Emr { get; set; }
        string Project { get; set; }
    }
}