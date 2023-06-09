namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedZscore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientVisitExtract", "ZScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.StageVisitExtract", "ZScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageVisitExtract", "ZScore");
            DropColumn("dbo.PatientVisitExtract", "ZScore");
        }
    }
}
