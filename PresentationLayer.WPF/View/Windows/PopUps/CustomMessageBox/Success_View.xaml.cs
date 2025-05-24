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
using PresentationLayer.WPF.Services;

namespace PresentationLayer.WPF.View.Windows.PopUps.CustomMessageBox
{
    /// <summary>
    /// Interaction logic for Success_View.xaml
    /// </summary>
    public partial class Success_View : Window
    {
        public string MessageText
        {
            get { return MessageTextBlock.Text; }
            set { MessageTextBlock.Text = value; }
        }

        public MyMessageBoxDialogResult Result { get; private set; } = MyMessageBoxDialogResult.None;

        public Success_View()
        {
            InitializeComponent();

            btnOkay.Click += BtnOkay_Click;
        }

        private void BtnOkay_Click(object sender, RoutedEventArgs e)
        {
            Result = MyMessageBoxDialogResult.Okay;
            this.DialogResult = true;  // optional
            this.Close();
        }

    }
}
