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
    /// Interaction logic for Question_View.xaml
    /// </summary>
    public partial class Question_View : Window
    {
        public string MessageText
        {
            get { return MessageTextBlock.Text; }
            set { MessageTextBlock.Text = value; }
        }

        public MyMessageBoxDialogResult Result { get; private set; } = MyMessageBoxDialogResult.Cancel;

        public Question_View()
        {
            InitializeComponent();

            btnCancel.Click += BtnCancel_Click;
            btnNo.Click += BtnNo_Click;
            btnYes.Click += BtnYes_Click;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MyMessageBoxDialogResult.Cancel;
            this.DialogResult = false;  // optional
            this.Close();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = MyMessageBoxDialogResult.No;
            this.DialogResult = false;  // optional
            this.Close();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MyMessageBoxDialogResult.Yes;
            this.DialogResult = true;   // optional
            this.Close();
        }

    }
}
