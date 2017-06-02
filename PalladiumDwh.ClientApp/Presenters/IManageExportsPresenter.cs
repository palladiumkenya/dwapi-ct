using System.Threading.Tasks;
using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IManageExportsPresenter : IPresenter<IManageExportsView>
    {
        Task LoadExisitingExportsAsync();

        Task<bool> ExtractExportsAsync();
        Task LoadExportsAsync();
        Task SendExportsAsync();
        Task DeleteAllExportsAsync();
    }
}