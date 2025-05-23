using System.Windows;

namespace PresentationLayer.WPF.Services
{
    public class PopUpService : IPopUpService
    {
        private IServiceProvider _serviceProvider;
        private Window? _currentPopup;

        public Guid? IdSource { get; private set; }

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
                IdSource = Guid.Empty;
                IdSource = null;
            }
        }

        public void ShowPopUp<T>(Guid? idSource = null) where T : Window
        {
            ClosePopup();
            if (idSource != null)
            {
                IdSource = idSource;
            }
            _currentPopup = DIGetRequiredService<T>(_serviceProvider);
            bool? dialog = _currentPopup.ShowDialog();
        }

        //Resolves GetRequiredService for DI conflict
        private T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }
}
