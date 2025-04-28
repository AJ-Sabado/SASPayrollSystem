using PresentationLayer.WPF.Helpers;
using PresentationLayer.WPF.ViewModel;

namespace PresentationLayer.WPF.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private Base_ViewModel _currentView;
        private readonly Func<Type, Base_ViewModel> _viewModelFactory;

        public Base_ViewModel CurrentView 
        { 
            get => _currentView; 
            private set 
            {
                _currentView = value;
                OnPropertyChanged();
            } 
        }

        public NavigationService(Func<Type, Base_ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : Base_ViewModel
        {
            var vm = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = vm;
        }
    }
}
