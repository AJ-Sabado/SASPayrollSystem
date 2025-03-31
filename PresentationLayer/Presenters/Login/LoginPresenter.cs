using PresentationLayer.Views;
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

        public LoginPresenter(IUnitOfWork unitOfWork, ILogin_Form loginForm, IDashboard_Employee dashboardForm)
        {
            _unitOfWork = unitOfWork;
            _loginForm = loginForm;
            _dashboardForm = dashboardForm;

            _loginForm.SignIn += SignIn;
        }

        private async void SignIn(object? Sender, EventArgs e)
        {
            try
            {
                await _unitOfWork.LoginUser(_loginForm.UsernameField, _loginForm.PasswordField);
                _dashboardForm.Show();
                _loginForm.Hide();
            }

            catch (UserNotFoundException)
            {
                _loginForm.ShowMessage("User not found!");
            }
            catch (IncorrectPasswordException)
            {
                _loginForm.ShowMessage("Wrong password!");
            }
        }
    }
}
