namespace PresentationLayer.WPF.ViewModel.PopUpViewModel
{
    public class EmployeeDetails_ViewModel : Base_ViewModel
    {
        private string _fullName = string.Empty;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        private string _role = string.Empty;
        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }
    }
}
