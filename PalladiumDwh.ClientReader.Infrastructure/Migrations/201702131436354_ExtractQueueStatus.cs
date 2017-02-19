namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtractQueueStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientArtExtract", "QueueId", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientArtExtract", "Status", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientArtExtract", "StatusDate", c => c.DateTime());
            AddColumn("dbo.PatientBaselinesExtract", "QueueId", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientBaselinesExtract", "Status", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientBaselinesExtract", "StatusDate", c => c.DateTime());
            AddColumn("dbo.PatientExtract", "QueueId", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "Status", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "StatusDate", c => c.DateTime());
            AddColumn("dbo.PatientLaboratoryExtract", "QueueId", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientLaboratoryExtract", "Status", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientLaboratoryExtract", "StatusDate", c => c.DateTime());
            AddColumn("dbo.PatientPharmacyExtract", "QueueId", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientPharmacyExtract", "Status", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientPharmacyExtract", "StatusDate", c => c.DateTime());
            AddColumn("dbo.PatientStatusExtract", "QueueId", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientStatusExtract", "Status", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientStatusExtract", "StatusDate", c => c.DateTime());
            AddColumn("dbo.PatientVisitExtract", "QueueId", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "Status", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "StatusDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientVisitExtract", "StatusDate");
            DropColumn("dbo.PatientVisitExtract", "Status");
            DropColumn("dbo.PatientVisitExtract", "QueueId");
            DropColumn("dbo.PatientStatusExtract", "StatusDate");
            DropColumn("dbo.PatientStatusExtract", "Status");
            DropColumn("dbo.PatientStatusExtract", "QueueId");
            DropColumn("dbo.PatientPharmacyExtract", "StatusDate");
            DropColumn("dbo.PatientPharmacyExtract", "Status");
            DropColumn("dbo.PatientPharmacyExtract", "QueueId");
            DropColumn("dbo.PatientLaboratoryExtract", "StatusDate");
            DropColumn("dbo.PatientLaboratoryExtract", "Status");
            DropColumn("dbo.PatientLaboratoryExtract", "QueueId");
            DropColumn("dbo.PatientExtract", "StatusDate");
            DropColumn("dbo.PatientExtract", "Status");
            DropColumn("dbo.PatientExtract", "QueueId");
            DropColumn("dbo.PatientBaselinesExtract", "StatusDate");
            DropColumn("dbo.PatientBaselinesExtract", "Status");
            DropColumn("dbo.PatientBaselinesExtract", "QueueId");
            DropColumn("dbo.PatientArtExtract", "StatusDate");
            DropColumn("dbo.PatientArtExtract", "Status");
            DropColumn("dbo.PatientArtExtract", "QueueId");
        }
    }
}
