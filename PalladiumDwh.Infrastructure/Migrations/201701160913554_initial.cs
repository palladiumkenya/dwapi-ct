namespace PalladiumDwh.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facility",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.Int(nullable: false),
                        Name = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PatientCccNumber = c.String(maxLength: 150),
                        Gender = c.String(maxLength: 150),
                        DOB = c.DateTime(),
                        RegistrationDate = c.DateTime(),
                        RegistrationAtCCC = c.DateTime(),
                        RegistrationATPMTCT = c.DateTime(),
                        RegistrationAtTBClinic = c.DateTime(),
                        PatientSource = c.String(maxLength: 150),
                        Region = c.String(maxLength: 150),
                        District = c.String(maxLength: 150),
                        Village = c.String(maxLength: 150),
                        ContactRelation = c.String(maxLength: 150),
                        LastVisit = c.DateTime(),
                        MaritalStatus = c.String(maxLength: 150),
                        EducationLevel = c.String(maxLength: 150),
                        DateConfirmedHIVPositive = c.DateTime(),
                        PreviousARTExposure = c.String(maxLength: 150),
                        PreviousARTStartDate = c.DateTime(),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        FacilityId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facility", t => t.FacilityId, cascadeDelete: true)
                .Index(t => t.FacilityId);
            
            CreateTable(
                "dbo.PatientArtExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AgeEnrollment = c.Decimal(precision: 18, scale: 2),
                        AgeARTStart = c.Decimal(precision: 18, scale: 2),
                        AgeLastVisit = c.Decimal(precision: 18, scale: 2),
                        RegistrationDate = c.DateTime(),
                        PatientSource = c.String(maxLength: 150),
                        StartARTDate = c.DateTime(),
                        PreviousARTStartDate = c.DateTime(),
                        PreviousARTRegimen = c.String(maxLength: 150),
                        StartARTAtThisFacility = c.DateTime(),
                        StartRegimen = c.String(maxLength: 150),
                        StartRegimenLine = c.String(maxLength: 150),
                        LastARTDate = c.DateTime(),
                        LastRegimen = c.String(maxLength: 150),
                        LastRegimenLine = c.String(maxLength: 150),
                        Duration = c.Decimal(precision: 18, scale: 2),
                        ExpectedReturn = c.DateTime(),
                        LastVisit = c.DateTime(),
                        ExitReason = c.String(maxLength: 150),
                        ExitDate = c.DateTime(),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.PatientBaselinesExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        eCD4 = c.Int(),
                        eCD4Date = c.DateTime(),
                        eWHO = c.Int(),
                        eWHODate = c.DateTime(),
                        bCD4 = c.Int(),
                        bCD4Date = c.DateTime(),
                        bWHO = c.Int(),
                        bWHODate = c.DateTime(),
                        lastWHO = c.Int(),
                        lastWHODate = c.DateTime(),
                        lastCD4 = c.Int(),
                        lastCD4Date = c.DateTime(),
                        m12CD4 = c.Int(),
                        m12CD4Date = c.DateTime(),
                        m6CD4 = c.Int(),
                        m6CD4Date = c.DateTime(),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.PatientLaboratoryExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Int(),
                        OrderedByDate = c.DateTime(),
                        ReportedByDate = c.DateTime(),
                        TestName = c.String(maxLength: 150),
                        TestResult = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.PatientPharmacyExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitID = c.Int(),
                        Drug = c.String(maxLength: 150),
                        DispenseDate = c.DateTime(),
                        Duration = c.Decimal(precision: 18, scale: 2),
                        ExpectedReturn = c.String(maxLength: 150),
                        TreatmentType = c.String(maxLength: 150),
                        PeriodTaken = c.String(maxLength: 150),
                        ProphylaxisType = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Uploaded = c.Int(),
                        PatientId = c.Guid(nullable: false),
                        PatientExtractId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientExtractId)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.PatientExtractId);
            
            CreateTable(
                "dbo.PatientStatusExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExitDescription = c.String(maxLength: 150),
                        ExitDate = c.DateTime(),
                        ExitReason = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.PatientVisitExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Int(),
                        VisitDate = c.DateTime(),
                        Service = c.String(maxLength: 150),
                        VisitType = c.String(maxLength: 150),
                        WHOStage = c.Int(),
                        WABStage = c.String(maxLength: 150),
                        Pregnant = c.String(maxLength: 150),
                        LMP = c.DateTime(),
                        EDD = c.DateTime(),
                        Height = c.Decimal(precision: 18, scale: 2),
                        Weight = c.Decimal(precision: 18, scale: 2),
                        BP = c.String(maxLength: 150),
                        OI = c.String(maxLength: 150),
                        OIDate = c.DateTime(),
                        SubstitutionFirstlineRegimenDate = c.DateTime(),
                        SubstitutionFirstlineRegimenReason = c.String(maxLength: 150),
                        SubstitutionSecondlineRegimenDate = c.DateTime(),
                        SubstitutionSecondlineRegimenReason = c.String(maxLength: 150),
                        SecondlineRegimenChangeDate = c.DateTime(),
                        SecondlineRegimenChangeReason = c.String(maxLength: 150),
                        Adherence = c.String(maxLength: 150),
                        AdherenceCategory = c.String(maxLength: 150),
                        FamilyPlanningMethod = c.String(maxLength: 150),
                        PwP = c.String(maxLength: 150),
                        GestationAge = c.Decimal(precision: 18, scale: 2),
                        NextAppointmentDate = c.DateTime(),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientExtract", "FacilityId", "dbo.Facility");
            DropForeignKey("dbo.PatientVisitExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.PatientStatusExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.PatientPharmacyExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.PatientPharmacyExtract", "PatientExtractId", "dbo.PatientExtract");
            DropForeignKey("dbo.PatientLaboratoryExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.PatientBaselinesExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.PatientArtExtract", "PatientId", "dbo.PatientExtract");
            DropIndex("dbo.PatientVisitExtract", new[] { "PatientId" });
            DropIndex("dbo.PatientStatusExtract", new[] { "PatientId" });
            DropIndex("dbo.PatientPharmacyExtract", new[] { "PatientExtractId" });
            DropIndex("dbo.PatientPharmacyExtract", new[] { "PatientId" });
            DropIndex("dbo.PatientLaboratoryExtract", new[] { "PatientId" });
            DropIndex("dbo.PatientBaselinesExtract", new[] { "PatientId" });
            DropIndex("dbo.PatientArtExtract", new[] { "PatientId" });
            DropIndex("dbo.PatientExtract", new[] { "FacilityId" });
            DropTable("dbo.PatientVisitExtract");
            DropTable("dbo.PatientStatusExtract");
            DropTable("dbo.PatientPharmacyExtract");
            DropTable("dbo.PatientLaboratoryExtract");
            DropTable("dbo.PatientBaselinesExtract");
            DropTable("dbo.PatientArtExtract");
            DropTable("dbo.PatientExtract");
            DropTable("dbo.Facility");
        }
    }
}
