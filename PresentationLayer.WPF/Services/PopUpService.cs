using System.Windows;

namespace PresentationLayer.WPF.Services
{
    public class PopUpService : IPopUpService
    {
        private IServiceProvider _serviceProvider;
        private Window? _currentPopup;

        public PopUpService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ClosePopup()
        {
            if (_currentPopup != null)
            {
                _currentPopup.Close();
                _currentPopup = null;
            }
        }

        public void ShowPopUp<T>() where T : Window
        {
            ClosePopup();
            _currentPopup = DIGetRequiredService<T>(_serviceProvider);
            _currentPopup.Show();
        }

        //Resolves GetRequiredService for DI conflict
        private T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }
}
