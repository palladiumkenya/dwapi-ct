using System;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
   public class PatientExtractId
   {
      public Guid Id { get; set; }
      public int PatientPID { get; set; }
      public Guid FacilityId { get; set; }
   }
}
