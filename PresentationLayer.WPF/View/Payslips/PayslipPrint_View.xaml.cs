using PresentationLayer.WPF.ViewModel.ServicesViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class PayslipPrint_View : Window, INotifyPropertyChanged
    {
        private PayslipPrint_ViewModel _viewModel;

        public PayslipPrint_View(PayslipPrint_ViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm;
            DataContext = _viewModel;

            // Update bindings when the window loads
            Loaded += (s, e) => {
                OnPropertyChanged(nameof(DataContext));
            };
        }

        private void btnPreviewPayslip_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}