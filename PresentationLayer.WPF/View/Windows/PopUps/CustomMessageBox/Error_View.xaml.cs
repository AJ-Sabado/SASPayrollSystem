using System.Windows;
using PresentationLayer.WPF.Services;

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

        public MyMessageBoxDialogResult Result { get; private set; } = MyMessageBoxDialogResult.None;

        public Error_View()
        {
            InitializeComponent();

            btnOkay.Click += BtnOkay_Click;
        }

        private void BtnOkay_Click(object sender, RoutedEventArgs e)
        {
            Result = MyMessageBoxDialogResult.Okay;
            this.DialogResult = true; // Optional
            this.Close();
        }
    }
}
