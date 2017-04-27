using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class ExtractListPresenter:IExtractListPresenter
    {
        public IExtractListView View { get; }

        public ExtractListPresenter(IExtractListView view)
        {
            view.Presenter = this;
            View = view;
        }

        public void Initialize()
        {
            
        }

        public void Load()
        {
           
        }
    }
}