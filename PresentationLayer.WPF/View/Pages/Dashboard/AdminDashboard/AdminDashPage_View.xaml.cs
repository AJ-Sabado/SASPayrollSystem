using PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard;
using System.Windows.Controls;

namespace PresentationLayer.WPF.View.Pages.Dashboard.AdminDashboard
{
    /// <summary>
    /// Interaction logic for AdminDashPage_View.xaml
    /// </summary>
    public partial class AdminDashPage_View : UserControl
    {
        public AdminDashPage_View(AdminDashPage_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
