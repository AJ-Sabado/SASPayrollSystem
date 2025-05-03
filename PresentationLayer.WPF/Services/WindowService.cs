using System.Windows;

namespace PresentationLayer.WPF.Services
{
    public class WindowService : IWindowService
    {
        private IServiceProvider _serviceProvider;

        private Window? _currentWindow = null;

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowWindow<T>() where T : Window
        {
            if (_currentWindow == null)
            {
                _currentWindow = DIGetRequiredService<T>(_serviceProvider);
                _currentWindow.Show();
            }
            else
            {
                var window = DIGetRequiredService<T>(_serviceProvider);
                window.Show();
                _currentWindow.Close();
                _currentWindow = window;
            }
        }

        //Resolves GetRequiredService for DI conflict
        private T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }
}
