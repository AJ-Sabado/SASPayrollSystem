using System.Windows.Input;
using DomainLayer.Models.Department;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PopUpViewModel
{
    public class AddDepartment_ViewModel : Base_ViewModel
    {
        private readonly IAdminOperationsService _adminOperationsService;
        private readonly IPopUpService _popUpService;
        private readonly MyMessageBox _myMessageBox;

        private string _departmentName = string.Empty;
        public string DepartmentName
        {
            get => _departmentName;
            set
            {
                _departmentName = value.Trim();
                OnPropertyChanged(nameof(DepartmentName));
            }
        }

        public ICommand Add { get; set; }
        public ICommand Cancel { get; set; }

        public AddDepartment_ViewModel(IAdminOperationsService adminOperationsService, IPopUpService popUpService, MyMessageBox myMessageBox)
        {
            _adminOperationsService = adminOperationsService;
            _popUpService = popUpService;
            _myMessageBox = myMessageBox;

            Add = new RelayCommand(ExecuteAdd, _ => true);
            Cancel = new RelayCommand(ExecuteCancel, _ => true);
        }

        private void ExecuteCancel(object? obj)
        {
            _popUpService.ClosePopup();
        }

        private async void ExecuteAdd(object? obj)
        {
            if (string.IsNullOrEmpty(DepartmentName))
            {
                _myMessageBox.ShowDialog("Deparment name is empty.", MyMessageBoxType.Error);
                return;
            }
            var department = new DepartmentModel()
            {
                Name = DepartmentName
            };
            try
            {
                await _adminOperationsService.AddDepartment(department);
            }
            catch (Exception ex)
            {
                _myMessageBox.ShowDialog($"Error message: {ex.Message}");
            }


            _myMessageBox.ShowDialog($"Department '{DepartmentName}' was added!", MyMessageBoxType.Success);
            _popUpService.ClosePopup();
        }
    }
}
