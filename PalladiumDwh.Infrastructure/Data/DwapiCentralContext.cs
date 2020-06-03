using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Data;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data
{
    public class DwapiCentralContext : DwapiBaseContext
    {
        private SqlConnection connection;

        public DwapiCentralContext() : base("name=DWAPICentral")
        {
        }

        public DwapiCentralContext(DbConnection existingConnection, bool contextOwnsConnection) : base(
            existingConnection, contextOwnsConnection)
        {
        }

        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<PatientExtract> PatientExtracts { get; set; }
        public virtual DbSet<PatientArtExtract> PatientArtExtracts { get; set; }
        public virtual DbSet<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }
        public virtual DbSet<PatientLaboratoryExtract> PatientLaboratoryExtracts { get; set; }
        public virtual DbSet<PatientPharmacyExtract> PatientPharmacyExtracts { get; set; }
        public virtual DbSet<PatientStatusExtract> PatientStatusExtracts { get; set; }
        public virtual DbSet<PatientVisitExtract> PatientVisitExtracts { get; set; }
        public virtual DbSet<PatientAdverseEventExtract> PatientAdverseEventExtracts { get; set; }
        public virtual DbSet<MasterFacility> MasterFacilities { get; set; }

        public virtual DbSet<FacilityManifest> Manifests { get; set; }
        public virtual DbSet<FacilityManifestCargo> Cargoes { get; set; }
        public virtual DbSet<ActionRegister> ActionRegisters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Facility>()
                .HasMany(c => c.PatientExtracts)
                .WithRequired()
                .HasForeignKey(f => f.FacilityId);

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientArtExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientBaselinesExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientLaboratoryExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientPharmacyExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientStatusExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientVisitExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientAdverseEventExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);

            DapperPlusManager.Entity<Facility>().Table("Facility").Key(x => x.Id);
            DapperPlusManager.Entity<PatientExtract>().Table("PatientExtract").Key(x => x.Id);
            DapperPlusManager.Entity<PatientArtExtract>().Table("PatientArtExtract").Key(x => x.Id);
            DapperPlusManager.Entity<PatientBaselinesExtract>().Table("PatientBaselinesExtract").Key(x => x.Id);
            DapperPlusManager.Entity<PatientLaboratoryExtract>().Table("PatientLaboratoryExtract").Key(x => x.Id);
            DapperPlusManager.Entity<PatientPharmacyExtract>().Table("PatientPharmacyExtract").Key(x => x.Id);
            DapperPlusManager.Entity<PatientStatusExtract>().Table("PatientStatusExtract").Key(x => x.Id);
            DapperPlusManager.Entity<PatientVisitExtract>().Table("PatientVisitExtract").Key(x => x.Id);
            DapperPlusManager.Entity<PatientAdverseEventExtract>().Table("PatientAdverseEventExtract").Key(x => x.Id);

            DapperPlusManager.Entity<FacilityManifest>().Table("FacilityManifest").Key(x => x.Id);
            DapperPlusManager.Entity<FacilityManifestCargo>().Table("FacilityManifestCargo").Key(x => x.Id);
        }

        public SqlConnection GetConnection()
        {
            if (null == connection)
                connection = new SqlConnection(Database.Connection.ConnectionString);

            if (connection.State == ConnectionState.Open)
                return connection;
 
            connection.Open();
            return connection;
        }
    }
}
