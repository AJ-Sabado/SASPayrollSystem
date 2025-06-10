using System.Windows.Input;
using DomainLayer.Services;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PopUpViewModel
{
    public class ChangePassword_ViewModel : Base_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MyMessageBox _messageBox;

        public string CurrentPassword { private get; set; }
        public string NewPassword { private get; set; }
        public string ConfirmNewPassword { private get; set; }

        public ICommand ChangePassword { get; set; }
        public ICommand Cancel { get; set; }

        public ChangePassword_ViewModel(IPopUpService popUpService, IUnitOfWork unitOfWork, MyMessageBox messageBox)
        {
            _popUpService = popUpService;
            _unitOfWork = unitOfWork;
            _messageBox = messageBox;

            ChangePassword = new RelayCommand(ExecuteChangePassword, _ => true);
            Cancel = new RelayCommand(ExecuteCancel, _ => true);
        }

        private void ExecuteCancel(object? obj)
        {
            _popUpService.ClosePopup();
        }

        private async void ExecuteChangePassword(object? obj)
        {

            if (string.IsNullOrEmpty(CurrentPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmNewPassword))
            {
                _messageBox.ShowDialog("Please fill in the fields.", MyMessageBoxType.Error);
                return;
            }

            if (!NewPassword.Equals(ConfirmNewPassword))
            {
                _messageBox.ShowDialog("Passwords do not match!", MyMessageBoxType.Error);
                return;
            }

            if (CurrentPassword.Equals(NewPassword))
            {
                _messageBox.ShowDialog("New password is the same as current password!", MyMessageBoxType.Error);
                return;
            }

            if (NewPassword.Length < 6)
            {
                _messageBox.ShowDialog("Password is too weak! Must be 6 characters or more", MyMessageBoxType.Error);
                return;
            }

            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == Properties.Settings.Default.CurrentUserGuid);

            if (user == null)
            {
                _messageBox.ShowDialog("User not found!", MyMessageBoxType.Error);
                _popUpService.ClosePopup();
                return;
            }

            var encryption = new Encryption();
            var passwordHash = encryption.GenerateHash(CurrentPassword, user.Salt);

            if (!user.PasswordHash.SequenceEqual(passwordHash))
            {
                _messageBox.ShowDialog("Invalid current password!", MyMessageBoxType.Error);
                return;
            }

            user.Password = NewPassword;
            try
            {
                await _unitOfWork.Save();
                _messageBox.ShowDialog("Password changed successfuly!", MyMessageBoxType.Success);
                _popUpService.ClosePopup();
            }
            catch (Exception ex)
            {
                _messageBox.ShowDialog($"Error message: {ex.Message}");
                _popUpService.ClosePopup();
            }
        }
    }
}
