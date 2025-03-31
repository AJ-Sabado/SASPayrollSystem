using MaterialSkin;
using MaterialSkin.Controls;
using PresentationLayer.Presenters.DashboardEmployee;
using PresentationLayer.Views;
using ServicesLayer;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class Dashboard_Employee : Form, IDashboard_Employee
    {
        private SfButton lastFocusedButton;

        public event EventHandler Exit;

        public Dashboard_Employee()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = false;
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; // Set to DARK if needed

            materialSkinManager.ColorScheme = new ColorScheme(
            primary: Color.FromArgb(255, 255, 255),         //Background Colors
            darkPrimary: Color.FromArgb(25, 118, 210),      // Dark primary color
            lightPrimary: Color.FromArgb(230, 230, 230),    // Light primary color
            accent: Color.FromArgb(252, 184, 49),           // Accent color
            textShade: TextShade.BLACK                  // Text shade (WHITE/BLACK)

        );

            //DASHBOARD PROPERTIES
            //Init MenuButtons Properties
            InitMenuButtonProperties();

            //Init Dashboard Cards
            InitDashboardCardProperties();

            //Attendance Button
            InitAttendanceButtonProperties();

            //SummaryPanels
            InitSummaryPanelProperties();

            //Attendacne Log Properties
            InitAttendanceLogProperties();

            //DataGrid Properties
            InitDataGrid();

            //JOB DESK PROPERTIES

            //InitJobDeskPanelProperties
            InitJobDeskPanelProperties();

            //View Profile Button Properties
            InitViewProfileButtonProperties();

            //PayslipButtons
            InitJobDeskButtons();

            //InitLeaveCarProerties
            InitLeaveCardProperties();

            //BINDINGS
            this.FormClosed += delegate
            {
                Exit?.Invoke(this, EventArgs.Empty);
            };
        }

        public async void Dashboard_Employee_Load(object sender, EventArgs e)
        {

            this.MaximizeBox = false;

            btnDashboard.Focus();
            pnlDashboard.Show();
            pnlJobDeskRegular.Hide();

            // Track the last focused button
            lastFocusedButton = btnDashboard;

            // Subscribe to LostFocus event
            btnDashboard.LostFocus += Btn_LostFocus;
            btnJobDesk.LostFocus += Btn_LostFocus;
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                using (Pen borderPen = new Pen(ColorTranslator.FromHtml("#bdc1ca"), 3))
                {
                    e.Graphics.DrawRectangle(borderPen, new Rectangle(1, 1, panel.Width - 3, panel.Height - 3));
                }
            }
        }

        //MENU BAR PROPERTIES
        public void InitMenuButtonProperties()
        {
            SfButton[] menuButtons = new SfButton[] { btnDashboard, btnJobDesk, btnAccount };

            foreach (SfButton btn in menuButtons)
            {
                btn.Style.BackColor = Color.FromArgb(0, 0, 0);
                btn.Style.ForeColor = Color.FromArgb(255, 255, 255);
                btn.Style.Border = new Pen(Color.FromArgb(0, 0, 0));

                btn.Style.HoverBackColor = Color.FromArgb(252, 184, 49);
                btn.Style.HoverForeColor = Color.FromArgb(0, 0, 0);
                btn.Style.HoverBorder = new Pen(Color.FromArgb(252, 184, 49));

                btn.Style.FocusedBackColor = Color.FromArgb(252, 184, 49);
                btn.Style.FocusedForeColor = Color.FromArgb(0, 0, 0);
                btn.Style.FocusedBorder = new Pen(Color.FromArgb(252, 184, 49));

                btn.Style.PressedBackColor = Color.FromArgb(247, 165, 2);
                btn.Style.PressedForeColor = Color.FromArgb(0, 0, 0);
                btn.Style.PressedBorder = new Pen(Color.FromArgb(247, 165, 2));

                if (btn.Image != null)
                {
                    Bitmap InitialBlackIcons = new Bitmap(btn.Image);
                    Bitmap InvertedWhiteIcons = ImageIconUtils.InvertImageColors(InitialBlackIcons);

                    btn.Style.Image = InvertedWhiteIcons;
                    btn.Style.HoverImage = InitialBlackIcons;
                    btn.Style.FocusedImage = InitialBlackIcons;
                    btn.Style.PressedImage = InitialBlackIcons;
                }

                RoundedElements.rounded(btn, 10);
                btn.Invalidate();
            }
        }

        //DASHBOARD PROERTIES
        public void InitDashboardCardProperties()
        {

            RoundedElements.rounded(pnlDBMainCard1, 15);
            RoundedElements.rounded(pnlDBMainCard2, 15);
            RoundedElements.rounded(pnlDBMainCard3, 15);
            RoundedElements.rounded(pnlDBMainCard4, 15);
            RoundedElements.rounded(pnlDBSubCard1, 12);
            RoundedElements.rounded(pnlDBSubCard2, 12);
            RoundedElements.rounded(pnlDBSubCard4, 12);


            pnlDBMainCard1.BackColor = Color.FromArgb(252, 184, 49);
        }

        public void InitAttendanceButtonProperties()
        {
            //Time In Button
            btnTimeIn.Style.BackColor = Color.FromArgb(192, 235, 166);
            btnTimeIn.Style.ForeColor = Color.FromArgb(255, 255, 255);
            btnTimeIn.Style.Border = new Pen(Color.FromArgb(192, 235, 166));

            btnTimeIn.Style.HoverBackColor = Color.FromArgb(52, 121, 40);
            btnTimeIn.Style.HoverForeColor = Color.FromArgb(255, 255, 255);
            btnTimeIn.Style.HoverBorder = new Pen(Color.FromArgb(52, 121, 40));

            btnTimeIn.Style.FocusedBackColor = Color.FromArgb(52, 121, 40);
            btnTimeIn.Style.FocusedForeColor = Color.FromArgb(255, 255, 255);
            btnTimeIn.Style.FocusedBorder = new Pen(Color.FromArgb(52, 121, 40));

            btnTimeIn.Style.PressedBackColor = Color.FromArgb(52, 121, 40);
            btnTimeIn.Style.PressedForeColor = Color.FromArgb(255, 255, 255);
            btnTimeIn.Style.PressedBorder = new Pen(Color.FromArgb(52, 121, 40));

            RoundedElements.rounded(btnTimeIn, 10);
            btnTimeIn.Invalidate();

            //Time Out Button
            btnTimeOut.Style.BackColor = Color.FromArgb(216, 64, 64);
            btnTimeOut.Style.ForeColor = Color.FromArgb(255, 255, 255);
            btnTimeOut.Style.Border = new Pen(Color.FromArgb(216, 64, 64));

            btnTimeOut.Style.HoverBackColor = Color.FromArgb(163, 29, 29);
            btnTimeOut.Style.HoverForeColor = Color.FromArgb(255, 255, 255);
            btnTimeOut.Style.HoverBorder = new Pen(Color.FromArgb(163, 29, 29));

            btnTimeOut.Style.FocusedBackColor = Color.FromArgb(163, 29, 29);
            btnTimeOut.Style.FocusedForeColor = Color.FromArgb(255, 255, 255);
            btnTimeOut.Style.FocusedBorder = new Pen(Color.FromArgb(163, 29, 29));

            btnTimeOut.Style.PressedBackColor = Color.FromArgb(163, 29, 29);
            btnTimeOut.Style.PressedForeColor = Color.FromArgb(255, 255, 255);
            btnTimeOut.Style.PressedBorder = new Pen(Color.FromArgb(163, 29, 29));

            RoundedElements.rounded(btnTimeOut, 10);
            btnTimeOut.Invalidate();
        }

        public void InitSummaryPanelProperties()
        {
            //Summary Panel
            RoundedElements.rounded(pnlSummaryBase, 15);
            RoundedElements.rounded(pnlSummaryBase2, 12);

            Panel[] mainSummaryCards = new Panel[] { pnlSummaryCard1, pnlSummaryCard2, pnlSummaryCard3, pnlSummaryCard4, pnlSummaryCard5 };
            Panel[] subSummaryCards = new Panel[] { pnlSummarySubCard2, pnlSummarySubCard3, pnlSummarySubCard4, pnlSummarySubCard5 };

            foreach (Panel pnl in mainSummaryCards)
            {
                RoundedElements.rounded(pnl, 15);
            }

            foreach (Panel pnl in subSummaryCards)
            {
                RoundedElements.rounded(pnl, 12);
            }
        }

        public void InitAttendanceLogProperties()
        {
            //Attendance Log
            RoundedElements.rounded(pnlAttendanceLogBase1, 15);
            RoundedElements.rounded(pnlAttendanceLogBase2, 12);

        }

        public void InitDataGrid()
        {
            RoundedElements.rounded(AttendanceDataGrid, 15);
            RoundedElements.rounded(pnlDataGridBase, 15);
        }

        //JOBDESK PROPERTIES
        public void InitJobDeskPanelProperties()
        {
            RoundedElements.rounded(pnlJobDeskDashMain, 15);
            RoundedElements.rounded(pnlJobDeskDashSub, 12);
            RoundedElements.rounded(pnlJobDeskProfileMain, 15);
            RoundedElements.rounded(pnlJobDeskProfileSub, 12);
        }

        public void InitViewProfileButtonProperties()
        {
            btnJobDeskViewProfile.Style.BackColor = Color.FromArgb(255, 255, 255);
            btnJobDeskViewProfile.Style.ForeColor = Color.FromArgb(51, 51, 51);
            btnJobDeskViewProfile.Style.Border = new Pen(Color.FromArgb(255, 255, 255));

            btnJobDeskViewProfile.Style.HoverBackColor = Color.FromArgb(255, 255, 255);
            btnJobDeskViewProfile.Style.HoverForeColor = Color.FromArgb(252, 184, 49);
            btnJobDeskViewProfile.Style.HoverBorder = new Pen(Color.FromArgb(255, 255, 255));

            btnJobDeskViewProfile.Style.FocusedBackColor = Color.FromArgb(255, 255, 255);
            btnJobDeskViewProfile.Style.FocusedForeColor = Color.FromArgb(252, 184, 49);
            btnJobDeskViewProfile.Style.FocusedBorder = new Pen(Color.FromArgb(255, 255, 255));

            btnJobDeskViewProfile.Style.PressedBackColor = Color.FromArgb(255, 255, 255);
            btnJobDeskViewProfile.Style.PressedForeColor = Color.FromArgb(209, 152, 38);
            btnJobDeskViewProfile.Style.PressedBorder = new Pen(Color.FromArgb(255, 255, 255));
        }

        public void InitJobDeskButtons()
        {
            SfButton[] PayslipButtons = { btnPrint, btnPreviewDocument, btnAttendanceRequest, btnFileLeave };

            foreach (SfButton btn in PayslipButtons)
            {
                RoundedElements.rounded(btn, 10);

                btn.Style.BackColor = Color.FromArgb(255, 218, 145);
                btn.Style.ForeColor = Color.FromArgb(189, 142, 0);
                btn.Style.Border = new Pen(Color.FromArgb(255, 218, 145));

                btn.Style.HoverBackColor = Color.FromArgb(252, 184, 49);
                btn.Style.HoverForeColor = Color.FromArgb(255, 255, 255);
                btn.Style.HoverBorder = new Pen(Color.FromArgb(252, 184, 49));

                btn.Style.FocusedBackColor = Color.FromArgb(217, 157, 39);
                btn.Style.FocusedForeColor = Color.FromArgb(255, 255, 255);
                btn.Style.FocusedBorder = new Pen(Color.FromArgb(217, 157, 39));

                btn.Style.PressedBackColor = Color.FromArgb(217, 157, 39);
                btn.Style.PressedForeColor = Color.FromArgb(255, 255, 255);
                btn.Style.PressedBorder = new Pen(Color.FromArgb(217, 157, 39));

            }
        }

        public void InitLeaveCardProperties()
        {
            RoundedElements.rounded(pnlLeaveCard1Main, 15);
            RoundedElements.rounded(pnlLeaveCard1Sub, 12);
            RoundedElements.rounded(pnlLeaveCard2Main, 15);
            RoundedElements.rounded(pnlLeaveCard2Sub, 12);
            RoundedElements.rounded(pnlLeaveCard3Main, 15);
        }

        //public IUnitOfWork Get_unitOfWork()
        //{
        //    return _unitOfWork;
        //}

        //BINDINGS

        //Action Events

        private void Btn_LostFocus(object sender, EventArgs e)
        {
            if (!(btnDashboard.Focused || btnJobDesk.Focused))
            {
                lastFocusedButton.Focus();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            lastFocusedButton = btnDashboard;
            btnDashboard.Focus();
            pnlDashboard.Show();
            pnlJobDeskRegular.Hide();
        }

        private void btnJobDesk_Click(object sender, EventArgs e)
        {
            lastFocusedButton = btnJobDesk;
            btnJobDesk.Focus();
            pnlDashboard.Hide();
            pnlJobDeskRegular.Show();
        }

    }

}
