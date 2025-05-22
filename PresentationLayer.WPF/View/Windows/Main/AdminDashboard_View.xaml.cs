using PresentationLayer.WPF.ViewModel.RegularViewModel;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.WPF.View.Windows.Main
{
    /// <summary>
    /// Interaction logic for AdminDashboard_View.xaml
    /// </summary>
    public partial class AdminDashboard_View : Window
    {
        public AdminDashboard_View(AdminDashboard_ViewModel vm)
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
