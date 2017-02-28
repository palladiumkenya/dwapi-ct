namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MysqlInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facility",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150, storeType: "nvarchar"),
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.PatientArtExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        Processed = c.Boolean(),
                        Id = c.Guid(nullable: false),
                        DOB = c.DateTime(precision: 0),
                        AgeEnrollment = c.Decimal(precision: 18, scale: 2),
                        AgeARTStart = c.Decimal(precision: 18, scale: 2),
                        AgeLastVisit = c.Decimal(precision: 18, scale: 2),
                        RegistrationDate = c.DateTime(precision: 0),
                        Gender = c.String(maxLength: 150, storeType: "nvarchar"),
                        PatientSource = c.String(maxLength: 150, storeType: "nvarchar"),
                        StartARTDate = c.DateTime(precision: 0),
                        PreviousARTStartDate = c.DateTime(precision: 0),
                        PreviousARTRegimen = c.String(maxLength: 150, storeType: "nvarchar"),
                        StartARTAtThisFacility = c.DateTime(precision: 0),
                        StartRegimen = c.String(maxLength: 150, storeType: "nvarchar"),
                        StartRegimenLine = c.String(maxLength: 150, storeType: "nvarchar"),
                        LastARTDate = c.DateTime(precision: 0),
                        LastRegimen = c.String(maxLength: 150, storeType: "nvarchar"),
                        LastRegimenLine = c.String(maxLength: 150, storeType: "nvarchar"),
                        Duration = c.Decimal(precision: 18, scale: 2),
                        ExpectedReturn = c.DateTime(precision: 0),
                        Provider = c.String(maxLength: 150, storeType: "nvarchar"),
                        LastVisit = c.DateTime(precision: 0),
                        ExitReason = c.String(maxLength: 150, storeType: "nvarchar"),
                        ExitDate = c.DateTime(precision: 0),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        SiteCode = c.Int(nullable: false),
                        QueueId = c.String(maxLength: 150, storeType: "nvarchar"),
                        Status = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientBaselinesExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        Processed = c.Boolean(),
                        Id = c.Guid(nullable: false),
                        bCD4 = c.Int(),
                        bCD4Date = c.DateTime(precision: 0),
                        bWAB = c.Int(),
                        bWABDate = c.DateTime(precision: 0),
                        bWHO = c.Int(),
                        bWHODate = c.DateTime(precision: 0),
                        eWAB = c.Int(),
                        eWABDate = c.DateTime(precision: 0),
                        eCD4 = c.Int(),
                        eCD4Date = c.DateTime(precision: 0),
                        eWHO = c.Int(),
                        eWHODate = c.DateTime(precision: 0),
                        lastWHO = c.Int(),
                        lastWHODate = c.DateTime(precision: 0),
                        lastCD4 = c.Int(),
                        lastCD4Date = c.DateTime(precision: 0),
                        lastWAB = c.Int(),
                        lastWABDate = c.DateTime(precision: 0),
                        m12CD4 = c.Int(),
                        m12CD4Date = c.DateTime(precision: 0),
                        m6CD4 = c.Int(),
                        m6CD4Date = c.DateTime(precision: 0),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        SiteCode = c.Int(nullable: false),
                        QueueId = c.String(maxLength: 150, storeType: "nvarchar"),
                        Status = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientExtract",
                c => new
                    {
                        PatientPK = c.Int(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        Processed = c.Boolean(),
                        FacilityName = c.String(maxLength: 150, storeType: "nvarchar"),
                        Gender = c.String(maxLength: 150, storeType: "nvarchar"),
                        DOB = c.DateTime(precision: 0),
                        RegistrationDate = c.DateTime(precision: 0),
                        RegistrationAtCCC = c.DateTime(precision: 0),
                        RegistrationATPMTCT = c.DateTime(precision: 0),
                        RegistrationAtTBClinic = c.DateTime(precision: 0),
                        PatientSource = c.String(maxLength: 150, storeType: "nvarchar"),
                        Region = c.String(maxLength: 150, storeType: "nvarchar"),
                        District = c.String(maxLength: 150, storeType: "nvarchar"),
                        Village = c.String(maxLength: 150, storeType: "nvarchar"),
                        ContactRelation = c.String(maxLength: 150, storeType: "nvarchar"),
                        LastVisit = c.DateTime(precision: 0),
                        MaritalStatus = c.String(maxLength: 150, storeType: "nvarchar"),
                        EducationLevel = c.String(maxLength: 150, storeType: "nvarchar"),
                        DateConfirmedHIVPositive = c.DateTime(precision: 0),
                        PreviousARTExposure = c.String(maxLength: 150, storeType: "nvarchar"),
                        PreviousARTStartDate = c.DateTime(precision: 0),
                        StatusAtCCC = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusAtPMTCT = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusAtTBClinic = c.String(maxLength: 150, storeType: "nvarchar"),
                        Id = c.Guid(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        QueueId = c.String(maxLength: 150, storeType: "nvarchar"),
                        Status = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientLaboratoryExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        Processed = c.Boolean(),
                        Id = c.Guid(nullable: false),
                        VisitId = c.Int(),
                        OrderedByDate = c.DateTime(precision: 0),
                        ReportedByDate = c.DateTime(precision: 0),
                        TestName = c.String(maxLength: 150, storeType: "nvarchar"),
                        EnrollmentTest = c.Int(),
                        TestResult = c.String(maxLength: 150, storeType: "nvarchar"),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        SiteCode = c.Int(nullable: false),
                        QueueId = c.String(maxLength: 150, storeType: "nvarchar"),
                        Status = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientPharmacyExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        Processed = c.Boolean(),
                        Id = c.Guid(nullable: false),
                        VisitID = c.Int(),
                        Drug = c.String(maxLength: 150, storeType: "nvarchar"),
                        Provider = c.String(maxLength: 150, storeType: "nvarchar"),
                        DispenseDate = c.DateTime(precision: 0),
                        Duration = c.Decimal(precision: 18, scale: 2),
                        ExpectedReturn = c.DateTime(precision: 0),
                        TreatmentType = c.String(maxLength: 150, storeType: "nvarchar"),
                        RegimenLine = c.String(maxLength: 150, storeType: "nvarchar"),
                        PeriodTaken = c.String(maxLength: 150, storeType: "nvarchar"),
                        ProphylaxisType = c.String(maxLength: 150, storeType: "nvarchar"),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        SiteCode = c.Int(nullable: false),
                        QueueId = c.String(maxLength: 150, storeType: "nvarchar"),
                        Status = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientStatusExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        Processed = c.Boolean(),
                        Id = c.Guid(nullable: false),
                        ExitDescription = c.String(maxLength: 150, storeType: "nvarchar"),
                        ExitDate = c.DateTime(precision: 0),
                        ExitReason = c.String(maxLength: 150, storeType: "nvarchar"),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        SiteCode = c.Int(nullable: false),
                        QueueId = c.String(maxLength: 150, storeType: "nvarchar"),
                        Status = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.PatientVisitExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        Processed = c.Boolean(),
                        Id = c.Guid(nullable: false),
                        VisitId = c.Int(),
                        VisitDate = c.DateTime(precision: 0),
                        Service = c.String(maxLength: 150, storeType: "nvarchar"),
                        VisitType = c.String(maxLength: 150, storeType: "nvarchar"),
                        WHOStage = c.Int(),
                        WABStage = c.String(maxLength: 150, storeType: "nvarchar"),
                        Pregnant = c.String(maxLength: 150, storeType: "nvarchar"),
                        LMP = c.DateTime(precision: 0),
                        EDD = c.DateTime(precision: 0),
                        Height = c.Decimal(precision: 18, scale: 2),
                        Weight = c.Decimal(precision: 18, scale: 2),
                        BP = c.String(maxLength: 150, storeType: "nvarchar"),
                        OI = c.String(maxLength: 150, storeType: "nvarchar"),
                        OIDate = c.DateTime(precision: 0),
                        SubstitutionFirstlineRegimenDate = c.DateTime(precision: 0),
                        SubstitutionFirstlineRegimenReason = c.String(maxLength: 150, storeType: "nvarchar"),
                        SubstitutionSecondlineRegimenDate = c.DateTime(precision: 0),
                        SubstitutionSecondlineRegimenReason = c.String(maxLength: 150, storeType: "nvarchar"),
                        SecondlineRegimenChangeDate = c.DateTime(precision: 0),
                        SecondlineRegimenChangeReason = c.String(maxLength: 150, storeType: "nvarchar"),
                        Adherence = c.String(maxLength: 150, storeType: "nvarchar"),
                        AdherenceCategory = c.String(maxLength: 150, storeType: "nvarchar"),
                        FamilyPlanningMethod = c.String(maxLength: 150, storeType: "nvarchar"),
                        PwP = c.String(maxLength: 150, storeType: "nvarchar"),
                        GestationAge = c.Decimal(precision: 18, scale: 2),
                        NextAppointmentDate = c.DateTime(precision: 0),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        SiteCode = c.Int(nullable: false),
                        QueueId = c.String(maxLength: 150, storeType: "nvarchar"),
                        Status = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => new { t.PatientPK, t.SiteCode }, cascadeDelete: true)
                .Index(t => new { t.PatientPK, t.SiteCode });
            
            CreateTable(
                "dbo.EMR",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 150, storeType: "nvarchar"),
                        Version = c.String(maxLength: 150, storeType: "nvarchar"),
                        ConnectionKey = c.String(maxLength: 150, storeType: "nvarchar"),
                        IsDefault = c.Boolean(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.ExtractSetting",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 150, storeType: "nvarchar"),
                        Display = c.String(maxLength: 150, storeType: "nvarchar"),
                        ExtractCsv = c.String(maxLength: 150, storeType: "nvarchar"),
                        ExtractSql = c.String(maxLength: 8000, storeType: "nvarchar"),
                        Destination = c.String(maxLength: 150, storeType: "nvarchar"),
                        Rank = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        IsPriority = c.Boolean(nullable: false),
                        EmrId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMR", t => t.EmrId, cascadeDelete: true)
                .Index(t => t.EmrId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(maxLength: 150, storeType: "nvarchar"),
                        Name = c.String(maxLength: 150, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientArtExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150, storeType: "nvarchar"),
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        DOB = c.DateTime(precision: 0),
                        AgeEnrollment = c.Decimal(precision: 18, scale: 2),
                        AgeARTStart = c.Decimal(precision: 18, scale: 2),
                        AgeLastVisit = c.Decimal(precision: 18, scale: 2),
                        RegistrationDate = c.DateTime(precision: 0),
                        PatientSource = c.String(maxLength: 150, storeType: "nvarchar"),
                        Gender = c.String(maxLength: 150, storeType: "nvarchar"),
                        StartARTDate = c.DateTime(precision: 0),
                        PreviousARTStartDate = c.DateTime(precision: 0),
                        PreviousARTRegimen = c.String(maxLength: 150, storeType: "nvarchar"),
                        StartARTAtThisFacility = c.DateTime(precision: 0),
                        StartRegimen = c.String(maxLength: 150, storeType: "nvarchar"),
                        StartRegimenLine = c.String(maxLength: 150, storeType: "nvarchar"),
                        LastARTDate = c.DateTime(precision: 0),
                        LastRegimen = c.String(maxLength: 150, storeType: "nvarchar"),
                        LastRegimenLine = c.String(maxLength: 150, storeType: "nvarchar"),
                        Duration = c.Decimal(precision: 18, scale: 2),
                        ExpectedReturn = c.DateTime(precision: 0),
                        Provider = c.String(maxLength: 150, storeType: "nvarchar"),
                        LastVisit = c.DateTime(precision: 0),
                        ExitReason = c.String(maxLength: 150, storeType: "nvarchar"),
                        ExitDate = c.DateTime(precision: 0),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientBaselinesExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        bCD4 = c.Int(),
                        bCD4Date = c.DateTime(precision: 0),
                        bWAB = c.Int(),
                        bWABDate = c.DateTime(precision: 0),
                        bWHO = c.Int(),
                        bWHODate = c.DateTime(precision: 0),
                        eWAB = c.Int(),
                        eWABDate = c.DateTime(precision: 0),
                        eCD4 = c.Int(),
                        eCD4Date = c.DateTime(precision: 0),
                        eWHO = c.Int(),
                        eWHODate = c.DateTime(precision: 0),
                        lastWHO = c.Int(),
                        lastWHODate = c.DateTime(precision: 0),
                        lastCD4 = c.Int(),
                        lastCD4Date = c.DateTime(precision: 0),
                        lastWAB = c.Int(),
                        lastWABDate = c.DateTime(precision: 0),
                        m12CD4 = c.Int(),
                        m12CD4Date = c.DateTime(precision: 0),
                        m6CD4 = c.Int(),
                        m6CD4Date = c.DateTime(precision: 0),
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150, storeType: "nvarchar"),
                        SatelliteName = c.String(maxLength: 150, storeType: "nvarchar"),
                        Gender = c.String(maxLength: 150, storeType: "nvarchar"),
                        DOB = c.DateTime(precision: 0),
                        RegistrationDate = c.DateTime(precision: 0),
                        RegistrationAtCCC = c.DateTime(precision: 0),
                        RegistrationATPMTCT = c.DateTime(precision: 0),
                        RegistrationAtTBClinic = c.DateTime(precision: 0),
                        Region = c.String(maxLength: 150, storeType: "nvarchar"),
                        PatientSource = c.String(maxLength: 150, storeType: "nvarchar"),
                        District = c.String(maxLength: 150, storeType: "nvarchar"),
                        Village = c.String(maxLength: 150, storeType: "nvarchar"),
                        ContactRelation = c.String(maxLength: 150, storeType: "nvarchar"),
                        LastVisit = c.DateTime(precision: 0),
                        MaritalStatus = c.String(maxLength: 150, storeType: "nvarchar"),
                        EducationLevel = c.String(maxLength: 150, storeType: "nvarchar"),
                        DateConfirmedHIVPositive = c.DateTime(precision: 0),
                        PreviousARTExposure = c.String(maxLength: 150, storeType: "nvarchar"),
                        PreviousARTStartDate = c.DateTime(precision: 0),
                        StatusAtCCC = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusAtPMTCT = c.String(maxLength: 150, storeType: "nvarchar"),
                        StatusAtTBClinic = c.String(maxLength: 150, storeType: "nvarchar"),
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientLaboratoryExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150, storeType: "nvarchar"),
                        SatelliteName = c.String(maxLength: 150, storeType: "nvarchar"),
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        VisitId = c.Int(),
                        OrderedByDate = c.DateTime(precision: 0),
                        ReportedByDate = c.DateTime(precision: 0),
                        TestName = c.String(maxLength: 150, storeType: "nvarchar"),
                        EnrollmentTest = c.Int(),
                        TestResult = c.String(maxLength: 150, storeType: "nvarchar"),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientPharmacyExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitID = c.Int(),
                        Drug = c.String(maxLength: 150, storeType: "nvarchar"),
                        Provider = c.String(maxLength: 150, storeType: "nvarchar"),
                        DispenseDate = c.DateTime(precision: 0),
                        Duration = c.Decimal(precision: 18, scale: 2),
                        ExpectedReturn = c.DateTime(precision: 0),
                        TreatmentType = c.String(maxLength: 150, storeType: "nvarchar"),
                        RegimenLine = c.String(maxLength: 150, storeType: "nvarchar"),
                        PeriodTaken = c.String(maxLength: 150, storeType: "nvarchar"),
                        ProphylaxisType = c.String(maxLength: 150, storeType: "nvarchar"),
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientStatusExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150, storeType: "nvarchar"),
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        ExitDescription = c.String(maxLength: 150, storeType: "nvarchar"),
                        ExitDate = c.DateTime(precision: 0),
                        ExitReason = c.String(maxLength: 150, storeType: "nvarchar"),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TempPatientVisitExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150, storeType: "nvarchar"),
                        Emr = c.String(maxLength: 150, storeType: "nvarchar"),
                        Project = c.String(maxLength: 150, storeType: "nvarchar"),
                        VisitId = c.Int(),
                        VisitDate = c.DateTime(precision: 0),
                        Service = c.String(maxLength: 150, storeType: "nvarchar"),
                        VisitType = c.String(maxLength: 150, storeType: "nvarchar"),
                        WHOStage = c.Int(),
                        WABStage = c.String(maxLength: 150, storeType: "nvarchar"),
                        Pregnant = c.String(maxLength: 150, storeType: "nvarchar"),
                        LMP = c.DateTime(precision: 0),
                        EDD = c.DateTime(precision: 0),
                        Height = c.Decimal(precision: 18, scale: 2),
                        Weight = c.Decimal(precision: 18, scale: 2),
                        BP = c.String(maxLength: 150, storeType: "nvarchar"),
                        OI = c.String(maxLength: 150, storeType: "nvarchar"),
                        OIDate = c.DateTime(precision: 0),
                        Adherence = c.String(maxLength: 150, storeType: "nvarchar"),
                        AdherenceCategory = c.String(maxLength: 150, storeType: "nvarchar"),
                        SubstitutionFirstlineRegimenDate = c.DateTime(precision: 0),
                        SubstitutionFirstlineRegimenReason = c.String(maxLength: 150, storeType: "nvarchar"),
                        SubstitutionSecondlineRegimenDate = c.DateTime(precision: 0),
                        SubstitutionSecondlineRegimenReason = c.String(maxLength: 150, storeType: "nvarchar"),
                        SecondlineRegimenChangeDate = c.DateTime(precision: 0),
                        SecondlineRegimenChangeReason = c.String(maxLength: 150, storeType: "nvarchar"),
                        FamilyPlanningMethod = c.String(maxLength: 150, storeType: "nvarchar"),
                        PwP = c.String(maxLength: 150, storeType: "nvarchar"),
                        GestationAge = c.Decimal(precision: 18, scale: 2),
                        NextAppointmentDate = c.DateTime(precision: 0),
                        PatientPK = c.Int(nullable: false),
                        PatientID = c.String(maxLength: 150, storeType: "nvarchar"),
                        FacilityId = c.Int(),
                        SiteCode = c.Int(nullable: false),
                        DateExtracted = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EMR", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ExtractSetting", "EmrId", "dbo.EMR");
            DropForeignKey("dbo.PatientVisitExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropForeignKey("dbo.PatientStatusExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropForeignKey("dbo.PatientPharmacyExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropForeignKey("dbo.PatientLaboratoryExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropForeignKey("dbo.PatientBaselinesExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropForeignKey("dbo.PatientArtExtract", new[] { "PatientPK", "SiteCode" }, "dbo.PatientExtract");
            DropIndex("dbo.ExtractSetting", new[] { "EmrId" });
            DropIndex("dbo.EMR", new[] { "ProjectId" });
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
            DropTable("dbo.Project");
            DropTable("dbo.ExtractSetting");
            DropTable("dbo.EMR");
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
