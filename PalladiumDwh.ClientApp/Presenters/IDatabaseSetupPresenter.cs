using System.Threading.Tasks;
using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IDatabaseSetupPresenter: IPresenter<IDatabaseSetupView>
    {
        Task Load();
        Task Refresh();
        Task Save();
        Task Test();       
    }
}