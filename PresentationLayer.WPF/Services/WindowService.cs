using System.Windows;

namespace PresentationLayer.WPF.Services
{
    public class WindowService : IWindowService
    {
        private IServiceProvider _serviceProvider;

        private Window? _currentWindow = null;
        private Window? _nextWindow = null;

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowWindow<T>() where T : Window
        {
            if (_currentWindow == null)
            {
                _currentWindow = DIGetRequiredService<T>(_serviceProvider);
                _currentWindow.Closed += OnWindowClosed;
                _currentWindow.Show();
            }
            else
            {
                _nextWindow = DIGetRequiredService<T>(_serviceProvider);
                _currentWindow.Close();
            }
        }

        private void OnWindowClosed(object? sender, EventArgs e)
        {
            if (_currentWindow != null)
                _currentWindow.Closed -= OnWindowClosed;

            if (_nextWindow != null)
            {
                _currentWindow = _nextWindow;
                _nextWindow = null;
                _currentWindow.Closed += OnWindowClosed;
                _currentWindow.Show();
            }
            else
            {
                _currentWindow = null;
            }
        }

        //Resolves GetRequiredService for DI conflict
        private T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }
}
