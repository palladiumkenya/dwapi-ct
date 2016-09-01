using System;

namespace PalladiumDwh.Wapi.Service.PatientResponse
{
    public class PutPatientResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PutPatientResponse"/> is success.
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