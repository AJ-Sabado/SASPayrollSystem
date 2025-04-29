using PresentationLayer.WPF.View.Windows;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class RegJobDesk_ViewModel : Base_ViewModel
    {
        public ICommand FileLeaveCommand { get; }

        public ICommand AttendanceRequestCommand { get; }

        public RegJobDesk_ViewModel()
        {
            FileLeaveCommand = new RelayCommand(FileLeave);
            AttendanceRequestCommand = new RelayCommand(AttendanceRequest); // Initialize AttendanceRequestCommand  
        }

        // FUNCTIONS  

        // LEAVE FORM FUNCTIONS  
        private void FileLeave(object? obj)
        {
            var _fileLeaveDialog = new FileLeaveForm_View();
            _fileLeaveDialog.ShowDialog();
        }

        // ATTENDANCE REQUEST FUNCTIONS  
        private void AttendanceRequest(object? obj)
        {
            var _attendanceRequestDialog = new AttendanceRequest_View();
            _attendanceRequestDialog.ShowDialog();
        }
    }
}
