using PresentationLayer.WPF.View.Pages.Dashboard;
using PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardReg;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel
{
    public class EmployeeDashboardReg_ViewModel : Base_ViewModel
    {
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
        public ICommand ShowJobDeskCommand { get; }
        public ICommand ShowAccountsCommand { get; }

        public EmployeeDashboardReg_ViewModel()
        {
            ShowDashboardCommand = new RelayCommand(_ => ShowView(new RegDashboard(), "Dashboard"));
            ShowJobDeskCommand = new RelayCommand(_ => ShowView(new RegJobDesk(), "JobDesk"));
            ShowAccountsCommand = new RelayCommand(_ => ShowView(new AccountsPage(), "Accounts"));

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
