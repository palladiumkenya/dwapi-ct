using PalladiumDwh.ClientApp.Presenters;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IStartupView: IView<IStartupPresenter>
    {
        string Status { get; set; }
        void ShowDash();
    }
}