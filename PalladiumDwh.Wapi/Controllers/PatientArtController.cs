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
    public class PatientArtController : ApiController
    {
        /// <summary>
        /// The _patient art service
        /// </summary>
        private readonly IPatientArtExtractService _patientArtService;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientArtController"/> class.
        /// </summary>
        /// <param name="patientArtService">The patient art service.</param>
        public PatientArtController(IPatientArtExtractService patientArtService)
        {
            _patientArtService = patientArtService;
        }
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            var response = await _patientArtService.GetArtPatientsAsync();
            if (response.Success)
            {
                return Ok(response.PatientArtExtracts);
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Posts the specified patient art extract.
        /// </summary>
        /// <param name="patientArtExtract">The patient art extract.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(PatientArtExtract patientArtExtract)
        {
            var response = await _patientArtService.PostArtPatientAsync(patientArtExtract);
            if (response.Success)
            {
                return Created(response.CreateMessage + patientArtExtract.PatientId, patientArtExtract);
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Puts the specified patient art extract.
        /// </summary>
        /// <param name="patientArtExtract">The patient art extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(PatientArtExtract patientArtExtract, int id)
        {
            var response = await _patientArtService.PutPatientAsync(patientArtExtract, id);
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
            var response = await _patientArtService.DeleteArtPatientAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            return InternalServerError(response.OperationException);
        }
    }
}
