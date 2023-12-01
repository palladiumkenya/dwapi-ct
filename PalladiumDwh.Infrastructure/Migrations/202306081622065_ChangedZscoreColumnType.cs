namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedZscoreColumnType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientVisitExtract", "ZScore", c => c.String(maxLength: 150));
            AlterColumn("dbo.StageVisitExtract", "ZScore", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StageVisitExtract", "ZScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PatientVisitExtract", "ZScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
