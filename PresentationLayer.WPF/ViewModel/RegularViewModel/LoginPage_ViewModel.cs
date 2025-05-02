using PresentationLayer.WPF.Services;
using ServicesLayer;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class LoginPage_ViewModel : Base_ViewModel
    {
        private INavigationService _navigationService;
        private IUnitOfWork _unitOfWork;

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

        public ICommand Login { get; set; }

        public LoginPage_ViewModel(INavigationService navigationService, IUnitOfWork unitOfWork)
        {
            Navigation = navigationService;
            _unitOfWork = unitOfWork;

            Login = new RelayCommand(AuthenticateUser, canExecute: o_object => true);

            _unitOfWork.InitialSeeding();
        }

        private void AuthenticateUser(object? obj)
        {
            System.Windows.MessageBox.Show(PasswordSignIn);
        }
    }
}
