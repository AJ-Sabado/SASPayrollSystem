using PresentationLayer.Views.Forgot_Password_Forms;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Presenters
{
    public class ForgotPasswordPresenter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IForgotPassword_View _forgotPasswordView;

        public ForgotPasswordPresenter(IUnitOfWork servicesMesh, IForgotPassword_View view)
        {
            _unitOfWork = servicesMesh;
            _forgotPasswordView = view;

            _forgotPasswordView.NextClick += OnNextClick;
            _forgotPasswordView.NextClick2 += OnNextClick2;
            _forgotPasswordView.CancelClick += CancelClick;
        }

        public async Task ForgotPasswordRequest(string username, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

            if (string.IsNullOrWhiteSpace(confirmPassword))
                throw new ArgumentException("Confirm password cannot be empty");

            if (password != confirmPassword)
                throw new ArgumentException("Passwords do not match");

            await _unitOfWork.ForgotPasswordRequest(username, email, password, confirmPassword);
        }

        public void OnNextClick(object? Sender, EventArgs e)
        {
            _forgotPasswordView.btnNext_Click();
        }

        public void OnNextClick2(object? Sender, EventArgs e)
        {
            _forgotPasswordView.btnNext2_Click();
        }

        public void CancelClick(object? Sender, EventArgs e)
        {
            _forgotPasswordView.Close();
        }
    }
}
