using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using MediatR;
using PalladiumDwh.Core.Application.Stage.Repositories;

namespace PalladiumDwh.Core.Application.Commands
{
    public class ClearFacility : IRequest
    {
        public Guid FacilityId { get; }
        public Guid ManifestId { get; }

        public ClearFacility(Guid facilityId, Guid manifestId)
        {
            FacilityId = facilityId;
            ManifestId = manifestId;
        }
    }

    public class ClearFacilityHandler:IRequestHandler<ClearFacility>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IStagePatientExtractRepository _repository;

        public ClearFacilityHandler(IStagePatientExtractRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ClearFacility request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.ClearSite(request.FacilityId, request.ManifestId);
                return Unit.Value;
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }
    }
}
