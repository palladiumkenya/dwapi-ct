

using System.Threading.Tasks;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IView<T>
    {
        Task StartUp();
        string Title { get; set; }
        string Header { get; set; }
        string HeaderDescription { get; set; }
        T Presenter { get; set; }
        void ShowErrorMessage(string message);

    }
}