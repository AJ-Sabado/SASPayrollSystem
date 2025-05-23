using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardIC;

namespace PresentationLayer.WPF.View.Pages.Dashboard.EmployeeDashboardIC
{
    /// <summary>
    /// Interaction logic for ICJobDesk.xaml
    /// </summary>
    public partial class ICJobDesk : UserControl
    {
        public ICJobDesk(ICJobDesk_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        
    }
}
