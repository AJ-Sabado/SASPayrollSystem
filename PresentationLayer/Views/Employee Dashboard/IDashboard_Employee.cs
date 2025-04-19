using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Views
{
    public interface IDashboard_Employee
    {
        void Hide();
        void Show();
        void Close();
        
        event EventHandler Exit;

        event EventHandler printPayslip;
        event EventHandler downloadPayslip;
    }
}
