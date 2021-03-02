namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllergiesChronicIllnessExtract",
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
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.ContactListingExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
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
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.DepressionScreeningExtract",
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
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                        PatientExtractId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientExtractId)
                .Index(t => t.PatientExtractId);
            
            CreateTable(
                "dbo.DrugAlcoholScreeningExtract",
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
                        DrinkingAlcohol = c.String(maxLength: 150),
                        Smoking = c.String(maxLength: 150),
                        DrugUse = c.String(maxLength: 150),
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.EnhancedAdherenceCounsellingExtract",
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
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.GbvScreeningExtract",
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
                        IPV = c.String(maxLength: 150),
                        PhysicalIPV = c.String(maxLength: 150),
                        EmotionalIPV = c.String(maxLength: 150),
                        SexualIPV = c.String(maxLength: 150),
                        IPVRelationship = c.String(maxLength: 150),
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.IptExtract",
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
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.OtzExtract",
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
                        OTZEnrollmentDate = c.DateTime(),
                        TransferInStatus = c.String(maxLength: 150),
                        ModulesPreviouslyCovered = c.String(maxLength: 150),
                        ModulesCompletedToday = c.String(maxLength: 150),
                        SupportGroupInvolvement = c.String(maxLength: 150),
                        Remarks = c.String(maxLength: 150),
                        TransitionAttritionReason = c.String(maxLength: 150),
                        OutcomeDate = c.DateTime(),
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.OvcExtract",
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
                        OVCEnrollmentDate = c.DateTime(),
                        RelationshipToClient = c.String(maxLength: 150),
                        EnrolledinCPIMS = c.String(maxLength: 150),
                        CPIMSUniqueIdentifier = c.String(maxLength: 150),
                        PartnerOfferingOVCServices = c.String(maxLength: 150),
                        OVCExitReason = c.String(maxLength: 150),
                        ExitDate = c.DateTime(),
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OvcExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.OtzExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.IptExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.GbvScreeningExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.EnhancedAdherenceCounsellingExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.DrugAlcoholScreeningExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.DepressionScreeningExtract", "PatientExtractId", "dbo.PatientExtract");
            DropForeignKey("dbo.ContactListingExtract", "PatientId", "dbo.PatientExtract");
            DropForeignKey("dbo.AllergiesChronicIllnessExtract", "PatientId", "dbo.PatientExtract");
            DropIndex("dbo.OvcExtract", new[] { "PatientId" });
            DropIndex("dbo.OtzExtract", new[] { "PatientId" });
            DropIndex("dbo.IptExtract", new[] { "PatientId" });
            DropIndex("dbo.GbvScreeningExtract", new[] { "PatientId" });
            DropIndex("dbo.EnhancedAdherenceCounsellingExtract", new[] { "PatientId" });
            DropIndex("dbo.DrugAlcoholScreeningExtract", new[] { "PatientId" });
            DropIndex("dbo.DepressionScreeningExtract", new[] { "PatientExtractId" });
            DropIndex("dbo.ContactListingExtract", new[] { "PatientId" });
            DropIndex("dbo.AllergiesChronicIllnessExtract", new[] { "PatientId" });
            DropTable("dbo.OvcExtract");
            DropTable("dbo.OtzExtract");
            DropTable("dbo.IptExtract");
            DropTable("dbo.GbvScreeningExtract");
            DropTable("dbo.EnhancedAdherenceCounsellingExtract");
            DropTable("dbo.DrugAlcoholScreeningExtract");
            DropTable("dbo.DepressionScreeningExtract");
            DropTable("dbo.ContactListingExtract");
            DropTable("dbo.AllergiesChronicIllnessExtract");
        }
    }
}
