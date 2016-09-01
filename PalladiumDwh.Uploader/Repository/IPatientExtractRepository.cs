using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public interface IPatientExtractRepository : IRepository<PatientExtract, int>
    {
        
        int LoadFromIqTools(string iqToolsDb, string iqToolsServer);
        void PutComposite(int id,  int siteCode,PatientExtract entity);
    }
}
