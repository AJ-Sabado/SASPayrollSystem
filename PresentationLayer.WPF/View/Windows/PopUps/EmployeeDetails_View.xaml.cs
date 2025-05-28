using System.Windows;
using PresentationLayer.WPF.ViewModel.PopUpViewModel;

namespace PresentationLayer.WPF.View.Windows.PopUps
{
    /// <summary>
    /// Interaction logic for EmployeeDetails_View.xaml
    /// </summary>
    public partial class EmployeeDetails_View : Window
    {
        public EmployeeDetails_View(EmployeeDetails_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
