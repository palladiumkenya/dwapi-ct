using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Service.PatientLabResponse;

namespace PalladiumDwh.Wapi.Service
{
    public interface IPatientLabService
    {
        /// <summary>
        /// Gets the patient lab asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<GetLabResponse> GetPatientLabAsync();
        /// <summary>
        /// Posts the patient lab asynchronous.
        /// </summary>
        /// <param name="patientLaboratoryExtract">The patient laboratory extract.</param>
        /// <returns></returns>
        Task<PostLabResponse> PostPatientLabAsync(PatientLaboratoryExtract patientLaboratoryExtract);
        /// <summary>
        /// Puts the patient lab asynchronous.
        /// </summary>
        /// <param name="patientLaboratoryExtract">The patient laboratory extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<PutLabResponse> PutPatientLabAsync(PatientLaboratoryExtract patientLaboratoryExtract, int id);
        /// <summary>
        /// Deletes the patient lab asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<DeleteLabResponse> DeletePatientLabAsync(int id);
    }
}
