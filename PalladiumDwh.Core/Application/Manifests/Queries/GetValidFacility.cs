using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using MediatR;
using Newtonsoft.Json.Linq;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Application.Manifests.Queries
{
    public class GetValidFacility:IRequest<MasterFacility>
    {
        public Manifest Manifest { get; }
        public bool AllowSnapshot { get; }

        public GetValidFacility(Manifest manifest)
        {
            Manifest = manifest;
        }
    }

    public class GetValidFacilityHandler:IRequestHandler<GetValidFacility,MasterFacility>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IFacilityRepository _facilityRepository;

        public GetValidFacilityHandler(IPatientExtractRepository patientExtractRepository, IFacilityRepository facilityRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<MasterFacility> Handle(GetValidFacility request, CancellationToken cancellationToken)
        {
            var manifest = request.Manifest;

            try
            {
                manifest.Validate();
                
                JObject DwapiVersionSending = JObject.Parse(manifest.FacMetrics.Select(o => o.Metric).Where(x => x.Contains("CareTreatment")).ToList()[0]);
                
                // if (DwapiVersionSending["Version"].ToString() != "3.1.1.2")
                // {
                //     throw new Exception($" ====> You're using DWAPI Version [{DwapiVersionSending["Version"]}]. Older Versions of DWAPI are not allowed to send to NDWH. Upgrade to the latest version");
                //
                // }

               var masterFacility = await _patientExtractRepository.VerifyFacility(manifest.SiteCode);
                if (null == masterFacility)
                    throw new Exception($"SiteCode [{manifest.SiteCode}] NOT FOUND in Master Facility List");

                _facilityRepository.Enroll(masterFacility, manifest.EmrName,request.AllowSnapshot);

                return masterFacility;
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }
    }
}
