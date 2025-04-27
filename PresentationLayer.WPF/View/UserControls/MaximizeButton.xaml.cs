using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.WPF.View.UserControls
{
    public partial class MaximizeButton : UserControl
    {
        public event RoutedEventHandler MaximizeButtonClick;
        public MaximizeButton()
        {
            InitializeComponent();
        }

        private void MaximizeBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MaximizeButtonClick?.Invoke(this, e);
        }
    }
}
