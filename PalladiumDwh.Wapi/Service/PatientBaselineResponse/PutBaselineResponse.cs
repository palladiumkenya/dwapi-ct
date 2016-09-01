using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PalladiumDwh.Wapi.Service.PatientBaselineResponse
{
    public class PutBaselineResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PutBaselineResponse"/> is success.
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