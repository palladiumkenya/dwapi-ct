using System;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class PatientFacilityProfile
    {
        public Guid Id { get; set; }
        public Guid FacilityId { get; set; }

        public PatientFacilityProfile(Guid id, Guid facilityId)
        {
            Id = id;
            FacilityId = facilityId;
        }

        protected bool Equals(PatientFacilityProfile other)
        {
            return Id.Equals(other.Id) && FacilityId.Equals(other.FacilityId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PatientFacilityProfile) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ FacilityId.GetHashCode();
            }
        }
    }
}