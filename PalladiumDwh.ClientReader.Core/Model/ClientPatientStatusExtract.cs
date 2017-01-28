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

        public ClientPatientStatusExtract()
        {
        }

        public ClientPatientStatusExtract(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project)
        {
            ExitDescription = exitDescription;
            ExitDate = exitDate;
            ExitReason = exitReason;
            Emr = emr;
            Project = project;
        }

        public ClientPatientStatusExtract(TempPatientStatusExtract extract)
        {
            ExitDescription = extract.ExitDescription;
            ExitDate = extract.ExitDate;
            ExitReason = extract.ExitReason;
            //TODO add to Status
            //Emr = extract.Emr;
            //Project = extract.Project;

        }
    }
}
