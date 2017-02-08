namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TempPatientArtExtract", "Emr", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientArtExtract", "Project", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientBaselinesExtract", "Emr", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientBaselinesExtract", "Project", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientLaboratoryExtract", "Emr", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientLaboratoryExtract", "Project", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientPharmacyExtract", "Emr", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientPharmacyExtract", "Project", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientStatusExtract", "Emr", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientStatusExtract", "Project", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientVisitExtract", "Emr", c => c.String(maxLength: 150));
            AddColumn("dbo.TempPatientVisitExtract", "Project", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TempPatientVisitExtract", "Project");
            DropColumn("dbo.TempPatientVisitExtract", "Emr");
            DropColumn("dbo.TempPatientStatusExtract", "Project");
            DropColumn("dbo.TempPatientStatusExtract", "Emr");
            DropColumn("dbo.TempPatientPharmacyExtract", "Project");
            DropColumn("dbo.TempPatientPharmacyExtract", "Emr");
            DropColumn("dbo.TempPatientLaboratoryExtract", "Project");
            DropColumn("dbo.TempPatientLaboratoryExtract", "Emr");
            DropColumn("dbo.TempPatientBaselinesExtract", "Project");
            DropColumn("dbo.TempPatientBaselinesExtract", "Emr");
            DropColumn("dbo.TempPatientArtExtract", "Project");
            DropColumn("dbo.TempPatientArtExtract", "Emr");
        }
    }
}
