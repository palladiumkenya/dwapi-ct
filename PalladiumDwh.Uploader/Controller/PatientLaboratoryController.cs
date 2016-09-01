using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Repository;

namespace PalladiumDwh.Uploader.Controller
{
    public class PatientLaboratoryController
    {
        
        private readonly IPatientLaboratoryRepository _repository = null;

        public PatientLaboratoryController()
        {
            _repository = new PatientLaboratoryRepository(new DwhClientEntities());
        }

        public IEnumerable<PatientLaboratoryExtract> GetAllPatientLaboratoryExtracts()
        {
            return _repository.Get();
        }

        public void UpdateUploadedPatientLaboratory(int id,  PatientLaboratoryExtract entity)
        {
            _repository.PutComposite(id,  entity);
        }

        public int LoadFromIqTools(string iqtoolsDb, string iqToolsServer)
        {
            return _repository.LoadFromIqTools(iqtoolsDb, iqToolsServer);
        }
    }
}
