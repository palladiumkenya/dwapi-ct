using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Service;

namespace PalladiumDwh.Wapi.Controllers
{
    public class PatientLabController : ApiController
    {
          private readonly IPatientLabService _patientLabService;

          public PatientLabController(IPatientLabService patientLabService)
        {
            _patientLabService = patientLabService;
        }

        public async Task<IHttpActionResult> Get()
        {
            var response = await _patientLabService.GetPatientLabAsync();
            if (response.Success)
            {
                return Ok(response.PatientLaboratoryExtracts);
            }
            return InternalServerError(response.OperationException);
        }

        public async Task<IHttpActionResult> Post(PatientLaboratoryExtract patientLaboratoryExtract)
        {
            var response = await _patientLabService.PostPatientLabAsync(patientLaboratoryExtract);
            if (response.Success)
            {
                return Created(response.CreateMessage + patientLaboratoryExtract.PatientId, patientLaboratoryExtract);
            }
            return InternalServerError(response.OperationException);
        }
        public async Task<IHttpActionResult> Put(PatientLaboratoryExtract patientLaboratoryExtract, int id)
        {
            var response = await _patientLabService.PutPatientLabAsync(patientLaboratoryExtract, id);
            if (response.Success)
            {
                return Ok();
            }
            return InternalServerError(response.OperationException);
        }
        public async Task<IHttpActionResult> Delete(int id)
        {
            var response = await _patientLabService.DeletePatientLabAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            return InternalServerError(response.OperationException);
        }
    }
}
