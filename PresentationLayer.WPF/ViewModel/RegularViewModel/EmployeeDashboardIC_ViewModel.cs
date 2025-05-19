using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Pages.Dashboard;
using PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardIC;
using PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardReg;
using ServicesLayer;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class EmployeeDashboardIC_ViewModel:Base_ViewModel
    {
        private object _currentView;

        private IPageService _pageService;

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

        public EmployeeDashboardIC_ViewModel(IUnitOfWork unitOfWork, IPageService pageService)
        {
            _pageService = pageService;

            ShowDashboardCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<ICDashboard>(), "Dashboard"));
            ShowJobDeskCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<ICJobDesk>(), "JobDesk"));
            ShowAccountsCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<AccountsPage>(), "Accounts"));

            // Set the default page and selected menu when opening
            ShowDashboardCommand.Execute(null);
        }

        private void ShowView(object view, string menu)
        {
            CurrentView = view;
            SelectedMenu = menu;
        }
    }
}
