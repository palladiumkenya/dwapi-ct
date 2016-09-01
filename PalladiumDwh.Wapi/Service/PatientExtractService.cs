using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Repository;
using PalladiumDwh.Wapi.Service.PatientResponse;

namespace PalladiumDwh.Wapi.Service
{
    public class PatientExtractService : IPatientExtactService
    {
        private readonly IPatientExtractRepository _repository;

        public PatientExtractService(IPatientExtractRepository patienteExtractrepository)
        {
           
            _repository = patienteExtractrepository;
        }
        /// <summary>
        /// Gets the patients asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<GetPatientExtractResponse> GetPatientsAsync()
        {
            return await Task.Run(() => GetPatients());
        }
        /// <summary>
        /// Gets the patients.
        /// </summary>
        /// <returns></returns>
        private GetPatientExtractResponse GetPatients()
        {
            GetPatientExtractResponse response = new GetPatientExtractResponse();
            try
            {
                IEnumerable<PatientExtract> patientExtracts = _repository.Get();
                response.PatientExtracts = patientExtracts;
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
        /// Posts the patient asynchronous.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        public async Task<PostPatientResponse> PostPatientAsync(PatientExtract patient)
        {
            return await Task.Run(() => PostPatient(patient));
        }
        /// <summary>
        /// Posts the patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        private PostPatientResponse PostPatient(PatientExtract patient)
        {
            PostPatientResponse response = new PostPatientResponse();
            try
            {
                _repository.Post(patient);
                response.CreateMessage = "Succesfully Uploaded Patient ";
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
        /// <param name="patient">The patient.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<PutPatientResponse> PutPatientAsync(PatientExtract patient, int id)
        {
            return await Task.Run(() => PutPatient(id, patient));
        }
        /// <summary>
        /// Puts the patient.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        private PutPatientResponse PutPatient(int id, PatientExtract patient)
        {
            PutPatientResponse response = new PutPatientResponse();
            try
            {
                _repository.Put(id, patient);
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
        /// Deletes the patient asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DeletePatientResponse> DeletePatientAsync(int id)
        {
            return await Task.Run(() => DeletePatient(id));
        }
        /// <summary>
        /// Deletes the patient.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public DeletePatientResponse DeletePatient( int id)
        {
            DeletePatientResponse response = new DeletePatientResponse();
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