using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Pages.Dashboard.AdminDashboard;
using PresentationLayer.WPF.View.Pages;
using System.Windows.Input;
using ServicesLayer;
using PresentationLayer.WPF.View.Pages.Dashboard;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class AdminDashboard_ViewModel : Base_ViewModel
    {
        private readonly IPageService _pageService;
        private readonly IAdminOperationsService _adminOperationService;
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        private string _selectedMenu;
        public string SelectedMenu
        {
            get => _selectedMenu;
            set => SetProperty(ref _selectedMenu, value);
        }

        public ICommand ShowDashboardCommand { get; }
        public ICommand ShowPayrollCommand { get; }
        public ICommand ShowEmployeeCommand { get; }
        public ICommand ShowWorkLogsCommand { get; }
        public ICommand ShowAdministrationCommand { get; }
        public ICommand ShowAccountCommand { get; }

        public AdminDashboard_ViewModel(IPageService pageService, IAdminOperationsService adminOperationsService)
        {
            _pageService = pageService;
            _adminOperationService = adminOperationsService;

            InitializeServices();

            ShowDashboardCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<AdminDashPage_View>(), "Dashboard"));
            ShowPayrollCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<AdminPayrollPage_View>(), "Payroll"));
            ShowEmployeeCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<AdminEmployee_View>(), "Employee"));
            ShowWorkLogsCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<AdminWorkforce_View>(), "WorkLogs"));
            ShowAdministrationCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<AdminAdminPage_View>(), "Administration"));
            ShowAccountCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<AccountsPage>(), "Account"));

            // Initialize with Dashboard page and menu selected
            ShowDashboardCommand.Execute(null);
        }

        private async void InitializeServices()
        {
            if (Properties.Settings.Default.CurrentUserGuid != Guid.Empty)
                await _adminOperationService.InitializeService(Properties.Settings.Default.CurrentUserGuid);
        }

        private void ShowView(object view, string menu)
        {
            CurrentView = view;
            SelectedMenu = menu;
        }
    }
}
