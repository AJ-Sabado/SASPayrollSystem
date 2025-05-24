using System.Windows;
using DomainLayer.Services;

namespace PresentationLayer.WPF.View.Windows.PopUps.CustomMessageBox
{
    /// <summary>
    /// Interaction logic for PasswordPrompt_View.xaml
    /// </summary>
    public partial class PasswordPrompt_View : Window
    {
        public bool PasswordMatch { get; private set; } = false;
        public byte[] PasswordHash { private get; set; } = [];
        public byte[] Salt { private get; set; } = [];

        public PasswordPrompt_View()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var encryption = new Encryption();
            var enteredPassHash = encryption.GenerateHash(customPasswordBox.pbPasswordBox.Password, Salt);
            PasswordMatch = enteredPassHash.SequenceEqual(PasswordHash);
            DialogResult = true;
            this.Close();
        }
    }
}
