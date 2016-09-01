using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Repository;
using PalladiumDwh.Wapi.Service.PatientLabResponse;
using PalladiumDwh.Wapi.Service.PatientResponse;

namespace PalladiumDwh.Wapi.Service
{
    public class PatientLabService : IPatientLabService
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private readonly IPatientLabRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientLabService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public PatientLabService(IPatientLabRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Gets the patient lab asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<GetLabResponse> GetPatientLabAsync()
        {
            return await Task.Run(() => GetPatientLabs());
        }
        /// <summary>
        /// Gets the patient labs.
        /// </summary>
        /// <returns></returns>
        private GetLabResponse GetPatientLabs()
        {
            GetLabResponse response = new GetLabResponse();
            try
            {
                IEnumerable<PatientLaboratoryExtract> patientLaboratoryExtracts = _repository.Get();
                response.PatientLaboratoryExtracts = patientLaboratoryExtracts;
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
        /// Posts the patient lab asynchronous.
        /// </summary>
        /// <param name="patientLaboratoryExtract">The patient laboratory extract.</param>
        /// <returns></returns>
        public async Task<PostLabResponse> PostPatientLabAsync(PatientLaboratoryExtract patientLaboratoryExtract)
        {
            return await Task.Run(() => PostPatientLab(patientLaboratoryExtract));
        }
        /// <summary>
        /// Posts the patient lab.
        /// </summary>
        /// <param name="patientLaboratoryExtract">The patient laboratory extract.</param>
        /// <returns></returns>
        private PostLabResponse PostPatientLab(PatientLaboratoryExtract patientLaboratoryExtract)
        {
            PostLabResponse response = new PostLabResponse();
            try
            {
                _repository.Post(patientLaboratoryExtract);
                response.CreateMessage = "Succesfully Uploaded Patient Laboratory ";
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
        /// Puts the patient lab asynchronous.
        /// </summary>
        /// <param name="patientLaboratoryExtract">The patient laboratory extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<PutLabResponse> PutPatientLabAsync(PatientLaboratoryExtract patientLaboratoryExtract, int id)
        {
            return await Task.Run(() => PutPatientLab(id, patientLaboratoryExtract));
        }
        /// <summary>
        /// Puts the patient lab.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="patientLaboratoryExtract">The patient laboratory extract.</param>
        /// <returns></returns>
        private PutLabResponse PutPatientLab(int id, PatientLaboratoryExtract patientLaboratoryExtract)
        {
            PutLabResponse response = new PutLabResponse();
            try
            {
                _repository.Put(id, patientLaboratoryExtract);
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
        /// Deletes the patient lab asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DeleteLabResponse> DeletePatientLabAsync(int id)
        {
            return await Task.Run(() => DeleteLabPatient(id));
        }
        /// <summary>
        /// Deletes the lab patient.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private DeleteLabResponse DeleteLabPatient(int id)
        {
            DeleteLabResponse response = new DeleteLabResponse();
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