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
    public class PatientPharmacyController : ApiController
    {
        /// <summary>
        /// The _patient pharmacy service
        /// </summary>
         private readonly IPatientPharmacyService _patientPharmacyService;
         /// <summary>
         /// Initializes a new instance of the <see cref="PatientPharmacyController"/> class.
         /// </summary>
         /// <param name="patientPharmacyService">The patient pharmacy service.</param>
         public PatientPharmacyController(IPatientPharmacyService patientPharmacyService)
        {
            _patientPharmacyService = patientPharmacyService;
        }
         /// <summary>
         /// Gets this instance.
         /// </summary>
         /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            var response = await _patientPharmacyService.GetPatientPharmacyAsync();
            if (response.Success)
            {
                return Ok(response.PatientPharmacyExtracts);
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Posts the specified patient pharmacy extract.
        /// </summary>
        /// <param name="patientPharmacyExtract">The patient pharmacy extract.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(PatientPharmacyExtract patientPharmacyExtract)
        {
            var response = await _patientPharmacyService.PostPatientPharmacyAsync(patientPharmacyExtract);
            if (response.Success)
            {
                return Created(response.CreateMessage + patientPharmacyExtract.PatientId, patientPharmacyExtract);
            }
            return InternalServerError(response.OperationException);
        }
        /// <summary>
        /// Puts the specified patient pharmacy extract.
        /// </summary>
        /// <param name="patientPharmacyExtract">The patient pharmacy extract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(PatientPharmacyExtract patientPharmacyExtract, int id)
        {
            var response = await _patientPharmacyService.PutPatientPharmacyAsync(patientPharmacyExtract, id);
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
            var response = await _patientPharmacyService.DeletePatientPharmacyAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            return InternalServerError(response.OperationException);
        }
    }
}
