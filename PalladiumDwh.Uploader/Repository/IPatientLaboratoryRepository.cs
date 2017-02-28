using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
     public interface IPatientLaboratoryRepository : IRepository<PatientLaboratoryExtract, int>
     {

         int LoadFromIqTools(string iqToolsDb, string iqToolsServer);
         void PutComposite(int id, PatientLaboratoryExtract entity);
    }
}
