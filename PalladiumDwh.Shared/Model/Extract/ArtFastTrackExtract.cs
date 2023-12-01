using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class ArtFastTrackExtract : Entity, IArtFastTrackExtract
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
        public bool Voided { get; set; }
        public DateTime? Date_Created  { get; set; }
        public DateTime? Date_Last_Modified  { get; set; }
        public DateTime? Created  { get; set; }
        public Guid PatientId { get; set; }


        public ArtFastTrackExtract()
        {
            Created = DateTime.Now;
        }

        public ArtFastTrackExtract(string facilityName, string artRefillModel,DateTime?  visitDate,string ctxDispensed,string dapsoneDispensed,string condomsDistributed,string oralContraceptivesDispensed,string missedDoses,string fatigue,string cough,string fever,string rash,string nauseaOrVomiting,
            string genitalSoreOrDischarge,string diarrhea,string otherSymptoms,string pregnancyStatus,string fpStatus,string fpMethod,string reasonNotOnFP,string referredToClinic,DateTime?  returnVisitDate,
            Guid patientId, string emr, string project, DateTime? date_Created,DateTime? date_Last_Modified,string recordUUID,bool voided)
        {
            FacilityName = facilityName;
            ARTRefillModel  = artRefillModel ;
            VisitDate  = visitDate ;
            CTXDispensed  = ctxDispensed ;
            DapsoneDispensed  = dapsoneDispensed ;
            CondomsDistributed  = condomsDistributed ;
            OralContraceptivesDispensed  = oralContraceptivesDispensed ;
            MissedDoses  = missedDoses ;
            Fatigue  = fatigue ;
            Cough  = cough ;
            Fever  = fever ;
            Rash  = rash ;
            NauseaOrVomiting = nauseaOrVomiting;
            GenitalSoreOrDischarge  = genitalSoreOrDischarge ;
            Diarrhea  = diarrhea ;
            OtherSymptoms  = otherSymptoms ;
            PregnancyStatus  = pregnancyStatus ;
            FPStatus  = fpStatus ;
            FPMethod  = fpMethod ;
            ReasonNotOnFP  = reasonNotOnFP ;
            ReferredToClinic  = referredToClinic ;
            ReturnVisitDate          = returnVisitDate         ;
RecordUUID = recordUUID;
            Voided = voided;
            Date_Created  = date_Created ;
            Date_Last_Modified  = date_Last_Modified ;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
            this.StandardizeExtract();
        }
    }
}
