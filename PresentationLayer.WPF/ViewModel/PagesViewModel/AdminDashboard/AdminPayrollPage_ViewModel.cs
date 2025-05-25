using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Payslips;
using PresentationLayer.WPF.Helpers;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminPayrollPage_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IPrintService _printService;
        private readonly IUnitOfWork _unitOfWork;

        public ICommand PrintAllPayrollCommand { get; set; }

        public AdminPayrollPage_ViewModel(IUnitOfWork unitOfWork, IPopUpService popUpService, IPrintService printService)
        {
            _popUpService = popUpService;
            _printService = printService;
            _unitOfWork = unitOfWork;

            PrintAllPayrollCommand = new RelayCommand(PrintAllPayroll);
        }

        private void PrintAllPayroll(object? obj)
        {
            // Set the template type to PayrollReport before opening the print preview
            _printService.SetCurrentPrintTemplate(PrintTemplateType.PayrollReport);
            _popUpService.ShowPopUp<PayslipPrint_View>();
        }
    }
}