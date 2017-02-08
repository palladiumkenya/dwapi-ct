namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddFacilityName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientExtract", "FacilityName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientExtract", "FacilityName");
        }
    }
}
