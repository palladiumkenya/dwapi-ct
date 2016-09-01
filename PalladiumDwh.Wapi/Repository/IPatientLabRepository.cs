using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PalladiumDwh.Wapi.Repository.IRepository{PalladiumDwh.Wapi.Models.PatientLaboratoryExtract,System.Int32}" />
    public interface IPatientLabRepository : IRepository<PatientLaboratoryExtract,int>
    {
    }
}
