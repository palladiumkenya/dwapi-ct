using System.Reflection;
using AutoMapper;
using log4net;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageEnhancedAdherenceCounsellingExtractRepository :
        StageExtractRepository<StageEnhancedAdherenceCounsellingExtract,EnhancedAdherenceCounsellingExtract>, IStageEnhancedAdherenceCounsellingExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;
        private readonly IMapper _mapper;

        public StageEnhancedAdherenceCounsellingExtractRepository(DwapiCentralContext context, IMapper mapper,
            string stageName = nameof(StageEnhancedAdherenceCounsellingExtract), string extractName = nameof(EnhancedAdherenceCounsellingExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}
