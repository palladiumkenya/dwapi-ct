namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialReview : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facility",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.PatientArtExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Processed = c.Boolean(nullable: false),
                        UId = c.Guid(nullable: false),
                        DOB = c.DateTime(),
                        AgeEnrollment = c.Decimal(precision: 18, scale: 2),
                        AgeARTStart = c.Decimal(precision: 18, scale: 2),
                        AgeLastVisit = c.Decimal(precision: 18, scale: 2),
                        RegistrationDate = c.DateTime(),
                        Gender = c.String(maxLength: 150),
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
                        Provider = c.String(maxLength: 150),
                        LastVisit = c.DateTime(),
                        ExitReason = c.String(maxLength: 150),
                        ExitDate = c.DateTime(),
                        PatientPK = c.Int(nullable: false),
                        SiteCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UId)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientBaselinesExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Processed = c.Boolean(nullable: false),
                        UId = c.Guid(nullable: false),
                        bCD4 = c.Int(),
                        bCD4Date = c.DateTime(),
                        bWAB = c.Int(),
                        bWABDate = c.DateTime(),
                        bWHO = c.Int(),
                        bWHODate = c.DateTime(),
                        eWAB = c.Int(),
                        eWABDate = c.DateTime(),
                        eCD4 = c.Int(),
                        eCD4Date = c.DateTime(),
                        eWHO = c.Int(),
                        eWHODate = c.DateTime(),
                        lastWHO = c.Int(),
                        lastWHODate = c.DateTime(),
                        lastCD4 = c.Int(),
                        lastCD4Date = c.DateTime(),
                        lastWAB = c.Int(),
                        lastWABDate = c.DateTime(),
                        m12CD4 = c.Int(),
                        m12CD4Date = c.DateTime(),
                        m6CD4 = c.Int(),
                        m6CD4Date = c.DateTime(),
                        PatientPK = c.Int(nullable: false),
                        SiteCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UId)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientExtract",
                c => new
                    {
                        PatientPK = c.Int(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Processed = c.Boolean(nullable: false),
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
                        StatusAtCCC = c.String(maxLength: 150),
                        StatusAtPMTCT = c.String(maxLength: 150),
                        StatusAtTBClinic = c.String(maxLength: 150),
                        UId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientLaboratoryExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Processed = c.Boolean(nullable: false),
                        UId = c.Guid(nullable: false),
                        VisitId = c.Int(),
                        OrderedByDate = c.DateTime(),
                        ReportedByDate = c.DateTime(),
                        TestName = c.String(maxLength: 150),
                        EnrollmentTest = c.Int(),
                        TestResult = c.String(maxLength: 150),
                        PatientPK = c.Int(nullable: false),
                        SiteCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UId)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientPharmacyExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Processed = c.Boolean(nullable: false),
                        UId = c.Guid(nullable: false),
                        VisitID = c.Int(),
                        Drug = c.String(maxLength: 150),
                        Provider = c.String(maxLength: 150),
                        DispenseDate = c.DateTime(),
                        Duration = c.Decimal(precision: 18, scale: 2),
                        ExpectedReturn = c.DateTime(),
                        TreatmentType = c.String(maxLength: 150),
                        RegimenLine = c.String(maxLength: 150),
                        PeriodTaken = c.String(maxLength: 150),
                        ProphylaxisType = c.String(maxLength: 150),
                        PatientPK = c.Int(nullable: false),
                        SiteCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UId)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientStatusExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Processed = c.Boolean(nullable: false),
                        UId = c.Guid(nullable: false),
                        ExitDescription = c.String(maxLength: 150),
                        ExitDate = c.DateTime(),
                        ExitReason = c.String(maxLength: 150),
                        PatientPK = c.Int(nullable: false),
                        SiteCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UId)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientVisitExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Processed = c.Boolean(nullable: false),
                        UId = c.Guid(nullable: false),
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
                        PatientPK = c.Int(nullable: false),
                        SiteCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UId)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.TempPatientArtExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        DOB = c.DateTime(),
                        AgeEnrollment = c.Decimal(precision: 18, scale: 2),
                        AgeARTStart = c.Decimal(precision: 18, scale: 2),
                        AgeLastVisit = c.Decimal(precision: 18, scale: 2),
                        RegistrationDate = c.DateTime(),
                        PatientSource = c.String(maxLength: 150),
                        Gender = c.String(maxLength: 150),
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
                        Provider = c.String(maxLength: 150),
                        LastVisit = c.DateTime(),
                        ExitReason = c.String(maxLength: 150),
                        ExitDate = c.DateTime(),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientBaselinesExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        bCD4 = c.Int(),
                        bCD4Date = c.DateTime(),
                        bWAB = c.Int(),
                        bWABDate = c.DateTime(),
                        bWHO = c.Int(),
                        bWHODate = c.DateTime(),
                        eWAB = c.Int(),
                        eWABDate = c.DateTime(),
                        eCD4 = c.Int(),
                        eCD4Date = c.DateTime(),
                        eWHO = c.Int(),
                        eWHODate = c.DateTime(),
                        lastWHO = c.Int(),
                        lastWHODate = c.DateTime(),
                        lastCD4 = c.Int(),
                        lastCD4Date = c.DateTime(),
                        lastWAB = c.Int(),
                        lastWABDate = c.DateTime(),
                        m12CD4 = c.Int(),
                        m12CD4Date = c.DateTime(),
                        m6CD4 = c.Int(),
                        m6CD4Date = c.DateTime(),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        SatelliteName = c.String(maxLength: 150),
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
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientLaboratoryExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        SatelliteName = c.String(maxLength: 150),
                        VisitId = c.Int(),
                        OrderedByDate = c.DateTime(),
                        ReportedByDate = c.DateTime(),
                        TestName = c.String(maxLength: 150),
                        EnrollmentTest = c.Int(),
                        TestResult = c.String(maxLength: 150),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientPharmacyExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitID = c.Int(),
                        Drug = c.String(maxLength: 150),
                        Provider = c.String(maxLength: 150),
                        DispenseDate = c.DateTime(),
                        Duration = c.Decimal(precision: 18, scale: 2),
                        ExpectedReturn = c.DateTime(),
                        TreatmentType = c.String(maxLength: 150),
                        RegimenLine = c.String(maxLength: 150),
                        PeriodTaken = c.String(maxLength: 150),
                        ProphylaxisType = c.String(maxLength: 150),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientStatusExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        ExitDescription = c.String(maxLength: 150),
                        ExitDate = c.DateTime(),
                        ExitReason = c.String(maxLength: 150),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientVisitExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
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
                        Adherence = c.String(maxLength: 150),
                        AdherenceCategory = c.String(maxLength: 150),
                        SubstitutionFirstlineRegimenDate = c.DateTime(),
                        SubstitutionFirstlineRegimenReason = c.String(maxLength: 150),
                        SubstitutionSecondlineRegimenDate = c.DateTime(),
                        SubstitutionSecondlineRegimenReason = c.String(maxLength: 150),
                        SecondlineRegimenChangeDate = c.DateTime(),
                        SecondlineRegimenChangeReason = c.String(maxLength: 150),
                        FamilyPlanningMethod = c.String(maxLength: 150),
                        PwP = c.String(maxLength: 150),
                        GestationAge = c.Decimal(precision: 18, scale: 2),
                        NextAppointmentDate = c.DateTime(),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientVisitExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropForeignKey("dbo.PatientStatusExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropForeignKey("dbo.PatientPharmacyExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropForeignKey("dbo.PatientLaboratoryExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropForeignKey("dbo.PatientBaselinesExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropForeignKey("dbo.PatientArtExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropIndex("dbo.PatientVisitExtract", new[] { "PatientPK", "SiteCode" });
            DropIndex("dbo.PatientStatusExtract", new[] { "PatientPK", "SiteCode" });
            DropIndex("dbo.PatientPharmacyExtract", new[] { "PatientPK", "SiteCode" });
            DropIndex("dbo.PatientLaboratoryExtract", new[] { "PatientPK", "SiteCode" });
            DropIndex("dbo.PatientBaselinesExtract", new[] { "PatientPK", "SiteCode" });
            DropIndex("dbo.PatientArtExtract", new[] { "PatientPK", "SiteCode" });
            DropTable("dbo.TempPatientVisitExtract");
            DropTable("dbo.TempPatientStatusExtract");
            DropTable("dbo.TempPatientPharmacyExtract");
            DropTable("dbo.TempPatientLaboratoryExtract");
            DropTable("dbo.TempPatientExtract");
            DropTable("dbo.TempPatientBaselinesExtract");
            DropTable("dbo.TempPatientArtExtract");
            DropTable("dbo.PatientVisitExtract");
            DropTable("dbo.PatientStatusExtract");
            DropTable("dbo.PatientPharmacyExtract");
            DropTable("dbo.PatientLaboratoryExtract");
            DropTable("dbo.PatientExtract");
            DropTable("dbo.PatientBaselinesExtract");
            DropTable("dbo.PatientArtExtract");
            DropTable("dbo.Facility");
        }
    }
}
