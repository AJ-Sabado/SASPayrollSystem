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

namespace PresentationLayer.WPF.View.Pages
{
    /// <summary>
    /// Interaction logic for EmployeeDahboard_View.xaml
    /// </summary>
    public partial class EmployeeDahboard_View : Window
    {
        public EmployeeDahboard_View()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
