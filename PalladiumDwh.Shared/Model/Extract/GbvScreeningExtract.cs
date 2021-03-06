using System;
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

        public GbvScreeningExtract()
        {
            Created = DateTime.Now;
        }

        public GbvScreeningExtract(string facilityName, int? visitId, DateTime? visitDate, string ipv, string physicalIpv, string emotionalIpv, string sexualIpv, string ipvRelationship,
            Guid patientId, string emr, string project)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            IPV = ipv;
            PhysicalIPV = physicalIpv;
            EmotionalIPV = emotionalIpv;
            SexualIPV = sexualIpv;
            IPVRelationship = ipvRelationship;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
        }
    }
}
