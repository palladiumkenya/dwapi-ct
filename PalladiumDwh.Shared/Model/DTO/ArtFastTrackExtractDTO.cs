using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class ArtFastTrackExtractDTO : IArtFastTrackExtractDTO
    {
        public string FacilityName { get; set; }

        public string ARTRefillModel  { get; set; }
        public DateTime?  VisitDate  { get; set; }
        public string CTXDispensed  { get; set; }
        public string DapsoneDispensed  { get; set; }
        public string CondomsDistributed  { get; set; }
        public string OralContraceptivesDispensed  { get; set; }
        public string MissedDoses  { get; set; }
        public string Fatigue  { get; set; }
        public string Cough  { get; set; }
        public string Fever  { get; set; }
        public string Rash  { get; set; }
        public string NauseaOrVomiting { get; set; }
        public string GenitalSoreOrDischarge  { get; set; }
        public string Diarrhea  { get; set; }
        public string OtherSymptoms  { get; set; }
        public string PregnancyStatus  { get; set; }
        public string FPStatus  { get; set; }
        public string FPMethod  { get; set; }
        public string ReasonNotOnFP  { get; set; }
        public string ReferredToClinic  { get; set; }
        public DateTime?  ReturnVisitDate  { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
       

        public ArtFastTrackExtractDTO()
        {
        }

        public ArtFastTrackExtractDTO(ArtFastTrackExtract ArtFastTrackExtract)
        {
            FacilityName=ArtFastTrackExtract.FacilityName;
            FacilityName = ArtFastTrackExtract.FacilityName;
            ARTRefillModel  = ArtFastTrackExtract.ARTRefillModel ;
            VisitDate  = ArtFastTrackExtract.VisitDate ;
            CTXDispensed  = ArtFastTrackExtract.CTXDispensed ;
            DapsoneDispensed  = ArtFastTrackExtract.DapsoneDispensed ;
            CondomsDistributed  = ArtFastTrackExtract.CondomsDistributed ;
            OralContraceptivesDispensed  = ArtFastTrackExtract.OralContraceptivesDispensed ;
            MissedDoses  = ArtFastTrackExtract.MissedDoses ;
            Fatigue  = ArtFastTrackExtract.Fatigue ;
            Cough  = ArtFastTrackExtract.Cough ;
            Fever  = ArtFastTrackExtract.Fever ;
            Rash  = ArtFastTrackExtract.Rash ;
            NauseaOrVomiting = ArtFastTrackExtract.NauseaOrVomiting;
            GenitalSoreOrDischarge  = ArtFastTrackExtract.GenitalSoreOrDischarge;
            Diarrhea  = ArtFastTrackExtract.Diarrhea;
            OtherSymptoms  = ArtFastTrackExtract.OtherSymptoms;
            PregnancyStatus  = ArtFastTrackExtract.PregnancyStatus;
            FPStatus  = ArtFastTrackExtract.FPStatus;
            FPMethod  = ArtFastTrackExtract.FPMethod;
            ReasonNotOnFP  = ArtFastTrackExtract.ReasonNotOnFP;
            ReferredToClinic  = ArtFastTrackExtract.ReferredToClinic;
            ReturnVisitDate  = ArtFastTrackExtract.ReturnVisitDate;
            Voided = ArtFastTrackExtract.Voided;
            RecordUUID=ArtFastTrackExtract.RecordUUID;

            Emr=ArtFastTrackExtract.Emr;
            Project=ArtFastTrackExtract.Project;
            PatientId=ArtFastTrackExtract.PatientId;
            Date_Created=ArtFastTrackExtract.Date_Created;
            Date_Last_Modified=ArtFastTrackExtract.Date_Last_Modified;

        }

        public IEnumerable<ArtFastTrackExtractDTO> GenerateArtFastTrackExtractDtOs(IEnumerable<ArtFastTrackExtract> extracts)
        {
            var statusExtractDtos = new List<ArtFastTrackExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new ArtFastTrackExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public ArtFastTrackExtract GenerateArtFastTrackExtract(Guid patientId)
        {
            PatientId = patientId;
            return new ArtFastTrackExtract(
                FacilityName,
                ARTRefillModel ,
                VisitDate ,
                CTXDispensed ,
                DapsoneDispensed ,
                CondomsDistributed ,
                OralContraceptivesDispensed ,
                MissedDoses ,
                Fatigue ,
                Cough ,
                Fever ,
                Rash ,
                NauseaOrVomiting,
                GenitalSoreOrDischarge ,
                Diarrhea ,
                OtherSymptoms ,
                PregnancyStatus ,
                FPStatus ,
                FPMethod ,
                ReasonNotOnFP ,
                ReferredToClinic ,
                ReturnVisitDate,
                PatientId,
                Emr,
                Project,
                Date_Created,
                Date_Last_Modified,
                Voided,
                RecordUUID
            );
        }

    }
}
