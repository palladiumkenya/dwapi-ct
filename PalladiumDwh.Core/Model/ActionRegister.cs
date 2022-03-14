using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;

namespace PalladiumDwh.Core.Model
{
    public class ActionRegister : Entity
    {
        public string Action { get; set; }
        public string Area { get; set; }
        public Guid FacilityId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime Created { get; set; }=DateTime.Now;

        public ActionRegister()
        {
        }

        public ActionRegister(string action, string area, Guid facilityId,Guid patientId)
        {
            Action = action;
            Area = area;
            FacilityId = facilityId;
            PatientId = patientId;
            Created=DateTime.Now;
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
