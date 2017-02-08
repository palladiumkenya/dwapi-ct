using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IOptionPresenter:IPresenter<IOptionView>
    {
        void Load();
        void SetAsDefault();
        void ShowSelected();
    }
}