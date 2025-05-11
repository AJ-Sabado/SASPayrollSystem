using System.Windows;

namespace PresentationLayer.WPF.Services
{
    public class PopUpService : IPopUpService
    {
        private IServiceProvider _serviceProvider;

        public PopUpService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowPopUp<T>() where T : Window
        {
            var window = DIGetRequiredService<T>(_serviceProvider);
            window.Show();
        }

        //Resolves GetRequiredService for DI conflict
        private T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }
}
