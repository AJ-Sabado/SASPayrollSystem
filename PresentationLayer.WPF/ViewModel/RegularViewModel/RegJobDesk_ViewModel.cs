using PresentationLayer.WPF.View.Windows;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class RegJobDesk_ViewModel
    {
        public ICommand FileLeaveCommand { get; }

        public RegJobDesk_ViewModel()
        {
            FileLeaveCommand = new RelayCommand(FileLeave);
        }

        //FUNCTIONS

        //LEAVE FORM FUNCTIONS
        private void FileLeave(object? obj)
        {
            var _fileLeaveDialog = new FileLeaveForm_View();
            _fileLeaveDialog.ShowDialog();
        }
    }
}
