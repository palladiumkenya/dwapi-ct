namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcheckerorrFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TempPatientArtExtractError", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientArtExtract", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientBaselinesExtractError", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientBaselinesExtract", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientExtractError", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientExtract", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientLaboratoryExtractError", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientLaboratoryExtract", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientPharmacyExtractError", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientPharmacyExtract", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientStatusExtractError", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientStatusExtract", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientVisitExtractError", "CheckError", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempPatientVisitExtract", "CheckError", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TempPatientVisitExtract", "CheckError");
            DropColumn("dbo.TempPatientVisitExtractError", "CheckError");
            DropColumn("dbo.TempPatientStatusExtract", "CheckError");
            DropColumn("dbo.TempPatientStatusExtractError", "CheckError");
            DropColumn("dbo.TempPatientPharmacyExtract", "CheckError");
            DropColumn("dbo.TempPatientPharmacyExtractError", "CheckError");
            DropColumn("dbo.TempPatientLaboratoryExtract", "CheckError");
            DropColumn("dbo.TempPatientLaboratoryExtractError", "CheckError");
            DropColumn("dbo.TempPatientExtract", "CheckError");
            DropColumn("dbo.TempPatientExtractError", "CheckError");
            DropColumn("dbo.TempPatientBaselinesExtract", "CheckError");
            DropColumn("dbo.TempPatientBaselinesExtractError", "CheckError");
            DropColumn("dbo.TempPatientArtExtract", "CheckError");
            DropColumn("dbo.TempPatientArtExtractError", "CheckError");
        }
    }
}
