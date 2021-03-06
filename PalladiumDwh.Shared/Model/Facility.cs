using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model
{
    public class Facility : Entity, IEquatable<Facility>
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }

        public DateTime? SnapshotDate { get; set; }
        public int? SnapshotSiteCode { get; set; }
        public int? SnapshotVersion { get; set; }

        public virtual ICollection<PatientExtract> PatientExtracts { get; set; } = new List<PatientExtract>();

        public Facility()
        {
            Created = DateTime.Now;
        }

        public Facility(int code, string name, string emr, string project)
            : this()
        {
            Code = code;
            Name = name;
            Emr = emr;
            Project = project;
        }

        public void UpdateProfile(string emr, string project)
        {
            Emr = emr;
            Project = project;
        }
        public bool Equals(Facility other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return Code.Equals(other.Code);
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {

            //Get hash code for the Code field.
            int hashProductCode = Code.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductCode;
        }

        public string SqlInsert()
        {
            var sql = $@"
BEGIN
   IF NOT EXISTS (SELECT id FROM Facility 
                   WHERE Code = {Code})
   BEGIN
    Insert into Facility(
        {nameof(Id)},
        {nameof(Code)},
        {nameof(Name)},
        {nameof(Emr)},
        {nameof(Project)},
        {nameof(Voided)},
        {nameof(Processed)},
        {nameof(Created)}
        ) 
    values(
        '{Id}',
         {Code},
        '{Name}',
        '{Emr}',
        '{Project}',
        0,
        0,
        GetDate()
    )
  END
END
";
            return sql;
        }

        public bool ProfileMissing()
        {
            return string.IsNullOrWhiteSpace(Emr) || string.IsNullOrWhiteSpace(Project);
        }

        public string SqlUpdateProfile()
        {
            var sql = $@"
    Update Facility
    set {nameof(Emr)}='{Emr}',
        {nameof(Project)}='{Project}'
    where {nameof(Id)}='{Id}'
";
            return sql;
        }

        public string GetStatus()
        {
            return $"{Name} ({Code}) | Patients:{PatientExtracts.Count}";
        }

        public bool EmrChanged(string requestEmr)
        {
            if (string.IsNullOrWhiteSpace(requestEmr))
                return false;

            if (string.IsNullOrWhiteSpace(Emr))
                return false;

            if (requestEmr.IsSameAs("CHAK"))
                requestEmr = "IQCare";

            if (requestEmr.IsSameAs("IQCare") || requestEmr.IsSameAs("KenyaEMR"))
                return !Emr.IsSameAs(requestEmr);

            return false;
        }
        public Facility TakeSnapFrom(MasterFacility snapMfl)
        {
            var fac = this;

            fac.SnapshotDate = DateTime.Now;
            fac.Code = snapMfl.Code;
            fac.SnapshotSiteCode = snapMfl.SnapshotSiteCode;
            fac.SnapshotVersion = snapMfl.SnapshotVersion;

            return fac;
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }

        public static Facility create(MasterFacility masterFacility)
        {
            return new Facility(masterFacility.Code, masterFacility.Name, "", "");
        }
    }
}
