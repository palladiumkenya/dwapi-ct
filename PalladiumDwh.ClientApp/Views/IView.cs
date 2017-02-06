

using PalladiumDwh.ClientApp.Presenters;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IView<T>
    {
        string Title { get; set; }
        string Header { get; set; }
        string HeaderDescription { get; set; }
        T Presenter { get; set; }
    }
}