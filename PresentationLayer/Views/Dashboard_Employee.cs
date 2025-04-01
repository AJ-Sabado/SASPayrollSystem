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

        }



        //Action Events

        private void btnTimeIn_Click(object sender, EventArgs e)
        {
            if(EmployeeAttendanceForm == null)
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

        }

        private void btnJobDesk_Click(object sender, EventArgs e)
        {

        }

        private void btnAccount_Click(object sender, EventArgs e)
        {

        }

        
    }

}
