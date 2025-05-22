using System.Windows;
using System.Windows.Input;
using PresentationLayer.WPF.ViewModel.RegularViewModel;

namespace PresentationLayer.WPF.View.Windows.Main
{
    /// <summary>
    /// Interaction logic for EmployeeDashboardIC_View.xaml
    /// </summary>
    public partial class EmployeeDashboardIC_View : Window
    {
        public EmployeeDashboardIC_View(EmployeeDashboardIC_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Check if the left mouse button is pressed
            if (e.ChangedButton == MouseButton.Left)
            {
                // Allow the window to be dragged
                DragMove();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBoxResult.No == MessageBox.Show("Are you sure you want to logout/exit? This will end your current work session.", "Exit Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                e.Cancel = true; // Cancel the closing event
            }
            else
                ((EmployeeDashboardIC_ViewModel)DataContext).OnClosing();
        }
    }
}
