using DomainLayer.Models.User;
using MaterialSkin;
using MaterialSkin.Controls;
using PresentationLayer.Presenters;
using PresentationLayer.Views;
using ServicesLayer;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using System.Diagnostics.Contracts;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class Dashboard_Employee : Form, IDashboard_Employee
    {
        private IUnitOfWork _unitOfWork;

        private IUserModel _userModel;

        private Dashboard_EmployeePresenter _presenter;

        private SfButton lastFocusedButton;

        private EmployeeAttendance EmployeeAttendanceForm;

        private Dashboard_EmployeeUIStyles _dashboardUIStyles;

        public Dashboard_Employee(IUserModel currentUser, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userModel = currentUser;
            InitializeComponent();
            _presenter = new Dashboard_EmployeePresenter(_unitOfWork);
            _presenter.SetView(this);

            _dashboardUIStyles = new Dashboard_EmployeeUIStyles(this);
            _dashboardUIStyles.InitializeUI();
            _dashboardUIStyles.ApplyMaterialSkin(MaterialSkinManager.Instance);

        }

        public async void Dashboard_Employee_Load(object sender, EventArgs e)
        {

            this.MaximizeBox = false;

            this.MaximizeBox = false;
            btnDashboard.Focus();
            _presenter.LoadDashboard();

            /*btnDashboard.Focus();
            pnlDashboard.Show();
            pnlJobDeskRegular.Hide();
            pnlAccountsRegular.Hide();

            // Track the last focused button
            lastFocusedButton = btnDashboard;

            // Subscribe to LostFocus event
            btnDashboard.LostFocus += Btn_LostFocus;
            btnJobDesk.LostFocus += Btn_LostFocus;
            btnAccount.LostFocus += Btn_LostFocus;*/
        }



        //Action Events

        private void btnTimeIn_Click(object sender, EventArgs e)
        {
            EmployeeAttendanceForm = new EmployeeAttendance();
            EmployeeAttendanceForm.isTimeIn(true);
            EmployeeAttendanceForm.Text = "Time In";
            EmployeeAttendanceForm.ShowDialog();
        }

        private void btnTimeOut_Click(object sender, EventArgs e)
        {
            EmployeeAttendanceForm.isTimeIn(false);
            EmployeeAttendanceForm.Text = "Time Out";
            EmployeeAttendanceForm.ShowDialog();
            EmployeeAttendanceForm.Close();
        }

        public void ShowDashboard()
        {
            pnlDashboard.Show();
            pnlJobDeskRegular.Hide();
            pnlAccountsRegular.Hide();
        }

        public void ShowJobDesk()
        {
            pnlJobDeskRegular.Show();
            pnlDashboard.Hide();
            pnlAccountsRegular.Hide();
        }

        public void ShowAccounts()
        {
            pnlAccountsRegular.Show();
            pnlDashboard.Hide();
            pnlJobDeskRegular.Hide();
        }

        public void SetLastFocusedButton(SfButton button)
        {
            lastFocusedButton = button;
        }

        public void InitializeComponents()
        {
            throw new NotImplementedException();
        }

        public void BindAttendanceTableAsync()
        {
            throw new NotImplementedException();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            _presenter.ShowDashboard();
        }

        private void btnJobDesk_Click(object sender, EventArgs e)
        {
            _presenter.ShowJobDesk();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            _presenter.ShowAccounts();
        }
    }

}
