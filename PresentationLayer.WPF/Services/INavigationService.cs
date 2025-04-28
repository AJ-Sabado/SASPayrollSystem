using PresentationLayer.WPF.ViewModel;

namespace PresentationLayer.WPF.Services
{
    public interface INavigationService
    {
        Base_ViewModel CurrentView { get; }
        void NavigateTo<T>() where T : Base_ViewModel;
    }
}