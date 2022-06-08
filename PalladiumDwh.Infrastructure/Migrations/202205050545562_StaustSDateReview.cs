namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StaustSDateReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientStatusExtract", "EffectiveDiscontinuationDate", c => c.DateTime());
            AddColumn("dbo.StageStatusExtract", "EffectiveDiscontinuationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageStatusExtract", "EffectiveDiscontinuationDate");
            DropColumn("dbo.PatientStatusExtract", "EffectiveDiscontinuationDate");
        }
    }
}
