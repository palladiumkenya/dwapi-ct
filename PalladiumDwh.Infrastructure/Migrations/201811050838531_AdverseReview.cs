namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdverseReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientAdverseEventExtract", "AdverseEventRegimen", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientAdverseEventExtract", "AdverseEventCause", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientAdverseEventExtract", "AdverseEventCause");
            DropColumn("dbo.PatientAdverseEventExtract", "AdverseEventRegimen");
        }
    }
}
