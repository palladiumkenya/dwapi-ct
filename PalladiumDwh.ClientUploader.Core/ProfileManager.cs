using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Profile;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.ClientUploader.Core.Model;

namespace PalladiumDwh.ClientUploader.Core
{
    public class ProfileManager:IProfileManager
    {
        public ProfileManager()
        {
        }

        public ClientPatientExtract Decode(string encodedPatient)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodedPatient);
            var decodedPatient = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return JsonConvert.DeserializeObject<ClientPatientExtract>(decodedPatient);
        }

        public IEnumerable<IClientExtractProfile> Generate(ClientPatientExtract patient)
        {
            var list = new List<IClientExtractProfile>();

            if (null == patient)
                throw new Exception("Patient not Initialized");

            var facility = new ClientFacility(patient.SiteCode, patient.FacilityName, patient.Emr, patient.Project);

            if (patient.HasArt())
            {
                list.Add(ClientPatientARTProfile.Create(facility, patient));
            }

            if (patient.HasBaselines())
            {
                list.Add(ClientPatientBaselineProfile.Create(facility, patient));
            }

            if (patient.HasLabs())
            {
                list.Add(ClientPatientLabProfile.Create(facility, patient));
            }

            if (patient.HasPharmacy())
            {
                list.Add(ClientPatientPharmacyProfile.Create(facility, patient));
            }

            if (patient.HasStatus())
            {
                list.Add(ClientPatientStatusProfile.Create(facility, patient));
            }

            if (patient.HasVisits())
            {
                list.Add(ClientPatientVisitProfile.Create(facility, patient));
            }
          
            return list;
        }

        public IEnumerable<SiteProfile> GenerateSiteProfiles(SiteManifest siteManifest)
        {
            return SiteProfile.Create(siteManifest);
        }
    }
}