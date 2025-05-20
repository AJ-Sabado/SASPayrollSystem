using PresentationLayer.WPF.ViewModel.PagesViewModel;
using System.Windows.Controls;
using DomainLayer.Enums.EmployeePersonalInfo;
using System.Windows;

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

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                string password = passwordBox.Password;
                SendPasswordToViewModel(password);
            }
        }

        //Code-behind pasword binding
        private void SendPasswordToViewModel(string password)
        {
            if (DataContext is AccountPage_ViewModel viewModel)
            {
                viewModel.Password = password;
            }
        }
    }
}
