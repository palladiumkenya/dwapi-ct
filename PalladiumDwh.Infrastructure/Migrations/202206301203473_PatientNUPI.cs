namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientNUPI : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientExtract", "NUPI", c => c.String(maxLength: 150));
            AddColumn("dbo.StagePatientExtract", "NUPI", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StagePatientExtract", "NUPI");
            DropColumn("dbo.PatientExtract", "NUPI");
        }
    }
}
