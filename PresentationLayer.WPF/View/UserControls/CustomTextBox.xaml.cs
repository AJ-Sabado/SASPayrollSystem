using System.Windows;
using System.Windows.Controls;

namespace SASPayrolSystemProject.View.UserControls
{
    public partial class CustomTextBox : UserControl
    {
        //Create bindanble property implementation

        public CustomTextBox()
        {
            
            InitializeComponent();
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
        DependencyProperty.Register("PlaceholderText", typeof(string), typeof(CustomTextBox),
            new PropertyMetadata(string.Empty, OnPlaceholderTextChanged));

        public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register("TextBoxText", typeof(string), typeof(CustomTextBox),
            new PropertyMetadata(string.Empty, OnTextBoxChanged));

        private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CustomTextBox;
            if (control != null)
            {
                control.tbTxtInput.Text = (string)e.NewValue;
            }
        }

        public string TextBoxText
        {
            get { return (string)GetValue(TextBoxProperty); }
            set { SetValue(TextBoxProperty, value); }
        }

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }



        private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CustomTextBox;
            if (control != null)
            {
                control.tblPlaceholderText.Text = (string)e.NewValue;
            }
        }


        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(tbTxtInput.Text))
            {
                tblPlaceholderText.Visibility = Visibility.Visible;
            }
            else
            {
                tblPlaceholderText.Visibility = Visibility.Collapsed;
            }
        }
    }
}
