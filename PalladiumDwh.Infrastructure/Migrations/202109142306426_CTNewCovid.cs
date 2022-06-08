namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CTNewCovid : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CovidExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        Covid19AssessmentDate = c.DateTime(),
                        ReceivedCOVID19Vaccine = c.String(maxLength: 150),
                        DateGivenFirstDose = c.DateTime(),
                        FirstDoseVaccineAdministered = c.String(maxLength: 150),
                        DateGivenSecondDose = c.DateTime(),
                        SecondDoseVaccineAdministered = c.String(maxLength: 150),
                        VaccinationStatus = c.String(maxLength: 150),
                        VaccineVerification = c.String(maxLength: 150),
                        BoosterGiven = c.String(maxLength: 150),
                        BoosterDose = c.String(maxLength: 150),
                        BoosterDoseDate = c.DateTime(),
                        EverCOVID19Positive = c.String(maxLength: 150),
                        COVID19TestDate = c.DateTime(),
                        PatientStatus = c.String(maxLength: 150),
                        AdmissionStatus = c.String(maxLength: 150),
                        AdmissionUnit = c.String(maxLength: 150),
                        MissedAppointmentDueToCOVID19 = c.String(maxLength: 150),
                        COVID19PositiveSinceLasVisit = c.String(maxLength: 150),
                        COVID19TestDateSinceLastVisit = c.DateTime(),
                        PatientStatusSinceLastVisit = c.String(maxLength: 150),
                        AdmissionStatusSinceLastVisit = c.String(maxLength: 150),
                        AdmissionStartDate = c.DateTime(),
                        AdmissionEndDate = c.DateTime(),
                        AdmissionUnitSinceLastVisit = c.String(maxLength: 150),
                        SupplementalOxygenReceived = c.String(maxLength: 150),
                        PatientVentilated = c.String(maxLength: 150),
                        TracingFinalOutcome = c.String(maxLength: 150),
                        CauseOfDeath = c.String(maxLength: 150),
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.DefaulterTracingExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        EncounterId = c.Int(),
                        TracingType = c.String(maxLength: 150),
                        TracingOutcome = c.String(maxLength: 150),
                        AttemptNumber = c.Int(),
                        IsFinalTrace = c.String(maxLength: 150),
                        TrueStatus = c.String(maxLength: 150),
                        CauseOfDeath = c.String(maxLength: 150),
                        Comments = c.String(maxLength: 150),
                        BookingDate = c.DateTime(),
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            AddColumn("dbo.PatientStatusExtract", "ReasonForDeath", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientStatusExtract", "SpecificDeathReason", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientStatusExtract", "DeathDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefaulterTracingExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.CovidExtract", "PatientId", "dbo.PatientExtract");
            DropIndex("dbo.DefaulterTracingExtract", new[] { "PatientId" });
            DropIndex("dbo.CovidExtract", new[] { "PatientId" });
            DropColumn("dbo.PatientStatusExtract", "DeathDate");
            DropColumn("dbo.PatientStatusExtract", "SpecificDeathReason");
            DropColumn("dbo.PatientStatusExtract", "ReasonForDeath");
            DropTable("dbo.DefaulterTracingExtract");
            DropTable("dbo.CovidExtract");
        }
    }
}
