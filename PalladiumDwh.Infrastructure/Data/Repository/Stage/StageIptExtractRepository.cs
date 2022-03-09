using System.Reflection;
using AutoMapper;
using log4net;
using PalladiumDwh.Core.Application.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageIptExtractRepository :
        StageExtractRepository<StageIptExtract, IptExtract>, IStageIptExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;
        private readonly IMapper _mapper;

        public StageIptExtractRepository(DwapiCentralContext context, IMapper mapper,
            string stageName = nameof(StageIptExtract), string extractName = nameof(IptExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}