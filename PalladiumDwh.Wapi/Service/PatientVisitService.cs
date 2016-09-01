using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Repository;
using PalladiumDwh.Wapi.Service.PatientStatusResponse;
using PalladiumDwh.Wapi.Service.PatientVisitResponse;

namespace PalladiumDwh.Wapi.Service
{
    public class PatientVisitService : IPatientVisitService
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private readonly IPatientVisitRepository _repository;

        public PatientVisitService(IPatientVisitRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Gets the patient visit asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<GetVisitResponse> GetPatientVisitAsync()
        {
            return await Task.Run(() => GetPatientVists());
        }
        /// <summary>
        /// Gets the patient vists.
        /// </summary>
        /// <returns></returns>
        private GetVisitResponse GetPatientVists()
        {
            GetVisitResponse response = new GetVisitResponse();
            try
            {
                IEnumerable<PatientVisitExtract> patientVisitExtracts = _repository.Get();
                response.PatientStatusExtracts = patientVisitExtracts;
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
        /// Posts the patient visit asynchronous.
        /// </summary>
        /// <param name="patientVisitExtract">The patient visit extract.</param>
        /// <returns></returns>
        public async Task<PostVisitResponse> PostPatientVisitAsync(PatientVisitExtract patientVisitExtract)
        {
            return await Task.Run(() => PostPatientVisit(patientVisitExtract));
        }
        /// <summary>
        /// Posts the patient visit.
        /// </summary>
        /// <param name="patientVisitExtract">The patient visit extract.</param>
        /// <returns></returns>
        private PostVisitResponse PostPatientVisit(PatientVisitExtract patientVisitExtract)
        {
            PostVisitResponse response = new PostVisitResponse();
            try
            {
                _repository.Post(patientVisitExtract);
                response.CreateMessage = "Succesfully Uploaded Patient Visit ";
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
        /// Puts the patient visit asynchronous.
        /// </summary>
        /// <param name="patientVisitExtract">The patient visit extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<PutVisitResponse> PutPatientVisitAsync(PatientVisitExtract patientVisitExtract, int id)
        {
            return await Task.Run(() => PutPatientVisit(id, patientVisitExtract));
        }
        /// <summary>
        /// Puts the patient visit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="patientVisitExtract">The patient visit extract.</param>
        /// <returns></returns>
        private PutVisitResponse PutPatientVisit(int id, PatientVisitExtract patientVisitExtract)
        {
            PutVisitResponse response = new PutVisitResponse();
            try
            {
                _repository.Put(id, patientVisitExtract);
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
        /// Deletes the patient visit asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DeleteVisitResponse> DeletePatientVisitAsync(int id)
        {
            return await Task.Run(() => DeletePatientVisit(id));
        }
        /// <summary>
        /// Deletes the patient visit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private DeleteVisitResponse DeletePatientVisit(int id)
        {
            DeleteVisitResponse response = new DeleteVisitResponse();
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