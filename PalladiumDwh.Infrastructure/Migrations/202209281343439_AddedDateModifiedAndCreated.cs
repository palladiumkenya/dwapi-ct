namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateModifiedAndCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllergiesChronicIllnessExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.AllergiesChronicIllnessExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.ContactListingExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.ContactListingExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.CovidExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.CovidExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.DefaulterTracingExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.DefaulterTracingExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.DepressionScreeningExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.DepressionScreeningExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.DrugAlcoholScreeningExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.DrugAlcoholScreeningExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.EnhancedAdherenceCounsellingExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.EnhancedAdherenceCounsellingExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.PatientExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.PatientExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.GbvScreeningExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.GbvScreeningExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.IptExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.IptExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.OtzExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.OtzExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.OvcExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.OvcExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.PatientAdverseEventExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.PatientAdverseEventExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.PatientArtExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.PatientArtExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.PatientBaselinesExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.PatientBaselinesExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.PatientLaboratoryExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.PatientLaboratoryExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.PatientPharmacyExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.PatientPharmacyExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.PatientStatusExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.PatientStatusExtract", "Date_Last_Modified", c => c.DateTime());
            AddColumn("dbo.PatientVisitExtract", "Date_Created", c => c.DateTime());
            AddColumn("dbo.PatientVisitExtract", "Date_Last_Modified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientVisitExtract", "Date_Last_Modified");
            DropColumn("dbo.PatientVisitExtract", "Date_Created");
            DropColumn("dbo.PatientStatusExtract", "Date_Last_Modified");
            DropColumn("dbo.PatientStatusExtract", "Date_Created");
            DropColumn("dbo.PatientPharmacyExtract", "Date_Last_Modified");
            DropColumn("dbo.PatientPharmacyExtract", "Date_Created");
            DropColumn("dbo.PatientLaboratoryExtract", "Date_Last_Modified");
            DropColumn("dbo.PatientLaboratoryExtract", "Date_Created");
            DropColumn("dbo.PatientBaselinesExtract", "Date_Last_Modified");
            DropColumn("dbo.PatientBaselinesExtract", "Date_Created");
            DropColumn("dbo.PatientArtExtract", "Date_Last_Modified");
            DropColumn("dbo.PatientArtExtract", "Date_Created");
            DropColumn("dbo.PatientAdverseEventExtract", "Date_Last_Modified");
            DropColumn("dbo.PatientAdverseEventExtract", "Date_Created");
            DropColumn("dbo.OvcExtract", "Date_Last_Modified");
            DropColumn("dbo.OvcExtract", "Date_Created");
            DropColumn("dbo.OtzExtract", "Date_Last_Modified");
            DropColumn("dbo.OtzExtract", "Date_Created");
            DropColumn("dbo.IptExtract", "Date_Last_Modified");
            DropColumn("dbo.IptExtract", "Date_Created");
            DropColumn("dbo.GbvScreeningExtract", "Date_Last_Modified");
            DropColumn("dbo.GbvScreeningExtract", "Date_Created");
            DropColumn("dbo.PatientExtract", "Date_Last_Modified");
            DropColumn("dbo.PatientExtract", "Date_Created");
            DropColumn("dbo.EnhancedAdherenceCounsellingExtract", "Date_Last_Modified");
            DropColumn("dbo.EnhancedAdherenceCounsellingExtract", "Date_Created");
            DropColumn("dbo.DrugAlcoholScreeningExtract", "Date_Last_Modified");
            DropColumn("dbo.DrugAlcoholScreeningExtract", "Date_Created");
            DropColumn("dbo.DepressionScreeningExtract", "Date_Last_Modified");
            DropColumn("dbo.DepressionScreeningExtract", "Date_Created");
            DropColumn("dbo.DefaulterTracingExtract", "Date_Last_Modified");
            DropColumn("dbo.DefaulterTracingExtract", "Date_Created");
            DropColumn("dbo.CovidExtract", "Date_Last_Modified");
            DropColumn("dbo.CovidExtract", "Date_Created");
            DropColumn("dbo.ContactListingExtract", "Date_Last_Modified");
            DropColumn("dbo.ContactListingExtract", "Date_Created");
            DropColumn("dbo.AllergiesChronicIllnessExtract", "Date_Last_Modified");
            DropColumn("dbo.AllergiesChronicIllnessExtract", "Date_Created");
        }
    }
}
