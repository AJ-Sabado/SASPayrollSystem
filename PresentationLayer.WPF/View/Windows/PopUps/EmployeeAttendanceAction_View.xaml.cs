using PresentationLayer.WPF.ViewModel.PopUpViewModel;
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
using System.Windows.Shapes;

namespace PresentationLayer.WPF.View.Windows.PopUps
{
    /// <summary>
    /// Interaction logic for EmployeeAttendanceAction_View.xaml
    /// </summary>
    public partial class EmployeeAttendanceAction_View : Window
    {
        public EmployeeAttendanceAction_View(EmployeeAttendanceAction_ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
