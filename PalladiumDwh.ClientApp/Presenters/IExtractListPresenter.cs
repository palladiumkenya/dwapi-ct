using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IExtractListPresenter:IPresenter<IExtractListView>
    {
        void Load();
    }
}