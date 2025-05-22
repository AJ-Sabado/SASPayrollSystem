using System.Windows;
using System.Windows.Input;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Pages.Dashboard;
using PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardIC;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class EmployeeDashboardIC_ViewModel : Base_ViewModel
    {
        private object _currentView;
        private readonly IContractorTrackerService _contractorTrackerService;
        private readonly IPageService _pageService;

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
        public ICommand ShowJobDeskCommand { get; }
        public ICommand ShowAccountsCommand { get; }

        public EmployeeDashboardIC_ViewModel(IPageService pageService, IContractorTrackerService contractorTrackerService)
        {
            _contractorTrackerService = contractorTrackerService;
            _pageService = pageService;

            

            ShowDashboardCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<ICDashboard>(), "Dashboard"));
            ShowJobDeskCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<ICJobDesk>(), "JobDesk"));
            ShowAccountsCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<AccountsPage>(), "Accounts"));

            // Set the default page and selected menu when opening
            InitializeServices();
        }

        private async void InitializeServices()
        { 
            var contractor = await _contractorTrackerService.InitializeService(Properties.Settings.Default.CurrentUserGuid);
            ShowDashboardCommand.Execute(null);
        }


        private void ShowView(object view, string menu)
        {
            CurrentView = view;
            SelectedMenu = menu;
        }

        //Methods
        public void OnClosing()
        {
            // Perform any cleanup or save state here
            // For example, you might want to save the current state of the dashboard
            // or close any open connections to databases or services.
            MessageBox.Show("Closing the dashboard. Please save your work.", "Closing", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
