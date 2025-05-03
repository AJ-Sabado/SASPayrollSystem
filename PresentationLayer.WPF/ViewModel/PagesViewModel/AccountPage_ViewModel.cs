using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel
{
    public class AccountPage_ViewModel : Base_ViewModel
    {
        private IUnitOfWork _unitOfWork;

        //Bindable properties
        private string _fullName = "Full Name";
        public string FullName
        {
            get => _fullName;
            private set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }
        private string _role = "Role";
        public string Role
        {
            get => _role;
            private set
            {
                _role = value;
                OnPropertyChanged();
            }
        }
        private string _firstName = "First Name";
        public string FirstName
        {
            get => _firstName;
            private set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        private string _lastName = "Last Name";
        public string LastName
        {
            get => _lastName;
            private set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        private string _middleInitial = "M.I.";
        public string MiddleInitial
        {
            get => _middleInitial;
            private set
            {
                _middleInitial = value;
                OnPropertyChanged();
            }
        }
        private string _username = "Username";
        public string Username
        {
            get => _username;
            private set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        //Unsure how to bind this pls help
        private DateTime _dateOfBirth = DateTime.Now;
        public DateTime DateOfBirth
        { 
            get => _dateOfBirth;
            private set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }
        private string _gender = string.Empty;
        public string Gender
        {
            get => _gender;
            private set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }
        private string _nationality = string.Empty;
        public string Nationality
        {
            get => _nationality;
            private set
            {
                _nationality = string.Empty;
                OnPropertyChanged();
            }
        }



        public AccountPage_ViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            LoadUserData();
        }

        private async void LoadUserData()
        {
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "Employee,Admin,Contractor");
            if (user != null)
            {
                if (user.Employee != null)
                {
                    // Load employee data
                    var employee = await _unitOfWork.EmployeeRepository.GetAsync(x => x.UserId == user.UserId, includeProperties: "EmployeeAccountInfo");
                    if (employee != null && employee.EmployeeAccountInfo != null)
                    {
                        //Basic Info
                        FullName = employee.EmployeeAccountInfo.FullName;
                        Role = employee.EmployeeAccountInfo.Role;
                        FirstName = employee.EmployeeAccountInfo.FirstName;
                        LastName = employee.EmployeeAccountInfo.LastName;
                        MiddleInitial = employee.EmployeeAccountInfo.MiddleInitial;
                        Username = user.Username;
                        DateOfBirth = employee.EmployeeAccountInfo.DateOfBirth.ToDateTime(TimeOnly.MinValue);
                        Gender = employee.EmployeeAccountInfo.Gender.ToString();
                        Nationality = employee.EmployeeAccountInfo.Nationality.ToString();
                    }
                }
                else if (user.Admin != null)
                {
                    // Load admin data
                }
                else if (user.Contractor != null)
                {
                    // Load contractor data
                }
            }
        }
    }
}
