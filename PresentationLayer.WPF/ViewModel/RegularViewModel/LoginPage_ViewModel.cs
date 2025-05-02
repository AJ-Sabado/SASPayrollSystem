using PresentationLayer.WPF.Services;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class LoginPage_ViewModel : Base_ViewModel
    {
        private INavigationService _navigationService;
        private IUnitOfWork _unitOfWork;

        public INavigationService Navigation 
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand Login { get; set; }

        public LoginPage_ViewModel(INavigationService navigationService, IUnitOfWork unitOfWork)
        {
            Navigation = navigationService;
            _unitOfWork = unitOfWork;

            //Login = new RelayCommand(execute: o_object => AuthenticateUser, canExecute: o_object => true);

            _unitOfWork.InitialSeeding();
        }

    }
}
