using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model
{
    public class Facility : Entity, IEquatable<Facility>
   {
        public int Code { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }

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

      public string GetStatus()
        {
            return $"{Name} ({Code}) | Patients:{PatientExtracts.Count}";
        }
        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
