using PresentationLayer.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PresentationLayer.WPF.View.Payslips;
using ServicesLayer;
using PresentationLayer.WPF.View.Windows.PopUps.CustomMessageBox;

namespace PresentationLayer.WPF.ViewModel.ServicesViewModels
{
    public class PayslipPreview_ViewModel:Base_ViewModel
    {
        private readonly IPdfExportService _pdfExportService;
        private readonly IPopUpService _popUpService;
        private readonly IUnitOfWork _unitOfWork;

        public FrameworkElement PayslipVisual { get; set; }

        public ICommand PreviewPayslipCommand { get; }

        public PayslipPreview_ViewModel(IPdfExportService pdfExportService, IPopUpService popUpService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _popUpService = popUpService;
            _pdfExportService = pdfExportService;

            PreviewPayslipCommand = new RelayCommand(SaveAsPdf);
        }

        private void SaveAsPdf(Object? item)
        {
            var payslip = new PayslipRegTemplate();
            _pdfExportService.SaveUIElementAsPdf(payslip);
            _popUpService.ClosePopup();
        }
    }
}
