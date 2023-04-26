using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class OvcExtractDTO : IOvcExtractDTO
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? OVCEnrollmentDate { get; set; }
        public string RelationshipToClient { get; set; }
        public string EnrolledinCPIMS { get; set; }
        public string CPIMSUniqueIdentifier { get; set; }
        public string PartnerOfferingOVCServices { get; set; }
        public string OVCExitReason { get; set; }
        public DateTime? ExitDate { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public OvcExtractDTO()
        {
        }

        public OvcExtractDTO(OvcExtract OvcExtract)
        {
            FacilityName=OvcExtract.FacilityName;
            VisitID=OvcExtract.VisitID;
            VisitDate=OvcExtract.VisitDate;
            OVCEnrollmentDate=OvcExtract.OVCEnrollmentDate;
            RelationshipToClient=OvcExtract.RelationshipToClient;
            EnrolledinCPIMS=OvcExtract.EnrolledinCPIMS;
            CPIMSUniqueIdentifier=OvcExtract.CPIMSUniqueIdentifier;
            PartnerOfferingOVCServices=OvcExtract.PartnerOfferingOVCServices;
            OVCExitReason=OvcExtract.OVCExitReason;
            ExitDate=OvcExtract.ExitDate;

            PatientId=OvcExtract.PatientId;
            Emr = OvcExtract.Emr;
            Project = OvcExtract.Project;
            Date_Created=OvcExtract.Date_Created;
            Date_Last_Modified=OvcExtract.Date_Last_Modified;
        }

        public IEnumerable<OvcExtractDTO> GenerateOvcExtractDtOs(IEnumerable<OvcExtract> extracts)
        {
            var statusExtractDtos = new List<OvcExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new OvcExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public OvcExtract GenerateOvcExtract(Guid patientId)
        {
            PatientId = patientId;
            return new OvcExtract(
                FacilityName,
                VisitID,
                VisitDate,
                OVCEnrollmentDate,
                RelationshipToClient,
                EnrolledinCPIMS,
                CPIMSUniqueIdentifier,
                PartnerOfferingOVCServices,
                OVCExitReason,
                ExitDate,
                PatientId,Emr,Project,
                Date_Created,
                Date_Last_Modified
                );
        }
    }
}
