using System.Windows;
using PresentationLayer.WPF.Services;

namespace PresentationLayer.WPF.View.Windows.PopUps.CustomMessageBox
{
    /// <summary>
    /// Interaction logic for Warning_View.xaml
    /// </summary>
    public partial class Warning_View : Window
    {
        public string MessageText
        {
            get { return MessageTextBlock.Text; }
            set { MessageTextBlock.Text = value; }
        }

        public MyMessageBoxDialogResult Result { get; private set; } = MyMessageBoxDialogResult.Cancel;  // Default to CANCEL

        public Warning_View()
        {
            InitializeComponent();

            // Wire button click events
            btnCancel.Click += BtnCancel_Click;
            btnOkay.Click += BtnOkay_Click;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MyMessageBoxDialogResult.Cancel;
            this.DialogResult = false;  // Optional: for standard dialog usage
            this.Close();
        }

        private void BtnOkay_Click(object sender, RoutedEventArgs e)
        {
            Result = MyMessageBoxDialogResult.Yes;
            this.DialogResult = true;   // Optional
            this.Close();
        }

    }
}
