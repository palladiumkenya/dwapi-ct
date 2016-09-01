using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Service.PatientBaselineResponse
{
    public class GetBaselineResponse
    {
        /// <summary>
        /// Gets or sets the patient baselines extracts.
        /// </summary>
        /// <value>
        /// The patient baselines extracts.
        /// </value>
        public IEnumerable<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GetBaselineResponse"/> is success.
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