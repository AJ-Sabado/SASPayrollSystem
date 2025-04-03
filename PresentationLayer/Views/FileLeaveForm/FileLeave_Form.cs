using MaterialSkin;
using PresentationLayer.Presenters.FileLeave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Views.FileLeaveForm
{
    public partial class FileLeave_Form : Form, IFileLeave_Form
    {
        private readonly FileLeave_Presenter _presenter;
        public FileLeave_Form()
        {
            _presenter = new FileLeave_Presenter(this);

            InitializeComponent();

            MaterialSkinManager manager = MaterialSkinManager.Instance;
            manager.EnforceBackcolorOnAllComponents = false;
            manager.Theme = MaterialSkinManager.Themes.LIGHT;
            manager.ColorScheme = new ColorScheme(
                primary: Color.FromArgb(255, 215, 0),       // Gold
                darkPrimary: Color.FromArgb(218, 165, 32),  // Goldenrod
                lightPrimary: Color.FromArgb(255, 239, 184), // Light gold/yellow
                accent: Color.FromArgb(255, 204, 0),        // Deep golden yellow
                textShade: TextShade.BLACK
            );
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
