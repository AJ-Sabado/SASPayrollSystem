using DomainLayer.Enums;
using MaterialSkin;
using PresentationLayer.Views;
using PresentationLayer.Views.Attendance_Forms;
using Syncfusion.WinForms.Controls;

namespace PresentationLayer
{
    public partial class Dashboard_Employee : Form, IDashboard_Employee
    {

        private SfButton lastFocusedButton;

        public event EventHandler Exit;

        private EmployeeAttendance_Form? EmployeeAttendanceForm;

        private Dashboard_EmployeeUIStyles _dashboardUIStyles;

        private IEmployeeAttendance_View _employeeAttendance;

        public Dashboard_Employee()
        {
            InitializeComponent();

            _dashboardUIStyles = new Dashboard_EmployeeUIStyles(this);
            _dashboardUIStyles.InitializeUI();
            _dashboardUIStyles.ApplyMaterialSkin(MaterialSkinManager.Instance);

            _employeeAttendance = new EmployeeAttendance_Form();

            this.FormClosed += delegate
            {
                Exit?.Invoke(this, EventArgs.Empty);
            };
        }

        public void Dashboard_Employee_Load(object sender, EventArgs e)
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

            //Updating Attendance Button Status
            updateAttendanceButtonStatus();
        }

        private void Btn_LostFocus(object sender, EventArgs e)
        {
            if (!(btnDashboard.Focused || btnJobDesk.Focused))
            {
                lastFocusedButton.Focus();
            }
        }

        private void updateAttendanceButtonStatus()
        {
            if (_employeeAttendance.isWorking)
            {
                btnTimeIn.Enabled = false;
                btnTimeOut.Enabled = true;
            }
            else
            {
                btnTimeIn.Enabled = true;
                btnTimeOut.Enabled = false;
            }
        }

        //Action Events
        private void btnTimeIn_Click(object sender, EventArgs e)
        {
            if (EmployeeAttendanceForm == null)
            {
                EmployeeAttendanceForm = (EmployeeAttendance_Form)_employeeAttendance;
            }

            EmployeeAttendanceForm.ShowDialog();
            updateAttendanceButtonStatus();
        }

        private void btnTimeOut_Click(object sender, EventArgs e)
        {
            if (EmployeeAttendanceForm == null)
            {
                EmployeeAttendanceForm = (EmployeeAttendance_Form)_employeeAttendance;
            }

            EmployeeAttendanceForm.ShowDialog();
            updateAttendanceButtonStatus();
        }

        private void btnAttendanceRequest_Click(object sender, EventArgs e)
        {
            if (EmployeeAttendanceForm == null)
            {
                EmployeeAttendanceForm = new EmployeeAttendance_Form();
            }
            EmployeeAttendanceForm.FormStatus(3);
            EmployeeAttendanceForm.Text = "Attendance Request";
            EmployeeAttendanceForm.ShowDialog();

            EmployeeAttendanceForm.Dispose();
            EmployeeAttendanceForm = null;
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

        private void btnEditAccountInfo_Click(object sender, EventArgs e)
        {
            var form = new EditPersonalInformation_View();
            form.ShowDialog();
            form.Close();
            form.Dispose();
        }
    }

}