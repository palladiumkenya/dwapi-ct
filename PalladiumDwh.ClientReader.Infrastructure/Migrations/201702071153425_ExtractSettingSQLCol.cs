namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ExtractSettingSQLCol : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ExtractSetting", "ExtractSql", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExtractSetting", "ExtractSql", c => c.String(maxLength: 150));
        }
    }
}
