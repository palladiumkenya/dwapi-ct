using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class CervicalCancerScreeningExtractDTO : ICervicalCancerScreeningExtractDTO
    {
        public string Emr { get; set; }
        public string Project { get; set; }
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string VisitType { get; set; }
        public string ScreeningMethod { get; set; }
        public string TreatmentToday { get; set; }
        public string ReferredOut { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string ScreeningType { get; set; }
        public string ScreeningResult { get; set; }
        public string PostTreatmentComplicationCause { get; set; }
        public string OtherPostTreatmentComplication { get; set; }
        public string ReferralReason { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public Guid PatientId { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }

        
        public CervicalCancerScreeningExtractDTO()
        {
        }

        public CervicalCancerScreeningExtractDTO(CervicalCancerScreeningExtract CervicalCancerScreeningExtract)
        {
            FacilityName=CervicalCancerScreeningExtract.FacilityName;
            VisitID=CervicalCancerScreeningExtract.VisitID;
            VisitDate=CervicalCancerScreeningExtract.VisitDate;
            
            VisitType = CervicalCancerScreeningExtract.VisitType;
            ScreeningMethod = CervicalCancerScreeningExtract.ScreeningMethod;
            TreatmentToday = CervicalCancerScreeningExtract.TreatmentToday;
            ReferredOut = CervicalCancerScreeningExtract.ReferredOut;
            NextAppointmentDate = CervicalCancerScreeningExtract.NextAppointmentDate;
            ScreeningType = CervicalCancerScreeningExtract.ScreeningType;
            ScreeningResult = CervicalCancerScreeningExtract.ScreeningResult;
            PostTreatmentComplicationCause = CervicalCancerScreeningExtract.PostTreatmentComplicationCause;
            OtherPostTreatmentComplication = CervicalCancerScreeningExtract.OtherPostTreatmentComplication;
            ReferralReason = CervicalCancerScreeningExtract.ReferralReason;
            
            Emr=CervicalCancerScreeningExtract.Emr;
            Project=CervicalCancerScreeningExtract.Project;
            PatientId=CervicalCancerScreeningExtract.PatientId;
            Date_Created=CervicalCancerScreeningExtract.Date_Created;
            Date_Last_Modified=CervicalCancerScreeningExtract.Date_Last_Modified;
            RecordUUID=CervicalCancerScreeningExtract.RecordUUID;


        }

        public IEnumerable<CervicalCancerScreeningExtractDTO> GenerateCervicalCancerScreeningExtractDtOs(IEnumerable<CervicalCancerScreeningExtract> extracts)
        {
            var statusExtractDtos = new List<CervicalCancerScreeningExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new CervicalCancerScreeningExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public CervicalCancerScreeningExtract GenerateCervicalCancerScreeningExtract(Guid patientId)
        {
            PatientId = patientId;
            return new CervicalCancerScreeningExtract(
                FacilityName,
                VisitID,
                VisitDate,
                VisitType,
                ScreeningMethod,
                TreatmentToday,
                ReferredOut,
                NextAppointmentDate,
                ScreeningType,
                ScreeningResult,
                PostTreatmentComplicationCause,
                OtherPostTreatmentComplication,
                ReferralReason,
                PatientId,
                Emr,
                Project,
                Date_Created,
                Date_Last_Modified,
                RecordUUID,
                Voided
            );
        }

    }
}