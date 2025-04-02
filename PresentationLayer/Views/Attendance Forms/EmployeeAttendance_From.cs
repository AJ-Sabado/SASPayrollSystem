using MaterialSkin;
using Syncfusion.WinForms.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Views
{
    public partial class EmployeeAttendance_Form : Form
    {
        int formStatus;
        public EmployeeAttendance_Form()
        {
            InitializeComponent();

            UpdateLogButtonProperties();
            AttendanceLogTimePickerProperties();
            buttonRequestProperties();

        }


        public void FormStatus(int stat)
        {
            formStatus = stat;

            if (formStatus == 1 || formStatus == 2)
            {
                pnlAttendanceLog.Show();
                pnlAttendanceRequest.Hide();
                setScreenSize();
                UpdateLogButtonProperties();
            }
            else if (formStatus == 3)
            {
                pnlAttendanceLog.Hide();
                pnlAttendanceRequest.Show();
                setScreenSize();
            }
        }

        public void UpdateLogButtonProperties()
        {
            RoundedElements.rounded(btnAttendanceLog, 10);

            if (formStatus == 1)
            {
                ButtonPropertiesTimeIn();
            }
            else if (formStatus == 2)
            {
                ButtonPropertiesTimeOut();
            }
        }

        public void setScreenSize()
        {
            if (formStatus == 1 || formStatus == 2)
            {
                this.Size = new Size(467, 537);
            }
            else if (formStatus == 3)
            {
                this.Size = new Size(559, 632);
            }
        }

        public void ButtonPropertiesTimeIn()
        {
            btnAttendanceLog.Text = "Time In";
            btnAttendanceLog.Style.BackColor = Color.FromArgb(192, 235, 166);
            btnAttendanceLog.Style.ForeColor = Color.FromArgb(255, 255, 255);
            btnAttendanceLog.Style.Border = new Pen(Color.FromArgb(192, 235, 166));

            btnAttendanceLog.Style.HoverBackColor = Color.FromArgb(52, 121, 40);
            btnAttendanceLog.Style.HoverForeColor = Color.FromArgb(255, 255, 255);
            btnAttendanceLog.Style.HoverBorder = new Pen(Color.FromArgb(52, 121, 40));

            btnAttendanceLog.Style.FocusedBackColor = Color.FromArgb(192, 235, 166);
            btnAttendanceLog.Style.FocusedForeColor = Color.FromArgb(255, 255, 255);
            btnAttendanceLog.Style.FocusedBorder = new Pen(Color.FromArgb(192, 235, 166));

            btnAttendanceLog.Style.PressedBackColor = Color.FromArgb(52, 121, 40);
            btnAttendanceLog.Style.PressedForeColor = Color.FromArgb(255, 255, 255);
            btnAttendanceLog.Style.PressedBorder = new Pen(Color.FromArgb(52, 121, 40));
        }

        public void ButtonPropertiesTimeOut()
        {
            btnAttendanceLog.Text = "Time Out";
            btnAttendanceLog.Style.BackColor = Color.FromArgb(216, 64, 64);
            btnAttendanceLog.Style.ForeColor = Color.FromArgb(255, 255, 255);
            btnAttendanceLog.Style.Border = new Pen(Color.FromArgb(216, 64, 64));

            btnAttendanceLog.Style.HoverBackColor = Color.FromArgb(163, 29, 29);
            btnAttendanceLog.Style.HoverForeColor = Color.FromArgb(255, 255, 255);
            btnAttendanceLog.Style.HoverBorder = new Pen(Color.FromArgb(163, 29, 29));

            btnAttendanceLog.Style.FocusedBackColor = Color.FromArgb(216, 64, 64);
            btnAttendanceLog.Style.FocusedForeColor = Color.FromArgb(255, 255, 255);
            btnAttendanceLog.Style.FocusedBorder = new Pen(Color.FromArgb(216, 64, 64));

            btnAttendanceLog.Style.PressedBackColor = Color.FromArgb(163, 29, 29);
            btnAttendanceLog.Style.PressedForeColor = Color.FromArgb(255, 255, 255);
            btnAttendanceLog.Style.PressedBorder = new Pen(Color.FromArgb(163, 29, 29));
        }

        public void buttonRequestProperties()
        {
            btnRequest.Style.BackColor = Color.FromArgb(252, 184, 49); ;
            btnRequest.Style.ForeColor = Color.White;
            btnRequest.Style.Border = new Pen(Color.FromArgb(252, 184, 49));

            btnRequest.Style.HoverBackColor = Color.FromArgb(247, 164, 0);
            btnRequest.Style.HoverForeColor = Color.White;
            btnRequest.Style.HoverBorder = new Pen(Color.FromArgb(247, 164, 0));

            btnRequest.Style.FocusedBackColor = Color.FromArgb(247, 164, 0);
            btnRequest.Style.FocusedForeColor = Color.White;
            btnRequest.Style.FocusedBorder = new Pen(Color.FromArgb(247, 164, 0));

            btnRequest.Style.PressedBackColor = Color.FromArgb(247, 164, 0);
            btnRequest.Style.PressedForeColor = Color.White;
            btnRequest.Style.PressedBorder = new Pen(Color.FromArgb(247, 164, 0));
            RoundedElements.rounded(btnRequest, 10);
        }

        public void AttendanceLogTimePickerProperties()
        {
            SfDateTimeEdit[] dtp = { dtpTimeIn, dtpTimeOut };

            foreach (SfDateTimeEdit d in dtp)
            {
                d.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
                d.DateTimePattern = Syncfusion.WinForms.Input.Enums.DateTimePattern.Custom;
                d.Format = "hh:mm:ss tt";
                d.ShowDropDown = false;
            }
        }

        //Action Events
        private void btnAttendanceLog_Click(object sender, EventArgs e)
        {
            if (formStatus == 1)
            {
                string timeInTime = "00:00 AM";
                MessageBox.Show("Time in successful! Time in time @" + timeInTime);
            }
            else if (formStatus == 2)
            {
                string timeOutTime = "00:00 PM";
                string TotalHours = "256 hours";
                MessageBox.Show("Time out successful! Time out time @" + timeOutTime + ", Total hours worked: " + TotalHours);
            }
            this.Close();
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdProof = new OpenFileDialog();

            fdProof.Title = "Attach Proof of Attendance";
            fdProof.Filter = "PDF Files|*.pdf|Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (fdProof.ShowDialog() == DialogResult.OK)
            {
                tbAttachFile.Text = Path.GetFileName(fdProof.FileName);
                byte[] fileData = File.ReadAllBytes(fdProof.FileName);
            }
        }
    }
}
