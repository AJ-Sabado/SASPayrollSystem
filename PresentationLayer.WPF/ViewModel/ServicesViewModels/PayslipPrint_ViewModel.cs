using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Payslips;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel.ServicesViewModels
{
    public class PayslipPrint_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPrintService _printService;

        public ICommand PrintPayslipCommand { get; }

        // Property to get the current template to display
        public UserControl CurrentTemplate
        {
            get
            {
                var templateType = _printService.GetCurrentPrintTemplate();
                return templateType switch
                {
                    PrintTemplateType.PayslipRegTemplate => new PayslipRegTemplate(),
                    PrintTemplateType.PayrollReport => new PayrollReport(),
                    _ => new PayslipRegTemplate()
                };
            }
        }

        // Property to get the window title based on current template
        public string WindowTitle
        {
            get
            {
                var templateType = _printService.GetCurrentPrintTemplate();
                return templateType switch
                {
                    PrintTemplateType.PayslipRegTemplate => "Employee Payslip",
                    PrintTemplateType.PayrollReport => "Payroll Report",
                    _ => "Print Preview"
                };
            }
        }

        // Property to get the header text based on current template
        public string HeaderText
        {
            get
            {
                var templateType = _printService.GetCurrentPrintTemplate();
                return templateType switch
                {
                    PrintTemplateType.PayslipRegTemplate => "Print Employee Payslip",
                    PrintTemplateType.PayrollReport => "Print Payroll Report",
                    _ => "Print Document"
                };
            }
        }

        public PayslipPrint_ViewModel(IPopUpService popUpService, IUnitOfWork unitOfWork, IPrintService printService)
        {
            _unitOfWork = unitOfWork;
            _popUpService = popUpService;
            _printService = printService;
            PrintPayslipCommand = new RelayCommand(PrintPayslip);
        }

        public void PrintPayslip(Object? item)
        {
            // Get the current template based on the template type
            var currentTemplate = CurrentTemplate;
            _printService.PrintUserControl(currentTemplate);

            // Close the popup after printing
            _popUpService.ClosePopup();
        }
    }
}