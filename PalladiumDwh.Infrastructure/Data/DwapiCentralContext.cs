using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Infrastructure.Migrations;
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
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 900; // s
        }

        public DwapiCentralContext(DbConnection existingConnection, bool contextOwnsConnection) : base(
            existingConnection, contextOwnsConnection)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 900; // s
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

        public virtual DbSet<AllergiesChronicIllnessExtract> AllergiesChronicIllnessExtracts { get; set; }

        public virtual DbSet<ContactListingExtract> ContactListingExtracts { get; set; }

        public virtual DbSet<DepressionScreeningExtract> DepressionScreeningExtracts { get; set; }

        public virtual DbSet<EnhancedAdherenceCounsellingExtract> EnhancedAdherenceCounsellingExtracts { get; set; }

        public virtual DbSet<DrugAlcoholScreeningExtract> DrugAlcoholScreeningExtracts { get; set; }

        public virtual DbSet<GbvScreeningExtract> GbvScreeningExtracts { get; set; }
        public virtual DbSet<IptExtract> IptExtracts { get; set; }
        public virtual DbSet<OtzExtract> OtzExtracts { get; set; }
        public virtual DbSet<OvcExtract> OvcExtracts { get; set; }
        public virtual DbSet<CovidExtract> CovidExtracts { get; set; }
        public virtual DbSet<DefaulterTracingExtract> DefaulterTracingExtracts { get; set; }
        public virtual DbSet<CervicalCancerScreeningExtract> CervicalCancerScreeningExtracts { get; set; }
        public virtual DbSet<IITRiskScoresExtract> IITRiskScoresExtracts { get; set; }


        public virtual DbSet<StagePatientExtract> StagePatientExtracts { get; set; }
        public virtual DbSet<StageVisitExtract> StageVisitExtracts { get; set; }
        public virtual DbSet<StageAdverseEventExtract> StageAdverseEventExtracts { get; set; }
        public virtual DbSet<StageAllergiesChronicIllnessExtract> StageAllergiesChronicIllnessExtracts { get; set; }
        public virtual DbSet<StageArtExtract> StageArtExtracts { get; set; }
        public virtual DbSet<StageBaselineExtract> StageBaselineExtracts { get; set; }
        public virtual DbSet<StageContactListingExtract> StageContactListingExtracts { get; set; }
        public virtual DbSet<StageCovidExtract> StageCovidExtracts { get; set; }
        public virtual DbSet<StageDefaulterTracingExtract> StageDefaulterTracingExtracts { get; set; }
        public virtual DbSet<StageDepressionScreeningExtract> StageDepressionScreeningExtracts { get; set; }
        public virtual DbSet<StageDrugAlcoholScreeningExtract> StageDrugAlcoholScreeningExtracts { get; set; }
        public virtual DbSet<StageEnhancedAdherenceCounsellingExtract> StageEnhancedAdherenceCounsellingExtracts { get; set; }
        public virtual DbSet<StageIptExtract> StageIptExtracts { get; set; }
        public virtual DbSet<StageLaboratoryExtract> StageLaboratoryExtracts { get; set; }
        public virtual DbSet<StageOtzExtract> StageOtzExtracts { get; set; }
        public virtual DbSet<StageOvcExtract> StageOvcExtracts { get; set; }
        public virtual DbSet<StagePharmacyExtract> StagePharmacyExtracts { get; set; }
        public virtual DbSet<StageStatusExtract> StageStatusExtracts { get; set; }
        public virtual DbSet<StageGbvScreeningExtract> StageGbvScreeningExtracts { get; set; }
        public virtual DbSet<StageCervicalCancerScreeningExtract> StageCervicalCancerScreeningExtracts { get; set; }
        public virtual DbSet<StageIITRiskScoresExtract> StageIITRiskScoresExtracts { get; set; }

        public virtual DbSet<SmartActionRegister> SmartActionRegisters { get; set; }


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


            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.AllergiesChronicIllnessExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.ContactListingExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientAdverseEventExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.DrugAlcoholScreeningExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.EnhancedAdherenceCounsellingExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.GbvScreeningExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.IptExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.OtzExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.OvcExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.CovidExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.DefaulterTracingExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);
            
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.CervicalCancerScreeningExtracts)
                .WithRequired()
                .HasForeignKey(f => f.PatientId);
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.IITRiskScoresExtracts)
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

            DapperPlusManager.Entity<PatientExtract>().BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<PatientArtExtract>().BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<PatientBaselinesExtract>().BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<PatientLaboratoryExtract>().BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<PatientPharmacyExtract>().BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<PatientStatusExtract>().BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<PatientVisitExtract>().BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<PatientAdverseEventExtract>().BatchDelayInterval(Shared.Custom.Utility.GenDelay());

            DapperPlusManager.Entity<AllergiesChronicIllnessExtract>().Key(x => x.Id)
                .Table($"AllergiesChronicIllnessExtract").BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<IptExtract>().Key(x => x.Id).Table($"IptExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<DepressionScreeningExtract>().Key(x => x.Id).Table($"DepressionScreeningExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<ContactListingExtract>().Key(x => x.Id).Table($"ContactListingExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<GbvScreeningExtract>().Key(x => x.Id).Table($"GbvScreeningExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<EnhancedAdherenceCounsellingExtract>().Key(x => x.Id)
                .Table($"EnhancedAdherenceCounsellingExtract").BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<DrugAlcoholScreeningExtract>().Key(x => x.Id).Table($"DrugAlcoholScreeningExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<OvcExtract>().Key(x => x.Id).Table($"OvcExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<OtzExtract>().Key(x => x.Id).Table($"OtzExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<CovidExtract>().Key(x => x.Id).Table($"CovidExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<DefaulterTracingExtract>().Key(x => x.Id).Table($"DefaulterTracingExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<CervicalCancerScreeningExtract>().Key(x => x.Id).Table($"CervicalCancerScreeningExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            DapperPlusManager.Entity<IITRiskScoresExtract>().Key(x => x.Id).Table($"IITRiskScoresExtract")
                .BatchDelayInterval(Shared.Custom.Utility.GenDelay());
            
            DapperPlusManager.Entity<ActionRegister>().Key(x => x.Id).Table($"{nameof(ActionRegister)}");

            DapperPlusManager.Entity<StagePatientExtract>().Key(x => x.Id).Table($"{nameof(StagePatientExtract)}");
            DapperPlusManager.Entity<StageVisitExtract>().Key(x => x.Id).Table($"{nameof(StageVisitExtract)}");
            DapperPlusManager.Entity<StageAdverseEventExtract>().Key(x => x.Id)
                .Table($"{nameof(StageAdverseEventExtract)}");
            DapperPlusManager.Entity<StageAllergiesChronicIllnessExtract>().Key(x => x.Id)
                .Table($"{nameof(StageAllergiesChronicIllnessExtract)}");
            DapperPlusManager.Entity<StageArtExtract>().Key(x => x.Id).Table($"{nameof(StageArtExtract)}");
            DapperPlusManager.Entity<StageBaselineExtract>().Key(x => x.Id).Table($"{nameof(StageBaselineExtract)}");
            DapperPlusManager.Entity<StageContactListingExtract>().Key(x => x.Id)
                .Table($"{nameof(StageContactListingExtract)}");
            DapperPlusManager.Entity<StageCovidExtract>().Key(x => x.Id).Table($"{nameof(StageCovidExtract)}");
            DapperPlusManager.Entity<StageDefaulterTracingExtract>().Key(x => x.Id)
                .Table($"{nameof(StageDefaulterTracingExtract)}");
            DapperPlusManager.Entity<StageDepressionScreeningExtract>().Key(x => x.Id)
                .Table($"{nameof(StageDepressionScreeningExtract)}");
            DapperPlusManager.Entity<StageDrugAlcoholScreeningExtract>().Key(x => x.Id)
                .Table($"{nameof(StageDrugAlcoholScreeningExtract)}");
            DapperPlusManager.Entity<StageEnhancedAdherenceCounsellingExtract>().Key(x => x.Id)
                .Table($"{nameof(StageEnhancedAdherenceCounsellingExtract)}");
            DapperPlusManager.Entity<StageIptExtract>().Key(x => x.Id).Table($"{nameof(StageIptExtract)}");
            DapperPlusManager.Entity<StageLaboratoryExtract>().Key(x => x.Id)
                .Table($"{nameof(StageLaboratoryExtract)}");
            DapperPlusManager.Entity<StageOtzExtract>().Key(x => x.Id).Table($"{nameof(StageOtzExtract)}");
            DapperPlusManager.Entity<StageOvcExtract>().Key(x => x.Id).Table($"{nameof(StageOvcExtract)}");
            DapperPlusManager.Entity<StagePharmacyExtract>().Key(x => x.Id).Table($"{nameof(StagePharmacyExtract)}");
            DapperPlusManager.Entity<StageStatusExtract>().Key(x => x.Id).Table($"{nameof(StageStatusExtract)}");
            DapperPlusManager.Entity<StageGbvScreeningExtract>().Key(x => x.Id)
                .Table($"{nameof(StageGbvScreeningExtract)}");
            DapperPlusManager.Entity<StageCervicalCancerScreeningExtract>().Key(x => x.Id)
                .Table($"{nameof(StageCervicalCancerScreeningExtract)}");
            DapperPlusManager.Entity<StageIITRiskScoresExtract>().Key(x => x.Id)
                .Table($"{nameof(StageIITRiskScoresExtract)}");
            
            DapperPlusManager.Entity<SmartActionRegister>().Key(x => x.Id).Table($"{nameof(SmartActionRegister)}");


            DapperPlusManager.Entity<PatientExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<PatientArtExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<PatientBaselinesExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<PatientLaboratoryExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<PatientPharmacyExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<PatientStatusExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<PatientVisitExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<PatientAdverseEventExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<ActionRegister>().BatchTimeout(900);
            DapperPlusManager.Entity<AllergiesChronicIllnessExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<ContactListingExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<DepressionScreeningExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<EnhancedAdherenceCounsellingExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<DrugAlcoholScreeningExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<GbvScreeningExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<IptExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<OtzExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<OvcExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<CovidExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<DefaulterTracingExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<CervicalCancerScreeningExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<IITRiskScoresExtract>().BatchTimeout(900);

            DapperPlusManager.Entity<StagePatientExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageVisitExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageAdverseEventExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageAllergiesChronicIllnessExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageArtExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageBaselineExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageContactListingExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageCovidExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageDefaulterTracingExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageDepressionScreeningExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageDrugAlcoholScreeningExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageEnhancedAdherenceCounsellingExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageIptExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageLaboratoryExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageOtzExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageOvcExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StagePharmacyExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageStatusExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageGbvScreeningExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageCervicalCancerScreeningExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<StageIITRiskScoresExtract>().BatchTimeout(900);
            DapperPlusManager.Entity<SmartActionRegister>().BatchTimeout(900);
            DapperPlusManager.MapperFactory = mapper => mapper.BatchTimeout(900);
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

        public void UpgradeDb()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DwapiCentralContext, Configuration>());
            MasterFacilities.AddOrUpdate(MasterFacility.GenFacility());
            SaveChanges();
        }
    }
}
