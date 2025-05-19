using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.WPF.View.Pages
{
    /// <summary>
    /// Interaction logic for EmployeeDahboard_View.xaml
    /// </summary>
    public partial class EmployeeDahboard_View : Window
    {

        public EmployeeDahboard_View(EmployeeDashboardReg_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            }
            catch (InvalidOperationException ex)
            {
                // Handle the exception if needed
                // For example, you can log the error or show a message to the user
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMaximize_MaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void btnMinimize_MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
