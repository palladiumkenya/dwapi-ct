using System.Reflection;
using AutoMapper;
using log4net;
using PalladiumDwh.Core.Application.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageOvcExtractRepository :
        StageExtractRepository<StageOvcExtract, OvcExtract>, IStageOvcExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;
        private readonly IMapper _mapper;

        public StageOvcExtractRepository(DwapiCentralContext context, IMapper mapper,
            string stageName = nameof(StageOvcExtract), string extractName = nameof(OvcExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}