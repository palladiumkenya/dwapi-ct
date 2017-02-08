using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ProjectRepository: ClientRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}