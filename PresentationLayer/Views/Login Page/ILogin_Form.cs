using PresentationLayer.Views.Custom_Message_Box;
using Syncfusion.WinForms.Controls;

namespace PresentationLayer.Views
{
    public interface ILogin_Form
    {
        string PasswordField { get; set; }
        string UsernameField { get; set; }

        event EventHandler SignIn;
        event EventHandler ForgotPassword;
        event EventHandler SignUp;

        void Hide();

        void Close();

        void Show();

        void ShowMessage(string title, string message, dBoxType type);
    }
}