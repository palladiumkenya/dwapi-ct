namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CCCNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientExtract", "Orphan", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "Inschool", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "PatientType", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "PopulationType", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "KeyPopulationType", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "PatientResidentCounty", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "PatientResidentSubCounty", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "PatientResidentLocation", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "PatientResidentSubLocation", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "PatientResidentWard", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "PatientResidentVillage", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "TransferInDate", c => c.DateTime());
            AddColumn("dbo.PatientVisitExtract", "StabilityAssessment", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "DifferentiatedCare", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "PopulationType", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "KeyPopulationType", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientVisitExtract", "KeyPopulationType");
            DropColumn("dbo.PatientVisitExtract", "PopulationType");
            DropColumn("dbo.PatientVisitExtract", "DifferentiatedCare");
            DropColumn("dbo.PatientVisitExtract", "StabilityAssessment");
            DropColumn("dbo.PatientExtract", "TransferInDate");
            DropColumn("dbo.PatientExtract", "PatientResidentVillage");
            DropColumn("dbo.PatientExtract", "PatientResidentWard");
            DropColumn("dbo.PatientExtract", "PatientResidentSubLocation");
            DropColumn("dbo.PatientExtract", "PatientResidentLocation");
            DropColumn("dbo.PatientExtract", "PatientResidentSubCounty");
            DropColumn("dbo.PatientExtract", "PatientResidentCounty");
            DropColumn("dbo.PatientExtract", "KeyPopulationType");
            DropColumn("dbo.PatientExtract", "PopulationType");
            DropColumn("dbo.PatientExtract", "PatientType");
            DropColumn("dbo.PatientExtract", "Inschool");
            DropColumn("dbo.PatientExtract", "Orphan");
        }
    }
}
