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
    public class PatientVisitController : ApiController
    {
        /// <summary>
        /// The _patient visit service
        /// </summary>
        private readonly IPatientVisitService _patientVisitService;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientVisitController"/> class.
        /// </summary>
        /// <param name="patientStatusService">The patient status service.</param>
        public PatientVisitController(IPatientVisitService patientStatusService)
        {
            _patientVisitService = patientStatusService;
        }
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            var response = await _patientVisitService.GetPatientVisitAsync();
            if (response.Success)
            {
                return Ok(response.PatientStatusExtracts);
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Posts the specified patient visit extract.
        /// </summary>
        /// <param name="patientVisitExtract">The patient visit extract.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(PatientVisitExtract patientVisitExtract)
        {
            var response = await _patientVisitService.PostPatientVisitAsync(patientVisitExtract);
            if (response.Success)
            {
                return Created(response.CreateMessage + patientVisitExtract.PatientId, patientVisitExtract);
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Puts the specified patient visit extract.
        /// </summary>
        /// <param name="patientVisitExtract">The patient visit extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(PatientVisitExtract patientVisitExtract, int id)
        {
            var response = await _patientVisitService.PutPatientVisitAsync(patientVisitExtract, id);
            if (response.Success)
            {
                return Ok();
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            var response = await _patientVisitService.DeletePatientVisitAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            return InternalServerError(response.OperationException);
        }
    }
}
