namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialAllValidations : DbMigration
    {
        public override void Up()
        {
            Sql(Scripts.vTempPatientArtExtractError);
            Sql(Scripts.vTempPatientArtExtractErrorSummary);
            Sql(Scripts.vTempPatientBaselinesExtractError);
            Sql(Scripts.vTempPatientBaselinesExtractErrorSummary);
            Sql(Scripts.vTempPatientLaboratoryExtractError);
            Sql(Scripts.vTempPatientLaboratoryExtractErrorSummary);
            Sql(Scripts.vTempPatientPharmacyExtractError);
            Sql(Scripts.vTempPatientPharmacyExtractErrorSummary);
            Sql(Scripts.vTempPatientStatusExtractError);
            Sql(Scripts.vTempPatientStatusExtractErrorSummary);
            Sql(Scripts.vTempPatientVisitExtractError);
            Sql(Scripts.vTempPatientVisitExtractErrorSummary);
        }
        
        public override void Down()
        {
            Sql("DROP VIEW vTempPatientArtExtractErrorSummary");
            Sql("DROP VIEW vTempPatientArtExtractError");
            Sql("DROP VIEW vTempPatientBaselinesExtractErrorSummary");
            Sql("DROP VIEW vTempPatientBaselinesExtractError");
            Sql("DROP VIEW vTempPatientLaboratoryExtractErrorSummary");
            Sql("DROP VIEW vTempPatientLaboratoryExtractError");
            Sql("DROP VIEW vTempPatientPharmacyExtractErrorSummary");
            Sql("DROP VIEW vTempPatientPharmacyExtractError");
            Sql("DROP VIEW vTempPatientStatusExtractErrorSummary");
            Sql("DROP VIEW vTempPatientStatusExtractError");
            Sql("DROP VIEW vTempPatientVisitExtractErrorSummary");
            Sql("DROP VIEW vTempPatientVisitExtractError");
        }
    }
}
