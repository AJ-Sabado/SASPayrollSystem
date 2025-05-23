
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationLayer.WPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for AdminSideMenuBar.xaml
    /// </summary>
    public partial class AdminSideMenuBar : UserControl
    {
        public AdminSideMenuBar()
        {
            InitializeComponent();
        }

        public ICommand DashboardCommand
        {
            get => (ICommand)GetValue(DashboardCommandProperty);
            set => SetValue(DashboardCommandProperty, value);
        }
        public static readonly DependencyProperty DashboardCommandProperty =
            DependencyProperty.Register(nameof(DashboardCommand), typeof(ICommand), typeof(AdminSideMenuBar));

        public ICommand PayrollCommand
        {
            get => (ICommand)GetValue(PayrollCommandProperty);
            set => SetValue(PayrollCommandProperty, value);
        }
        public static readonly DependencyProperty PayrollCommandProperty =
            DependencyProperty.Register(nameof(PayrollCommand), typeof(ICommand), typeof(AdminSideMenuBar));

        public ICommand EmployeeCommand
        {
            get => (ICommand)GetValue(EmployeeCommandProperty);
            set => SetValue(EmployeeCommandProperty, value);
        }
        public static readonly DependencyProperty EmployeeCommandProperty =
            DependencyProperty.Register(nameof(EmployeeCommand), typeof(ICommand), typeof(AdminSideMenuBar));

        public ICommand WorkLogsCommand
        {
            get => (ICommand)GetValue(WorkLogsCommandProperty);
            set => SetValue(WorkLogsCommandProperty, value);
        }
        public static readonly DependencyProperty WorkLogsCommandProperty =
            DependencyProperty.Register(nameof(WorkLogsCommand), typeof(ICommand), typeof(AdminSideMenuBar));

        public ICommand AdministrationCommand
        {
            get => (ICommand)GetValue(AdministrationCommandProperty);
            set => SetValue(AdministrationCommandProperty, value);
        }
        public static readonly DependencyProperty AdministrationCommandProperty =
            DependencyProperty.Register(nameof(AdministrationCommand), typeof(ICommand), typeof(AdminSideMenuBar));

        public ICommand AccountCommand
        {
            get => (ICommand)GetValue(AccountCommandProperty);
            set => SetValue(AccountCommandProperty, value);
        }
        public static readonly DependencyProperty AccountCommandProperty =
            DependencyProperty.Register(nameof(AccountCommand), typeof(ICommand), typeof(AdminSideMenuBar));

        public string SelectedMenu
        {
            get => (string)GetValue(SelectedMenuProperty);
            set => SetValue(SelectedMenuProperty, value);
        }
        public static readonly DependencyProperty SelectedMenuProperty =
            DependencyProperty.Register(nameof(SelectedMenu), typeof(string), typeof(AdminSideMenuBar));
    }
}
