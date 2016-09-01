using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PalladiumDwh.Wapi.Models;
using StructureMap.AutoMocking;

namespace PalladiumDwh.Wapi.Service.PatientArtResponse
{
    public class GetArtPatientResponse
    {
        /// <summary>
        /// Gets or sets the patient art extracts.
        /// </summary>
        /// <value>
        /// The patient art extracts.
        /// </value>
        public IEnumerable<PatientArtExtract> PatientArtExtracts { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GetArtPatientResponse"/> is success.
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