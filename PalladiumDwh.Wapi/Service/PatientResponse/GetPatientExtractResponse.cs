using System;
using System.Collections.Generic;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Service.PatientResponse
{
    public class GetPatientExtractResponse
    {
        /// <summary>
        /// Gets or sets the patient extracts.
        /// </summary>
        /// <value>
        /// The patient extracts.
        /// </value>
        public IEnumerable<PatientExtract> PatientExtracts { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GetPatientExtractResponse"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }
        /// <summary>
        /// Gets or sets the operation exception.
        /// </summary>
        /// <value>
        /// The operation exception.
        /// </value>
        public Exception OperationException { get; set; }


    }
}