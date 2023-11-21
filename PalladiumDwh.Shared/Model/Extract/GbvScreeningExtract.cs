using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class GbvScreeningExtract : Entity, IGbvScreeningExtract
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string IPV { get; set; }
        public string PhysicalIPV { get; set; }
        public string EmotionalIPV { get; set; }
        public string SexualIPV { get; set; }
        public string IPVRelationship { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }


        public GbvScreeningExtract()
        {
            Created = DateTime.Now;
        }

        public GbvScreeningExtract(string facilityName, int? visitId, DateTime? visitDate, string ipv, string physicalIpv, string emotionalIpv, string sexualIpv, string ipvRelationship,
            Guid patientId, string emr, string project, DateTime? date_Created,DateTime? date_Last_Modified,string recordUUID,bool voided)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            IPV = ipv;
            PhysicalIPV = physicalIpv;
            EmotionalIPV = emotionalIpv;
            SexualIPV = sexualIpv;
            IPVRelationship = ipvRelationship;
RecordUUID = recordUUID;
            Voided = voided;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
        }
    }
}
