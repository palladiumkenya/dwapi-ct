using System.Collections.Generic;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Repository;

namespace PalladiumDwh.Uploader.Controller
{
    public class PatientStatusController
    {
           private readonly IPatientStatusRepository _repository = null;

           public PatientStatusController()
        {
            _repository = new PatientStatusRepository(new DwhClientEntities());
        }

        public IEnumerable<PatientStatusExtract> GetAllPatientStatusExtracts()
        {
            return _repository.Get();
        }
        public void UpdateUploadedPatientStatus(int id,  PatientStatusExtract entity)
        {
            _repository.PutComposite(id,  entity);
        }
        public int LoadFromIqTools(string iqtoolsDb, string iqToolsServer)
        {
            return _repository.LoadFromIqTools(iqtoolsDb, iqToolsServer);
        }
    }
}
