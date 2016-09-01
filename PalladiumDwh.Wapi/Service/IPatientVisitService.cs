using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Service.PatientStatusResponse;
using PalladiumDwh.Wapi.Service.PatientVisitResponse;

namespace PalladiumDwh.Wapi.Service
{
    public interface IPatientVisitService
    {
        /// <summary>
        /// Gets the patient visit asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<GetVisitResponse> GetPatientVisitAsync();
        /// <summary>
        /// Posts the patient visit asynchronous.
        /// </summary>
        /// <param name="patientVisitExtract">The patient visit extract.</param>
        /// <returns></returns>
        Task<PostVisitResponse> PostPatientVisitAsync(PatientVisitExtract patientVisitExtract);
        /// <summary>
        /// Puts the patient visit asynchronous.
        /// </summary>
        /// <param name="patientVisitExtract">The patient visit extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<PutVisitResponse> PutPatientVisitAsync(PatientVisitExtract patientVisitExtract, int id);
        /// <summary>
        /// Deletes the patient visit asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<DeleteVisitResponse> DeletePatientVisitAsync(int id);
    }
}
