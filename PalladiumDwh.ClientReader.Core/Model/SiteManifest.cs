using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class SiteManifest
    {
        public List<Manifest> Manifests { get; set; }=new List<Manifest>();
        public List<ClientPatientExtract> PatientExtracts { get; set; }=new List<ClientPatientExtract>();
        public bool ReadComplete { get; set; }
        public string ReadStatus { get; set; }

        public static SiteManifest Create(string manifestFile)
        {
            var siteManifest=new SiteManifest();
            siteManifest.ReadManifest(manifestFile);
            return siteManifest;
        }
        private void ReadManifest(string manifestFile)
        {
            ReadComplete = true;
            try
            {
                var siteManifests = JsonConvert.DeserializeObject<List<Manifest>>(manifestFile);
                Manifests = siteManifests;
            }
            catch (Exception e)
            {
                ReadComplete = false;
                ReadStatus = $"Error reading Facility information:{e.Message}";
            }
        }

        public void AddProfie(string profileFile)
        {
            try
            {
                var patientExtract = JsonConvert.DeserializeObject<ClientPatientExtract>(profileFile);
                PatientExtracts.Add(patientExtract);
            }
            catch (Exception e)
            {
                ReadComplete = false;
                ReadStatus = $"Error reading Patient data:{e.Message}";
            }
        }

        public override string ToString()
        {
            return $"Manifests:{Manifests.Count}:Patient:{PatientExtracts.Count}:Complete:{ReadComplete}|{ReadStatus}";
        }
    }
}