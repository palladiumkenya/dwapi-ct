using System.Reflection;
using AutoMapper;
using log4net;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageIITRiskScoresExtractRepository :
        StageExtractRepository<StageIITRiskScoresExtract, IITRiskScoresExtract>, IStageIITRiskScoresExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;
        private readonly IMapper _mapper;

        public StageIITRiskScoresExtractRepository(DwapiCentralContext context, IMapper mapper,
            string stageName = nameof(StageIITRiskScoresExtract), string extractName = nameof(IITRiskScoresExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}
