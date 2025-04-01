using DomainLayer.Models.User;
using MaterialSkin;
using PresentationLayer.Views;
using ServicesLayer;
using Syncfusion.WinForms.Controls;

namespace PresentationLayer
{
    public partial class Dashboard_Employee : Form, IDashboard_Employee
    {
        private IUnitOfWork _unitOfWork;

        private IUserModel _userModel;

        private SfButton lastFocusedButton;

        private EmployeeAttendance EmployeeAttendanceForm;

        private Dashboard_EmployeeUIStyles _dashboardUIStyles;

        public Dashboard_Employee(IUserModel currentUser, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userModel = currentUser;
            InitializeComponent();

            _dashboardUIStyles = new Dashboard_EmployeeUIStyles(this);
            _dashboardUIStyles.InitializeUI();
            _dashboardUIStyles.ApplyMaterialSkin(MaterialSkinManager.Instance);

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
            
        }

        private void btnJobDesk_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            
        }
    }

}
