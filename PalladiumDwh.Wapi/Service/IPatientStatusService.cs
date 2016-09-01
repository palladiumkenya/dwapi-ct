using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Service.PatientPharmacyResponse;
using PalladiumDwh.Wapi.Service.PatientStatusResponse;

namespace PalladiumDwh.Wapi.Service
{
    public interface IPatientStatusService
    {
        /// <summary>
        /// Gets the patient status asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<GetStatusResponse> GetPatientStatusAsync();
        /// <summary>
        /// Posts the patient status asynchronous.
        /// </summary>
        /// <param name="patientStatusExtract">The patient status extract.</param>
        /// <returns></returns>
        Task<PostStatusResponse> PostPatientStatusAsync(PatientStatusExtract patientStatusExtract);
        /// <summary>
        /// Puts the patient status asynchronous.
        /// </summary>
        /// <param name="patientStatusExtract">The patient status extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<PutStatusResponse> PutPatientStatusAsync(PatientStatusExtract patientStatusExtract, int id);
        /// <summary>
        /// Deletes the patient status asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<DeleteStatusResponse> DeletePatientStatusAsync(int id);
    }
}
