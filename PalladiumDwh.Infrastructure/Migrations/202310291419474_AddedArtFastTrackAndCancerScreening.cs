namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArtFastTrackAndCancerScreening : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CervicalCancerScreeningExtract", "PatientId", "dbo.PatientExtract");
            DropIndex("dbo.CervicalCancerScreeningExtract", new[] { "PatientId" });
            CreateTable(
                "dbo.ArtFastTrackExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        ARTRefillModel = c.String(maxLength: 150),
                        VisitDate = c.DateTime(),
                        CTXDispensed = c.String(maxLength: 150),
                        DapsoneDispensed = c.String(maxLength: 150),
                        CondomsDistributed = c.String(maxLength: 150),
                        OralContraceptivesDispensed = c.String(maxLength: 150),
                        MissedDoses = c.String(maxLength: 150),
                        Fatigue = c.String(maxLength: 150),
                        Cough = c.String(maxLength: 150),
                        Fever = c.String(maxLength: 150),
                        Rash = c.String(maxLength: 150),
                        NauseaOrVomiting = c.String(maxLength: 150),
                        GenitalSoreOrDischarge = c.String(maxLength: 150),
                        Diarrhea = c.String(maxLength: 150),
                        OtherSymptoms = c.String(maxLength: 150),
                        PregnancyStatus = c.String(maxLength: 150),
                        FPStatus = c.String(maxLength: 150),
                        FPMethod = c.String(maxLength: 150),
                        ReasonNotOnFP = c.String(maxLength: 150),
                        ReferredToClinic = c.String(maxLength: 150),
                        ReturnVisitDate = c.DateTime(),
                        RecordUUID = c.String(maxLength: 150),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
                        Created = c.DateTime(),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.CancerScreeningExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitType = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        SmokesCigarette = c.String(maxLength: 150),
                        NumberYearsSmoked = c.Int(),
                        NumberCigarettesPerDay = c.Int(),
                        OtherFormTobacco = c.String(maxLength: 150),
                        TakesAlcohol = c.String(maxLength: 150),
                        HIVStatus = c.String(maxLength: 150),
                        FamilyHistoryOfCa = c.String(maxLength: 150),
                        PreviousCaTreatment = c.String(maxLength: 150),
                        SymptomsCa = c.String(maxLength: 150),
                        CancerType = c.String(maxLength: 150),
                        FecalOccultBloodTest = c.String(maxLength: 150),
                        TreatmentOccultBlood = c.String(maxLength: 150),
                        Colonoscopy = c.String(maxLength: 150),
                        TreatmentColonoscopy = c.String(maxLength: 150),
                        EUA = c.String(maxLength: 150),
                        TreatmentRetinoblastoma = c.String(maxLength: 150),
                        RetinoblastomaGene = c.String(maxLength: 150),
                        TreatmentEUA = c.String(maxLength: 150),
                        DRE = c.String(maxLength: 150),
                        TreatmentDRE = c.String(maxLength: 150),
                        PSA = c.String(maxLength: 150),
                        TreatmentPSA = c.String(maxLength: 150),
                        VisualExamination = c.String(maxLength: 150),
                        TreatmentVE = c.String(maxLength: 150),
                        Cytology = c.String(maxLength: 150),
                        TreatmentCytology = c.String(maxLength: 150),
                        Imaging = c.String(maxLength: 150),
                        TreatmentImaging = c.String(maxLength: 150),
                        Biopsy = c.String(maxLength: 150),
                        TreatmentBiopsy = c.String(maxLength: 150),
                        PostTreatmentComplicationCause = c.String(maxLength: 150),
                        OtherPostTreatmentComplication = c.String(maxLength: 150),
                        ReferralReason = c.String(maxLength: 150),
                        ScreeningMethod = c.String(maxLength: 150),
                        TreatmentToday = c.String(maxLength: 150),
                        ReferredOut = c.String(maxLength: 150),
                        NextAppointmentDate = c.DateTime(),
                        ScreeningType = c.String(maxLength: 150),
                        HPVScreeningResult = c.String(maxLength: 150),
                        TreatmentHPV = c.String(maxLength: 150),
                        VIAScreeningResult = c.String(maxLength: 150),
                        VIAVILIScreeningResult = c.String(maxLength: 150),
                        VIATreatmentOptions = c.String(maxLength: 150),
                        PAPSmearScreeningResult = c.String(maxLength: 150),
                        TreatmentPapSmear = c.String(maxLength: 150),
                        ReferalOrdered = c.String(maxLength: 150),
                        Colposcopy = c.String(maxLength: 150),
                        TreatmentColposcopy = c.String(maxLength: 150),
                        BiopsyCINIIandAbove = c.String(maxLength: 150),
                        BiopsyCINIIandBelow = c.String(maxLength: 150),
                        BiopsyNotAvailable = c.String(maxLength: 150),
                        CBE = c.String(maxLength: 150),
                        TreatmentCBE = c.String(maxLength: 150),
                        Ultrasound = c.String(maxLength: 150),
                        TreatmentUltraSound = c.String(maxLength: 150),
                        IfTissueDiagnosis = c.String(maxLength: 150),
                        DateTissueDiagnosis = c.DateTime(),
                        ReasonNotDone = c.String(maxLength: 150),
                        FollowUpDate = c.DateTime(),
                        Referred = c.String(maxLength: 150),
                        ReasonForReferral = c.String(maxLength: 150),
                        RecordUUID = c.String(maxLength: 150),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
                        Created = c.DateTime(),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.StageArtFastTrackExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        ARTRefillModel = c.String(maxLength: 150),
                        VisitDate = c.DateTime(),
                        CTXDispensed = c.String(maxLength: 150),
                        DapsoneDispensed = c.String(maxLength: 150),
                        CondomsDistributed = c.String(maxLength: 150),
                        OralContraceptivesDispensed = c.String(maxLength: 150),
                        MissedDoses = c.String(maxLength: 150),
                        Fatigue = c.String(maxLength: 150),
                        Cough = c.String(maxLength: 150),
                        Fever = c.String(maxLength: 150),
                        Rash = c.String(maxLength: 150),
                        NauseaOrVomiting = c.String(maxLength: 150),
                        GenitalSoreOrDischarge = c.String(maxLength: 150),
                        Diarrhea = c.String(maxLength: 150),
                        OtherSymptoms = c.String(maxLength: 150),
                        PregnancyStatus = c.String(maxLength: 150),
                        FPStatus = c.String(maxLength: 150),
                        FPMethod = c.String(maxLength: 150),
                        ReasonNotOnFP = c.String(maxLength: 150),
                        ReferredToClinic = c.String(maxLength: 150),
                        ReturnVisitDate = c.DateTime(),
                        RecordUUID = c.String(maxLength: 150),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
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
                        Generated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageCancerScreeningExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitType = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        SmokesCigarette = c.String(maxLength: 150),
                        NumberYearsSmoked = c.Int(),
                        NumberCigarettesPerDay = c.Int(),
                        OtherFormTobacco = c.String(maxLength: 150),
                        TakesAlcohol = c.String(maxLength: 150),
                        HIVStatus = c.String(maxLength: 150),
                        FamilyHistoryOfCa = c.String(maxLength: 150),
                        PreviousCaTreatment = c.String(maxLength: 150),
                        SymptomsCa = c.String(maxLength: 150),
                        CancerType = c.String(maxLength: 150),
                        FecalOccultBloodTest = c.String(maxLength: 150),
                        TreatmentOccultBlood = c.String(maxLength: 150),
                        Colonoscopy = c.String(maxLength: 150),
                        TreatmentColonoscopy = c.String(maxLength: 150),
                        EUA = c.String(maxLength: 150),
                        TreatmentRetinoblastoma = c.String(maxLength: 150),
                        RetinoblastomaGene = c.String(maxLength: 150),
                        TreatmentEUA = c.String(maxLength: 150),
                        DRE = c.String(maxLength: 150),
                        TreatmentDRE = c.String(maxLength: 150),
                        PSA = c.String(maxLength: 150),
                        TreatmentPSA = c.String(maxLength: 150),
                        VisualExamination = c.String(maxLength: 150),
                        TreatmentVE = c.String(maxLength: 150),
                        Cytology = c.String(maxLength: 150),
                        TreatmentCytology = c.String(maxLength: 150),
                        Imaging = c.String(maxLength: 150),
                        TreatmentImaging = c.String(maxLength: 150),
                        Biopsy = c.String(maxLength: 150),
                        TreatmentBiopsy = c.String(maxLength: 150),
                        PostTreatmentComplicationCause = c.String(maxLength: 150),
                        OtherPostTreatmentComplication = c.String(maxLength: 150),
                        ReferralReason = c.String(maxLength: 150),
                        ScreeningMethod = c.String(maxLength: 150),
                        TreatmentToday = c.String(maxLength: 150),
                        ReferredOut = c.String(maxLength: 150),
                        NextAppointmentDate = c.DateTime(),
                        ScreeningType = c.String(maxLength: 150),
                        HPVScreeningResult = c.String(maxLength: 150),
                        TreatmentHPV = c.String(maxLength: 150),
                        VIAScreeningResult = c.String(maxLength: 150),
                        VIAVILIScreeningResult = c.String(maxLength: 150),
                        VIATreatmentOptions = c.String(maxLength: 150),
                        PAPSmearScreeningResult = c.String(maxLength: 150),
                        TreatmentPapSmear = c.String(maxLength: 150),
                        ReferalOrdered = c.String(maxLength: 150),
                        Colposcopy = c.String(maxLength: 150),
                        TreatmentColposcopy = c.String(maxLength: 150),
                        BiopsyCINIIandAbove = c.String(maxLength: 150),
                        BiopsyCINIIandBelow = c.String(maxLength: 150),
                        BiopsyNotAvailable = c.String(maxLength: 150),
                        CBE = c.String(maxLength: 150),
                        TreatmentCBE = c.String(maxLength: 150),
                        Ultrasound = c.String(maxLength: 150),
                        TreatmentUltraSound = c.String(maxLength: 150),
                        IfTissueDiagnosis = c.String(maxLength: 150),
                        DateTissueDiagnosis = c.DateTime(),
                        ReasonNotDone = c.String(maxLength: 150),
                        FollowUpDate = c.DateTime(),
                        Referred = c.String(maxLength: 150),
                        ReasonForReferral = c.String(maxLength: 150),
                        RecordUUID = c.String(maxLength: 150),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
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
                        Generated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.CervicalCancerScreeningExtract");
            DropTable("dbo.StageCervicalCancerScreeningExtract");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StageCervicalCancerScreeningExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        VisitType = c.String(maxLength: 150),
                        ScreeningMethod = c.String(maxLength: 150),
                        TreatmentToday = c.String(maxLength: 150),
                        ReferredOut = c.String(maxLength: 150),
                        NextAppointmentDate = c.DateTime(),
                        ScreeningType = c.String(maxLength: 150),
                        ScreeningResult = c.String(maxLength: 150),
                        PostTreatmentComplicationCause = c.String(maxLength: 150),
                        OtherPostTreatmentComplication = c.String(maxLength: 150),
                        ReferralReason = c.String(maxLength: 150),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
                        RecordUUID = c.String(maxLength: 150),
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
                        Generated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CervicalCancerScreeningExtract",
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
                        VisitType = c.String(maxLength: 150),
                        ScreeningMethod = c.String(maxLength: 150),
                        TreatmentToday = c.String(maxLength: 150),
                        ReferredOut = c.String(maxLength: 150),
                        NextAppointmentDate = c.DateTime(),
                        ScreeningType = c.String(maxLength: 150),
                        ScreeningResult = c.String(maxLength: 150),
                        PostTreatmentComplicationCause = c.String(maxLength: 150),
                        OtherPostTreatmentComplication = c.String(maxLength: 150),
                        ReferralReason = c.String(maxLength: 150),
                        Created = c.DateTime(),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
                        RecordUUID = c.String(maxLength: 150),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.CancerScreeningExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.ArtFastTrackExtract", "PatientId", "dbo.PatientExtract");
            DropIndex("dbo.CancerScreeningExtract", new[] { "PatientId" });
            DropIndex("dbo.ArtFastTrackExtract", new[] { "PatientId" });
            DropTable("dbo.StageCancerScreeningExtract");
            DropTable("dbo.StageArtFastTrackExtract");
            DropTable("dbo.CancerScreeningExtract");
            DropTable("dbo.ArtFastTrackExtract");
            CreateIndex("dbo.CervicalCancerScreeningExtract", "PatientId");
            AddForeignKey("dbo.CervicalCancerScreeningExtract", "PatientId", "dbo.PatientExtract", "Id", cascadeDelete: true);
        }
    }
}
