namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonBPatientPkToRelationships : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RelationshipsExtract", "PersonAPatientPk", c => c.Int(nullable: false));
            AddColumn("dbo.RelationshipsExtract", "PersonBPatientPk", c => c.Int(nullable: false));
            AddColumn("dbo.RelationshipsExtract", "PatientRelationshipToOther", c => c.String(maxLength: 150));
            AddColumn("dbo.StageRelationshipsExtract", "PersonAPatientPk", c => c.Int(nullable: false));
            AddColumn("dbo.StageRelationshipsExtract", "PersonBPatientPk", c => c.Int(nullable: false));
            AddColumn("dbo.StageRelationshipsExtract", "PatientRelationshipToOther", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StageRelationshipsExtract", "PatientRelationshipToOther");
            DropColumn("dbo.StageRelationshipsExtract", "PersonBPatientPk");
            DropColumn("dbo.StageRelationshipsExtract", "PersonAPatientPk");
            DropColumn("dbo.RelationshipsExtract", "PatientRelationshipToOther");
            DropColumn("dbo.RelationshipsExtract", "PersonBPatientPk");
            DropColumn("dbo.RelationshipsExtract", "PersonAPatientPk");
        }
    }
}
