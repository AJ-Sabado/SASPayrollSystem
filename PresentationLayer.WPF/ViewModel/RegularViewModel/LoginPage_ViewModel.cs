using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Pages;
using ServicesLayer;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class LoginPage_ViewModel : Base_ViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWindowService _windowService;

        public string Role { get; private set; } = string.Empty;

        public string UsernameSignIn { private get; set; } = string.Empty;
        public string PasswordSignIn { private get; set; }

        private string _loginMessage = string.Empty;
        public string LoginMessage
        {
            get => _loginMessage;
            private set
            {
                _loginMessage = value;
                OnPropertyChanged();
            }
        }

        private string _foregroundColor = "Black";
        public string ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = value;
                OnPropertyChanged();
            }
        }

        public LoginPage_ViewModel(IUnitOfWork unitOfWork, IWindowService windowService)
        {
            _unitOfWork = unitOfWork;
            _windowService = windowService;
            _unitOfWork.InitialSeeding();

            Login = new RelayCommand(AuthenticateUser, _ => true);
        }

        public ICommand Login { get; set; }

        private async void AuthenticateUser(object? parameter)
        {
            if (string.IsNullOrEmpty(UsernameSignIn) || string.IsNullOrEmpty(PasswordSignIn))
            {
                ForegroundColor = "Black";
                LoginMessage = "Please fill in the fields.";
                return;
            }

            var user = await _unitOfWork.Login(UsernameSignIn, PasswordSignIn);
            if (user != null)
            {
                ForegroundColor = "Green";
                LoginMessage = "Login successful!";
                Properties.Settings.Default.CurrentUserGuid = user.UserId;
                Properties.Settings.Default.Save();
                await Task.Delay(2000);
                if (user.Role.NormalizedName == "EMPLOYEE")
                    _windowService.ShowWindow<EmployeeDahboard_View>();
                else if (user.Role.NormalizedName == "ADMIN")
                    System.Windows.MessageBox.Show("Admin Functionality to be added");
                else if (user.Role.NormalizedName == "CONTRACTOR")
                    System.Windows.MessageBox.Show("Contractor Functionality to be added");
            }
            else
            {
                ForegroundColor = "Red";
                LoginMessage = "Invalid username or password!";
            }
        }
    }
}
