namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecordUUID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllergiesChronicIllnessExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.CervicalCancerScreeningExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.ContactListingExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.CovidExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.DefaulterTracingExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.DepressionScreeningExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.DrugAlcoholScreeningExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.EnhancedAdherenceCounsellingExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.GbvScreeningExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.IptExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.IptExtract", "TPTInitiationDate", c => c.DateTime());
            AddColumn("dbo.IptExtract", "IPTDiscontinuation", c => c.String(maxLength: 150));
            AddColumn("dbo.IptExtract", "DateOfDiscontinuation", c => c.DateTime());
            AddColumn("dbo.OtzExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.OvcExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientAdverseEventExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientArtExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientBaselinesExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientLaboratoryExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientPharmacyExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientStatusExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageAdverseEventExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageAllergiesChronicIllnessExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageArtExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageBaselineExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageCervicalCancerScreeningExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageContactListingExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageCovidExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageDefaulterTracingExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageDepressionScreeningExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageDrugAlcoholScreeningExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageEnhancedAdherenceCounsellingExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageGbvScreeningExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageIptExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageIptExtract", "TPTInitiationDate", c => c.DateTime());
            AddColumn("dbo.StageIptExtract", "IPTDiscontinuation", c => c.String(maxLength: 150));
            AddColumn("dbo.StageIptExtract", "DateOfDiscontinuation", c => c.DateTime());
            AddColumn("dbo.StageLaboratoryExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageOtzExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageOvcExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StagePatientExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StagePharmacyExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageStatusExtract", "RecordUUID", c => c.String(maxLength: 150));
            AddColumn("dbo.StageVisitExtract", "RecordUUID", c => c.String(maxLength: 150));
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageVisitExtract", "RecordUUID");
            DropColumn("dbo.StageStatusExtract", "RecordUUID");
            DropColumn("dbo.StagePharmacyExtract", "RecordUUID");
            DropColumn("dbo.StagePatientExtract", "RecordUUID");
            DropColumn("dbo.StageOvcExtract", "RecordUUID");
            DropColumn("dbo.StageOtzExtract", "RecordUUID");
            DropColumn("dbo.StageLaboratoryExtract", "RecordUUID");
            DropColumn("dbo.StageIptExtract", "DateOfDiscontinuation");
            DropColumn("dbo.StageIptExtract", "IPTDiscontinuation");
            DropColumn("dbo.StageIptExtract", "TPTInitiationDate");
            DropColumn("dbo.StageIptExtract", "RecordUUID");
            DropColumn("dbo.StageGbvScreeningExtract", "RecordUUID");
            DropColumn("dbo.StageEnhancedAdherenceCounsellingExtract", "RecordUUID");
            DropColumn("dbo.StageDrugAlcoholScreeningExtract", "RecordUUID");
            DropColumn("dbo.StageDepressionScreeningExtract", "RecordUUID");
            DropColumn("dbo.StageDefaulterTracingExtract", "RecordUUID");
            DropColumn("dbo.StageCovidExtract", "RecordUUID");
            DropColumn("dbo.StageContactListingExtract", "RecordUUID");
            DropColumn("dbo.StageCervicalCancerScreeningExtract", "RecordUUID");
            DropColumn("dbo.StageBaselineExtract", "RecordUUID");
            DropColumn("dbo.StageArtExtract", "RecordUUID");
            DropColumn("dbo.StageAllergiesChronicIllnessExtract", "RecordUUID");
            DropColumn("dbo.StageAdverseEventExtract", "RecordUUID");
            DropColumn("dbo.PatientVisitExtract", "RecordUUID");
            DropColumn("dbo.PatientStatusExtract", "RecordUUID");
            DropColumn("dbo.PatientPharmacyExtract", "RecordUUID");
            DropColumn("dbo.PatientLaboratoryExtract", "RecordUUID");
            DropColumn("dbo.PatientBaselinesExtract", "RecordUUID");
            DropColumn("dbo.PatientArtExtract", "RecordUUID");
            DropColumn("dbo.PatientAdverseEventExtract", "RecordUUID");
            DropColumn("dbo.OvcExtract", "RecordUUID");
            DropColumn("dbo.OtzExtract", "RecordUUID");
            DropColumn("dbo.IptExtract", "DateOfDiscontinuation");
            DropColumn("dbo.IptExtract", "IPTDiscontinuation");
            DropColumn("dbo.IptExtract", "TPTInitiationDate");
            DropColumn("dbo.IptExtract", "RecordUUID");
            DropColumn("dbo.GbvScreeningExtract", "RecordUUID");
            DropColumn("dbo.PatientExtract", "RecordUUID");
            DropColumn("dbo.EnhancedAdherenceCounsellingExtract", "RecordUUID");
            DropColumn("dbo.DrugAlcoholScreeningExtract", "RecordUUID");
            DropColumn("dbo.DepressionScreeningExtract", "RecordUUID");
            DropColumn("dbo.DefaulterTracingExtract", "RecordUUID");
            DropColumn("dbo.CovidExtract", "RecordUUID");
            DropColumn("dbo.ContactListingExtract", "RecordUUID");
            DropColumn("dbo.CervicalCancerScreeningExtract", "RecordUUID");
            DropColumn("dbo.AllergiesChronicIllnessExtract", "RecordUUID");
        }
    }
}
