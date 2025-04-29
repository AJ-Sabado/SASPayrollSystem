using Microsoft.Extensions.DependencyInjection;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Pages;
using PresentationLayer.WPF.ViewModel;
using PresentationLayer.WPF.ViewModel.RegularViewModel;
using ServicesLayer;
using System.Windows;

//HERE IS WHERE WHAT VIEW IS SHOWED NOT THROUGH STARTUP URI IN APP.XML 

namespace SASPayrolSystemProject
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public App()
        {
            //TO DO - Add all Views, User controls, and View Models to Service Collection as Singletons

            //Contains singletons of all application objects
            var services = new ServiceCollection();

            //Login Page mapping
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = DIGetRequiredService<LoginPage_ViewModel>(provider)
            });
            services.AddSingleton<LoginPage_ViewModel>();

            //Employe Dashboard mapping
            services.AddSingleton<EmployeeDahboard_View>(provider => new EmployeeDahboard_View
            {
                DataContext = DIGetRequiredService<EmployeeDashboardReg_ViewModel>(provider)
            });
            services.AddSingleton<EmployeeDashboardReg_ViewModel>();

            services.AddSingleton<Func<Type, Base_ViewModel>>(serviceProvider => viewModelType => (Base_ViewModel)serviceProvider.GetRequiredService(viewModelType));

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = DIGetRequiredService<MainWindow>(_serviceProvider);
            mainWindow.Show();
            base.OnStartup(e);
        }

        private static T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {

            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }

}
