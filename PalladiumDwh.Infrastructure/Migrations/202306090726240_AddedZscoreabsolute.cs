namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedZscoreabsolute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientVisitExtract", "ZScoreAbsolute", c => c.Int());
            AddColumn("dbo.StageVisitExtract", "ZScoreAbsolute", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageVisitExtract", "ZScoreAbsolute");
            DropColumn("dbo.PatientVisitExtract", "ZScoreAbsolute");
        }
    }
}
