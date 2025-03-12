using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Presenters
{
    public class ForgotPasswordPresenter : BasePresenter, IForgotPasswordPresenter
    {
        private readonly IUnitOfWork _unitOfWork;

        public ForgotPasswordPresenter(IUnitOfWork servicesMesh)
        {
            _unitOfWork = servicesMesh;
        }

        public async Task ForgotPasswordRequest(string username, string email, string password, string confirmPassword)
        {
            await _unitOfWork.ForgotPasswordRequest(username, email, password, confirmPassword);
        }
    }
}
