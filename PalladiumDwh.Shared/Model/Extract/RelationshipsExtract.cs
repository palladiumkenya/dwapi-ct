using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class RelationshipsExtract : Entity, IRelationshipsExtract
    {
        public string FacilityName { get; set; }

        public string RelationshipToPatient { get; set; }
        public int PersonAPatientPk { get; set; }
        public int PersonBPatientPk { get; set; }
        public string PatientRelationshipToOther { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }
        public DateTime? Date_Created  { get; set; }
        public DateTime? Date_Last_Modified  { get; set; }
        public DateTime? Created  { get; set; }
        public Guid PatientId { get; set; }


        public RelationshipsExtract()
        {
            Created = DateTime.Now;
        }

        public RelationshipsExtract(string facilityName, string relationshipToPatient,int personAPatientPk,int personBPatientPk,  string patientRelationshipToOther,DateTime?  startDate,DateTime?  endDate,
            Guid patientId, string emr, string project, DateTime? date_Created,DateTime? date_Last_Modified,string recordUUID,bool voided)
        {
            FacilityName = facilityName;
            RelationshipToPatient  = relationshipToPatient ;
            PersonAPatientPk = personAPatientPk;
            PersonBPatientPk = personBPatientPk;
            PatientRelationshipToOther = patientRelationshipToOther ;
            StartDate  = startDate ;
            EndDate  = endDate ;
            
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
