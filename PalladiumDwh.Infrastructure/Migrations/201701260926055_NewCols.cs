namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCols : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientExtract", "StatusAtCCC", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "StatusAtPMTCT", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "StatusAtTBClinic", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientArtExtract", "DOB", c => c.DateTime());
            AddColumn("dbo.PatientArtExtract", "Gender", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientArtExtract", "Provider", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientBaselinesExtract", "bWAB", c => c.Int());
            AddColumn("dbo.PatientBaselinesExtract", "bWABDate", c => c.DateTime());
            AddColumn("dbo.PatientBaselinesExtract", "eWAB", c => c.Int());
            AddColumn("dbo.PatientBaselinesExtract", "eWABDate", c => c.DateTime());
            AddColumn("dbo.PatientBaselinesExtract", "lastWAB", c => c.Int());
            AddColumn("dbo.PatientBaselinesExtract", "lastWABDate", c => c.DateTime());
            AddColumn("dbo.PatientLaboratoryExtract", "EnrollmentTest", c => c.Int());
            AddColumn("dbo.PatientPharmacyExtract", "Provider", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientPharmacyExtract", "RegimenLine", c => c.String(maxLength: 150));
            AlterColumn("dbo.PatientPharmacyExtract", "ExpectedReturn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientPharmacyExtract", "ExpectedReturn", c => c.String(maxLength: 150));
            DropColumn("dbo.PatientPharmacyExtract", "RegimenLine");
            DropColumn("dbo.PatientPharmacyExtract", "Provider");
            DropColumn("dbo.PatientLaboratoryExtract", "EnrollmentTest");
            DropColumn("dbo.PatientBaselinesExtract", "lastWABDate");
            DropColumn("dbo.PatientBaselinesExtract", "lastWAB");
            DropColumn("dbo.PatientBaselinesExtract", "eWABDate");
            DropColumn("dbo.PatientBaselinesExtract", "eWAB");
            DropColumn("dbo.PatientBaselinesExtract", "bWABDate");
            DropColumn("dbo.PatientBaselinesExtract", "bWAB");
            DropColumn("dbo.PatientArtExtract", "Provider");
            DropColumn("dbo.PatientArtExtract", "Gender");
            DropColumn("dbo.PatientArtExtract", "DOB");
            DropColumn("dbo.PatientExtract", "StatusAtTBClinic");
            DropColumn("dbo.PatientExtract", "StatusAtPMTCT");
            DropColumn("dbo.PatientExtract", "StatusAtCCC");
        }
    }
}
