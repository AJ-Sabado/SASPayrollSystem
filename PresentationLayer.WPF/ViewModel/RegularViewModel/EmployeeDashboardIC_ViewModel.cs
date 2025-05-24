using System.ComponentModel;
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
        private readonly MyMessageBox _messageBox;

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
        public ICommand WindowClosing { get; }

        public EmployeeDashboardIC_ViewModel(IPageService pageService, 
            IContractorTrackerService contractorTrackerService,
            MyMessageBox messageBox)
        {
            _contractorTrackerService = contractorTrackerService;
            _pageService = pageService;
            _messageBox = messageBox;
            

            ShowDashboardCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<ICDashboard>(), "Dashboard"));
            ShowJobDeskCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<ICJobDesk>(), "JobDesk"));
            ShowAccountsCommand = new RelayCommand(_ => ShowView(_pageService.GetPage<AccountsPage>(), "Accounts"));
            WindowClosing = new RelayCommand(ExecuteOnClosing, _ => true);

            // Set the default page and selected menu when opening
            InitializeServices();
        }

        private async void ExecuteOnClosing(object? parameter)
        {
            if (parameter is CancelEventArgs e)
            {
                if (! await CanCloseWindow())
                    e.Cancel = true;
            }
        }

        private async Task<bool> CanCloseWindow()
        {
            var result = _messageBox.ShowDialog("You are about to logout/exit. This will end your session.");
            if (result == null || result.MyMessageBoxDialogResult == MyMessageBoxDialogResult.Yes)
            {
                await _contractorTrackerService.EndSession();
                return true;
            }
            return false;
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
        //public async void OnClosing()
        //{
        //    if (_contractorTrackerService.CurrentAttendanceLog != null)
        //    {
        //        var log = await _contractorTrackerService.EndSession();
        //        if (log != null)
        //        {
        //            Properties.Settings.Default.CurrentUserGuid = Guid.Empty;
        //            Properties.Settings.Default.Save();
        //            MessageBox.Show("Session ended. Any time in will be timed out.", "Closing", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //        else
        //            MessageBox.Show("There was a problem with");
        //    }
        //}
    }
}
