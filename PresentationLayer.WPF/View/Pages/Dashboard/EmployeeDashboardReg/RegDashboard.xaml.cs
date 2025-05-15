using PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardRegular;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardReg
{

    public partial class RegDashboard : UserControl
    {

        public RegDashboard(RegDashboard_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            StartClock();
        }

        //Clock
        private async void StartClock()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Dispatcher.Invoke(() =>
                    {
                        txtCurrentDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                        txtCurrentTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
                    });
                    Task.Delay(1000).Wait();
                }
            });
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTimeOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTimeIn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
