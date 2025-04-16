using PresentationLayer.Presenters.FileLeave;
using PresentationLayer.Views.Utility_Classes;

namespace PresentationLayer.Views.FileLeaveForm
{
    public partial class FileLeave_Form : Form, IFileLeave_Form
    {
        private readonly FileLeave_Presenter _presenter;
        public FileLeave_Form()
        {
            _presenter = new FileLeave_Presenter(this);

            InitializeComponent();

            MaterialSkinClass.initMaterialSkin();
        }

        public void initStartDateTime()
        {
            dteStartDate.Value = DateTime.Now.AddDays(1);
            dteStartDate.MinDateTime = DateTime.Now.AddDays(1);

            dteEndDate.MinDateTime = DateTime.Now.AddDays(2);
            dteEndDate.Value = DateTime.Now.AddDays(0);
        }


        public string EmployeeName
        {
            get => lblEmployeeName.Text;
            set => lblEmployeeName.Text = value;
        }

        public string EmployeeID
        {
            get => lblEmployeeID.Text;
            set => lblEmployeeID.Text = value;
        }

        public string Department
        {
            get => lblDepartment.Text;
            set => lblDepartment.Text = value;
        }

        public string LeaveType
        {
            get { return cbLeaveType.SelectedItem?.ToString(); }
        }

        public int Duration
        {
            get => slDuration.Value;
            set => slDuration.Value = value;
        }

        public DateTime StartDate
        {
            get { return (DateTime)dteStartDate.Value; }
        }

        public DateTime EndDate
        {
            get { return (DateTime)dteEndDate.Value; }
        }

        public string AttachmentFileName
        {
            get => tbAttachments.Text;
            set => tbAttachments.Text = value;
        }
        private void btnLeaveAttach_Click(object sender, EventArgs e)
        {
            _presenter.fetchDocument();
        }

        //ACTION EVENTS

        private void dteStartDate_ValueChanged(object sender, Syncfusion.WinForms.Input.Events.DateTimeValueChangedEventArgs e)
        {
            // Ensure that End Date is correctly updated when Start Date is changed
            dteEndDate.MinDateTime = (DateTime)dteStartDate.Value;

            // Update the EndDate based on the Duration value
            if (Duration > 0)
            {
                dteEndDate.Value = dteStartDate.Value?.AddDays(Duration - 1);
            }
            else
            {
                MessageBox.Show("Invalid duration. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteEndDate.Value = DateTime.Now.AddDays(1); // Default to a valid value
            }
        }

        private void slDuration_onValueChanged(object sender, int newValue)
        {

        }

        private void slDuration_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void slDuration_MouseCaptureChanged(object sender, EventArgs e)
        {
            // Ensure that End Date is correctly updated when Start Date is changed
            dteEndDate.MinDateTime = (DateTime)dteStartDate.Value;

            // Update the EndDate based on the Duration value
            if (Duration > 0)
            {
                dteEndDate.Value = dteStartDate.Value?.AddDays(Duration - 1);
            }
            else
            {
                MessageBox.Show("Invalid duration. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteEndDate.Value = DateTime.Now.AddDays(1);
                slDuration.Value = 1;
            }
        }
    }
}
