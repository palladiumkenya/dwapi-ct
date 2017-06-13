using System.Data.Common;
using System.Data.Entity;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Data;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class DwapiRemoteContext : DwapiBaseContext
    {

        public DwapiRemoteContext() : base("name=DWAPIRemote")
        {
            Database.SetInitializer(new DwapiRemoteDbInitializer());
        }

        public DwapiRemoteContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<EMR> Emrs { get; set; }
        public virtual DbSet<ExtractSetting> ExtractSettings { get; set; }
        public virtual DbSet<EventHistory> EventHistories { get; set; }


        public virtual DbSet<TempPatientExtract> TempPatientExtracts { get; set; }
        public virtual DbSet<TempPatientArtExtract> TempPatientArtExtracts { get; set; }
        public virtual DbSet<TempPatientBaselinesExtract> TempPatientBaselinesExtracts { get; set; }
        public virtual DbSet<TempPatientLaboratoryExtract> TempPatientLaboratoryExtracts { get; set; }
        public virtual DbSet<TempPatientPharmacyExtract> TempPatientPharmacyExtracts { get; set; }
        public virtual DbSet<TempPatientStatusExtract> TempPatientStatusExtracts { get; set; }
        public virtual DbSet<TempPatientVisitExtract> TempPatientVisitExtracts { get; set; }

        public virtual DbSet<Validator> Validators { get; set; }
        public virtual DbSet<ValidationError> ValidationErrors { get; set; }
       

        public virtual DbSet<TempPatientExtractError> TempPatientExtractErrors { get; set; }
        public virtual DbSet<TempPatientExtractErrorSummary> TempPatientExtractsErrorSummaries { get; set; }
        public virtual DbSet<TempPatientArtExtractError> TempPatientArtExtractErrors { get; set; }
        public virtual DbSet<TempPatientArtExtractErrorSummary> TempPatientArtExtractErrorSummaries { get; set; }
        public virtual DbSet<TempPatientBaselinesExtractError> TempPatientBaselinesExtractErrors  { get; set; }
        public virtual DbSet<TempPatientBaselinesExtractErrorSummary> TempPatientBaselinesExtractErrorSummaries { get; set; }
        public virtual DbSet<TempPatientLaboratoryExtractError> TmPatientLaboratoryExtractErrors { get; set; }
        public virtual DbSet<TempPatientLaboratoryExtractErrorSummary> TempPatientLaboratoryExtractErrorSummaries { get; set; }
        public virtual DbSet<TempPatientPharmacyExtractError> TempPatientPharmacyExtractErrors { get; set; }
        public virtual DbSet<TempPatientPharmacyExtractErrorSummary> TempPatientPharmacyExtractErrorSummaries { get; set; }
        public virtual DbSet<TempPatientStatusExtractError> TempPatientStatusExtractErrors { get; set; }
        public virtual DbSet<TempPatientStatusExtractErrorSummary> TempPatientStatusExtractErrorSummaries { get; set; }
        public virtual DbSet<TempPatientVisitExtractError> TempPatientVisitExtractErrors { get; set; }
        public virtual DbSet<TempPatientVisitExtractErrorSummary> TempPatientVisitExtractErrorSummaries { get; set; }
       

        public virtual DbSet<ClientFacility> ClientFacilities { get; set; }

        public virtual DbSet<ClientPatientExtract> ClientPatientExtracts { get; set; }
        public virtual DbSet<ClientPatientArtExtract> ClientPatientArtExtracts { get; set; }
        public virtual DbSet<ClientPatientBaselinesExtract> ClientPatientBaselinesExtracts { get; set; }
        public virtual DbSet<ClientPatientLaboratoryExtract> ClientPatientLaboratoryExtracts { get; set; }
        public virtual DbSet<ClientPatientPharmacyExtract> ClientPatientPharmacyExtracts { get; set; }
        public virtual DbSet<ClientPatientStatusExtract> ClientPatientStatusExtracts { get; set; }
        public virtual DbSet<ClientPatientVisitExtract> ClientPatientVisitExtracts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
            .HasMany(c => c.Emrs)
            .WithRequired()
            .HasForeignKey(f => new { f.ProjectId });

            modelBuilder.Entity<EMR>()
           .HasMany(c => c.ExtractSettings)
           .WithRequired()
           .HasForeignKey(f => new { f.EmrId });

            modelBuilder.Entity<ExtractSetting>()
                .HasMany(c => c.Events)
                .WithRequired()
                .HasForeignKey(f => new { f.ExtractSettingId });


            modelBuilder.Entity<ClientPatientExtract>()
               .HasMany(c => c.ClientPatientArtExtracts)
               .WithRequired()
               .HasForeignKey(f =>new { f.PatientPK,f.SiteCode});

            modelBuilder.Entity<ClientPatientExtract>()
               .HasMany(c => c.ClientPatientBaselinesExtracts)
               .WithRequired()
               .HasForeignKey(f => new { f.PatientPK, f.SiteCode });

            modelBuilder.Entity<ClientPatientExtract>()
               .HasMany(c => c.ClientPatientLaboratoryExtracts)
               .WithRequired()
               .HasForeignKey(f => new { f.PatientPK, f.SiteCode });

            modelBuilder.Entity<ClientPatientExtract>()
               .HasMany(c => c.ClientPatientPharmacyExtracts)
               .WithRequired()
               .HasForeignKey(f => new { f.PatientPK, f.SiteCode });

            modelBuilder.Entity<ClientPatientExtract>()
               .HasMany(c => c.ClientPatientStatusExtracts)
               .WithRequired()
               .HasForeignKey(f => new { f.PatientPK, f.SiteCode });

            modelBuilder.Entity<ClientPatientExtract>()
               .HasMany(c => c.ClientPatientVisitExtracts)
               .WithRequired()
               .HasForeignKey(f => new { f.PatientPK, f.SiteCode });

            modelBuilder.Entity<Validator>()
                .HasMany(c => c.ValidationErrors)
                .WithRequired()
                .HasForeignKey(f => new { f.ValidatorId });
            
            modelBuilder.Entity<TempPatientExtractError>()
                .HasMany(c => c.TempPatientExtractErrorSummaries)
                .WithRequired()
                .HasForeignKey(f => new { f.RecordId });

            modelBuilder.Entity<TempPatientArtExtractError>()
                .HasMany(c => c.TempPatientArtExtractErrorSummaries)
                .WithRequired()
                .HasForeignKey(f => new { f.RecordId });

            modelBuilder.Entity<TempPatientBaselinesExtractError>()
                .HasMany(c => c.TempPatientBaselinesExtractErrorSummaries)
                .WithRequired()
                .HasForeignKey(f => new { f.RecordId });

            modelBuilder.Entity<TempPatientLaboratoryExtractError>()
                .HasMany(c => c.TempPatientLaboratoryExtractErrorSummaries)
                .WithRequired()
                .HasForeignKey(f => new { f.RecordId });

            modelBuilder.Entity<TempPatientPharmacyExtractError>()
                .HasMany(c => c.TempPatientPharmacyExtractErrorSummaries)
                .WithRequired()
                .HasForeignKey(f => new { f.RecordId });

            modelBuilder.Entity<TempPatientStatusExtractError>()
                .HasMany(c => c.TempPatientStatusExtractErrorSummaries)
                .WithRequired()
                .HasForeignKey(f => new { f.RecordId });

            modelBuilder.Entity<TempPatientVisitExtractError>()
                .HasMany(c => c.TempPatientVisitExtractErrorSummaries)
                .WithRequired()
                .HasForeignKey(f => new { f.RecordId });
           

        }
    }
}
