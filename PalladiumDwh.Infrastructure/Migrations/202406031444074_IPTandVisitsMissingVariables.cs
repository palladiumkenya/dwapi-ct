namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IPTandVisitsMissingVariables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IptExtract", "Hepatoxicity", c => c.String(maxLength: 150));
            AddColumn("dbo.IptExtract", "PeripheralNeuropathy", c => c.String(maxLength: 150));
            AddColumn("dbo.IptExtract", "Rash", c => c.String(maxLength: 150));
            AddColumn("dbo.IptExtract", "Adherence", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "WantsToGetPregnant", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "AppointmentReminderWillingness", c => c.String(maxLength: 150));
            AddColumn("dbo.StageIptExtract", "Hepatoxicity", c => c.String(maxLength: 150));
            AddColumn("dbo.StageIptExtract", "PeripheralNeuropathy", c => c.String(maxLength: 150));
            AddColumn("dbo.StageIptExtract", "Rash", c => c.String(maxLength: 150));
            AddColumn("dbo.StageIptExtract", "Adherence", c => c.String(maxLength: 150));
            AddColumn("dbo.StageVisitExtract", "WantsToGetPregnant", c => c.String(maxLength: 150));
            AddColumn("dbo.StageVisitExtract", "AppointmentReminderWillingness", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageVisitExtract", "AppointmentReminderWillingness");
            DropColumn("dbo.StageVisitExtract", "WantsToGetPregnant");
            DropColumn("dbo.StageIptExtract", "Adherence");
            DropColumn("dbo.StageIptExtract", "Rash");
            DropColumn("dbo.StageIptExtract", "PeripheralNeuropathy");
            DropColumn("dbo.StageIptExtract", "Hepatoxicity");
            DropColumn("dbo.PatientVisitExtract", "AppointmentReminderWillingness");
            DropColumn("dbo.PatientVisitExtract", "WantsToGetPregnant");
            DropColumn("dbo.IptExtract", "Adherence");
            DropColumn("dbo.IptExtract", "Rash");
            DropColumn("dbo.IptExtract", "PeripheralNeuropathy");
            DropColumn("dbo.IptExtract", "Hepatoxicity");
        }
    }
}
