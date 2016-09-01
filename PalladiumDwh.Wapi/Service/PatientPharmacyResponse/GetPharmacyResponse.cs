using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Service.PatientPharmacyResponse
{
    public class GetPharmacyResponse
    {
        /// <summary>
        /// Gets or sets the patient pharmacy extracts.
        /// </summary>
        /// <value>
        /// The patient pharmacy extracts.
        /// </value>
        public IEnumerable<PatientPharmacyExtract> PatientPharmacyExtracts { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GetPharmacyResponse"/> is success.
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