using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Payslips;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.ServicesViewModels
{
    public class PayslipPrint_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPrintService _printService;

        public ICommand PrintPayslipCommand { get; }
       
        public PayslipPrint_ViewModel(IPopUpService popUpService, IUnitOfWork unitOfWork, IPrintService printService) {
            _unitOfWork = unitOfWork;
            _popUpService = popUpService;
            _printService = printService;

            PrintPayslipCommand = new RelayCommand(PrintPayslip);
        }

        public void PrintPayslip(Object? item)
        {
            var payslip = new PayslipRegTemplate();
            _printService.PrintUserControl(payslip);

            // Using your popup service
            _popUpService.ClosePopup();
        }
    }
}
