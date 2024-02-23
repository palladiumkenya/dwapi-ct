namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDefaulterTracingVariables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllergiesChronicIllnessExtract", "Controlled", c => c.String(maxLength: 150));
            AddColumn("dbo.DefaulterTracingExtract", "DatePromisedToCome", c => c.DateTime());
            AddColumn("dbo.DefaulterTracingExtract", "ReasonForMissedAppointment", c => c.String(maxLength: 150));
            AddColumn("dbo.DefaulterTracingExtract", "DateOfMissedAppointment", c => c.DateTime());
            AddColumn("dbo.StageAllergiesChronicIllnessExtract", "Controlled", c => c.String(maxLength: 150));
            AddColumn("dbo.StageDefaulterTracingExtract", "DatePromisedToCome", c => c.DateTime());
            AddColumn("dbo.StageDefaulterTracingExtract", "ReasonForMissedAppointment", c => c.String(maxLength: 150));
            AddColumn("dbo.StageDefaulterTracingExtract", "DateOfMissedAppointment", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageDefaulterTracingExtract", "DateOfMissedAppointment");
            DropColumn("dbo.StageDefaulterTracingExtract", "ReasonForMissedAppointment");
            DropColumn("dbo.StageDefaulterTracingExtract", "DatePromisedToCome");
            DropColumn("dbo.StageAllergiesChronicIllnessExtract", "Controlled");
            DropColumn("dbo.DefaulterTracingExtract", "DateOfMissedAppointment");
            DropColumn("dbo.DefaulterTracingExtract", "ReasonForMissedAppointment");
            DropColumn("dbo.DefaulterTracingExtract", "DatePromisedToCome");
            DropColumn("dbo.AllergiesChronicIllnessExtract", "Controlled");
        }
    }
}
