using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using log4net;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Application.Commands
{
    public class SendToSpot:IRequest<Result>
    {
        public Manifest Manifest { get; }
        public MasterFacility MasterFacility { get; }

        public SendToSpot(Manifest manifest, MasterFacility masterFacility)
        {
            Manifest = manifest;
            MasterFacility = masterFacility;
        }
    }
    public class SendToSpotHandler:IRequestHandler<SendToSpot, Result>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ILiveSyncService _liveSyncService;
        private readonly JsonSerializerSettings _serializerSettings;

        public SendToSpotHandler(ILiveSyncService liveSyncService)
        {
            _liveSyncService = liveSyncService;
            _serializerSettings = new JsonSerializerSettings()
                {ContractResolver = new CamelCasePropertyNamesContractResolver()};
        }

        public async Task<Result> Handle(SendToSpot request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Debug("posting to SPOT...");
                var facManifest = FacilityManifest.Create(request.Manifest);
                var manifestDto = new ManifestDto(request.MasterFacility, facManifest);
                var metrics = MetricDto.Generate(request.MasterFacility, facManifest);
                var metricDtos = metrics.Where(x => x.CargoType != CargoType.Indicator).ToList();
                var indicatorDtos = metrics.Where(x => x.CargoType == CargoType.Indicator).ToList();
                manifestDto.Cargo =
                    JsonConvert.SerializeObject(ExtractDto.GenerateCargo(metricDtos), _serializerSettings);

                var result = await _liveSyncService.SyncManifest(manifestDto);

                if (metricDtos.Any())
                    await _liveSyncService.SyncMetrics(metricDtos);
                if (indicatorDtos.Any())
                {
                    var indstats = IndicatorDto.Generate(indicatorDtos);
                    await _liveSyncService.SyncIndicators(indstats);
                }
                return Result.Ok();
            }
            catch (Exception e)
            {
                Log.Error("Send to SPOT Error", e);
                return Result.Fail(e.Message);
            }
        }
    }
}
