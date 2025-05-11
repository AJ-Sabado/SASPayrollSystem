using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Windows;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class RegJobDesk_ViewModel : Base_ViewModel
    {
        private IPopUpService _popUpService;

        public ICommand FileLeaveCommand { get; }

        public ICommand AttendanceRequestCommand { get; }

        public RegJobDesk_ViewModel(IPopUpService popUpService)
        {
            _popUpService = popUpService;
            FileLeaveCommand = new RelayCommand(FileLeave);
            AttendanceRequestCommand = new RelayCommand(AttendanceRequest); // Initialize AttendanceRequestCommand  
        }

        // FUNCTIONS  

        // LEAVE FORM FUNCTIONS  
        private void FileLeave(object? obj)
        {
            _popUpService.ShowPopUp<FileLeaveForm_View>();
        }

        // ATTENDANCE REQUEST FUNCTIONS  
        private void AttendanceRequest(object? obj)
        {
            _popUpService.ShowPopUp<AttendanceRequest_View>();
        }
    }
}
