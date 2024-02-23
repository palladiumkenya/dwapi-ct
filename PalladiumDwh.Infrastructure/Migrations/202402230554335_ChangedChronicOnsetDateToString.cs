namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedChronicOnsetDateToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AllergiesChronicIllnessExtract", "ChronicOnsetDate", c => c.String(maxLength: 150));
            AlterColumn("dbo.StageAllergiesChronicIllnessExtract", "ChronicOnsetDate", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StageAllergiesChronicIllnessExtract", "ChronicOnsetDate", c => c.DateTime());
            AlterColumn("dbo.AllergiesChronicIllnessExtract", "ChronicOnsetDate", c => c.DateTime());
        }
    }
}
