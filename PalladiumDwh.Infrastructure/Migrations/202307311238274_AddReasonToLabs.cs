namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReasonToLabs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientLaboratoryExtract", "Reason", c => c.String(maxLength: 150));
            AddColumn("dbo.StageLaboratoryExtract", "Reason", c => c.String(maxLength: 150));
            DropColumn("dbo.AllergiesChronicIllnessExtract", "PatientUUID");
            DropColumn("dbo.CervicalCancerScreeningExtract", "PatientUUID");
            DropColumn("dbo.ContactListingExtract", "PatientUUID");
            DropColumn("dbo.CovidExtract", "PatientUUID");
            DropColumn("dbo.DefaulterTracingExtract", "PatientUUID");
            DropColumn("dbo.DepressionScreeningExtract", "PatientUUID");
            DropColumn("dbo.DrugAlcoholScreeningExtract", "PatientUUID");
            DropColumn("dbo.EnhancedAdherenceCounsellingExtract", "PatientUUID");
            DropColumn("dbo.PatientExtract", "PatientUUID");
            DropColumn("dbo.GbvScreeningExtract", "PatientUUID");
            DropColumn("dbo.IptExtract", "PatientUUID");
            DropColumn("dbo.OtzExtract", "PatientUUID");
            DropColumn("dbo.OvcExtract", "PatientUUID");
            DropColumn("dbo.PatientAdverseEventExtract", "PatientUUID");
            DropColumn("dbo.PatientArtExtract", "PatientUUID");
            DropColumn("dbo.PatientBaselinesExtract", "PatientUUID");
            DropColumn("dbo.PatientLaboratoryExtract", "PatientUUID");
            DropColumn("dbo.PatientPharmacyExtract", "PatientUUID");
            DropColumn("dbo.PatientStatusExtract", "PatientUUID");
            DropColumn("dbo.PatientVisitExtract", "PatientUUID");
            DropColumn("dbo.StageAdverseEventExtract", "PatientUUID");
            DropColumn("dbo.StageAllergiesChronicIllnessExtract", "PatientUUID");
            DropColumn("dbo.StageArtExtract", "PatientUUID");
            DropColumn("dbo.StageBaselineExtract", "PatientUUID");
            DropColumn("dbo.StageCervicalCancerScreeningExtract", "PatientUUID");
            DropColumn("dbo.StageContactListingExtract", "PatientUUID");
            DropColumn("dbo.StageCovidExtract", "PatientUUID");
            DropColumn("dbo.StageDefaulterTracingExtract", "PatientUUID");
            DropColumn("dbo.StageDepressionScreeningExtract", "PatientUUID");
            DropColumn("dbo.StageDrugAlcoholScreeningExtract", "PatientUUID");
            DropColumn("dbo.StageEnhancedAdherenceCounsellingExtract", "PatientUUID");
            DropColumn("dbo.StageGbvScreeningExtract", "PatientUUID");
            DropColumn("dbo.StageIptExtract", "PatientUUID");
            DropColumn("dbo.StageLaboratoryExtract", "PatientUUID");
            DropColumn("dbo.StageOtzExtract", "PatientUUID");
            DropColumn("dbo.StageOvcExtract", "PatientUUID");
            DropColumn("dbo.StagePatientExtract", "PatientUUID");
            DropColumn("dbo.StagePharmacyExtract", "PatientUUID");
            DropColumn("dbo.StageStatusExtract", "PatientUUID");
            DropColumn("dbo.StageVisitExtract", "PatientUUID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StageVisitExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageStatusExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StagePharmacyExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StagePatientExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageOvcExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageOtzExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageLaboratoryExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageIptExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageGbvScreeningExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageEnhancedAdherenceCounsellingExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageDrugAlcoholScreeningExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageDepressionScreeningExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageDefaulterTracingExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageCovidExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageContactListingExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageCervicalCancerScreeningExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageBaselineExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageArtExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageAllergiesChronicIllnessExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageAdverseEventExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientStatusExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientPharmacyExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientLaboratoryExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientBaselinesExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientArtExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientAdverseEventExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.OvcExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.OtzExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.IptExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.GbvScreeningExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.EnhancedAdherenceCounsellingExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.DrugAlcoholScreeningExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.DepressionScreeningExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.DefaulterTracingExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.CovidExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.ContactListingExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.CervicalCancerScreeningExtract", "PatientUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.AllergiesChronicIllnessExtract", "PatientUUID", c => c.String(maxLength: 150));
            DropColumn("dbo.StageLaboratoryExtract", "Reason");
            DropColumn("dbo.PatientLaboratoryExtract", "Reason");
        }
    }
}
