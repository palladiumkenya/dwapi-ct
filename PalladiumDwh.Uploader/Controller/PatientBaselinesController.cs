using System.Collections.Generic;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Repository;

namespace PalladiumDwh.Uploader.Controller
{
    public class PatientBaselinesController
    {
        private readonly IPatientBaselineRepository _repository = null;

        public PatientBaselinesController()
        {
            _repository = new PatientBaselineRepository(new DwhClientEntities());
        }
          public IEnumerable<PatientBaselinesExtract> GetAllBaselinePatientExtracts()
        {
            return _repository.Get();
        }
          public void UpdateUploadedPatientBaseline(int id, PatientBaselinesExtract entity)
          {
              _repository.PutComposite(id, entity);
          }

          public int LoadFromIqTools(string iqtoolsDb, string iqToolsServer)
          {
              return _repository.LoadFromIqTools(iqtoolsDb, iqToolsServer);
          }
    }
}
