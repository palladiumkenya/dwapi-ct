using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Repository;

namespace PalladiumDwh.Uploader.Controller
{
    public class PatientPharmacyController 
    {
         private readonly IPatientPharamacyRepository _repository = null;

         public PatientPharmacyController()
        {
            _repository = new PatientPharmacyRepository(new DwhClientEntities());
        }

        public IEnumerable<PatientPharmacyExtract> GetAllPatientPharmacyExtracts()
        {
            return _repository.Get();
        }
        public void UpdateUploadedPatientPharmacy(int id,  PatientPharmacyExtract entity)
        {
            _repository.PutComposite(id,  entity);
        }
        public int LoadFromIqTools(string iqtoolsDb, string iqToolsServer)
        {
            return _repository.LoadFromIqTools(iqtoolsDb, iqToolsServer);
        }
    }
}
