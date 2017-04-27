

using System.Threading.Tasks;
using PalladiumDwh.ClientApp.Presenters;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IControlView<T>
    {
        T Presenter { get; set; }
    }
}