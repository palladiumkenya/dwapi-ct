using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Repository;
using PalladiumDwh.Wapi.Service.PatientPharmacyResponse;
using PalladiumDwh.Wapi.Service.PatientStatusResponse;

namespace PalladiumDwh.Wapi.Service
{
    public class PatientStatusService : IPatientStatusService
    {
        private readonly IPatientStatusRepository _repository;

        public PatientStatusService(IPatientStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetStatusResponse> GetPatientStatusAsync()
        {
            return await Task.Run(() => GetPatientStatus());
        }

        private GetStatusResponse GetPatientStatus()
        {

            GetStatusResponse response = new GetStatusResponse();
            try
            {
                IEnumerable<PatientStatusExtract> patientStatusExtracts = _repository.Get();
                response.PatientStatusExtracts = patientStatusExtracts;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.OperationException = ex;
            }
            return response;
        }

        public async Task<PostStatusResponse> PostPatientStatusAsync(PatientStatusExtract patientStatusExtract)
        {
            return await Task.Run(() => PostPatientStatus(patientStatusExtract));
        }

        private PostStatusResponse PostPatientStatus(PatientStatusExtract patientStatusExtract)
        {
            PostStatusResponse response = new PostStatusResponse();
            try
            {
                _repository.Post(patientStatusExtract);
                response.CreateMessage = "Succesfully Uploaded Patient Status ";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.OperationException = ex;
            }
            return response;
        }

        public async Task<PutStatusResponse> PutPatientStatusAsync(PatientStatusExtract patientStatusExtract, int id)
        {
            return await Task.Run(() => PutPatientStatus(id, patientStatusExtract));
        }

        private PutStatusResponse PutPatientStatus(int id, PatientStatusExtract patientStatusExtract)
        {
            PutStatusResponse response = new PutStatusResponse();
            try
            {
                _repository.Put(id, patientStatusExtract);
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.OperationException = ex;
            }
            return response;
        }

        public async Task<DeleteStatusResponse> DeletePatientStatusAsync(int id)
        {
            return await Task.Run(() => DeletePatientStatus(id));
        }

        private DeleteStatusResponse DeletePatientStatus(int id)
        {
            DeleteStatusResponse response = new DeleteStatusResponse();
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