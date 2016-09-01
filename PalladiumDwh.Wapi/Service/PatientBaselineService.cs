using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Repository;
using PalladiumDwh.Wapi.Service.PatientArtResponse;
using PalladiumDwh.Wapi.Service.PatientBaselineResponse;

namespace PalladiumDwh.Wapi.Service
{
    public class PatientBaselineService : IPatientBaselineService
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private readonly IPatientBaseLineRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientBaselineService"/> class.
        /// </summary>
        /// <param name="patientBaseLinerepository">The patient base linerepository.</param>
        public PatientBaselineService(IPatientBaseLineRepository patientBaseLinerepository)
        {
            _repository = patientBaseLinerepository;
        }
        /// <summary>
        /// Gets the patient baseline asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<GetBaselineResponse> GetPatientBaselineAsync()
        {
            return await Task.Run(() => GetPatientBaselines());
        }
        /// <summary>
        /// Gets the patient baselines.
        /// </summary>
        /// <returns></returns>
        private GetBaselineResponse GetPatientBaselines()
        {
            GetBaselineResponse response = new GetBaselineResponse();
            try
            {
                IEnumerable<PatientBaselinesExtract> patientBaselineExtracts = _repository.Get();
                response.PatientBaselinesExtracts = patientBaselineExtracts;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.OperationException = ex;
            }
            return response;
        }
        /// <summary>
        /// Posts the patient baseline asynchronous.
        /// </summary>
        /// <param name="patientBaselineExtact">The patient baseline extact.</param>
        /// <returns></returns>
        public async Task<PostBaselineResponse> PostPatientBaselineAsync(PatientBaselinesExtract patientBaselineExtact)
        {
            return await Task.Run(() => PostPatientBaseline(patientBaselineExtact));
        }
        /// <summary>
        /// Posts the patient baseline.
        /// </summary>
        /// <param name="patientBaselineExtact">The patient baseline extact.</param>
        /// <returns></returns>
        private PostBaselineResponse PostPatientBaseline(PatientBaselinesExtract patientBaselineExtact)
        {
            PostBaselineResponse response = new PostBaselineResponse();
            try
            {
                _repository.Post(patientBaselineExtact);
                response.CreateMessage = "Succesfully Uploaded Baseline Patient";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.OperationException = ex;
            }
            return response;
        }
        /// <summary>
        /// Puts the baselines asynchronous.
        /// </summary>
        /// <param name="patientBaselinesExtract">The patient baselines extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<PutBaselineResponse> PutBaselinesAsync(PatientBaselinesExtract patientBaselinesExtract, int id)
        {
            return await Task.Run(() => PutPatientBaseline(id, patientBaselinesExtract));
        }
        /// <summary>
        /// Puts the patient baseline.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="patientBaselinesExtract">The patient baselines extract.</param>
        /// <returns></returns>
        private PutBaselineResponse PutPatientBaseline(int id, PatientBaselinesExtract patientBaselinesExtract)
        {
            PutBaselineResponse response = new PutBaselineResponse();
            try
            {
                _repository.Put(id, patientBaselinesExtract);
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.OperationException = ex;
            }
            return response;
        }
        /// <summary>
        /// Deletes the baseline asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DeleteBaseLineResponse> DeleteBaselineAsync(int id)
        {
            return await Task.Run(() => DeleteBaselinePatient(id));
        }
        /// <summary>
        /// Deletes the baseline patient.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private DeleteBaseLineResponse DeleteBaselinePatient(int id)
        {
            DeleteBaseLineResponse response = new DeleteBaseLineResponse();
            try
            {
                _repository.Delete(id);
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.OperationException = ex;
            }
            return response;
        }
    }
}