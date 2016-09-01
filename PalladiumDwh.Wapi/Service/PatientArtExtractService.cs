using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Repository;
using PalladiumDwh.Wapi.Service.PatientArtResponse;

namespace PalladiumDwh.Wapi.Service
{
    public class PatientArtExtractService : IPatientArtExtractService
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private readonly IPatientArtExtractRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientArtExtractService"/> class.
        /// </summary>
        /// <param name="patientArtExtractrepository">The patient art extractrepository.</param>
        public PatientArtExtractService(IPatientArtExtractRepository patientArtExtractrepository)
        {
            _repository = patientArtExtractrepository;
        }
        /// <summary>
        /// Gets the art patients asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<GetArtPatientResponse> GetArtPatientsAsync()
        {
            return await Task.Run(() => GetArtPatients());
        }
        /// <summary>
        /// Gets the art patients.
        /// </summary>
        /// <returns></returns>
        private GetArtPatientResponse GetArtPatients()
        {
            GetArtPatientResponse response = new GetArtPatientResponse();
            try
            {
                IEnumerable<PatientArtExtract> patientArtExtracts = _repository.Get();
                response.PatientArtExtracts = patientArtExtracts;
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
        /// Posts the art patient asynchronous.
        /// </summary>
        /// <param name="patientArt">The patient art.</param>
        /// <returns></returns>
        public async Task<PostArtPatientResponse> PostArtPatientAsync(PatientArtExtract patientArt)
        {
            return await Task.Run(() => PostArtPatient(patientArt));
        }
        /// <summary>
        /// Posts the art patient.
        /// </summary>
        /// <param name="patientArt">The patient art.</param>
        /// <returns></returns>
        private PostArtPatientResponse PostArtPatient(PatientArtExtract patientArt)
        {
            PostArtPatientResponse response = new PostArtPatientResponse();
            try
            {
                _repository.Post(patientArt);
                response.CreateMessage = "Succesfully Uploaded Art Patient";
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
        /// Puts the patient asynchronous.
        /// </summary>
        /// <param name="patientArt">The patient art.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async  Task<PutArtPatientResponse> PutPatientAsync(PatientArtExtract patientArt, int id)
        {
            return await Task.Run(() => PutArtPatient(id, patientArt));
        }
        /// <summary>
        /// Puts the art patient.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="patientArt">The patient art.</param>
        /// <returns></returns>
        private PutArtPatientResponse PutArtPatient(int id, PatientArtExtract patientArt)
        {

            PutArtPatientResponse response = new PutArtPatientResponse();
            try
            {
                _repository.Put(id, patientArt);
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
        /// Deletes the art patient asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DeleteArtPatientResponse> DeleteArtPatientAsync(int id)
        {
            return await Task.Run(() => DeleteArtPatient(id));
        }
        /// <summary>
        /// Deletes the art patient.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private DeleteArtPatientResponse DeleteArtPatient(int id)
        {
            DeleteArtPatientResponse response = new DeleteArtPatientResponse();
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