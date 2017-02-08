using System;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ProjectRepository: ClientRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DwapiRemoteContext context) : base(context)
        {
        }

        public Project GetActiveProject()
        {
            Guid id=Guid.Empty;

            var defaultEmr = Context
                .Emrs
                .FirstOrDefault(x => x.IsDefault);

            if (null != defaultEmr)
                id = defaultEmr.ProjectId;
            
            return Context
                .Projects
                .FirstOrDefault(x=>x.Id==id);
        }
    }
}