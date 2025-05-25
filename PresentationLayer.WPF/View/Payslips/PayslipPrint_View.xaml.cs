using PresentationLayer.WPF.ViewModel.ServicesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationLayer.WPF.View.Payslips
{
    /// <summary>
    /// Interaction logic for PayslipPrint_View.xaml
    /// </summary>
    public partial class PayslipPrint_View : Window
    {
        public PayslipPrint_View(PayslipPrint_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void btnPreviewPayslip_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
