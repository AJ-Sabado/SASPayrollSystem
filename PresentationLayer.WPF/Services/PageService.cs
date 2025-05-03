using System.Windows.Controls;

namespace PresentationLayer.WPF.Services
{
    public class PageService : IPageService
    {
        private readonly IServiceProvider _serviceProvider;
        public PageService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TPage GetPage<TPage>() where TPage : UserControl
        {
            return DIGetRequiredService<TPage>(_serviceProvider);
        }

        //Resolves GetRequiredService for DI conflict
        private T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }
}
