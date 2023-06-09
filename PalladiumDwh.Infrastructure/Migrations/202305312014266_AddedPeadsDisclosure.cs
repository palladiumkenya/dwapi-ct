namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPeadsDisclosure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientVisitExtract", "PaedsDisclosure", c => c.String(maxLength: 150));
            AddColumn("dbo.StageVisitExtract", "PaedsDisclosure", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageVisitExtract", "PaedsDisclosure");
            DropColumn("dbo.PatientVisitExtract", "PaedsDisclosure");
        }
    }
}
