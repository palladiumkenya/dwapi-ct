using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Model
{
    [Table("PatientStatusExtract")]
    public class ClientPatientStatusExtract : ClientExtract, IClientPatientStatusExtract
    {
        [Key]
        public override Guid Id { get; set; }
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public string TOVerified { get; set; }
        public DateTime? TOVerifiedDate { get; set; }
        public DateTime? ReEnrollmentDate { get; set; }
        public string ReasonForDeath { get; set; }
        public string SpecificDeathReason { get; set; }
        public DateTime? DeathDate { get; set; }
        public DateTime? EffectiveDiscontinuationDate { get; set; }

        public ClientPatientStatusExtract()
        {
        }

        public ClientPatientStatusExtract(int patientPk, string patientId, int siteCode, string exitDescription, DateTime? exitDate, string exitReason, string emr, string project)
        {
            PatientPK = patientPk;
            PatientID = patientId;
            SiteCode = siteCode;
            ExitDescription = exitDescription;
            ExitDate = exitDate;
            ExitReason = exitReason;
            Emr = emr;
            Project = project;
        }

        public ClientPatientStatusExtract(TempPatientStatusExtract extract)
        {
            PatientPK = extract.PatientPK.Value;
            PatientID = extract.PatientID;
            SiteCode = extract.SiteCode.Value;
            ExitDescription = extract.ExitDescription;
            ExitDate = extract.ExitDate;
            ExitReason = extract.ExitReason;
            Emr = extract.Emr;
            Project = extract.Project;
        }


    }
}
