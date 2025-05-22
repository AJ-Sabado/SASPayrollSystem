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
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "AccountInfo");
            //var employee = await _unitOfWork.EmployeeRepository.GetAsync(x => x.UserId == user.UserId, includeProperties: "EmployeeAccountInfo");
            if (user != null && user.AccountInfo != null)
            {
                // Update employee data
                user.Username = Username;
                user.Email = PrimaryEmail;

                await _unitOfWork.UserRepository.UpdateAsync(user);

                user.AccountInfo.FirstName = FirstName;
                user.AccountInfo.LastName = LastName;
                user.AccountInfo.MiddleInitial = MiddleInitial;
                user.AccountInfo.DateOfBirth = DateOnly.FromDateTime(DateOfBirth);
                user.AccountInfo.Gender = SelectedGender;
                user.AccountInfo.Nationality = SelectedNationality;

                user.AccountInfo.PrimaryPhoneNumber = PrimaryPhoneNumber;
                user.AccountInfo.SecondaryPhoneNumber = SecondaryPhoneNumber;
                user.AccountInfo.Telephone = Telephone;
                user.AccountInfo.MailingAddress = MailingAddress;
                user.AccountInfo.SecondaryEmail = SecondaryEmail;
                user.AccountInfo.FacebookUrl = FacebookLink;
                user.AccountInfo.LinkedInUrl = LinkedInLink;
                user.AccountInfo.WebsiteUrl = WebsiteLink;

                user.AccountInfo.TaxIdNumber = TaxIdentificationNumber;
                user.AccountInfo.SSSIdNumber = SSSIdNumber;
                user.AccountInfo.PhilHealthIdNumber = PhilHealthIdNumber;
                user.AccountInfo.PagIbigIdNumber = PagIbigIdNumber;
                user.AccountInfo.BankName = BankName;
                user.AccountInfo.BankAccountName = BankAccountName;
                user.AccountInfo.BankAccountId = BankAccountNumber;

                await _unitOfWork.Save();
                LoadUserData();
            }
        }
        private async void LoadUserData()
        {
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "AccountInfo,Department");
            if (user != null && user.AccountInfo != null)
            {

                //Basic Info
                FullName = user.AccountInfo.FullName;
                Role = user.AccountInfo.Role;
                FirstName = user.AccountInfo.FirstName;
                LastName = user.AccountInfo.LastName;
                MiddleInitial = user.AccountInfo.MiddleInitial;
                Username = user.Username;
                DateOfBirth = user.AccountInfo.DateOfBirth.ToDateTime(TimeOnly.MinValue);
                SelectedGender = user.AccountInfo.Gender;
                SelectedNationality = user.AccountInfo.Nationality;

                //Contact Info
                PrimaryPhoneNumber = user.AccountInfo.PrimaryPhoneNumber;
                SecondaryPhoneNumber = user.AccountInfo.SecondaryPhoneNumber;
                Telephone = user.AccountInfo.Telephone;
                PrimaryEmail = user.Email != null ? user.Email : string.Empty;
                MailingAddress = user.AccountInfo.MailingAddress;
                SecondaryEmail = user.AccountInfo.SecondaryEmail;
                FacebookLink = user.AccountInfo.FacebookUrl;
                LinkedInLink = user.AccountInfo.LinkedInUrl;
                WebsiteLink = user.AccountInfo.WebsiteUrl;

                //Financial Info
                TaxIdentificationNumber = user.AccountInfo.TaxIdNumber;
                SSSIdNumber = user.AccountInfo.SSSIdNumber;
                PhilHealthIdNumber = user.AccountInfo.PhilHealthIdNumber;
                PagIbigIdNumber = user.AccountInfo.PagIbigIdNumber;
                BankName = user.AccountInfo.BankName;
                BankAccountName = user.AccountInfo.BankAccountName;
                BankAccountNumber = user.AccountInfo.BankAccountId;

                //Employment Info
                CompanyId = user.AccountInfo.CompanyId.ToString();
                Department = user.Department.Name;
                EmploymentType = user.AccountInfo.EmploymentType.ToString();
                DateHired = user.AccountInfo.DateHired.ToString("yyyy-MM-dd");
            }
        }
    }
}
