using Syncfusion.WinForms.Controls;

namespace PresentationLayer.Views
{
    public interface ILogin_Form
    {
        string PasswordField { get; set; }
        string UsernameField { get; set; }

        event EventHandler SignIn;

        void Hide();

        void Close();

        void Show();

        void ShowMessage(string message);
    }
}