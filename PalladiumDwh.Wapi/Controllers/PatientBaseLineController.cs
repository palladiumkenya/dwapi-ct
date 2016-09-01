using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Service;

namespace PalladiumDwh.Wapi.Controllers
{
    public class PatientBaseLineController : ApiController
    {
        private readonly IPatientBaselineService _patientBaselineService;

        public PatientBaseLineController(IPatientBaselineService patientArtService)
        {
            _patientBaselineService = patientArtService;
        }

        public async Task<IHttpActionResult> Get()
        {
            var response = await _patientBaselineService.GetPatientBaselineAsync();
            if (response.Success)
            {
                return Ok(response.PatientBaselinesExtracts);
            }
            return InternalServerError(response.OperationException);
        }

        public async Task<IHttpActionResult> Post(PatientBaselinesExtract patientBaselineExtract)
        {
            var response = await _patientBaselineService.PostPatientBaselineAsync(patientBaselineExtract);
            if (response.Success)
            {
                return Created(response.CreateMessage + patientBaselineExtract.PatientId, patientBaselineExtract);
            }
            return InternalServerError(response.OperationException);
        }
        public async Task<IHttpActionResult> Put(PatientBaselinesExtract patientBaselinesExtract, int id)
        {
            var response = await _patientBaselineService.PutBaselinesAsync(patientBaselinesExtract, id);
            if (response.Success)
            {
                return Ok();
            }
            return InternalServerError(response.OperationException);
        }
        public async Task<IHttpActionResult> Delete(int id)
        {
            var response = await _patientBaselineService.DeleteBaselineAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            return InternalServerError(response.OperationException);
        }
    }
}
