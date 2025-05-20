using PresentationLayer.WPF.ViewModel.RegularViewModel;
using System.Windows;
using System.Windows.Input;

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
    }
}
