using System.Windows;

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

        public WarningResult Result { get; private set; } = WarningResult.CANCEL;  // Default to CANCEL

        public Warning_View()
        {
            InitializeComponent();

            // Wire button click events
            btnCancel.Click += BtnCancel_Click;
            btnOkay.Click += BtnOkay_Click;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = WarningResult.CANCEL;
            this.DialogResult = false;  // Optional: for standard dialog usage
            this.Close();
        }

        private void BtnOkay_Click(object sender, RoutedEventArgs e)
        {
            Result = WarningResult.YES;
            this.DialogResult = true;   // Optional
            this.Close();
        }
        
    }

    public enum WarningResult
    {
        YES,
        CANCEL
    }
}
