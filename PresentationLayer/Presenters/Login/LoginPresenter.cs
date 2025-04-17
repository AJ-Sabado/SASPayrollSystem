using PresentationLayer.Views;
using PresentationLayer.Views.Custom_Message_Box;
using PresentationLayer.Views.Forgot_Password_Forms;
using ServicesLayer;
using ServicesLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Presenters.Login
{
    public class LoginPresenter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogin_Form _loginForm;
        private readonly IDashboard_Employee _dashboardForm;

        private IForgotPassword_View _forgotPassword;

        public LoginPresenter(IUnitOfWork unitOfWork, ILogin_Form loginForm, IDashboard_Employee dashboardForm)
        {
            _unitOfWork = unitOfWork;
            _loginForm = loginForm;
            _dashboardForm = dashboardForm;

            _loginForm.SignIn += SignIn;
            _loginForm.ForgotPassword += ForgotPassword;
            
        }

        private async void SignIn(object? Sender, EventArgs e)
        {

            if(string.IsNullOrEmpty(_loginForm.UsernameField) || string.IsNullOrEmpty(_loginForm.PasswordField))
            {
                _loginForm.ShowMessage("Missing Fields!", "Please fill in all fields before proceeding.", dBoxType.Error);
                return;
            }

            try
            {
                await _unitOfWork.LoginUser(_loginForm.UsernameField, _loginForm.PasswordField);
                _loginForm.ShowMessage("Success!", "Login successful! Welcome to your Employee Dashboard. You are now ready to manage your tasks and access important information. Enjoy your session!", dBoxType.Success);
                _dashboardForm.Show();
                _loginForm.Hide();
            }

            catch (UserNotFoundException)
            {
                _loginForm.ShowMessage("Not Found!", "We couldn't find a user matching the provided information.\r\nPlease check your credentials and try again.", dBoxType.Error);
            }
            catch (IncorrectPasswordException)
            {
                _loginForm.ShowMessage("Incorrect!", "The password you entered is incorrect.\r\nPlease try again or reset your password if you’ve forgotten it.\r\n", dBoxType.Error);
            }
        }

        private async void ForgotPassword(object? Sender, EventArgs e)
        {
            _forgotPassword = new ForgotPassword_View(_unitOfWork);
            _loginForm.Hide();
            _forgotPassword.Show();
            _loginForm.Show();
        }
    }
}
