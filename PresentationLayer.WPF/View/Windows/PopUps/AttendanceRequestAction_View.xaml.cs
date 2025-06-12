using System.Windows;
using PresentationLayer.WPF.ViewModel.PopUpViewModel;

namespace PresentationLayer.WPF.View.Windows.PopUps
{
    /// <summary>
    /// Interaction logic for AttendanceRequestAction_View.xaml
    /// </summary>
    public partial class AttendanceRequestAction_View : Window
    {
        public AttendanceRequestAction_View(AttendanceRequestAction_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
