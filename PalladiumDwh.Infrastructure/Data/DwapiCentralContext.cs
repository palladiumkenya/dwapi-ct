﻿using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using PalladiumDwh.Shared.Data;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data
{
    public class DwapiCentralContext : DwapiBaseContext
    {

        public DwapiCentralContext() : base("name=DWAPICentral")
        {
        }

        public DwapiCentralContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
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

        public virtual DbSet<MasterFacility> MasterFacilities { get; set; }

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

      DapperPlusManager.Entity<Facility>().Table("Facility").Identity(x => x.Id);
      DapperPlusManager.Entity<PatientExtract>().Table("PatientExtract").Identity(x => x.Id);
      DapperPlusManager.Entity<PatientArtExtract>().Table("PatientArtExtract").Identity(x => x.Id);
      DapperPlusManager.Entity<PatientBaselinesExtract>().Table("PatientBaselinesExtract").Identity(x => x.Id);
      DapperPlusManager.Entity<PatientLaboratoryExtract>().Table("PatientLaboratoryExtract").Identity(x => x.Id);
      DapperPlusManager.Entity<PatientPharmacyExtract>().Table("PatientPharmacyExtract").Identity(x => x.Id);
      DapperPlusManager.Entity<PatientStatusExtract>().Table("PatientStatusExtract").Identity(x => x.Id);
      DapperPlusManager.Entity<PatientVisitExtract>().Table("PatientVisitExtract").Identity(x => x.Id);

    }
    public SqlConnection GetConnection()
        {
          var cn = this.Database.Connection as SqlConnection;

          if (null == cn)
          {
            cn=new SqlConnection(this.Database.Connection.ConnectionString);
          }

          if (cn.State == ConnectionState.Open)
          {
            return cn;
          }
          else
          {
            cn.Open();
            return cn;
          }
        }
  }
}
