using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public interface IPatientStatusRepository :IRepository<PatientStatusExtract, int>
    {

        int LoadFromIqTools(string iqToolsDb, string iqToolsServer);
        void PutComposite(int id, PatientStatusExtract entity);
    }
}
