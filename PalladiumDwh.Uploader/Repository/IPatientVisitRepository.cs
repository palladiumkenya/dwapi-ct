using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public interface IPatientVisitRepository : IRepository<PatientVisitExtract, int>
    {

        int LoadFromIqTools(string iqToolsDb, string iqToolsServer);
        void PutComposite(int id,  PatientVisitExtract entity);
    }
}
