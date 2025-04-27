using PresentationLayer.WPF.ViewModel;
using System.Windows;

namespace PresentationLayer.WPF.View.Windows
{
    /// <summary>
    /// Interaction logic for AttendanceRequest_View.xaml
    /// </summary>
    public partial class AttendanceRequest_View : Window
    {
        public AttendanceRequest_View()
        {
            InitializeComponent();
            DataContext = new AttendanceRequest_ViewModel();
        }
    }
}
