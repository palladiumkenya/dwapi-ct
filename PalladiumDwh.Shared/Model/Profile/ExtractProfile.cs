using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public abstract class ExtractProfile<T> : IExtractProfile<T>
    {
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public Facility FacilityInfo { get;  set; }
        public PatientExtract PatientInfo { get;  set; }
        public List<T> Extracts { get;  set; }

        public virtual bool IsValid()
        {
            if (HasData())
                return
                    Facility.IsValid() &&
                    Demographic.IsValid();
                    
            return false;
        }

        public virtual bool HasData()
        {
            return null != Facility && null != Demographic;
        }
      
        public virtual void GeneratePatientRecord()
        {
            FacilityInfo = Facility.GenerateFacility();
            PatientInfo = Demographic.GeneratePatient(FacilityInfo.Id);
        }

        public virtual void GenerateRecords(Guid patientId)
        {
            PatientInfo.Id = patientId;
            Extracts = new List<T>();
        }
        
        public override string ToString()
        {
            return $"{FacilityInfo.Name} | {PatientInfo.PatientCccNumber}";
        }
    }
}