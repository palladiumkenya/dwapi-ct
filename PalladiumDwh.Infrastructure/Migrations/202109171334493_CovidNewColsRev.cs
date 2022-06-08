namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CovidNewColsRev : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CovidExtract", "COVID19TestResult", c => c.String(maxLength: 150));
            AddColumn("dbo.CovidExtract", "Sequence", c => c.String(maxLength: 150));
            AddColumn("dbo.CovidExtract", "BoosterDoseVerified", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CovidExtract", "BoosterDoseVerified");
            DropColumn("dbo.CovidExtract", "Sequence");
            DropColumn("dbo.CovidExtract", "COVID19TestResult");
        }
    }
}
