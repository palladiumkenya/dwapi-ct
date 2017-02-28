using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class FeedBackPresenter: IFeedBackPresenter
    {
        private IFeedBackView _view;

        public FeedBackPresenter(IFeedBackView view)
        {
            _view = view;
            _view.Presenter = this;
        }

        public IFeedBackView View => _view;

        public void Initialize()
        {
            _view.Title = "FeedBack";
            _view.Header = "FeedBack";
            _view.HeaderDescription = "FeedBack";
        }

        public void Send()
        {
            _view.Header = "Thank you for tour feedback";
        }
    }
}