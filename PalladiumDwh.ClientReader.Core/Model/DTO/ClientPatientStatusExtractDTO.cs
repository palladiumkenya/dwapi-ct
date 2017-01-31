using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.DTOs;

namespace PalladiumDwh.ClientReader.Core.Model.DTO
{
    public class ClientPatientStatusExtractDTO : IClientPatientStatusExtractDTO
    {
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        public ClientPatientStatusExtractDTO()
        {
        }

        public ClientPatientStatusExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            ExitDescription = exitDescription;
            ExitDate = exitDate;
            ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public ClientPatientStatusExtractDTO(ClientPatientStatusExtract patientStatusExtract)
        {
            ExitDescription = patientStatusExtract.ExitDescription;
            ExitDate = patientStatusExtract.ExitDate;
            ExitReason = patientStatusExtract.ExitReason;
            Emr = patientStatusExtract.Emr;
            Project = patientStatusExtract.Project;
            //PatientId = patientStatusExtract.PatientId;
        }

     

        public IEnumerable<ClientPatientStatusExtractDTO> GeneratePatientStatusExtractDtOs(IEnumerable<ClientPatientStatusExtract> extracts)
        {
            var statusExtractDtos = new List<ClientPatientStatusExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new ClientPatientStatusExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public ClientPatientStatusExtract GeneratePatientStatusExtract(Guid patientId)
        {
            PatientId = patientId;
            return new ClientPatientStatusExtract();
//                ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
        }
    }
}
