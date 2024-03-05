using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class RelationshipsExtractDTO : IRelationshipsExtractDTO
    {
        public string FacilityName { get; set; }

        public string RelationshipToPatient { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
       

        public RelationshipsExtractDTO()
        {
        }

        public RelationshipsExtractDTO(RelationshipsExtract RelationshipsExtract)
        {
            FacilityName=RelationshipsExtract.FacilityName;
            FacilityName = RelationshipsExtract.FacilityName;
            RelationshipToPatient  = RelationshipsExtract.RelationshipToPatient ;
            StartDate  = RelationshipsExtract.StartDate ;
            EndDate  = RelationshipsExtract.EndDate ;
            RecordUUID=RelationshipsExtract.RecordUUID;
            Voided=RelationshipsExtract.Voided;

            Emr=RelationshipsExtract.Emr;
            Project=RelationshipsExtract.Project;
            PatientId=RelationshipsExtract.PatientId;
            Date_Created=RelationshipsExtract.Date_Created;
            Date_Last_Modified=RelationshipsExtract.Date_Last_Modified;

        }

        public IEnumerable<RelationshipsExtractDTO> GenerateRelationshipsExtractDtOs(IEnumerable<RelationshipsExtract> extracts)
        {
            var statusExtractDtos = new List<RelationshipsExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new RelationshipsExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public RelationshipsExtract GenerateRelationshipsExtract(Guid patientId)
        {
            PatientId = patientId;
            return new RelationshipsExtract(
                FacilityName,
                RelationshipToPatient,
                StartDate,
                EndDate,
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
