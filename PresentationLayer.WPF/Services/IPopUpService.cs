using System.Windows;

namespace PresentationLayer.WPF.Services
{
    public interface IPopUpService
    {
        void ShowPopUp<T>() where T : Window;

        void ClosePopup();
    }
}