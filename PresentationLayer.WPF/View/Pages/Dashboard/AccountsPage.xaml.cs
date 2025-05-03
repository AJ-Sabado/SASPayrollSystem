using PresentationLayer.WPF.ViewModel.PagesViewModel;
using System.Windows.Controls;

namespace PresentationLayer.WPF.View.Pages.Dashboard
{
    /// <summary>
    /// Interaction logic for AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : UserControl
    {
        public AccountsPage(AccountPage_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
