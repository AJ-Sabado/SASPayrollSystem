using Microsoft.Extensions.DependencyInjection;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Pages;
using PresentationLayer.WPF.View.Pages.Dashboard;
using PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardReg;
using PresentationLayer.WPF.ViewModel;
using PresentationLayer.WPF.ViewModel.RegularViewModel;
using ServicesLayer;
using System.Windows;

namespace SASPayrolSystemProject
{
    public partial class App : Application
    {
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

            //Pages
            services.AddSingleton<RegDashboard>();
            services.AddSingleton<RegJobDesk>();
            services.AddSingleton<AccountsPage>();

            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IPageService, PageService>();
        }

        //Resolves GetRequiredService for DI conflict
        private T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }
}
