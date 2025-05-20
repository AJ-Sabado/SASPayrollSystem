using System.Windows.Input;
using DomainLayer.Enums.EmployeePersonalInfo;
using ServicesLayer;
using DomainLayer.Models.User;
using System.Collections.Specialized;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel
{
    public class AccountPage_ViewModel : Base_ViewModel
    {
        private IUnitOfWork _unitOfWork;


        //Basic Information
        private bool _editBasicInfo = false;
        public bool EditBasicInfo
        {
            get => _editBasicInfo;
            set
            {
                _editBasicInfo = value;
                OnPropertyChanged();
            }
        }
        private string _editBasicInfoButtonText = "Edit";
        public string EditBasicInfoButtonText
        {
            get => _editBasicInfoButtonText;
            set
            {
                _editBasicInfoButtonText = value;
                OnPropertyChanged();
            }
        }
        private string _editBasicInfoButtonIcon = "Edit";
        public string EditBasicInfoButtonIcon
        {
            get => _editBasicInfoButtonIcon;
            set
            {
                _editBasicInfoButtonIcon = value;
                OnPropertyChanged();
            }
        }
        private string _fullName = "Full Name";
        public string FullName
        {
            get => _fullName;
            set
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
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        private string _lastName = "Last Name";
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        private string _middleInitial = "M.I.";
        public string MiddleInitial
        {
            get => _middleInitial;
            set
            {
                _middleInitial = value;
                OnPropertyChanged();
            }
        }
        private string _username = "Username";
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        private DateTime _dateOfBirth = DateTime.Now;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }
        private Gender _selectedGender = Gender.Female;
        public Gender SelectedGender
        {
            get => _selectedGender;
            set
            {
                _selectedGender = value;
                OnPropertyChanged();
            }
        }
        private Nationality _selectedNationality = Nationality.Filipino;
        public Nationality SelectedNationality
        {
            get => _selectedNationality;
            set
            {
                _selectedNationality = value;
                OnPropertyChanged();
            }
        }

        //Contact Information
        private bool _editContactInfo = false;
        public bool EditContactInfo
        {
            get => _editContactInfo;
            set
            {
                _editContactInfo = value;
                OnPropertyChanged();
            }
        }
        private string _editContactInfoButtonText = "Edit";
        public string EditContactInfoButtonText
        {
            get => _editContactInfoButtonText;
            set
            {
                _editContactInfoButtonText = value;
                OnPropertyChanged();
            }
        }
        private string _editContactInfoButtonIcon = "Edit";
        public string EditContactInfoButtonIcon
        {
            get => _editContactInfoButtonIcon;
            set
            {
                _editContactInfoButtonIcon = value;
                OnPropertyChanged();
            }
        }
        private string _primaryPhoneNumber = "+639000000000";
        public string PrimaryPhoneNumber
        {
            get => _primaryPhoneNumber;
            set
            {
                _primaryPhoneNumber = value;
                OnPropertyChanged();
            }
        }
        private string _secondaryPhoneNumber = "+639000000001";
        public string SecondaryPhoneNumber
        {
            get => _secondaryPhoneNumber;
            set
            {
                _secondaryPhoneNumber = value;
                OnPropertyChanged();
            }
        }
        private string _telephoneNumber = "12321414";
        public string Telephone
        {
            get => _telephoneNumber;
            set
            {
                _telephoneNumber = value;
                OnPropertyChanged();
            }
        }
        private string _primaryEmail = "primary@email.com";
        public string PrimaryEmail
        {
            get => _primaryEmail;
            set
            {
                _primaryEmail = value;
                OnPropertyChanged();
            }
        }
        private string _mailingAddress = "1234 Street Name, City, State, Zip";
        public string MailingAddress
        {
            get => _mailingAddress;
            set
            {
                _mailingAddress = value;
                OnPropertyChanged();
            }
        }
        private string _secondaryEmail = "secondary@email.com";
        public string SecondaryEmail
        {
            get => _secondaryEmail;
            set
            {
                _secondaryEmail = value;
                OnPropertyChanged();
            }
        }
        private string _facebookLink = "https://facebook.com/user";
        public string FacebookLink
        {
            get => _facebookLink;
            set
            {
                _facebookLink = value;
                OnPropertyChanged();
            }
        }
        private string _linkedInLink = "https://linkedin.com/in/user";
        public string LinkedInLink
        {
            get => _linkedInLink;
            set
            {
                _linkedInLink = value;
                OnPropertyChanged();
            }
        }
        private string _websiteLink = "https://userwebsite.com";
        public string WebsiteLink
        {
            get => _websiteLink;
            set
            {
                _websiteLink = value;
                OnPropertyChanged();
            }
        }

        //Financial Information
        private bool _editFinancialInfo = false;
        public bool EditFinancialInfo
        {
            get => _editFinancialInfo;
            set
            {
                _editFinancialInfo = value;
                OnPropertyChanged();
            }
        }
        private string _editFinancialInfoButtonText = "Edit";
        public string EditFinancialInfoButtonText
        {
            get => _editFinancialInfoButtonText;
            set
            {
                _editFinancialInfoButtonText = value;
                OnPropertyChanged();
            }
        }
        private string _editFinancialInfoButtonIcon = "Edit";
        public string EditFinancialInfoButtonIcon
        {
            get => _editFinancialInfoButtonIcon;
            set
            {
                _editFinancialInfoButtonIcon = value;
                OnPropertyChanged();
            }
        }
        private string _taxIdentificationNumber = "123-456-789";
        public string TaxIdentificationNumber
        {
            get => _taxIdentificationNumber;
            set
            {
                _taxIdentificationNumber = value;
                OnPropertyChanged();
            }
        }
        private string _sssIdNumber = "123-456-789";
        public string SSSIdNumber
        {
            get => _sssIdNumber;
            set
            {
                _sssIdNumber = value;
                OnPropertyChanged();
            }
        }
        private string _philHealthIdNumber = "123-456-789";
        public string PhilHealthIdNumber
        {
            get => _philHealthIdNumber;
            set
            {
                _philHealthIdNumber = value;
                OnPropertyChanged();
            }
        }
        private string _pagIbigIdNumber = "123-456-789";
        public string PagIbigIdNumber
        {
            get => _pagIbigIdNumber;
            set
            {
                _pagIbigIdNumber = value;
                OnPropertyChanged();
            }
        }
        private string _bankName = "Bank Name";
        public string BankName
        {
            get => _bankName;
            set
            {
                _bankName = value;
                OnPropertyChanged();
            }
        }
        private string _bankAccountName = "ACCOUNT NAME";
        public string BankAccountName
        {
            get => _bankAccountName;
            set
            {
                _bankAccountName = value;
                OnPropertyChanged();
            }
        }
        private string _bankAccountNumber = "1234567890123456";
        public string BankAccountNumber
        {
            get => _bankAccountNumber;
            set
            {
                _bankAccountNumber = value;
                OnPropertyChanged();
            }
        }

        //Employment Information
        private string _companyId = "#000000";
        public string CompanyId
        {
            get => _companyId;
            private set
            {
                _companyId = value;
                OnPropertyChanged();
            }
        }
        private string _department = "Department";
        public string Department
        {
            get => _department;
            private set
            {
                _department = value;
                OnPropertyChanged();
            }
        }
        private string _employmentType = "Employment Type";
        public string EmploymentType
        {
            get => _employmentType;
            private set
            {
                _employmentType = value;
                OnPropertyChanged();
            }
        }
        private string _dateHired = "Date Hired";
        public string DateHired
        {
            get => _dateHired;
            private set
            {
                _dateHired = value;
                OnPropertyChanged();
            }
        }

        //Change password bindings
        public string Password { private get; set; } = string.Empty;

        public AccountPage_ViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            EditBasicInfoButton = new RelayCommand(EditBasicInfoButton_Click);
            EditContactInfoButton = new RelayCommand(EditContactInfoButton_Click);
            EditFinancialInfoButton = new RelayCommand(EditFinancialInfoButton_Click);
            LoadUserData();
        }

        //Commands
        public ICommand EditBasicInfoButton { get; set; }
        public ICommand EditContactInfoButton { get; set; }
        public ICommand EditFinancialInfoButton { get; set; }
        public ICommand ChangePassword { get; }

        public void EditFinancialInfoButton_Click(object? obj)
        {
            if (EditFinancialInfo)
            {
                EditFinancialInfoButtonText = "Edit";
                EditFinancialInfoButtonIcon = "Edit";
                EditFinancialInfo = false;
                SaveData();
            }
            else
            {
                EditFinancialInfoButtonText = "Save";
                EditFinancialInfoButtonIcon = "Cloud";
                EditFinancialInfo = true;
            }
        }

        private void EditBasicInfoButton_Click(object? obj)
        {
            if (EditBasicInfo)
            {
                EditBasicInfoButtonText = "Edit";
                EditBasicInfoButtonIcon = "Edit";
                EditBasicInfo = false;
                SaveData();
            }
            else
            {
                EditBasicInfoButtonText = "Save";
                EditBasicInfoButtonIcon = "Cloud";
                EditBasicInfo = true;
            }
        }

        public void EditContactInfoButton_Click(object? obj)
        {
            if (EditContactInfo)
            {
                EditContactInfoButtonText = "Edit";
                EditContactInfoButtonIcon = "Edit";
                EditContactInfo = false;
                SaveData();
            }
            else
            {
                EditContactInfoButtonText = "Save";
                EditContactInfoButtonIcon = "Cloud";
                EditContactInfo = true;
            }
        }

        private async void SaveData()
        { 
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "Employee,Admin,Contractor,Department");
            if (user != null)
            {
                if (user.Employee != null)
                {
                    var employee = await _unitOfWork.EmployeeRepository.GetAsync(x => x.UserId == user.UserId, includeProperties: "EmployeeAccountInfo");
                    if (employee != null && employee.EmployeeAccountInfo != null)
                    {
                        // Update employee data
                        user.Username = Username;
                        user.Email = PrimaryEmail;

                        await _unitOfWork.UserRepository.UpdateAsync(user);

                        employee.EmployeeAccountInfo.FirstName = FirstName;
                        employee.EmployeeAccountInfo.LastName = LastName;
                        employee.EmployeeAccountInfo.MiddleInitial = MiddleInitial;
                        employee.EmployeeAccountInfo.DateOfBirth = DateOnly.FromDateTime(DateOfBirth);
                        employee.EmployeeAccountInfo.Gender = SelectedGender;
                        employee.EmployeeAccountInfo.Nationality = SelectedNationality;

                        employee.EmployeeAccountInfo.PrimaryPhoneNumber = PrimaryPhoneNumber;
                        employee.EmployeeAccountInfo.SecondaryPhoneNumber = SecondaryPhoneNumber;
                        employee.EmployeeAccountInfo.Telephone = Telephone;
                        employee.EmployeeAccountInfo.MailingAddress = MailingAddress;
                        employee.EmployeeAccountInfo.SecondaryEmail = SecondaryEmail;
                        employee.EmployeeAccountInfo.FacebookUrl = FacebookLink;
                        employee.EmployeeAccountInfo.LinkedInUrl = LinkedInLink;
                        employee.EmployeeAccountInfo.WebsiteUrl = WebsiteLink;

                        employee.EmployeeAccountInfo.TaxIdNumber = TaxIdentificationNumber;
                        employee.EmployeeAccountInfo.SSSIdNumber = SSSIdNumber;
                        employee.EmployeeAccountInfo.PhilHealthIdNumber = PhilHealthIdNumber;
                        employee.EmployeeAccountInfo.PagIbigIdNumber = PagIbigIdNumber;
                        employee.EmployeeAccountInfo.BankName = BankName;
                        employee.EmployeeAccountInfo.BankAccountName = BankAccountName;
                        employee.EmployeeAccountInfo.BankAccountId = BankAccountNumber;

                        await _unitOfWork.Save();
                        LoadUserData();
                    }
                }
            }
        }
        private async void LoadUserData()
        {
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "Employee,Admin,Contractor,Department");
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
                        SelectedGender = employee.EmployeeAccountInfo.Gender;
                        SelectedNationality = employee.EmployeeAccountInfo.Nationality;

                        //Contact Info
                        PrimaryPhoneNumber = employee.EmployeeAccountInfo.PrimaryPhoneNumber;
                        SecondaryPhoneNumber = employee.EmployeeAccountInfo.SecondaryPhoneNumber;
                        Telephone = employee.EmployeeAccountInfo.Telephone;
                        PrimaryEmail = user.Email;
                        MailingAddress = employee.EmployeeAccountInfo.MailingAddress;
                        SecondaryEmail = employee.EmployeeAccountInfo.SecondaryEmail;
                        FacebookLink = employee.EmployeeAccountInfo.FacebookUrl;
                        LinkedInLink = employee.EmployeeAccountInfo.LinkedInUrl;
                        WebsiteLink = employee.EmployeeAccountInfo.WebsiteUrl;

                        //Financial Info
                        TaxIdentificationNumber = employee.EmployeeAccountInfo.TaxIdNumber;
                        SSSIdNumber = employee.EmployeeAccountInfo.SSSIdNumber;
                        PhilHealthIdNumber = employee.EmployeeAccountInfo.PhilHealthIdNumber;
                        PagIbigIdNumber = employee.EmployeeAccountInfo.PagIbigIdNumber;
                        BankName = employee.EmployeeAccountInfo.BankName;
                        BankAccountName = employee.EmployeeAccountInfo.BankAccountName;
                        BankAccountNumber = employee.EmployeeAccountInfo.BankAccountId;

                        //Employment Info
                        CompanyId = employee.EmployeeAccountInfo.CompanyId.ToString();
                        Department = user.Department.Name;
                        EmploymentType = employee.EmployeeAccountInfo.EmploymentType.ToString();
                        DateHired = employee.EmployeeAccountInfo.DateHired.ToString("yyyy-MM-dd");
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
