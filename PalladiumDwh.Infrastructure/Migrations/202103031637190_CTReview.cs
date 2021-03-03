namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CTReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientExtract", "Pkv", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientExtract", "Occupation", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientLaboratoryExtract", "DateSampleTaken", c => c.DateTime());
            AddColumn("dbo.PatientLaboratoryExtract", "SampleType", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientPharmacyExtract", "RegimenChangedSwitched", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientPharmacyExtract", "RegimenChangeSwitchReason", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientPharmacyExtract", "StopRegimenReason", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientPharmacyExtract", "StopRegimenDate", c => c.DateTime());
            AddColumn("dbo.PatientStatusExtract", "TOVerified", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientStatusExtract", "TOVerifiedDate", c => c.DateTime());
            AddColumn("dbo.PatientVisitExtract", "VisitBy", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "Temp", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PatientVisitExtract", "PulseRate", c => c.Int());
            AddColumn("dbo.PatientVisitExtract", "RespiratoryRate", c => c.Int());
            AddColumn("dbo.PatientVisitExtract", "OxygenSaturation", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PatientVisitExtract", "Muac", c => c.Int());
            AddColumn("dbo.PatientVisitExtract", "NutritionalStatus", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "EverHadMenses", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "Breastfeeding", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "Menopausal", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "NoFPReason", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "ProphylaxisUsed", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "CTXAdherence", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "CurrentRegimen", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "HCWConcern", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "TCAReason", c => c.String(maxLength: 150));
            AddColumn("dbo.PatientVisitExtract", "ClinicalNotes", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientVisitExtract", "ClinicalNotes");
            DropColumn("dbo.PatientVisitExtract", "TCAReason");
            DropColumn("dbo.PatientVisitExtract", "HCWConcern");
            DropColumn("dbo.PatientVisitExtract", "CurrentRegimen");
            DropColumn("dbo.PatientVisitExtract", "CTXAdherence");
            DropColumn("dbo.PatientVisitExtract", "ProphylaxisUsed");
            DropColumn("dbo.PatientVisitExtract", "NoFPReason");
            DropColumn("dbo.PatientVisitExtract", "Menopausal");
            DropColumn("dbo.PatientVisitExtract", "Breastfeeding");
            DropColumn("dbo.PatientVisitExtract", "EverHadMenses");
            DropColumn("dbo.PatientVisitExtract", "NutritionalStatus");
            DropColumn("dbo.PatientVisitExtract", "Muac");
            DropColumn("dbo.PatientVisitExtract", "OxygenSaturation");
            DropColumn("dbo.PatientVisitExtract", "RespiratoryRate");
            DropColumn("dbo.PatientVisitExtract", "PulseRate");
            DropColumn("dbo.PatientVisitExtract", "Temp");
            DropColumn("dbo.PatientVisitExtract", "VisitBy");
            DropColumn("dbo.PatientStatusExtract", "TOVerifiedDate");
            DropColumn("dbo.PatientStatusExtract", "TOVerified");
            DropColumn("dbo.PatientPharmacyExtract", "StopRegimenDate");
            DropColumn("dbo.PatientPharmacyExtract", "StopRegimenReason");
            DropColumn("dbo.PatientPharmacyExtract", "RegimenChangeSwitchReason");
            DropColumn("dbo.PatientPharmacyExtract", "RegimenChangedSwitched");
            DropColumn("dbo.PatientLaboratoryExtract", "SampleType");
            DropColumn("dbo.PatientLaboratoryExtract", "DateSampleTaken");
            DropColumn("dbo.PatientExtract", "Occupation");
            DropColumn("dbo.PatientExtract", "Pkv");
        }
    }
}
