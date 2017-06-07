
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IProjectRepository: IClientRepository<Project>
    {
        Project GetActiveProject();
        Task<Project> GetActiveProjectAsync();
    }
}