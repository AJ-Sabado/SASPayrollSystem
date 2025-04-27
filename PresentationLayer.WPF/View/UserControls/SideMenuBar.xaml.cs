    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    namespace PresentationLayer.WPF.View.UserControls
    {
        public partial class SideMenuBar : UserControl
        {
            public SideMenuBar()
            {
                InitializeComponent();
            }

            public ICommand DashboardCommand
            {
                get => (ICommand)GetValue(DashboardCommandProperty);
                set => SetValue(DashboardCommandProperty, value);
            }
            public static readonly DependencyProperty DashboardCommandProperty =
                DependencyProperty.Register(nameof(DashboardCommand), typeof(ICommand), typeof(SideMenuBar));

            public ICommand JobDeskCommand
            {
                get => (ICommand)GetValue(JobDeskCommandProperty);
                set => SetValue(JobDeskCommandProperty, value);
            }
            public static readonly DependencyProperty JobDeskCommandProperty =
                DependencyProperty.Register(nameof(JobDeskCommand), typeof(ICommand), typeof(SideMenuBar));

            public ICommand AccountsCommand
            {
                get => (ICommand)GetValue(AccountsCommandProperty);
                set => SetValue(AccountsCommandProperty, value);
            }
            public static readonly DependencyProperty AccountsCommandProperty =
                DependencyProperty.Register(nameof(AccountsCommand), typeof(ICommand), typeof(SideMenuBar));

            public string SelectedMenu
            {
                get => (string)GetValue(SelectedMenuProperty);
                set => SetValue(SelectedMenuProperty, value);
            }
            public static readonly DependencyProperty SelectedMenuProperty =
                DependencyProperty.Register(nameof(SelectedMenu), typeof(string), typeof(SideMenuBar));
        }
    }
