using PresentationLayer.WPF.View.Windows.PopUps;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminEmployee_ViewModel:Base_ViewModel
    {
        public ICommand AddEmployeeCommand { get; set; }

        //CONSTRUCTOR
        public AdminEmployee_ViewModel()
        {
            AddEmployeeCommand = new RelayCommand(addEmployeeCommand);
        }

        //METHODS
        private void addEmployeeCommand(object obj)
        {
            EmployeeAdd_View addEmployee = new EmployeeAdd_View();
            addEmployee.ShowDialog();
        }
    }
}
