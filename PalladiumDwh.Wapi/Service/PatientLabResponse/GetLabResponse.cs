using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Service.PatientLabResponse
{
    public class GetLabResponse
    {
        /// <summary>
        /// Gets or sets the patient laboratory extracts.
        /// </summary>
        /// <value>
        /// The patient laboratory extracts.
        /// </value>
        public IEnumerable<PatientLaboratoryExtract> PatientLaboratoryExtracts { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GetLabResponse"/> is success.
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