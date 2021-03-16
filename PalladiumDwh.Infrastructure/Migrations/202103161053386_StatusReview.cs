namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientStatusExtract", "ReEnrollmentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientStatusExtract", "ReEnrollmentDate");
        }
    }
}
