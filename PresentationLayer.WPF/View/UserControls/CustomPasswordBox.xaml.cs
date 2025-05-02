using PresentationLayer.WPF.ViewModel.RegularViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static MaterialDesignThemes.Wpf.Theme;

namespace SASPayrolSystemProject.View.UserControls
{
    public partial class CustomPasswordBox : UserControl
    {
        public CustomPasswordBox()
        {
            IsPasswordVisible = false;
            InitializeComponent();
        }

        private bool _isPasswordVisible;
        private bool _isPlaceholderVisible;


        public bool IsPasswordVisible
        {
            get { return _isPasswordVisible; }
            set { _isPasswordVisible = value; }
        }

        public bool IsPlaceholderVisible
        {
            get { return _isPlaceholderVisible; }
            set
            {
                _isPlaceholderVisible = value;
                UpdatePlaceholderVisibility();
            }
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
                DependencyProperty.Register("PlaceholderText", typeof(string), typeof(CustomPasswordBox), new PropertyMetadata("Password", OnPlaceholderTextChanged));
       

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CustomPasswordBox;
            if (control != null)
            {
                control.tblPasswordPlaceholderText.Text = e.NewValue as string;
            }
        }

        private void UpdatePlaceholderVisibility()
        {
            if (_isPlaceholderVisible)
            {
                tblPasswordPlaceholderText.Visibility = Visibility.Visible;
            }
            else
            {
                tblPasswordPlaceholderText.Visibility = Visibility.Collapsed;
            }
        }

        private void pbPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!IsPasswordVisible)
            {
                IsPlaceholderVisible = string.IsNullOrEmpty(pbPasswordBox.Password);
            }
            SendPasswordToViewModel(pbPasswordBox.Password);
        }

        private void tbVisiblePassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsPasswordVisible)
            {
                IsPlaceholderVisible = string.IsNullOrEmpty(tbVisiblePassword.Text);
            }
            SendPasswordToViewModel(tbVisiblePassword.Text);
        }

        private void SendPasswordToViewModel(string password)
        {
            if (this.DataContext != null)
            {
                ((LoginPage_ViewModel)this.DataContext).PasswordSignIn = password;
            }
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsPasswordVisible = !IsPasswordVisible;

                if (IsPasswordVisible)
                {
                    pbPasswordBox.Visibility = Visibility.Hidden;
                    tbVisiblePassword.Visibility = Visibility.Visible;
                    tbVisiblePassword.Text = pbPasswordBox.Password;

                    btnShowPassIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/eye_show.png"));
                }
                else
                {
                    pbPasswordBox.Visibility = Visibility.Visible;
                    tbVisiblePassword.Visibility = Visibility.Hidden;
                    pbPasswordBox.Password = tbVisiblePassword.Text;

                    btnShowPassIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/eye_hidden.png"));
                }

                if (IsPasswordVisible)
                {
                    IsPlaceholderVisible = string.IsNullOrEmpty(tbVisiblePassword.Text);
                }
                else
                {
                    IsPlaceholderVisible = string.IsNullOrEmpty(pbPasswordBox.Password);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
