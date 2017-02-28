using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public interface IPatientPharamacyRepository : IRepository<PatientPharmacyExtract,int>
    {

        int LoadFromIqTools(string iqToolsDb, string iqToolsServer);
        void PutComposite(int id,  PatientPharmacyExtract entity);
    }
}
