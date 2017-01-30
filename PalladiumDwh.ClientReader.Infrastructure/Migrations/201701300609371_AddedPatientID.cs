namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPatientID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientArtExtract", "PatientID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientBaselinesExtract", "PatientID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "PatientID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientLaboratoryExtract", "PatientID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientPharmacyExtract", "PatientID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientStatusExtract", "PatientID", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "PatientID", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientVisitExtract", "PatientID");
            DropColumn("dbo.PatientStatusExtract", "PatientID");
            DropColumn("dbo.PatientPharmacyExtract", "PatientID");
            DropColumn("dbo.PatientLaboratoryExtract", "PatientID");
            DropColumn("dbo.PatientExtract", "PatientID");
            DropColumn("dbo.PatientBaselinesExtract", "PatientID");
            DropColumn("dbo.PatientArtExtract", "PatientID");
        }
    }
}
