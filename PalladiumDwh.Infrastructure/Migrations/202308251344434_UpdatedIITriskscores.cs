namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedIITriskscores : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IITRiskScoresExtract", "RiskScore", c => c.String(maxLength: 150));
            AlterColumn("dbo.StageIITRiskScoresExtract", "RiskScore", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StageIITRiskScoresExtract", "RiskScore", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.IITRiskScoresExtract", "RiskScore", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
