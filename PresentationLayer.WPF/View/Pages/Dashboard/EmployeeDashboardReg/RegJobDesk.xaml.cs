using PresentationLayer.WPF.ViewModel.RegularViewModel;
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
