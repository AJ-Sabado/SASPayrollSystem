using PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard;
using System.Windows.Controls;

namespace PresentationLayer.WPF.View.Pages.Dashboard.AdminDashboard
{
    /// <summary>
    /// Interaction logic for AdminPayrollPage_View.xaml
    /// </summary>
    public partial class AdminPayrollPage_View : UserControl
    {
        public AdminPayrollPage_View(AdminPayrollPage_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
