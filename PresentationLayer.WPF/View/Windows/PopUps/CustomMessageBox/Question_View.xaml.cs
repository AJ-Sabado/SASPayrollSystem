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
    /// Interaction logic for Question_View.xaml
    /// </summary>
    public partial class Question_View : Window
    {
        public string MessageText
        {
            get { return MessageTextBlock.Text; }
            set { MessageTextBlock.Text = value; }
        }

        public QuestionResult Result { get; private set; } = QuestionResult.CANCEL; // Default

        public Question_View()
        {
            InitializeComponent();

            btnCancel.Click += BtnCancel_Click;
            btnNo.Click += BtnNo_Click;
            btnYes.Click += BtnYes_Click;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = QuestionResult.CANCEL;
            this.DialogResult = false;  // optional
            this.Close();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = QuestionResult.NO;
            this.DialogResult = false;  // optional
            this.Close();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = QuestionResult.YES;
            this.DialogResult = true;   // optional
            this.Close();
        }

    }

    public enum QuestionResult
    {
        YES,
        NO,
        CANCEL
    }

}
