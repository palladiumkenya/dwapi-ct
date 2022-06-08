using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Model.Dto
{
    public class PatientExtractHolderDto
    {
        public int PatientPID { get; set; }
        public Guid FacilityId { get; set; }
        public DateTime? Created { get; set; }

        public PatientExtract Generate()
        {
            return new PatientExtract
            {
                PatientPID = PatientPID,
                FacilityId = FacilityId,
                Created = Created
            };
        }

        public static List<PatientExtract> GeneratePatient(IEnumerable<PatientExtractHolderDto> holders)
        {
            var list = new List<PatientExtract>();

            foreach (var h in holders)
            {
                list.Add(h.Generate());
            }

            return list;
        }
    }
}
