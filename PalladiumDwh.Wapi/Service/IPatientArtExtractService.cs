using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Service.PatientArtResponse;


namespace PalladiumDwh.Wapi.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPatientArtExtractService
    {
        /// <summary>
        /// Gets the art patients asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<GetArtPatientResponse> GetArtPatientsAsync();
        /// <summary>
        /// Posts the art patient asynchronous.
        /// </summary>
        /// <param name="patientArt">The patient art.</param>
        /// <returns></returns>
        Task<PostArtPatientResponse> PostArtPatientAsync(PatientArtExtract patientArt);
        /// <summary>
        /// Puts the patient asynchronous.
        /// </summary>
        /// <param name="patientArt">The patient art.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<PutArtPatientResponse> PutPatientAsync(PatientArtExtract patientArt, int id);
        /// <summary>
        /// Deletes the art patient asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<DeleteArtPatientResponse> DeleteArtPatientAsync(int id);
    }
}
