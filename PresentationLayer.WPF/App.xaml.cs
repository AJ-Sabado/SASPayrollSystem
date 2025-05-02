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
            //TO DO - Add all Views, User controls, and View Models to Service Collection as Singletons

            //This creates all singletons/instances of application objects
            var services = new ServiceCollection();

            //Login Page mapping
            //services.AddSingleton<MainWindow>(provider => new MainWindow
            //{
            //    DataContext = DIGetRequiredService<LoginPage_ViewModel>(provider)
            //});
            //services.AddSingleton<LoginPage_ViewModel>();

            ////Employee Dashboard mapping
            services.AddSingleton<EmployeeDashboardReg_ViewModel>();
            services.AddSingleton<EmployeeDahboard_View>(provider => new EmployeeDahboard_View
            {
                DataContext = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<EmployeeDashboardReg_ViewModel>(provider)
            });
            services.AddSingleton<LoginPage_ViewModel>();

            //Employee Dashboard mapping
            services.AddSingleton<EmployeeDahboard_View>(provider => new EmployeeDahboard_View
            {
                DataContext = DIGetRequiredService<EmployeeDashboardReg_ViewModel>(provider)
            });
            services.AddSingleton<EmployeeDashboardReg_ViewModel>();

            services.AddSingleton<Func<Type, Base_ViewModel>>(serviceProvider => viewModelType => (Base_ViewModel)serviceProvider.GetRequiredService(viewModelType));

            //Navigation service func is mapped
            services.AddSingleton<INavigationService, NavigationService>();

            //Unit of work is mapped in DI
            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            //The service provider itself is built here
            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //This determines what view is displayed on startup
            var mainWindow = DIGetRequiredService<EmployeeDahboard_View>(_serviceProvider);
            mainWindow.Show();
            base.OnStartup(e);
        }

        //Localized version of GetRequiredService<T> method because of error lol
        private static T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }

}
