using System.Data.Common;
using System.Data.Entity;
using PalladiumDwh.Shared.Data;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class DwapiRemoteContext : DwapiBaseContext
    {

        public DwapiRemoteContext() : base("name=DWAPIRemote")
        {
        }

        public DwapiRemoteContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
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
        }
    }
}
