using System.Windows;
using PresentationLayer.WPF.ViewModel.PopUpViewModel;

namespace PresentationLayer.WPF.View.Windows.PopUps
{
    /// <summary>
    /// Interaction logic for AssignLeave_View.xaml
    /// </summary>
    public partial class AssignLeave_View : Window
    {
        public AssignLeave_View(AssignLeave_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
