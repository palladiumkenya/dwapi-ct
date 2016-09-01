using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Service.PatientBaselineResponse;


namespace PalladiumDwh.Wapi.Service
{
    public interface IPatientBaselineService
    {
        /// <summary>
        /// Gets the patient baseline asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<GetBaselineResponse> GetPatientBaselineAsync();
        /// <summary>
        /// Posts the patient baseline asynchronous.
        /// </summary>
        /// <param name="patientBaselineExtact">The patient baseline extact.</param>
        /// <returns></returns>
        Task<PostBaselineResponse> PostPatientBaselineAsync(PatientBaselinesExtract patientBaselineExtact);
        /// <summary>
        /// Puts the baselines asynchronous.
        /// </summary>
        /// <param name="patientBaselinesExtract">The patient baselines extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<PutBaselineResponse> PutBaselinesAsync(PatientBaselinesExtract patientBaselinesExtract, int id);
        /// <summary>
        /// Deletes the baseline asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<DeleteBaseLineResponse> DeleteBaselineAsync(int id);
    }
}
