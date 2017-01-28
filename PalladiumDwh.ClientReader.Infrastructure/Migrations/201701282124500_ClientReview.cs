namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientReview : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PatientArtExtract");
            DropPrimaryKey("dbo.PatientBaselinesExtract");
            DropPrimaryKey("dbo.PatientLaboratoryExtract");
            DropPrimaryKey("dbo.PatientPharmacyExtract");
            DropPrimaryKey("dbo.PatientStatusExtract");
            DropPrimaryKey("dbo.PatientVisitExtract");
            AddColumn("dbo.PatientArtExtract", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientBaselinesExtract", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientExtract", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientLaboratoryExtract", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientPharmacyExtract", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientStatusExtract", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientVisitExtract", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.PatientArtExtract", "Id");
            AddPrimaryKey("dbo.PatientBaselinesExtract", "Id");
            AddPrimaryKey("dbo.PatientLaboratoryExtract", "Id");
            AddPrimaryKey("dbo.PatientPharmacyExtract", "Id");
            AddPrimaryKey("dbo.PatientStatusExtract", "Id");
            AddPrimaryKey("dbo.PatientVisitExtract", "Id");
            DropColumn("dbo.PatientArtExtract", "UId");
            DropColumn("dbo.PatientBaselinesExtract", "UId");
            DropColumn("dbo.PatientExtract", "UId");
            DropColumn("dbo.PatientLaboratoryExtract", "UId");
            DropColumn("dbo.PatientPharmacyExtract", "UId");
            DropColumn("dbo.PatientStatusExtract", "UId");
            DropColumn("dbo.PatientVisitExtract", "UId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientVisitExtract", "UId", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientStatusExtract", "UId", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientPharmacyExtract", "UId", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientLaboratoryExtract", "UId", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientExtract", "UId", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientBaselinesExtract", "UId", c => c.Guid(nullable: false));
            AddColumn("dbo.PatientArtExtract", "UId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.PatientVisitExtract");
            DropPrimaryKey("dbo.PatientStatusExtract");
            DropPrimaryKey("dbo.PatientPharmacyExtract");
            DropPrimaryKey("dbo.PatientLaboratoryExtract");
            DropPrimaryKey("dbo.PatientBaselinesExtract");
            DropPrimaryKey("dbo.PatientArtExtract");
            DropColumn("dbo.PatientVisitExtract", "Id");
            DropColumn("dbo.PatientStatusExtract", "Id");
            DropColumn("dbo.PatientPharmacyExtract", "Id");
            DropColumn("dbo.PatientLaboratoryExtract", "Id");
            DropColumn("dbo.PatientExtract", "Id");
            DropColumn("dbo.PatientBaselinesExtract", "Id");
            DropColumn("dbo.PatientArtExtract", "Id");
            AddPrimaryKey("dbo.PatientVisitExtract", "UId");
            AddPrimaryKey("dbo.PatientStatusExtract", "UId");
            AddPrimaryKey("dbo.PatientPharmacyExtract", "UId");
            AddPrimaryKey("dbo.PatientLaboratoryExtract", "UId");
            AddPrimaryKey("dbo.PatientBaselinesExtract", "UId");
            AddPrimaryKey("dbo.PatientArtExtract", "UId");
        }
    }
}
