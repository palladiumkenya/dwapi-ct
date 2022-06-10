namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contactpk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactListingExtract", "ContactPatientPK", c => c.Int());
            AddColumn("dbo.StageContactListingExtract", "ContactPatientPK", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageContactListingExtract", "ContactPatientPK");
            DropColumn("dbo.ContactListingExtract", "ContactPatientPK");
        }
    }
}
