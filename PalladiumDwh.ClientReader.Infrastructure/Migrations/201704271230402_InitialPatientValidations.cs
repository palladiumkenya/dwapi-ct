namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialPatientValidations : DbMigration
    {
        public override void Up()
        {
            Sql(Scripts.vTempPatientExtractError);
            Sql(Scripts.vTempPatientExtractErrorSummary);
        }
        
        public override void Down()
        {
            Sql("DROP VIEW vTempPatientExtractError;");
            Sql("DROP VIEW vTempPatientExtractErrorSummary;");
        }
    }
}
