namespace PalladiumDwh.ClientApp.Views
{
    public interface IControlView<T>
    {
        T Presenter { get; set; }
    }
}