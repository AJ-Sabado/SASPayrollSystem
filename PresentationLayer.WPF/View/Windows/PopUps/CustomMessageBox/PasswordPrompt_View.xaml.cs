using System.Windows;

namespace PresentationLayer.WPF.View.Windows.PopUps.CustomMessageBox
{
    /// <summary>
    /// Interaction logic for PasswordPrompt_View.xaml
    /// </summary>
    public partial class PasswordPrompt_View : Window
    {
        public string EnteredPassword { get; private set; }

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
            // Retrieve the password from the custom password box
            EnteredPassword = customPasswordBox.pbPasswordBox.Password;
            DialogResult = true;
            Close();
        }
    }
}
