using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.WPF.Services
{
    public interface IPrintService
    {
        void PrintWindow(Window window);
        void PrintUserControl(UserControl control);
    }
}
