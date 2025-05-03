using DomainLayer.Models.User;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardRegular
{
    public class RegDashboard_ViewModel : Base_ViewModel
    {
        private IUnitOfWork _unitOfWork;

        //Binded properties
        //TO DO - Add other bindings and adjust backend model to fit View and ViewModel
        private string _employeeFirstName = "First Name";
        public string EmployeeFirstName 
        {
            get => _employeeFirstName;
            private set
            { 
                _employeeFirstName = value;
                OnPropertyChanged();
            }
        }

        private string _employeeCompanyId = "Company ID";
        public string EmployeeCompanyId
        {
            get => _employeeCompanyId;
            private set
            {
                _employeeCompanyId = value;
                OnPropertyChanged();
            }
        }

        private string _employeeRole = "Role";
        public string EmployeeRole
        {
            get => _employeeRole;
            private set
            {
                _employeeRole = value;
                OnPropertyChanged();
            }
        }

        public RegDashboard_ViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            LoadUserData();
        }

        private async void LoadUserData()
        {
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(x => x.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "User,EmployeeAccountInfo");
            if (employee != null)
            {
                if (employee.EmployeeAccountInfo != null)
                { 
                    EmployeeFirstName = employee.EmployeeAccountInfo.FirstName;
                    EmployeeCompanyId = employee.EmployeeAccountInfo.CompanyId;
                    EmployeeRole = employee.EmployeeAccountInfo.Role;
                }
            }
        }
    }
}
