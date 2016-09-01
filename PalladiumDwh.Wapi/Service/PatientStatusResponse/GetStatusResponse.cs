using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Service.PatientStatusResponse
{
    public class GetStatusResponse
    {
        /// <summary>
        /// Gets or sets the patient status extracts.
        /// </summary>
        /// <value>
        /// The patient status extracts.
        /// </value>
        public IEnumerable<PatientStatusExtract> PatientStatusExtracts { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GetStatusResponse"/> is success.
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