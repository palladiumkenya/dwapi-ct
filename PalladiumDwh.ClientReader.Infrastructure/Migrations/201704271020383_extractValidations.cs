using MySql.Data.MySqlClient;

namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class extractValidations : DbMigration
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
