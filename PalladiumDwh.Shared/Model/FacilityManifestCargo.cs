using System;

namespace PalladiumDwh.Shared.Model
{
    public class FacilityManifestCargo : Entity
    {
        public string Items { get; set; }
        public Guid FacilityManifestId { get; set; }

        public FacilityManifestCargo()
        {
        }

        public FacilityManifestCargo(string items, Guid facilityManifestId):this()
        {
            Items = items;
            FacilityManifestId = facilityManifestId;
        }

        public override string ToString()
        {
            return Items;
        }
    }
}