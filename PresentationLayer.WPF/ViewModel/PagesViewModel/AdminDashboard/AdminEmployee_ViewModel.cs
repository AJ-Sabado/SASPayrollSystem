using DomainLayer.Models.User;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Windows.PopUps;
using ServicesLayer;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminEmployee_ViewModel:Base_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IAdminOperationsService _adminOperationsService;

        private string _employeeNameFilter = string.Empty;
        public string EmployeeNameFilter 
        { 
            get => _employeeNameFilter; 
            set
            {
                _employeeNameFilter = value;
                OnPropertyChanged(nameof(EmployeeNameFilter));
            }
        }
        public IList<UserModel> CurrentEmployees { get; private set; } = [];
        public IList<UserModel> EmployeeRequests { get; private set; } = [];

        public ICommand AddEmployeeCommand { get; set; }

        //CONSTRUCTOR
        public AdminEmployee_ViewModel(IPopUpService popUpService, IAdminOperationsService adminOperationsService)
        {
            _popUpService = popUpService;
            _adminOperationsService = adminOperationsService;

            AddEmployeeCommand = new RelayCommand(addEmployeeCommand);

            LoadFromDb();
        }
        //METHODS

        private async void LoadFromDb()
        {
            await FilterCurrentEmployees();
        }

        private async Task FilterCurrentEmployees()
        {
            await _adminOperationsService.RefreshEmployees();
            CurrentEmployees = _adminOperationsService.CurrentEmployees;
            EmployeeRequests = _adminOperationsService.EmployeeRequests;
            OnPropertyChanged(nameof(CurrentEmployees));
            OnPropertyChanged(nameof(EmployeeRequests));
        }

        private void addEmployeeCommand(object? obj)
        {
            _popUpService.ShowPopUp<EmployeeAdd_View>();
        }
    }
}
