namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminWorkforce_ViewModel:Base_ViewModel
    {
        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                // Clamp the date to today if it's in the future
                if (value > DateTime.Today)
                {
                    _selectedDate = DateTime.Today;
                }
                else
                {
                    _selectedDate = value;
                }
                OnPropertyChanged();
            }
        }


    }
}
