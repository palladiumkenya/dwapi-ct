using PalladiumDwh.Shared.Enum;
using System;

namespace PalladiumDwh.Shared.Model
{
    public class FacilityManifestCargo : Entity
    {
        public string Items { get; set; }
        public Guid FacilityManifestId { get; set; }
        public CargoType CargoType { get; set; } = CargoType.Patient;

        public FacilityManifestCargo()
        {
        }

        public FacilityManifestCargo(string items, Guid facilityManifestId):this()
        {
            Items = items;
            FacilityManifestId = facilityManifestId;
        }

        public FacilityManifestCargo(string items, Guid facilityManifestId,CargoType cargoType) : this(items,facilityManifestId)
        {
            CargoType = cargoType;
        }

        public override string ToString()
        {
            return Items;
        }
    }
}