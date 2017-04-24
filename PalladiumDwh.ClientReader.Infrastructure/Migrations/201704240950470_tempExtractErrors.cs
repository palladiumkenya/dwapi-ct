namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tempExtractErrors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TempPatientArtExtractError",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
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
                "dbo.TempPatientBaselinesExtractError",
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
                "dbo.TempPatientExtractError",
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
                "dbo.TempPatientLaboratoryExtractError",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        SatelliteName = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
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
                "dbo.TempPatientPharmacyExtractError",
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
                "dbo.TempPatientStatusExtractError",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
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
                "dbo.TempPatientVisitExtractError",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
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
            DropTable("dbo.TempPatientVisitExtractError");
            DropTable("dbo.TempPatientStatusExtractError");
            DropTable("dbo.TempPatientPharmacyExtractError");
            DropTable("dbo.TempPatientLaboratoryExtractError");
            DropTable("dbo.TempPatientExtractError");
            DropTable("dbo.TempPatientBaselinesExtractError");
            DropTable("dbo.TempPatientArtExtractError");
        }
    }
}
