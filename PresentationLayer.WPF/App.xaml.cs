using Microsoft.Extensions.DependencyInjection;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Pages;
using PresentationLayer.WPF.ViewModel;
using PresentationLayer.WPF.ViewModel.RegularViewModel;
using ServicesLayer;
using System.Windows;

//READ ME FIRST HAHAHAHAHA
//HERE IS WHERE VIEW IS DISPLAYED NOT THROUGH STARTUP URI IN APP.XML 

namespace SASPayrolSystemProject
{
    public partial class App : Application
    {
        //This stores all application objects through Dependency Injection
        private IServiceProvider _serviceProvider;

        public App()
        {
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();

            var windowService = DIGetRequiredService<IWindowService>(_serviceProvider);
            windowService.ShowWindow<MainWindow>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<LoginPage_ViewModel>();

            services.AddTransient<EmployeeDahboard_View>();
            services.AddTransient<EmployeeDashboardReg_ViewModel>();

            //services.AddSingleton<Func<Type, Base_ViewModel>>(serviceProvider => viewModelType => (Base_ViewModel)serviceProvider.GetRequiredService(viewModelType));

            //Navigation service func is mapped
            //services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IWindowService, WindowService>();
        }

        //Resolves GetRequiredService for DI conflict
        private T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }
}
