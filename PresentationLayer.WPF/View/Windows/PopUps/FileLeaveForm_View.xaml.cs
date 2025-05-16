using System.Windows;
using PresentationLayer.WPF.ViewModel.PopUpViewModel;


namespace PresentationLayer.WPF.View.Windows
{
    /// <summary>
    /// Interaction logic for FileLeaveForm_View.xaml
    /// </summary>
    public partial class FileLeaveForm_View : Window
    {
        public FileLeaveForm_View(LeaveRequest_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
