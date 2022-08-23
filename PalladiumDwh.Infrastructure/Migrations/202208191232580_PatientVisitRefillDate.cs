namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientVisitRefillDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientVisitExtract", "RefillDate", c => c.DateTime());
            AddColumn("dbo.StageVisitExtract", "RefillDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageVisitExtract", "RefillDate");
            DropColumn("dbo.PatientVisitExtract", "RefillDate");
        }
    }
}
