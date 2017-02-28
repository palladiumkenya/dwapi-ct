using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public interface IPatientExtractRepository : IRepository<PatientExtract, int>
    {
        
        int LoadFromIqTools(string iqToolsDb, string iqToolsServer);
        void PutComposite(int id,  int siteCode,PatientExtract entity);
    }
}
