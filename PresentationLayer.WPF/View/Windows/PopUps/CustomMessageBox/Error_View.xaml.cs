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

namespace PresentationLayer.WPF.View.Windows.PopUps.CustomMessageBox
{
    /// <summary>
    /// Interaction logic for Error_View.xaml
    /// </summary>
    public partial class Error_View : Window
    {
        public string MessageText
        {
            get { return MessageTextBlock.Text; }
            set { MessageTextBlock.Text = value; }
        }

        public ErrorResult Result { get; private set; } = ErrorResult.NONE;

        public Error_View()
        {
            InitializeComponent();

            btnOkay.Click += BtnOkay_Click;
        }

        private void BtnOkay_Click(object sender, RoutedEventArgs e)
        {
            Result = ErrorResult.OKAY;
            this.DialogResult = true; // Optional
            this.Close();
        }
    }

    public enum ErrorResult
    {
        OKAY,
        NONE
    }

}
