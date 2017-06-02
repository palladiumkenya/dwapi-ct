using System.Threading.Tasks;
using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IManageExportsPresenter : IPresenter<IManageExportsView>
    {
        Task<bool> ExtractExportsAsync();
        Task LoadExportsAsync(bool startup);
        Task SendExportsAsync();
        Task DeleteAllExportsAsync();
    }
}