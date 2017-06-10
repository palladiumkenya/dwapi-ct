using System.Threading.Tasks;
using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IEmrDatabaseSetupPresenter: IPresenter<IEmrDatabaseSetupView>
    {
        Task Load();
        Task Refresh();
        Task RefreshDatabase();
        Task Save();
        Task Test();
        void Change();
        void ChangeDatabaseType();
    }
}