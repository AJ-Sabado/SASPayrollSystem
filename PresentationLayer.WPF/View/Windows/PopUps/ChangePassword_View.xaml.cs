using System.Windows;
using PresentationLayer.WPF.ViewModel.PopUpViewModel;

namespace PresentationLayer.WPF.View.Windows.PopUps
{
    /// <summary>
    /// Interaction logic for ChangePassword_View.xaml
    /// </summary>
    public partial class ChangePassword_View : Window
    {
        public ChangePassword_View(ChangePassword_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
