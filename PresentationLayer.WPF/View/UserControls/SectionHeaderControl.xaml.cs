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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.WPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for SectionHeaderControl.xaml
    /// </summary>
    public partial class SectionHeaderControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(SectionHeaderControl));

        public static readonly DependencyProperty ButtonNameProperty =
            DependencyProperty.Register(nameof(ButtonName), typeof(string), typeof(SectionHeaderControl));

        public static readonly DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register(nameof(ButtonCommand), typeof(ICommand), typeof(SectionHeaderControl));

        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register(nameof(ButtonText), typeof(string), typeof(SectionHeaderControl));

        public static readonly DependencyProperty ButtonIconProperty =
            DependencyProperty.Register(nameof(ButtonIcon), typeof(string), typeof(SectionHeaderControl));

        public string ButtonIcon
        {
            get => (string)GetValue(ButtonIconProperty);
            set => SetValue(ButtonIconProperty, value);
        }

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        public string ButtonName
        {
            get => (string)GetValue(ButtonNameProperty);
            set => SetValue(ButtonNameProperty, value);
        }

        public SectionHeaderControl() => InitializeComponent();
    }
}
