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
        public int PatientPID { get; set; }
        public string PatientCccNumber { get; set; }
        public int FacilityId { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }


        public ClientPatientStatusExtractDTO()
        {
        }

        public ClientPatientStatusExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, int patientPid, string patientCccNumber, int facilityId, string emr, string project)
        {
            ExitDescription = exitDescription;
            ExitDate = exitDate;
            ExitReason = exitReason;
            PatientPID = patientPid;
            PatientCccNumber = patientCccNumber;
            FacilityId = facilityId;
            Emr = emr;
            Project = project;
        }

        public ClientPatientStatusExtractDTO(ClientPatientStatusExtract extract)
        {
            PatientPID = extract.PatientPK; //TODO PatientPID = extract.PatientPK;
            PatientCccNumber = extract.PatientID; //TODO PatientCccNumber = extract.PatientID;
            FacilityId = extract.SiteCode; //TODO FacilityId = extract.SiteCode
            ExitDescription = extract.ExitDescription;
            ExitDate = extract.ExitDate;
            ExitReason = extract.ExitReason;
            Emr = extract.Emr;
            Project = extract.Project;
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

        public string TOVerified { get; set; }
        public DateTime? TOVerifiedDate { get; set; }
    }
}
