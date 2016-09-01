using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Repository;
using PalladiumDwh.Wapi.Service.PatientPharmacyResponse;

namespace PalladiumDwh.Wapi.Service
{
    public class PatientPharmacyService : IPatientPharmacyService
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private readonly IPatientPharmacyRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientPharmacyService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public PatientPharmacyService(IPatientPharmacyRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Gets the patient pharmacy asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<GetPharmacyResponse> GetPatientPharmacyAsync()
        {
            return await Task.Run(() => GetPatientPharmacy());
        }
        /// <summary>
        /// Gets the patient pharmacy.
        /// </summary>
        /// <returns></returns>
        private GetPharmacyResponse GetPatientPharmacy()
        {
            GetPharmacyResponse response = new GetPharmacyResponse();
            try
            {
                IEnumerable<PatientPharmacyExtract> patientPharmacyExtracts = _repository.Get();
                response.PatientPharmacyExtracts = patientPharmacyExtracts;
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
        /// Posts the patient pharmacy asynchronous.
        /// </summary>
        /// <param name="patientPharmacyExtract">The patient pharmacy extract.</param>
        /// <returns></returns>
        public async Task<PostPharmacyResponse> PostPatientPharmacyAsync(PatientPharmacyExtract patientPharmacyExtract)
        {
            return await Task.Run(() => PostPatientPharmacy(patientPharmacyExtract));
        }
        /// <summary>
        /// Posts the patient pharmacy.
        /// </summary>
        /// <param name="patientPharmacyExtract">The patient pharmacy extract.</param>
        /// <returns></returns>
        private PostPharmacyResponse PostPatientPharmacy(PatientPharmacyExtract patientPharmacyExtract)
        {
            PostPharmacyResponse response = new PostPharmacyResponse();
            try
            {
                _repository.Post(patientPharmacyExtract);
                response.CreateMessage = "Succesfully Uploaded Patient Pharmacy ";
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
        /// Puts the patient pharmacy asynchronous.
        /// </summary>
        /// <param name="patientPharmacyExtract">The patient pharmacy extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<PutPharmacyResponse> PutPatientPharmacyAsync(PatientPharmacyExtract patientPharmacyExtract, int id)
        {
            return await Task.Run(() => PutPatientPharmacy(id, patientPharmacyExtract));
        }
        /// <summary>
        /// Puts the patient pharmacy.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="patientPharmacyExtract">The patient pharmacy extract.</param>
        /// <returns></returns>
        private PutPharmacyResponse PutPatientPharmacy(int id, PatientPharmacyExtract patientPharmacyExtract)
        {
            PutPharmacyResponse response = new PutPharmacyResponse();
            try
            {
                _repository.Put(id, patientPharmacyExtract);
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
        /// Deletes the patient pharmacy asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DeletePharmacyResponse> DeletePatientPharmacyAsync(int id)
        {
            return await Task.Run(() => DeletePatientPharmacy(id));
        }
        /// <summary>
        /// Deletes the patient pharmacy.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private DeletePharmacyResponse DeletePatientPharmacy(int id)
        {
            DeletePharmacyResponse response = new DeletePharmacyResponse();
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