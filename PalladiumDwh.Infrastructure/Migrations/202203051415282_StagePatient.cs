namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StagePatient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StagePatientExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Pkv = c.String(maxLength: 150),
                        Occupation = c.String(maxLength: 150),
                        Gender = c.String(maxLength: 150),
                        DOB = c.DateTime(),
                        RegistrationDate = c.DateTime(),
                        RegistrationAtCCC = c.DateTime(),
                        RegistrationATPMTCT = c.DateTime(),
                        RegistrationAtTBClinic = c.DateTime(),
                        Region = c.String(maxLength: 150),
                        PatientSource = c.String(maxLength: 150),
                        District = c.String(maxLength: 150),
                        Village = c.String(maxLength: 150),
                        ContactRelation = c.String(maxLength: 150),
                        LastVisit = c.DateTime(),
                        MaritalStatus = c.String(maxLength: 150),
                        EducationLevel = c.String(maxLength: 150),
                        DateConfirmedHIVPositive = c.DateTime(),
                        PreviousARTExposure = c.String(maxLength: 150),
                        PreviousARTStartDate = c.DateTime(),
                        StatusAtCCC = c.String(maxLength: 150),
                        StatusAtPMTCT = c.String(maxLength: 150),
                        StatusAtTBClinic = c.String(maxLength: 150),
                        Orphan = c.String(maxLength: 150),
                        Inschool = c.String(maxLength: 150),
                        PatientType = c.String(maxLength: 150),
                        PopulationType = c.String(maxLength: 150),
                        KeyPopulationType = c.String(maxLength: 150),
                        PatientResidentCounty = c.String(maxLength: 150),
                        PatientResidentSubCounty = c.String(maxLength: 150),
                        PatientResidentLocation = c.String(maxLength: 150),
                        PatientResidentSubLocation = c.String(maxLength: 150),
                        PatientResidentWard = c.String(maxLength: 150),
                        PatientResidentVillage = c.String(maxLength: 150),
                        TransferInDate = c.DateTime(),
                        PatientPID = c.Int(nullable: false),
                        PatientCccNumber = c.String(maxLength: 150),
                        FacilityId = c.Guid(nullable: false),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PatientExtract", "Updated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientExtract", "Updated");
            DropTable("dbo.StagePatientExtract");
        }
    }
}
