namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReasonToLabs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientLaboratoryExtract", "Reason", c => c.String(maxLength: 150));
            AddColumn("dbo.StageLaboratoryExtract", "Reason", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageLaboratoryExtract", "Reason");
            DropColumn("dbo.PatientLaboratoryExtract", "Reason");
        }
    }
}
