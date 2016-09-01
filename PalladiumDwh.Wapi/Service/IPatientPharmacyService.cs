using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Service.PatientPharmacyResponse;

namespace PalladiumDwh.Wapi.Service
{
    public interface IPatientPharmacyService
    {
        /// <summary>
        /// Gets the patient pharmacy asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<GetPharmacyResponse> GetPatientPharmacyAsync();
        /// <summary>
        /// Posts the patient pharmacy asynchronous.
        /// </summary>
        /// <param name="patientPharmacyExtract">The patient pharmacy extract.</param>
        /// <returns></returns>
        Task<PostPharmacyResponse> PostPatientPharmacyAsync(PatientPharmacyExtract patientPharmacyExtract);
        /// <summary>
        /// Puts the patient pharmacy asynchronous.
        /// </summary>
        /// <param name="patientPharmacyExtract">The patient pharmacy extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<PutPharmacyResponse> PutPatientPharmacyAsync(PatientPharmacyExtract patientPharmacyExtract, int id);
        /// <summary>
        /// Deletes the patient pharmacy asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<DeletePharmacyResponse> DeletePatientPharmacyAsync(int id);
    }
}
