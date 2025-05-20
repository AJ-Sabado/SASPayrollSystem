using System.Windows;

namespace PresentationLayer.WPF.Services
{
    public interface IPopUpService
    {
        void ShowPopUp<T>(Guid? idSource = null) where T : Window;

        void ClosePopup();

        Guid? IdSource { get; }
    }
}