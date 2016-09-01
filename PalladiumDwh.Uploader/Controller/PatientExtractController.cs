using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Repository;

namespace PalladiumDwh.Uploader.Controller
{
    public class PatientExtractController
    {
        private readonly IPatientExtractRepository _repository = null;

        public PatientExtractController( )
        {
            _repository = new PatientExtractRepository(new DwhClientEntities());
        }
        public IEnumerable<PatientExtract> GetAllPatientExtracts()
        {
            return _repository.Get();
        }

        public void UpdateUploadedPatient(int id,  int siteCode,PatientExtract entity)
        {
            _repository.PutComposite(id, siteCode, entity);
        }

        public int LoadFromIqTools(string iqtoolsDb, string iqToolsServer)
        {
            return _repository.LoadFromIqTools(iqtoolsDb, iqToolsServer);
        }

    }
}
