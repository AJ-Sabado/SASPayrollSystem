using ServicesLayer;

namespace PresentationLayer.Presenters
{
    public class SignUpPresenter : ISignUpPresenter
    {
        private readonly IUnitOfWork _unitOfWork;

        public SignUpPresenter(IUnitOfWork servicesMesh)
        {
            _unitOfWork = servicesMesh;
        }

        public async Task NewUserRequest(string username, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty");

            await _unitOfWork.NewUserRequest(username, password, email);
        }
    }
}
