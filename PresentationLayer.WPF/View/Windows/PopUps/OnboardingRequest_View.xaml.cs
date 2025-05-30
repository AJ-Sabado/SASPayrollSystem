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
using PresentationLayer.WPF.ViewModel.PopUpViewModel;

namespace PresentationLayer.WPF.View.Windows.PopUps
{
    /// <summary>
    /// Interaction logic for OnboardingRequest_View.xaml
    /// </summary>
    public partial class OnboardingRequest_View : Window
    {
        public OnboardingRequest_View(OnboardingRequest_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }
    }
}
