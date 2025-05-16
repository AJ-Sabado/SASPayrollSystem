using PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardRegular;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardReg
{
    public partial class RegDashboard : UserControl
    {
        private bool isTimedIn = false;
        private bool isBreakAvailable = true;
        private readonly DispatcherTimer clockTimer;

        public RegDashboard(RegDashboard_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            clockTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            clockTimer.Tick += UpdateClock;

            Loaded += RegDashboard_Loaded;
            Unloaded += RegDashboard_Unloaded;
        }

        private void RegDashboard_Loaded(object sender, RoutedEventArgs e)
        {
            clockTimer.Start();
        }

        private void RegDashboard_Unloaded(object sender, RoutedEventArgs e)
        {
            clockTimer.Stop();
        }

        private void UpdateClock(object sender, EventArgs e)
        {
            txtCurrentDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
            txtCurrentTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void ApplyTimeInState()
        {
            btnTimeInOut.Style = (Style)this.Resources["TimeOutStyle"];
            EnableBreak();
        }

        private void ApplyTimeOutState()
        {
            btnTimeInOut.Style = (Style)this.Resources["TimeInStyle"];
            DisableBreak();
        }

        private void EnableBreak()
        {
            btnBreak.IsEnabled = true;
            isBreakAvailable = true;
        }

        private void DisableBreak()
        {
            btnBreak.IsEnabled = false;
            isBreakAvailable = false;
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement log-out logic
        }

        private void btnTimeInOut_Click(object sender, RoutedEventArgs e)
        {
            if (!isTimedIn)
                ApplyTimeInState();
            else
                ApplyTimeOutState();

            isTimedIn = !isTimedIn;
        }

        private void btnBreak_Click(object sender, RoutedEventArgs e)
        {
            if (isBreakAvailable)
            {
                MessageBox.Show("1 hour break started");
                DisableBreak();
            }
        }
    }
}
