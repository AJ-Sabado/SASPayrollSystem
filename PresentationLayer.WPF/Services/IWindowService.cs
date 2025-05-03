using System.Windows;

namespace PresentationLayer.WPF.Services
{
    public interface IWindowService
    {
        void ShowWindow<T>() where T : Window;
    }
}