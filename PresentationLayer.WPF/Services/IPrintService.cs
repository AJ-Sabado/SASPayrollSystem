using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.WPF.Services
{
    public enum PrintTemplateType
    {
        PayslipRegTemplate,
        PayrollReport
    }

    public interface IPrintService
    {
        void PrintWindow(Window window);
        void PrintUserControl(UserControl control);
        void SetCurrentPrintTemplate(PrintTemplateType templateType);
        PrintTemplateType GetCurrentPrintTemplate();
    }
}