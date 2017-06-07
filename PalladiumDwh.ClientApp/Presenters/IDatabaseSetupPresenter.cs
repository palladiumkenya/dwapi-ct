using System.Threading.Tasks;
using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IDatabaseSetupPresenter: IPresenter<IDatabaseSetupView>
    {
        Task Load();
        Task Save();
        Task Test();
        Task SaveEmr();
        Task TestEmr();
    }
}