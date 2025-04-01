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
    public partial class EmployeeAttendance : Form
    {
        bool TimeIn;
        public EmployeeAttendance()
        {
            InitializeComponent();

            UpdateButtonProperties();

        }

        public void isTimeIn(bool isTimein)
        {
            TimeIn = isTimein;
            UpdateButtonProperties();
        }

        public void UpdateButtonProperties()
        {
            RoundedElements.rounded(btnAttendanceLog, 10);

            if (TimeIn)
            {
                ButtonPropertiesTimeIn();
            }
            else if (!TimeIn)
            {
                ButtonPropertiesTimeOut();
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

        private void btnAttendanceLog_Click(object sender, EventArgs e)
        {
            if (TimeIn)
            {
                string timeInTime = "00:00 AM";
                MessageBox.Show("Time in successful! Time in time @" + timeInTime);
            }
            else if (!TimeIn)
            {
                string timeOutTime = "00:00 PM";
                string TotalHours = "256 hours";
                MessageBox.Show("Time out successful! Time out time @" + timeOutTime + ", Total hours worked: " + TotalHours);
            }
            this.Close();
        }
    }
}
