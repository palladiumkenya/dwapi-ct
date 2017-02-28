using PalladiumDwh.ClientApp.Presenters;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IFeedBackView:IView<IFeedBackPresenter>
    {
        int Phone { get; set; }
        string Email { get; set; }
        string Comment { get; set; }
        bool SendLogs { get; set; }
    }
}