using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;

namespace PalladiumDwh.Core.Model
{
    public class SmartActionRegister : Entity
    {
        public string Action { get; set; }
        public string Area { get; set; }
        public Guid FacilityId { get; set; }
        public Guid ManifestId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public SmartActionRegister()
        {
        }

        public SmartActionRegister(string action, string area, Guid facilityId, Guid manifestId)
        {
            Action = action;
            Area = area;
            FacilityId = facilityId;
            ManifestId = manifestId;
            Created = DateTime.Now;
        }

        public static List<ActionRegister> Generate(List<PatientFacilityProfile> profiles, string action, string area)
        {
            var list = new List<ActionRegister>();

            foreach (var profile in profiles)
            {
                list.Add(new ActionRegister(action, area, profile.FacilityId, profile.Id));
            }

            return list;
        }
    }
}