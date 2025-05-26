using System.Windows;
using System.Windows.Input;
using DomainLayer.Enums;
using DomainLayer.Models.Holiday;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PopUpViewModel
{
    public class AddHolidays_ViewModel : Base_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IAdminOperationsService _adminOperationsService;
        private readonly MyMessageBox _messageBox;

        private string _holidayName = string.Empty;
        public string HolidayName
        {
            get => _holidayName;
            set
            {
                _holidayName = value.Trim();
                OnPropertyChanged(nameof(HolidayName));
            }
        }
        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        private HolidayType _selectedType = HolidayType.Regular;
        public HolidayType SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        public ICommand Save { get; set; }
        public ICommand Cancel { get; set; }

        public AddHolidays_ViewModel(IPopUpService popUpService, MyMessageBox messageBox, IAdminOperationsService adminOperationsService)
        {
            _popUpService = popUpService;
            _messageBox = messageBox;
            _adminOperationsService = adminOperationsService;

            Save = new RelayCommand(ExecuteSave, _ => true);
            Cancel = new RelayCommand(ExecuteCancel, _ => true);
        }

        private void ExecuteCancel(object? obj)
        {
            _popUpService.ClosePopup();
        }

        private async void ExecuteSave(object? obj)
        {
            if (string.IsNullOrEmpty(HolidayName))
            {
                _messageBox.ShowDialog("Holiday Name is empty!", MyMessageBoxType.Error);
                return;
            }

            var holiday = new HolidayModel()
            {
                Date = DateOnly.FromDateTime(SelectedDate),
                Description = HolidayName,
                Type = SelectedType
            };

            try
            {
                await _adminOperationsService.AddHoliday(holiday);
            }
            catch (Exception ex)
            {
                _messageBox.ShowDialog($"Error message: {ex.Message}", MyMessageBoxType.Error);
            }

            _messageBox.ShowDialog("Successfully added new holiday!", MyMessageBoxType.Success);
            _popUpService.ClosePopup();
        }
    }
}
