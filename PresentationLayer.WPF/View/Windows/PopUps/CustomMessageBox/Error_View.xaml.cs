using System.Windows;

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
