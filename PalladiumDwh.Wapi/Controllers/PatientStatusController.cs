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
    public class PatientStatusController : ApiController
    {
        /// <summary>
        /// The _patient status service
        /// </summary>
         private readonly IPatientStatusService _patientStatusService;
         /// <summary>
         /// Initializes a new instance of the <see cref="PatientStatusController"/> class.
         /// </summary>
         /// <param name="patientStatusService">The patient status service.</param>
         public PatientStatusController(IPatientStatusService patientStatusService)
        {
            _patientStatusService = patientStatusService;
        }
         /// <summary>
         /// Gets this instance.
         /// </summary>
         /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            var response = await _patientStatusService.GetPatientStatusAsync();
            if (response.Success)
            {
                return Ok(response.PatientStatusExtracts);
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Posts the specified patient status extract.
        /// </summary>
        /// <param name="patientStatusExtract">The patient status extract.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(PatientStatusExtract patientStatusExtract)
        {
            var response = await _patientStatusService.PostPatientStatusAsync(patientStatusExtract);
            if (response.Success)
            {
                return Created(response.CreateMessage + patientStatusExtract.PatientId, patientStatusExtract);
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Puts the specified patient status extract.
        /// </summary>
        /// <param name="patientStatusExtract">The patient status extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(PatientStatusExtract patientStatusExtract, int id)
        {
            var response = await _patientStatusService.PutPatientStatusAsync(patientStatusExtract, id);
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
            var response = await _patientStatusService.DeletePatientStatusAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            return InternalServerError(response.OperationException);
        }
    }
}
