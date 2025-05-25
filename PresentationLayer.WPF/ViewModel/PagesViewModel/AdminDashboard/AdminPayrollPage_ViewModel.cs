using System.Threading.Tasks;
using System.Windows.Input;
using DomainLayer.Models.ContractorPayslip;
using DomainLayer.Models.EmployeePayslip;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminPayrollPage_ViewModel : Base_ViewModel
    {
        private IAdminOperationsService _adminOperationsService;
        private MyMessageBox _messageBox;

        private string _quickSearchEmployeeFilter = string.Empty;
        public string QuickSearchEmployeeFilter
        {
            get => _quickSearchEmployeeFilter;
            set
            {
                _quickSearchEmployeeFilter = value.Trim();
                OnPropertyChanged(nameof(QuickSearchEmployeeFilter));
            }
        }

        //Grouped payslips

        private int _regularEmployeesCount = 0;
        public int RegularEmployeesCount
        {
            get => _regularEmployeesCount;
            set
            {
                _regularEmployeesCount = value;
                OnPropertyChanged(nameof(RegularEmployeesCount));
            }
        }

        private decimal _regularEmployeesPayrollTotalAmount = 0m;
        public decimal RegularEmployeesPayrollTotalAmount
        {
            get => _regularEmployeesPayrollTotalAmount;
            set
            {
                _regularEmployeesPayrollTotalAmount = value;
                OnPropertyChanged(nameof(RegularEmployeesPayrollTotalAmount));
                OnPropertyChanged(nameof(RegularEmployeesPayrollTotal));
            }
        }
        public string RegularEmployeesPayrollTotal
        {
            get => $"Php {RegularEmployeesPayrollTotalAmount:F2}";
        }

        public IList<EmployeePayslipModel> EmployeePayslips { get; private set; } = [];

        private string _quickSearchContractorFilter = string.Empty;
        public string QuickSearchContractorFilter
        {
            get => _quickSearchContractorFilter;
            set
            {
                _quickSearchContractorFilter = value.Trim();
                OnPropertyChanged(nameof(QuickSearchContractorFilter));
            }
        }

        //Grouped Contractor Payroll

        private int _contractorsCount = 0;
        public int ContractorsCount
        {
            get => _contractorsCount;
            set
            {
                _contractorsCount = value;
                OnPropertyChanged(nameof(ContractorsCount));
            }
        }
        private decimal _contractorsPayrollTotalAmount = 0;
        public decimal ContractorsPayrollTotalAmount
        {
            get => _contractorsPayrollTotalAmount;
            set
            {
                _contractorsPayrollTotalAmount = value;
                OnPropertyChanged(nameof(ContractorsPayrollTotalAmount));
                OnPropertyChanged(nameof(ContractorsPayrollTotal));
            }
        }
        public string ContractorsPayrollTotal
        {
            get => $"Php {ContractorsPayrollTotalAmount:F2}";
        }
        public IList<ContractorPayslipModel> ContractorPayslips { get; private set; } = [];

        public ICommand QuickSearchRegular { get; set; }
        public ICommand PrintRegularPayroll { get; set; }
        public ICommand PrintContractorPayroll { get; set; }
        public ICommand QuickSearchContractor { get; set; }

        public AdminPayrollPage_ViewModel(IAdminOperationsService adminOperationsService, MyMessageBox myMessageBox)
        {
            _adminOperationsService = adminOperationsService;
            _messageBox = myMessageBox;

            QuickSearchRegular = new RelayCommand(ExecuteQuickSearchRegular, _ => true);
            QuickSearchContractor = new RelayCommand(ExecuteQuickSearchContractor, _ => true);

            LoadDbData();
        }

        private async void LoadDbData()
        {
            await LoadContractorPayslips();
            await LoadEmployeesPayslips();
        }

        private Task LoadEmployeesPayslips()
        {
            EmployeePayslips = _adminOperationsService.EmployeePayslips;
            RegularEmployeesPayrollTotalAmount = EmployeePayslips.Sum(p => p.NetSalary);
            RegularEmployeesCount = _adminOperationsService.Employees.Count;
            OnPropertyChanged(nameof(EmployeePayslips));
            return Task.CompletedTask;
        }

        private Task LoadContractorPayslips()
        {
            ContractorPayslips = _adminOperationsService.ContractorPayslips;
            ContractorsPayrollTotalAmount = ContractorPayslips.Sum(p => p.NetPay);
            ContractorsCount = _adminOperationsService.Contractors.Count;
            OnPropertyChanged(nameof(ContractorPayslips));
            return Task.CompletedTask;
        }

        private void ExecuteQuickSearchContractor(object? obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteQuickSearchRegular(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
