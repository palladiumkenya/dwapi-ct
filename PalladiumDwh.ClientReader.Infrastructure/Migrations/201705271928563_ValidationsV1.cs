namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationsV1 : DbMigration
    {
        public override void Up()
        {
            Sql(Scripts.vTempPatientExtractErrorSummaryV1);
            Sql(Scripts.vTempPatientArtExtractErrorSummaryV1);
            Sql(Scripts.vTempPatientBaselinesExtractErrorSummaryV1);
            Sql(Scripts.vTempPatientLaboratoryExtractErrorSummaryV1);
            Sql(Scripts.vTempPatientPharmacyExtractErrorSummaryV1);
            Sql(Scripts.vTempPatientStatusExtractErrorSummaryV1);
            Sql(Scripts.vTempPatientVisitExtractErrorSummaryV1);
        }

        public override void Down()
        {
            Sql(Scripts.vTempPatientExtractErrorSummary.Replace("CREATE", "ALTER"));
            Sql(Scripts.vTempPatientArtExtractErrorSummary.Replace("CREATE", "ALTER"));
            Sql(Scripts.vTempPatientBaselinesExtractErrorSummary.Replace("CREATE", "ALTER"));
            Sql(Scripts.vTempPatientLaboratoryExtractErrorSummary.Replace("CREATE", "ALTER"));
            Sql(Scripts.vTempPatientPharmacyExtractErrorSummary.Replace("CREATE", "ALTER"));
            Sql(Scripts.vTempPatientStatusExtractErrorSummary.Replace("CREATE", "ALTER")); ;
            Sql(Scripts.vTempPatientVisitExtractErrorSummary.Replace("CREATE", "ALTER"));
        }
    }
}
