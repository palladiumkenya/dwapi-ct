using System.Collections.Generic;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Repository;

namespace PalladiumDwh.Uploader.Controller
{
    public class PatientArtController
    {
        private readonly IPatientArtExtractRepository _repository = null;

        public PatientArtController()
        {
            _repository = new PatientArtRepository(new DwhClientEntities());
        }
        public IEnumerable<PatientArtExtract> GetAllArtPatientExtracts()
        {
            return _repository.Get();
        }
        public void UpdateUploadedPatientArt(int id, PatientArtExtract entity)
        {
            _repository.PutComposite(id, entity);
        }

        public int LoadFromIqTools(string iqtoolsDb, string iqToolsServer)
        {
            return _repository.LoadFromIqTools(iqtoolsDb, iqToolsServer);
        }
    }
}
