using PresentationLayer.WPF.Services;
using ServicesLayer;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class LoginPage_ViewModel : Base_ViewModel
    {
        private INavigationService _navigationService;
        private readonly IUnitOfWork _unitOfWork;

        public string Role { get; private set; }

        public INavigationService Navigation
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }

        public string UsernameSignIn { private get; set; } = string.Empty;
        public string PasswordSignIn { private get; set; }

        private string _loginMessage = string.Empty;
        public string LoginMessage 
        { 
            get => _loginMessage; 
            set
            {
                _loginMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand Login { get; set; }

        public LoginPage_ViewModel(INavigationService navigationService, IUnitOfWork unitOfWork)
        {
            Navigation = navigationService;
            _unitOfWork = unitOfWork;

            Login = new RelayCommand(AuthenticateUser, canExecute: o_object => true);

            _unitOfWork.InitialSeeding();
        }

        private async void AuthenticateUser(object? obj)
        {
            if (string.IsNullOrEmpty(UsernameSignIn) || string.IsNullOrEmpty(PasswordSignIn))
            {
                LoginMessage = "";
                return;
            }

            var user = await _unitOfWork.Login(UsernameSignIn, PasswordSignIn);
            if (user != null)
            {
                LoginMessage = "";
                Properties.Settings.Default.CurrentUserGuid = user.UserId;
                System.Windows.MessageBox.Show("Login successful!");
            }
            else
            {
                LoginMessage = "Invalid username or password!";
            }
        }
    }
}
