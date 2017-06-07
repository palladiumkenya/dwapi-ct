using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ProjectRepository : ClientRepository<Project>, IProjectRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ProjectRepository(DwapiRemoteContext context) : base(context)
        {
        }

        public Project GetActiveProject()
        {
            Guid id = Guid.Empty;

            var defaultEmr = Context
                .Emrs
                .FirstOrDefault(x => x.IsDefault);

            if (null != defaultEmr)
                id = defaultEmr.ProjectId;

            return Context
                .Projects
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<Project> GetActiveProjectAsync()
        {
            Guid id = Guid.Empty;

            var defaultEmr = await Context.Emrs.FirstOrDefaultAsync(x => x.IsDefault);

            if (null == defaultEmr)
            {
                var message = "The Default EMR has not been Setup";
                Log.Debug(message);
                throw new ArgumentException(message);
            }

            id = defaultEmr.ProjectId;
            Log.Debug($"Default EMR:{defaultEmr}");
            return await Context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}