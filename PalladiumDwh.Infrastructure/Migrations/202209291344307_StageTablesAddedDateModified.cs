namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StageTablesAddedDateModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StageAdverseEventExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageAdverseEventExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageAllergiesChronicIllnessExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageAllergiesChronicIllnessExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageArtExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageArtExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageBaselineExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageBaselineExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageContactListingExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageContactListingExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageCovidExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageCovidExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageDefaulterTracingExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageDefaulterTracingExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageDepressionScreeningExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageDepressionScreeningExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageDrugAlcoholScreeningExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageDrugAlcoholScreeningExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageEnhancedAdherenceCounsellingExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageEnhancedAdherenceCounsellingExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageGbvScreeningExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageGbvScreeningExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageIptExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageIptExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageLaboratoryExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageLaboratoryExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageOtzExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageOtzExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageOvcExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageOvcExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StagePatientExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StagePatientExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StagePharmacyExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StagePharmacyExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageStatusExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageStatusExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.StageVisitExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.StageVisitExtract", "Date_Last_Modified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageVisitExtract", "Date_Last_Modified");
            DropColumn("dbo.StageVisitExtract", "Date_Created");
            DropColumn("dbo.StageStatusExtract", "Date_Last_Modified");
            DropColumn("dbo.StageStatusExtract", "Date_Created");
            DropColumn("dbo.StagePharmacyExtract", "Date_Last_Modified");
            DropColumn("dbo.StagePharmacyExtract", "Date_Created");
            DropColumn("dbo.StagePatientExtract", "Date_Last_Modified");
            DropColumn("dbo.StagePatientExtract", "Date_Created");
            DropColumn("dbo.StageOvcExtract", "Date_Last_Modified");
            DropColumn("dbo.StageOvcExtract", "Date_Created");
            DropColumn("dbo.StageOtzExtract", "Date_Last_Modified");
            DropColumn("dbo.StageOtzExtract", "Date_Created");
            DropColumn("dbo.StageLaboratoryExtract", "Date_Last_Modified");
            DropColumn("dbo.StageLaboratoryExtract", "Date_Created");
            DropColumn("dbo.StageIptExtract", "Date_Last_Modified");
            DropColumn("dbo.StageIptExtract", "Date_Created");
            DropColumn("dbo.StageGbvScreeningExtract", "Date_Last_Modified");
            DropColumn("dbo.StageGbvScreeningExtract", "Date_Created");
            DropColumn("dbo.StageEnhancedAdherenceCounsellingExtract", "Date_Last_Modified");
            DropColumn("dbo.StageEnhancedAdherenceCounsellingExtract", "Date_Created");
            DropColumn("dbo.StageDrugAlcoholScreeningExtract", "Date_Last_Modified");
            DropColumn("dbo.StageDrugAlcoholScreeningExtract", "Date_Created");
            DropColumn("dbo.StageDepressionScreeningExtract", "Date_Last_Modified");
            DropColumn("dbo.StageDepressionScreeningExtract", "Date_Created");
            DropColumn("dbo.StageDefaulterTracingExtract", "Date_Last_Modified");
            DropColumn("dbo.StageDefaulterTracingExtract", "Date_Created");
            DropColumn("dbo.StageCovidExtract", "Date_Last_Modified");
            DropColumn("dbo.StageCovidExtract", "Date_Created");
            DropColumn("dbo.StageContactListingExtract", "Date_Last_Modified");
            DropColumn("dbo.StageContactListingExtract", "Date_Created");
            DropColumn("dbo.StageBaselineExtract", "Date_Last_Modified");
            DropColumn("dbo.StageBaselineExtract", "Date_Created");
            DropColumn("dbo.StageArtExtract", "Date_Last_Modified");
            DropColumn("dbo.StageArtExtract", "Date_Created");
            DropColumn("dbo.StageAllergiesChronicIllnessExtract", "Date_Last_Modified");
            DropColumn("dbo.StageAllergiesChronicIllnessExtract", "Date_Created");
            DropColumn("dbo.StageAdverseEventExtract", "Date_Last_Modified");
            DropColumn("dbo.StageAdverseEventExtract", "Date_Created");
        }
    }
}
