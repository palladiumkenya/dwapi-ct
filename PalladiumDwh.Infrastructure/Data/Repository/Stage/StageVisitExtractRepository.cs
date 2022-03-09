using System.Reflection;
using AutoMapper;
using log4net;
using PalladiumDwh.Core.Application.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageVisitExtractRepository :
        StageExtractRepository<StageVisitExtract, PatientVisitExtract>, IStageVisitExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;
        private readonly IMapper _mapper;

        public StageVisitExtractRepository(DwapiCentralContext context, IMapper mapper,
            string stageName = nameof(StageVisitExtract), string extractName = nameof(PatientVisitExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}



