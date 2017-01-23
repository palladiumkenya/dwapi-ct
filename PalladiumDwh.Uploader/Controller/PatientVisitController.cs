using System.Collections.Generic;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Repository;

namespace PalladiumDwh.Uploader.Controller
{
    public class PatientVisitController

    {
        private readonly IPatientVisitRepository _repository = null;

        public PatientVisitController()
        {
            _repository = new PatientVisitRepository(new DwhClientEntities());
        }

        public IEnumerable<PatientVisitExtract> GetAllPatientVisitExtracts()
        {
            return _repository.Get();
        }
        public void UpdateUploadedPatientVisit(int id, PatientVisitExtract entity)
        {
            _repository.PutComposite(id,  entity);
        }
        public int LoadFromIqTools(string iqtoolsDb, string iqToolsServer)
        {
            return _repository.LoadFromIqTools(iqtoolsDb, iqToolsServer);
        }
    }
}
