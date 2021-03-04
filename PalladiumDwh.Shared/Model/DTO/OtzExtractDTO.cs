using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class OtzExtractDTO : IOtzExtractDTO
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? OTZEnrollmentDate { get; set; }
        public string TransferInStatus { get; set; }
        public string ModulesPreviouslyCovered { get; set; }
        public string ModulesCompletedToday { get; set; }
        public string SupportGroupInvolvement { get; set; }
        public string Remarks { get; set; }
        public string TransitionAttritionReason { get; set; }
        public DateTime? OutcomeDate { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public OtzExtractDTO()
        {
        }

        public OtzExtractDTO(OtzExtract OtzExtract)
        {
            FacilityName=OtzExtract.FacilityName;
            VisitID=OtzExtract.VisitID;
            VisitDate=OtzExtract.VisitDate;
            OTZEnrollmentDate=OtzExtract.OTZEnrollmentDate;
            TransferInStatus=OtzExtract.TransferInStatus;
            ModulesPreviouslyCovered=OtzExtract.ModulesPreviouslyCovered;
            ModulesCompletedToday=OtzExtract.ModulesCompletedToday;
            SupportGroupInvolvement=OtzExtract.SupportGroupInvolvement;
            Remarks=OtzExtract.Remarks;
            TransitionAttritionReason=OtzExtract.TransitionAttritionReason;
            OutcomeDate=OtzExtract.OutcomeDate;

            PatientId = OtzExtract.PatientId;
            Emr = OtzExtract.Emr;
            Project = OtzExtract.Project;
        }



        public IEnumerable<OtzExtractDTO> GenerateOtzExtractDtOs(IEnumerable<OtzExtract> extracts)
        {
            var statusExtractDtos = new List<OtzExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new OtzExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public OtzExtract GenerateOtzExtract(Guid patientId)
        {
            PatientId = patientId;
            // return new OtzExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
            return new OtzExtract(
                FacilityName,
                VisitID,
                VisitDate,
                OTZEnrollmentDate,
                TransferInStatus,
                ModulesPreviouslyCovered,
                ModulesCompletedToday,
                SupportGroupInvolvement,
                Remarks,
                TransitionAttritionReason,
                OutcomeDate,
                PatientId,Emr,Project
                );
        }
    }
}
