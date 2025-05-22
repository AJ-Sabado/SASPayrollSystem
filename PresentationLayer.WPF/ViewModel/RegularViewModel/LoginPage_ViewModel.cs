using System.Windows;
using System.Windows.Input;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Pages;
using PresentationLayer.WPF.View.Windows.Main;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class LoginPage_ViewModel : Base_ViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWindowService _windowService;

        public string Role { get; private set; } = string.Empty;

        public string UsernameSignIn { private get; set; } = string.Empty;
        public string PasswordSignIn { private get; set; }

        public string UsernameSignUp { private get; set; } = string.Empty;
        public string EmailSignUp { private get; set; } = string.Empty;
        public string PasswordSignUp { private get; set; }
        public string ConfirmPasswordSignUp { private get; set; } = string.Empty;


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

            SignIn = new RelayCommand(AuthenticateUser, _ => true);
            SignUp = new RelayCommand(SignUpNewUser, _ => true);
        }

        public ICommand SignIn { get; set; }
        public ICommand SignUp { get; set; }


        private async void SignUpNewUser(object? parameter)
        {
            if (string.IsNullOrEmpty(UsernameSignUp)
                || string.IsNullOrEmpty(EmailSignUp)
                || string.IsNullOrEmpty(PasswordSignUp)
                || string.IsNullOrEmpty(ConfirmPasswordSignUp))
            {
                System.Windows.MessageBox.Show("Please fill in all fields.");
                return;
            }
            try
            {
                var result = await _unitOfWork.RegisterUser(UsernameSignUp, EmailSignUp, PasswordSignUp, ConfirmPasswordSignUp);
                if (result == ServicesLayer.Enums.RegisterUserResult.UserAlreadyExists)
                {
                    System.Windows.MessageBox.Show("User already exists.");
                }
                else if (result == ServicesLayer.Enums.RegisterUserResult.InvalidEmail)
                {
                    System.Windows.MessageBox.Show("Invalid email address.");
                }
                else if (result == ServicesLayer.Enums.RegisterUserResult.PasswordMismatch)
                {
                    System.Windows.MessageBox.Show("Passwords do not match.");
                }
                else if (result == ServicesLayer.Enums.RegisterUserResult.WeakPassword)
                {
                    System.Windows.MessageBox.Show("Password is too weak.");
                }
                else if (result == ServicesLayer.Enums.RegisterUserResult.Success)
                {
                    System.Windows.MessageBox.Show("Registration successful!");
                }
                else
                {
                    System.Windows.MessageBox.Show("An unknown error occurred.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occured! {ex.Message}");
                return;
            }


        }

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
                if (user.Role.NormalizedName == "EMPLOYEE" && user.Employee != null)
                {


                    //Test attendance evaluation
                    var periodStart = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 16);
                    var periodEnd = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 31);
                    var payDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month + 1, 15);
                    try
                    {
                        //await _unitOfWork.EvaluateAllEmployeeAttendanceLog(periodStart, periodEnd);
                        //await _unitOfWork.GenerateAllEmployeePayslips(periodStart, periodEnd, payDate);
                        _windowService.ShowWindow<EmployeeDahboard_View>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (user.Role.NormalizedName == "ADMIN" && user.Admin != null)
                    System.Windows.MessageBox.Show("Admin Functionality to be added");
                else if (user.Role.NormalizedName == "CONTRACTOR" && user.Contractor != null)
                {
                    try
                    {
                        _windowService.ShowWindow<EmployeeDashboardIC_View>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    System.Windows.MessageBox.Show("Please contact admin to verify account!");
            }
            else
            {
                ForegroundColor = "Red";
                LoginMessage = "Invalid username or password!";
            }
        }
    }
}
