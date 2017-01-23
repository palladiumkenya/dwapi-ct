namespace PalladiumDwh.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class PatientPharmacy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatientPharmacyExtract", "PatientExtractId", "dbo.PatientExtract");
            DropIndex("dbo.PatientPharmacyExtract", new[] { "PatientExtractId" });
            DropColumn("dbo.PatientPharmacyExtract", "PatientExtractId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientPharmacyExtract", "PatientExtractId", c => c.Guid());
            CreateIndex("dbo.PatientPharmacyExtract", "PatientExtractId");
            AddForeignKey("dbo.PatientPharmacyExtract", "PatientExtractId", "dbo.PatientExtract", "Id");
        }
    }
}
