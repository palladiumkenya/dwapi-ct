namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GeneratedStageId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StageAdverseEventExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageAllergiesChronicIllnessExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageArtExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageBaselineExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageContactListingExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageCovidExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageDefaulterTracingExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageDepressionScreeningExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageDrugAlcoholScreeningExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageEnhancedAdherenceCounsellingExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageGbvScreeningExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageIptExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageLaboratoryExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageOtzExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageOvcExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StagePharmacyExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageStatusExtract", "Generated", c => c.DateTime());
            AddColumn("dbo.StageVisitExtract", "Generated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageVisitExtract", "Generated");
            DropColumn("dbo.StageStatusExtract", "Generated");
            DropColumn("dbo.StagePharmacyExtract", "Generated");
            DropColumn("dbo.StageOvcExtract", "Generated");
            DropColumn("dbo.StageOtzExtract", "Generated");
            DropColumn("dbo.StageLaboratoryExtract", "Generated");
            DropColumn("dbo.StageIptExtract", "Generated");
            DropColumn("dbo.StageGbvScreeningExtract", "Generated");
            DropColumn("dbo.StageEnhancedAdherenceCounsellingExtract", "Generated");
            DropColumn("dbo.StageDrugAlcoholScreeningExtract", "Generated");
            DropColumn("dbo.StageDepressionScreeningExtract", "Generated");
            DropColumn("dbo.StageDefaulterTracingExtract", "Generated");
            DropColumn("dbo.StageCovidExtract", "Generated");
            DropColumn("dbo.StageContactListingExtract", "Generated");
            DropColumn("dbo.StageBaselineExtract", "Generated");
            DropColumn("dbo.StageArtExtract", "Generated");
            DropColumn("dbo.StageAllergiesChronicIllnessExtract", "Generated");
            DropColumn("dbo.StageAdverseEventExtract", "Generated");
        }
    }
}
