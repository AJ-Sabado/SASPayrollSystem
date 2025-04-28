using Microsoft.Extensions.DependencyInjection;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.ViewModel;
using PresentationLayer.WPF.ViewModel.RegularViewModel;
using ServicesLayer;
using System.Windows;


namespace SASPayrolSystemProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public App()
        {
            //Contains singletons of all application objects
            var services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<LoginPage_ViewModel>(provider)
            });
            services.AddSingleton<LoginPage_ViewModel>();

            services.AddSingleton<Func<Type, Base_ViewModel>>(serviceProvider => viewModelType => (Base_ViewModel)serviceProvider.GetRequiredService(viewModelType));

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<MainWindow>(_serviceProvider);
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
