using System.Threading.Tasks;
using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IStartupPresenter:IPresenter<IStartupView>
    {
       Task<bool> UpdateDatabase();
        void LoadDashboard();
    }
}