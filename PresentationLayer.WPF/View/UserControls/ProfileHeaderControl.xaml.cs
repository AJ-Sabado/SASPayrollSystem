using System.Windows.Controls;
using System.Windows;

namespace PresentationLayer.WPF.View.UserControls {
    public partial class ProfileHeaderControl : UserControl
    {

        public ProfileHeaderControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ProfileNameProperty =
            DependencyProperty.Register(nameof(ProfileName), typeof(string), typeof(ProfileHeaderControl));

        public static readonly DependencyProperty ProfilePositionProperty =
            DependencyProperty.Register(nameof(ProfilePosition), typeof(string), typeof(ProfileHeaderControl));

        public static readonly DependencyProperty ProfileImageProperty =
            DependencyProperty.Register(nameof(ProfileImage), typeof(string), typeof(ProfileHeaderControl));

        public string ProfileName
        {
            get => (string)GetValue(ProfileNameProperty);
            set => SetValue(ProfileNameProperty, value);
        }

        public string ProfilePosition
        {
            get => (string)GetValue(ProfilePositionProperty);
            set => SetValue(ProfilePositionProperty, value);
        }

        public string ProfileImage
        {
            get => (string)GetValue(ProfileImageProperty);
            set => SetValue(ProfileImageProperty, value);
        }
    }
    
}