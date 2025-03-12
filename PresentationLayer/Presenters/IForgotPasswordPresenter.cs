
namespace PresentationLayer.Presenters
{
    public interface IForgotPasswordPresenter
    {
        Task ForgotPasswordRequest(string username, string email, string password, string confirmPassword);
    }
}