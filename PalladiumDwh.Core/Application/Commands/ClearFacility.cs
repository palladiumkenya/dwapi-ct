using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using MediatR;
using PalladiumDwh.Core.Interfaces.Sync;

namespace PalladiumDwh.Core.Application.Commands
{
    public class ClearFacility : IRequest
    {
        public Guid FacilityId { get; }
        public Guid SessionId { get; }

        public ClearFacility(Guid facilityId, Guid sessionId)
        {
            FacilityId = facilityId;
            SessionId = sessionId;
        }
    }

    public class ClearFacilitySessionHandler:IRequestHandler<ClearFacility>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IStagePatientExtractRepository _patientExtractRepository;

        public ClearFacilitySessionHandler(IStagePatientExtractRepository patientExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
        }
        
        public async Task<Unit> Handle(ClearFacility request, CancellationToken cancellationToken)
        {
            try
            {
                await _patientExtractRepository.ClearSite(request.FacilityId, request.SessionId);
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
