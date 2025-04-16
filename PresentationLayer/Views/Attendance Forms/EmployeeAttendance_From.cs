using PresentationLayer.Presenters.Employee_Attendance;
using PresentationLayer.Views.Attendance_Forms;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.Input;


namespace PresentationLayer.Views
{
    public partial class EmployeeAttendance_Form : Form, IEmployeeAttendance_View
    {
        private EmployeeAttendance_Presenter _presenter;
        private int _formStatus;
        public Action<bool> OnAttendanceLogged { get; set; } = _ => { };
        public bool isWorking { get; set; } = false;


        public EmployeeAttendance_Form()
        {
            InitializeComponent();
            _presenter = new EmployeeAttendance_Presenter(this);
            InitializeControls();
            _presenter.SetFormStatus(1);
        }

        private void InitializeControls()
        {
            ConfigureTimePickers();
            ConfigureButtonStyles(btnRequest, Color.FromArgb(252, 184, 49), Color.FromArgb(247, 165, 2));
        }

        public int formStatus => _formStatus;

        public void FormStatus(int stat)
        {
            _formStatus = stat;

            bool isLogVisible = _formStatus == 1 || _formStatus == 2;
            pnlAttendanceLog.Visible = isLogVisible;
            pnlAttendanceRequest.Visible = !isLogVisible;

            SetScreenSize();
        }


        public void UpdateLogButtonProperties(string buttonText)
        {
            btnAttendanceLog.Text = buttonText;

            switch (buttonText)
            {
                case "Time In":
                    ApplyLogButtonStyle(Color.FromArgb(192, 235, 166), Color.FromArgb(52, 121, 40));
                    break;
                case "Time Out":
                    ApplyLogButtonStyle(Color.FromArgb(216, 64, 64), Color.FromArgb(163, 29, 29));
                    break;
            }
        }

        private void SetScreenSize()
        {
            this.Size = _formStatus switch
            {
                1 or 2 => new Size(467, 537),
                3 => new Size(559, 632),
                _ => this.Size
            };
        }

        public void ShowMessage(string message) => MessageBox.Show(message);
        public void CloseForm() => this.Close();

        private void ApplyLogButtonStyle(Color baseColor, Color hoverColor)
        {
            var style = btnAttendanceLog.Style;
            style.BackColor = baseColor;
            style.ForeColor = Color.White;
            style.Border = new Pen(baseColor);

            style.HoverBackColor = hoverColor;
            style.HoverForeColor = Color.White;
            style.HoverBorder = new Pen(hoverColor);

            style.FocusedBackColor = baseColor;
            style.FocusedForeColor = Color.White;
            style.FocusedBorder = new Pen(baseColor);

            style.PressedBackColor = hoverColor;
            style.PressedForeColor = Color.White;
            style.PressedBorder = new Pen(hoverColor);

            RoundedElements.rounded(btnAttendanceLog, 10);
        }

        private void ConfigureButtonStyles(SfButton button, Color baseColor, Color hoverColor)
        {
            var style = button.Style;
            style.BackColor = baseColor;
            style.ForeColor = Color.White;
            style.Border = new Pen(baseColor);

            style.HoverBackColor = hoverColor;
            style.HoverForeColor = Color.White;
            style.HoverBorder = new Pen(hoverColor);

            style.FocusedBackColor = hoverColor;
            style.FocusedForeColor = Color.White;
            style.FocusedBorder = new Pen(hoverColor);

            style.PressedBackColor = hoverColor;
            style.PressedForeColor = Color.White;
            style.PressedBorder = new Pen(hoverColor);

            RoundedElements.rounded(button, 10);
        }

        private void ConfigureTimePickers()
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

        

        // Event handlers
        private void btnAttendanceLog_Click(object sender, EventArgs e) => _presenter.HandleAttendanceLogClick();

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            _presenter.HandleAttachFileClick();
        }

        public string ShowFileDialog(string filter, string title)
        {
            using OpenFileDialog fdProof = new OpenFileDialog
            {
                Title = title,
                Filter = filter
            };

            return fdProof.ShowDialog() == DialogResult.OK ? fdProof.FileName : null;
        }

        public void SetAttachedFile(string fileName, byte[] fileData)
        {
            tbAttachFile.Text = Path.GetFileName(fileName);
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            _presenter.HandleRequestClick();
        }

        
    }
}
