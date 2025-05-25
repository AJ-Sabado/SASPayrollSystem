using PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard;
using System.Windows.Controls;

namespace PresentationLayer.WPF.View.Pages.Dashboard.AdminDashboard
{

    public partial class AdminEmployee_View : UserControl
    {
        public AdminEmployee_View(AdminEmployee_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
