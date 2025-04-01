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

        public ForgotPasswordPresenter(IUnitOfWork servicesMesh)
        {
            _unitOfWork = servicesMesh;
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
    }
}
