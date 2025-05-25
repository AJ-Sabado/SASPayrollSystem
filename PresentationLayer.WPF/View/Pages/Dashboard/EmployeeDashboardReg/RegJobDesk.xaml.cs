using PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardRegular;
using System.Windows.Controls;

namespace PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardReg
{
    public partial class RegJobDesk : UserControl
    {
        public RegJobDesk(RegJobDesk_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
