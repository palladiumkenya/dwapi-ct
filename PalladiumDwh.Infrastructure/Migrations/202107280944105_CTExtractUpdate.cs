namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CTExtractUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientArtExtract", "PreviousARTUse", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientArtExtract", "PreviousARTPurpose", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientArtExtract", "DateLastUsed", c => c.DateTime());
            AddColumn("dbo.PatientVisitExtract", "GeneralExamination", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "SystemExamination", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "Skin", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "Eyes", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "ENT", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "Chest", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "CVS", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "Abdomen", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "CNS", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "Genitourinary", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientVisitExtract", "Genitourinary");
            DropColumn("dbo.PatientVisitExtract", "CNS");
            DropColumn("dbo.PatientVisitExtract", "Abdomen");
            DropColumn("dbo.PatientVisitExtract", "CVS");
            DropColumn("dbo.PatientVisitExtract", "Chest");
            DropColumn("dbo.PatientVisitExtract", "ENT");
            DropColumn("dbo.PatientVisitExtract", "Eyes");
            DropColumn("dbo.PatientVisitExtract", "Skin");
            DropColumn("dbo.PatientVisitExtract", "SystemExamination");
            DropColumn("dbo.PatientVisitExtract", "GeneralExamination");
            DropColumn("dbo.PatientArtExtract", "DateLastUsed");
            DropColumn("dbo.PatientArtExtract", "PreviousARTPurpose");
            DropColumn("dbo.PatientArtExtract", "PreviousARTUse");
        }
    }
}
