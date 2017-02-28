using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IFeedBackPresenter:IPresenter<IFeedBackView>
    {
        void Send();
    }
}