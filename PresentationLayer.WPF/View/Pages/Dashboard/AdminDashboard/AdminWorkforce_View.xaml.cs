using PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard;
using System.Windows.Controls;

namespace PresentationLayer.WPF.View.Pages.Dashboard.AdminDashboard
{
    /// <summary>
    /// Interaction logic for AdminWorkforce_View.xaml
    /// </summary>
    public partial class AdminWorkforce_View : UserControl
    {
        public AdminWorkforce_View(AdminWorkforce_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
