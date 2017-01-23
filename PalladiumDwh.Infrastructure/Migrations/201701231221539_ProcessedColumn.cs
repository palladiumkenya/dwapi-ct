namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProcessedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Facility", "Processed", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientExtract", "Processed", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientArtExtract", "Processed", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientBaselinesExtract", "Processed", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientLaboratoryExtract", "Processed", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientPharmacyExtract", "Processed", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientStatusExtract", "Processed", c => c.Boolean(nullable: false));
            AddColumn("dbo.PatientVisitExtract", "Processed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientVisitExtract", "Processed");
            DropColumn("dbo.PatientStatusExtract", "Processed");
            DropColumn("dbo.PatientPharmacyExtract", "Processed");
            DropColumn("dbo.PatientLaboratoryExtract", "Processed");
            DropColumn("dbo.PatientBaselinesExtract", "Processed");
            DropColumn("dbo.PatientArtExtract", "Processed");
            DropColumn("dbo.PatientExtract", "Processed");
            DropColumn("dbo.Facility", "Processed");
        }
    }
}
