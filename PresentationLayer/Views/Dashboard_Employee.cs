using DomainLayer.Models.User;
using MaterialSkin;
using PresentationLayer.Views;
using ServicesLayer;
using Syncfusion.WinForms.Controls;

namespace PresentationLayer
{
    public partial class Dashboard_Employee : Form, IDashboard_Employee
    {
        //private IUnitOfWork _unitOfWork;  No need since sa presenter na ang db access

        //private IUserModel _userModel;    Also no need

        private SfButton lastFocusedButton;

        public event EventHandler Exit;

        private EmployeeAttendance EmployeeAttendanceForm;

        private Dashboard_EmployeeUIStyles _dashboardUIStyles;

        public Dashboard_Employee() //No need
        {
            //_unitOfWork = unitOfWork;
            //_userModel = currentUser;
            InitializeComponent();

            _dashboardUIStyles = new Dashboard_EmployeeUIStyles(this);
            _dashboardUIStyles.InitializeUI();
            _dashboardUIStyles.ApplyMaterialSkin(MaterialSkinManager.Instance);

            this.FormClosed += delegate
            {
                Exit?.Invoke(this, EventArgs.Empty);
            };
        }

        public async void Dashboard_Employee_Load(object sender, EventArgs e)
        {

            this.MaximizeBox = false;
            this.MaximizeBox = false;
            btnDashboard.Focus();

            // Track the last focused button
            lastFocusedButton = btnDashboard;

            lastFocusedButton.Focus();
            pnlDashboard.Show();
            pnlJobDeskRegular.Hide();
            pnlAccountsRegular.Hide();

            // Subscribe to LostFocus event
            btnDashboard.LostFocus += Btn_LostFocus;
            btnJobDesk.LostFocus += Btn_LostFocus;
            btnAccount.LostFocus += Btn_LostFocus;
        }

        private void Btn_LostFocus(object sender, EventArgs e)
        {
            if (!(btnDashboard.Focused || btnJobDesk.Focused))
            {
                lastFocusedButton.Focus();
            }
        }

        //Action Events

        private void btnTimeIn_Click(object sender, EventArgs e)
        {
            if (EmployeeAttendanceForm == null)
            {
                EmployeeAttendanceForm = new EmployeeAttendance();
            }
            EmployeeAttendanceForm.FormStatus(1);
            EmployeeAttendanceForm.Text = "Time In";
            EmployeeAttendanceForm.ShowDialog();
        }

        private void btnTimeOut_Click(object sender, EventArgs e)
        {
            EmployeeAttendanceForm.FormStatus(2);
            EmployeeAttendanceForm.Text = "Time Out";
            EmployeeAttendanceForm.ShowDialog();
            EmployeeAttendanceForm.Dispose();
            EmployeeAttendanceForm = null;
        }

        private void btnAttendanceRequest_Click(object sender, EventArgs e)
        {
            if (EmployeeAttendanceForm == null)
            {
                EmployeeAttendanceForm = new EmployeeAttendance();
            }
            EmployeeAttendanceForm.FormStatus(3);
            EmployeeAttendanceForm.Text = "Attendance Request";
            EmployeeAttendanceForm.ShowDialog();
            EmployeeAttendanceForm.Dispose();
            EmployeeAttendanceForm = null;
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
            lastFocusedButton = btnDashboard;
            lastFocusedButton.Focus();
            pnlDashboard.Show();
            pnlJobDeskRegular.Hide();
            pnlAccountsRegular.Hide();
        }

        private void btnJobDesk_Click(object sender, EventArgs e)
        {
            lastFocusedButton = btnJobDesk;
            lastFocusedButton.Focus();
            pnlJobDeskRegular.Show();
            pnlDashboard.Hide();
            pnlAccountsRegular.Hide();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            lastFocusedButton = btnAccount;
            lastFocusedButton.Focus();
            pnlAccountsRegular.Show();
            pnlDashboard.Hide();
            pnlJobDeskRegular.Hide();
        }
    }

}