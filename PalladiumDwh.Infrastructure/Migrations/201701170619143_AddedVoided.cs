namespace PalladiumDwh.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedVoided : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientExtract", "Voided", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientExtract", "PatientPID", c => c.Int(nullable: false));
            AddColumn("dbo.PatientArtExtract", "Voided", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientBaselinesExtract", "Voided", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientLaboratoryExtract", "Voided", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientPharmacyExtract", "Voided", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientStatusExtract", "Voided", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientVisitExtract", "Voided", c => c.Boolean(nullable: false));
            DropColumn("dbo.PatientPharmacyExtract", "Uploaded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientPharmacyExtract", "Uploaded", c => c.Int());
            DropColumn("dbo.PatientVisitExtract", "Voided");
            DropColumn("dbo.PatientStatusExtract", "Voided");
            DropColumn("dbo.PatientPharmacyExtract", "Voided");
            DropColumn("dbo.PatientLaboratoryExtract", "Voided");
            DropColumn("dbo.PatientBaselinesExtract", "Voided");
            DropColumn("dbo.PatientArtExtract", "Voided");
            DropColumn("dbo.PatientExtract", "PatientPID");
            DropColumn("dbo.PatientExtract", "Voided");
        }
    }
}
