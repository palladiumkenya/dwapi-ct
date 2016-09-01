using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PalladiumDwh.Wapi.Models;
using PalladiumDwh.Wapi.Repository;
using PalladiumDwh.Wapi.Service;

namespace PalladiumDwh.Wapi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class PatientController : ApiController
    {
        private readonly IPatientExtactService _patientExtactService;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientController"/> class.
        /// </summary>
        /// <param name="patientExtactService">The patient extact service.</param>
        public PatientController(IPatientExtactService patientExtactService)
        {
            _patientExtactService = patientExtactService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            var response = await _patientExtactService.GetPatientsAsync();
            if (response.Success)
            {
                return Ok(response.PatientExtracts);
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Posts the specified patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(PatientExtract patient)
        {
            var response = await _patientExtactService.PostPatientAsync(patient);
            if (response.Success)
            {
                return Created(response.CreateMessage + patient.Id,patient);
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Puts the specified patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(PatientExtract patient, int id)
        {
            var response = await _patientExtactService.PutPatientAsync(patient, id);
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
            var response = await _patientExtactService.DeletePatientAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            return InternalServerError(response.OperationException);
        }
    }

}
