namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtractsStaging : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StageAdverseEventExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AdverseEvent = c.String(maxLength: 150),
                        AdverseEventStartDate = c.DateTime(),
                        AdverseEventEndDate = c.DateTime(),
                        Severity = c.String(maxLength: 150),
                        AdverseEventClinicalOutcome = c.String(maxLength: 150),
                        AdverseEventActionTaken = c.String(maxLength: 150),
                        AdverseEventIsPregnant = c.Boolean(),
                        VisitDate = c.DateTime(),
                        AdverseEventRegimen = c.String(maxLength: 150),
                        AdverseEventCause = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageAllergiesChronicIllnessExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        ChronicIllness = c.String(maxLength: 150),
                        ChronicOnsetDate = c.DateTime(),
                        knownAllergies = c.String(maxLength: 150),
                        AllergyCausativeAgent = c.String(maxLength: 150),
                        AllergicReaction = c.String(maxLength: 150),
                        AllergySeverity = c.String(maxLength: 150),
                        AllergyOnsetDate = c.DateTime(),
                        Skin = c.String(maxLength: 150),
                        Eyes = c.String(maxLength: 150),
                        ENT = c.String(maxLength: 150),
                        Chest = c.String(maxLength: 150),
                        CVS = c.String(maxLength: 150),
                        Abdomen = c.String(maxLength: 150),
                        CNS = c.String(maxLength: 150),
                        Genitourinary = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageArtExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PreviousARTUse = c.String(maxLength: 150),
                        PreviousARTPurpose = c.String(maxLength: 150),
                        DateLastUsed = c.DateTime(),
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
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageBaselineExtract",
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
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageContactListingExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        PartnerPersonID = c.Int(),
                        ContactAge = c.String(maxLength: 150),
                        ContactSex = c.String(maxLength: 150),
                        ContactMaritalStatus = c.String(maxLength: 150),
                        RelationshipWithPatient = c.String(maxLength: 150),
                        ScreenedForIpv = c.String(maxLength: 150),
                        IpvScreening = c.String(maxLength: 150),
                        IPVScreeningOutcome = c.String(maxLength: 150),
                        CurrentlyLivingWithIndexClient = c.String(maxLength: 150),
                        KnowledgeOfHivStatus = c.String(maxLength: 150),
                        PnsApproach = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageCovidExtract",
                c => new
                    {
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
                        COVID19TestResult = c.String(maxLength: 150),
                        Sequence = c.String(maxLength: 150),
                        BoosterDoseVerified = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageDefaulterTracingExtract",
                c => new
                    {
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
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageDepressionScreeningExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        PHQ9_1 = c.String(maxLength: 150),
                        PHQ9_2 = c.String(maxLength: 150),
                        PHQ9_3 = c.String(maxLength: 150),
                        PHQ9_4 = c.String(maxLength: 150),
                        PHQ9_5 = c.String(maxLength: 150),
                        PHQ9_6 = c.String(maxLength: 150),
                        PHQ9_7 = c.String(maxLength: 150),
                        PHQ9_8 = c.String(maxLength: 150),
                        PHQ9_9 = c.String(maxLength: 150),
                        PHQ_9_rating = c.String(maxLength: 150),
                        DepressionAssesmentScore = c.Int(),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageDrugAlcoholScreeningExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        DrinkingAlcohol = c.String(maxLength: 150),
                        Smoking = c.String(maxLength: 150),
                        DrugUse = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageEnhancedAdherenceCounsellingExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        SessionNumber = c.Int(),
                        DateOfFirstSession = c.DateTime(),
                        PillCountAdherence = c.Int(),
                        MMAS4_1 = c.String(maxLength: 150),
                        MMAS4_2 = c.String(maxLength: 150),
                        MMAS4_3 = c.String(maxLength: 150),
                        MMAS4_4 = c.String(maxLength: 150),
                        MMSA8_1 = c.String(maxLength: 150),
                        MMSA8_2 = c.String(maxLength: 150),
                        MMSA8_3 = c.String(maxLength: 150),
                        MMSA8_4 = c.String(maxLength: 150),
                        MMSAScore = c.String(maxLength: 150),
                        EACRecievedVL = c.String(maxLength: 150),
                        EACVL = c.String(maxLength: 150),
                        EACVLConcerns = c.String(maxLength: 150),
                        EACVLThoughts = c.String(maxLength: 150),
                        EACWayForward = c.String(maxLength: 150),
                        EACCognitiveBarrier = c.String(maxLength: 150),
                        EACBehaviouralBarrier_1 = c.String(maxLength: 150),
                        EACBehaviouralBarrier_2 = c.String(maxLength: 150),
                        EACBehaviouralBarrier_3 = c.String(maxLength: 150),
                        EACBehaviouralBarrier_4 = c.String(maxLength: 150),
                        EACBehaviouralBarrier_5 = c.String(maxLength: 150),
                        EACEmotionalBarriers_1 = c.String(maxLength: 150),
                        EACEmotionalBarriers_2 = c.String(maxLength: 150),
                        EACEconBarrier_1 = c.String(maxLength: 150),
                        EACEconBarrier_2 = c.String(maxLength: 150),
                        EACEconBarrier_3 = c.String(maxLength: 150),
                        EACEconBarrier_4 = c.String(maxLength: 150),
                        EACEconBarrier_5 = c.String(maxLength: 150),
                        EACEconBarrier_6 = c.String(maxLength: 150),
                        EACEconBarrier_7 = c.String(maxLength: 150),
                        EACEconBarrier_8 = c.String(maxLength: 150),
                        EACReviewImprovement = c.String(maxLength: 150),
                        EACReviewMissedDoses = c.String(maxLength: 150),
                        EACReviewStrategy = c.String(maxLength: 150),
                        EACReferral = c.String(maxLength: 150),
                        EACReferralApp = c.String(maxLength: 150),
                        EACReferralExperience = c.String(maxLength: 150),
                        EACHomevisit = c.String(maxLength: 150),
                        EACAdherencePlan = c.String(maxLength: 150),
                        EACFollowupDate = c.DateTime(),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageGbvScreeningExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        IPV = c.String(maxLength: 150),
                        PhysicalIPV = c.String(maxLength: 150),
                        EmotionalIPV = c.String(maxLength: 150),
                        SexualIPV = c.String(maxLength: 150),
                        IPVRelationship = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageIptExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        OnTBDrugs = c.String(maxLength: 150),
                        OnIPT = c.String(maxLength: 150),
                        EverOnIPT = c.String(maxLength: 150),
                        Cough = c.String(maxLength: 150),
                        Fever = c.String(maxLength: 150),
                        NoticeableWeightLoss = c.String(maxLength: 150),
                        NightSweats = c.String(maxLength: 150),
                        Lethargy = c.String(maxLength: 150),
                        ICFActionTaken = c.String(maxLength: 150),
                        TestResult = c.String(maxLength: 150),
                        TBClinicalDiagnosis = c.String(maxLength: 150),
                        ContactsInvited = c.String(maxLength: 150),
                        EvaluatedForIPT = c.String(maxLength: 150),
                        StartAntiTBs = c.String(maxLength: 150),
                        TBRxStartDate = c.DateTime(),
                        TBScreening = c.String(maxLength: 150),
                        IPTClientWorkUp = c.String(maxLength: 150),
                        StartIPT = c.String(maxLength: 150),
                        IndicationForIPT = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageLaboratoryExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateSampleTaken = c.DateTime(),
                        SampleType = c.String(maxLength: 150),
                        VisitId = c.Int(),
                        OrderedByDate = c.DateTime(),
                        ReportedByDate = c.DateTime(),
                        TestName = c.String(maxLength: 150),
                        EnrollmentTest = c.Int(),
                        TestResult = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageOtzExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        OTZEnrollmentDate = c.DateTime(),
                        TransferInStatus = c.String(maxLength: 150),
                        ModulesPreviouslyCovered = c.String(maxLength: 150),
                        ModulesCompletedToday = c.String(maxLength: 150),
                        SupportGroupInvolvement = c.String(maxLength: 150),
                        Remarks = c.String(maxLength: 150),
                        TransitionAttritionReason = c.String(maxLength: 150),
                        OutcomeDate = c.DateTime(),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageOvcExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        OVCEnrollmentDate = c.DateTime(),
                        RelationshipToClient = c.String(maxLength: 150),
                        EnrolledinCPIMS = c.String(maxLength: 150),
                        CPIMSUniqueIdentifier = c.String(maxLength: 150),
                        PartnerOfferingOVCServices = c.String(maxLength: 150),
                        OVCExitReason = c.String(maxLength: 150),
                        ExitDate = c.DateTime(),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        SiteCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StagePharmacyExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegimenChangedSwitched = c.String(maxLength: 150),
                        RegimenChangeSwitchReason = c.String(maxLength: 150),
                        StopRegimenReason = c.String(maxLength: 150),
                        StopRegimenDate = c.DateTime(),
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
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageStatusExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TOVerified = c.String(maxLength: 150),
                        TOVerifiedDate = c.DateTime(),
                        ReEnrollmentDate = c.DateTime(),
                        ReasonForDeath = c.String(maxLength: 150),
                        SpecificDeathReason = c.String(maxLength: 150),
                        DeathDate = c.DateTime(),
                        ExitDescription = c.String(maxLength: 150),
                        ExitDate = c.DateTime(),
                        ExitReason = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageVisitExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitBy = c.String(maxLength: 150),
                        Temp = c.Decimal(precision: 18, scale: 2),
                        PulseRate = c.Int(),
                        RespiratoryRate = c.Int(),
                        OxygenSaturation = c.Decimal(precision: 18, scale: 2),
                        Muac = c.Int(),
                        NutritionalStatus = c.String(maxLength: 150),
                        EverHadMenses = c.String(maxLength: 150),
                        Breastfeeding = c.String(maxLength: 150),
                        Menopausal = c.String(maxLength: 150),
                        NoFPReason = c.String(maxLength: 150),
                        ProphylaxisUsed = c.String(maxLength: 150),
                        CTXAdherence = c.String(maxLength: 150),
                        CurrentRegimen = c.String(maxLength: 150),
                        HCWConcern = c.String(maxLength: 150),
                        TCAReason = c.String(maxLength: 150),
                        ClinicalNotes = c.String(maxLength: 150),
                        GeneralExamination = c.String(maxLength: 150),
                        SystemExamination = c.String(maxLength: 150),
                        Skin = c.String(maxLength: 150),
                        Eyes = c.String(maxLength: 150),
                        ENT = c.String(maxLength: 150),
                        Chest = c.String(maxLength: 150),
                        CVS = c.String(maxLength: 150),
                        Abdomen = c.String(maxLength: 150),
                        CNS = c.String(maxLength: 150),
                        Genitourinary = c.String(maxLength: 150),
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
                        StabilityAssessment = c.String(maxLength: 150),
                        DifferentiatedCare = c.String(maxLength: 150),
                        PopulationType = c.String(maxLength: 150),
                        KeyPopulationType = c.String(maxLength: 150),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
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
            DropTable("dbo.StageVisitExtract");
            DropTable("dbo.StageStatusExtract");
            DropTable("dbo.StagePharmacyExtract");
            DropTable("dbo.StagePatientExtract");
            DropTable("dbo.StageOvcExtract");
            DropTable("dbo.StageOtzExtract");
            DropTable("dbo.StageLaboratoryExtract");
            DropTable("dbo.StageIptExtract");
            DropTable("dbo.StageGbvScreeningExtract");
            DropTable("dbo.StageEnhancedAdherenceCounsellingExtract");
            DropTable("dbo.StageDrugAlcoholScreeningExtract");
            DropTable("dbo.StageDepressionScreeningExtract");
            DropTable("dbo.StageDefaulterTracingExtract");
            DropTable("dbo.StageCovidExtract");
            DropTable("dbo.StageContactListingExtract");
            DropTable("dbo.StageBaselineExtract");
            DropTable("dbo.StageArtExtract");
            DropTable("dbo.StageAllergiesChronicIllnessExtract");
            DropTable("dbo.StageAdverseEventExtract");
        }
    }
}
