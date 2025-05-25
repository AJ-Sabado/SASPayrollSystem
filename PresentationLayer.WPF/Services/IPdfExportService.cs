using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.WPF.Services
{
   public interface IPdfExportService
    {
        void SaveUIElementAsPdf(FrameworkElement element);
    }
}
