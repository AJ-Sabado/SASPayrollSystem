using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Presenters
{
    public class SignInPresenter
    {
        private readonly IUnitOfWork _unitOfWork;

        public SignInPresenter(IUnitOfWork servicesMesh)
        {
            _unitOfWork = servicesMesh;
        }

        public async Task AuthenticateUser(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Username cannot be empty");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

            await _unitOfWork.LoginUser(userName, password);

        }
    }
}
