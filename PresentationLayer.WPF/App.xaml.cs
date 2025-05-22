using System.Windows;
using InfrastructureLayer.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Pages;
using PresentationLayer.WPF.View.Pages.Dashboard;
using PresentationLayer.WPF.View.Pages.Dashboard.AdminDashboard;
using PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardIC;
using PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardReg;
using PresentationLayer.WPF.View.Windows;
using PresentationLayer.WPF.View.Windows.Main;
using PresentationLayer.WPF.ViewModel;
using PresentationLayer.WPF.ViewModel.PagesViewModel;
using PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardIC;
using PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardRegular;
using PresentationLayer.WPF.ViewModel.PopUpViewModel;
using PresentationLayer.WPF.ViewModel.RegularViewModel;
using ServicesLayer;

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

            //This determines the startup window
            var windowService = DIGetRequiredService<IWindowService>(_serviceProvider);
            windowService.ShowWindow<MainWindow>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //Windows
            services.AddTransient<MainWindow>();
            services.AddTransient<LoginPage_ViewModel>();

            services.AddTransient<EmployeeDahboard_View>();
            services.AddTransient<EmployeeDashboardReg_ViewModel>();

            services.AddTransient<EmployeeDashboardIC_View>();
            services.AddTransient<EmployeeDashboardIC_ViewModel>();

            //Pages
            services.AddTransient<RegDashboard>();
            services.AddTransient<RegJobDesk>();
            services.AddTransient<AccountsPage>();
            services.AddTransient<RegDashboard_ViewModel>();
            services.AddTransient<RegJobDesk_ViewModel>();
            services.AddTransient<AccountPage_ViewModel>();

            services.AddTransient<AdminDashboard>();

            services.AddTransient<ICDashboard>();
            services.AddTransient<ICDashboard_ViewModel>();
            services.AddTransient<ICJobDesk>();
            services.AddTransient<ICJobDesk_ViewModel>();

            //Popups
            services.AddTransient<FileLeaveForm_View>();
            services.AddTransient<LeaveRequest_ViewModel>();
            services.AddTransient<AttendanceRequest_View>();
            services.AddTransient<AttendanceRequest_ViewModel>();
            services.AddTransient<FileLeaveForm_View>();

            //DbContext
            services.AddDbContext<AppDbContext>();
            //Code below is used when actually connecting to the database
            //services.AddDbContext<AppDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(CONNECTION_STRING));

            //Other Services
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<IPopUpService, PopUpService>();
            services.AddSingleton<IContractorTrackerService, ContractorTrackerService>();
        }

        //Resolves GetRequiredService for DI conflict
        private T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }
}
