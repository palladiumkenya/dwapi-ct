using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class GbvScreeningExtractDTO : IGbvScreeningExtractDTO
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string IPV { get; set; }
        public string PhysicalIPV { get; set; }
        public string EmotionalIPV { get; set; }
        public string SexualIPV { get; set; }
        public string IPVRelationship { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public GbvScreeningExtractDTO()
        {
        }

        public GbvScreeningExtractDTO(GbvScreeningExtract GbvScreeningExtract)
        {
            FacilityName=GbvScreeningExtract.FacilityName;
            VisitID=GbvScreeningExtract.VisitID;
            VisitDate=GbvScreeningExtract.VisitDate;
            IPV=GbvScreeningExtract.IPV;
            PhysicalIPV=GbvScreeningExtract.PhysicalIPV;
            EmotionalIPV=GbvScreeningExtract.EmotionalIPV;
            SexualIPV=GbvScreeningExtract.SexualIPV;
            IPVRelationship=GbvScreeningExtract.IPVRelationship;

            Emr = GbvScreeningExtract.Emr;
            Project = GbvScreeningExtract.Project;
            PatientId = GbvScreeningExtract.PatientId;
            Date_Created=GbvScreeningExtract.Date_Created;
            Date_Last_Modified=GbvScreeningExtract.Date_Last_Modified;
        }



        public IEnumerable<GbvScreeningExtractDTO> GenerateGbvScreeningExtractDtOs(IEnumerable<GbvScreeningExtract> extracts)
        {
            var statusExtractDtos = new List<GbvScreeningExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new GbvScreeningExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public GbvScreeningExtract GenerateGbvScreeningExtract(Guid patientId)
        {
            PatientId = patientId;
            return new GbvScreeningExtract(
                FacilityName,
                VisitID,
                VisitDate,
                IPV,
                PhysicalIPV,
                EmotionalIPV,
                SexualIPV,
                IPVRelationship,
                PatientId,
                Emr,Project,
                Date_Created,
                Date_Last_Modified);
        }
    }
}
