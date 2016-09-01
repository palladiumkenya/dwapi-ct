using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Service.PatientResponse;

namespace PalladiumDwh.Wapi.Service
{
    public interface IPatientExtactService
    {
        /// <summary>
        /// Gets the patients asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<GetPatientExtractResponse> GetPatientsAsync();
        /// <summary>
        /// Posts the patient asynchronous.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        Task<PostPatientResponse> PostPatientAsync(PatientExtract patient);
        /// <summary>
        /// Puts the patient asynchronous.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<PutPatientResponse> PutPatientAsync(PatientExtract patient, int id);
        /// <summary>
        /// Deletes the patient asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<DeletePatientResponse> DeletePatientAsync( int id);
    }
}
