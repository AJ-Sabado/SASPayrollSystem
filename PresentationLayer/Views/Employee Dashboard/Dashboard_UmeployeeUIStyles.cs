using MaterialSkin;
using Syncfusion.WinForms.Controls;

namespace PresentationLayer.Views
{
    internal class Dashboard_EmployeeUIStyles
    {
        private readonly Dashboard_Employee _dashboard;
      
        public Dashboard_EmployeeUIStyles(Dashboard_Employee dashboard)
        {
            _dashboard = dashboard;
        }

        public void InitializeUI()
        {
            // Initialize Menu Button Properties
            InitMenuButtonProperties();
            // Initialize Dashboard Card Properties
            InitDashboardCardProperties();
            // Initialize Attendance Button Properties
            InitAttendanceButtonProperties();
            // Initialize Summary Panel Properties
            InitSummaryPanelProperties();
            // Initialize Attendance Log Properties
            InitAttendanceLogProperties();
            // Initialize Data Grid
            InitDataGrid();
            // Initialize Job Desk Panel Properties
            InitJobDeskPanelProperties();
            // Initialize View Profile Button Properties
            InitViewProfileButtonProperties();
            // Initialize Job Desk Buttons
            InitJobDeskButtons();
            // Initialize Leave Card Properties
            InitLeaveCardProperties();
            // Initialize Accounts Panel Properties
            InitAccountsPanelProperties();
            // Initialize Account Button Properties
            InitAccountButtonProperties();
        }

        public void ApplyMaterialSkin(MaterialSkinManager manager)
        {
            manager.EnforceBackcolorOnAllComponents = false;
            manager.Theme = MaterialSkinManager.Themes.LIGHT;
            manager.ColorScheme = new ColorScheme(
                primary: Color.FromArgb(255, 255, 255),
                darkPrimary: Color.FromArgb(25, 118, 210),
                lightPrimary: Color.FromArgb(230, 230, 230),
                accent: Color.FromArgb(252, 184, 49),
                textShade: TextShade.BLACK
            );
        }


        private void InitMenuButtonProperties()
        {
            SfButton[] menuButtons = { _dashboard.btnDashboard, _dashboard.btnJobDesk, _dashboard.btnAccount };

            foreach (SfButton btn in menuButtons)
            {
                btn.Style.BackColor = Color.Black;
                btn.Style.ForeColor = Color.White;
                btn.Style.Border = new Pen(Color.Black);

                btn.Style.HoverBackColor = Color.FromArgb(252, 184, 49);
                btn.Style.HoverForeColor = Color.Black;
                btn.Style.HoverBorder = new Pen(Color.FromArgb(252, 184, 49));

                btn.Style.FocusedBackColor = Color.FromArgb(252, 184, 49);
                btn.Style.FocusedForeColor = Color.Black;
                btn.Style.FocusedBorder = new Pen(Color.FromArgb(252, 184, 49));

                btn.Style.PressedBackColor = Color.FromArgb(247, 165, 2);
                btn.Style.PressedForeColor = Color.Black;
                btn.Style.PressedBorder = new Pen(Color.FromArgb(247, 165, 2));

                RoundedElements.rounded(btn, 10);
                var blackIcon = btn.Image;
                var whiteIcon = ImageIconUtils.InvertImageColors((Bitmap)blackIcon);
                btn.Style.Image = whiteIcon;
                btn.Style.HoverImage = blackIcon;
                btn.Style.PressedImage = blackIcon;
                btn.Style.FocusedImage = blackIcon;
                btn.Invalidate();
            }
        }

        private void InitDashboardCardProperties()
        {
            RoundedElements.rounded(_dashboard.pnlDBMainCard1, 15);
            RoundedElements.rounded(_dashboard.pnlDBMainCard2, 15);
            RoundedElements.rounded(_dashboard.pnlDBMainCard3, 15);
            RoundedElements.rounded(_dashboard.pnlDBMainCard4, 15);
            RoundedElements.rounded(_dashboard.pnlDBSubCard1, 12);
            RoundedElements.rounded(_dashboard.pnlDBSubCard2, 12);
            RoundedElements.rounded(_dashboard.pnlDBSubCard4, 12);

            _dashboard.pnlDBMainCard1.BackColor = Color.FromArgb(252, 184, 49);
        }

        private void InitAttendanceButtonProperties()
        {
            SfButton btnTimeIn = _dashboard.btnTimeIn;
            SfButton btnTimeOut = _dashboard.btnTimeOut;

            btnTimeIn.Style.BackColor = Color.FromArgb(192, 235, 166);
            btnTimeIn.Style.ForeColor = Color.White;
            btnTimeIn.Style.Border = new Pen(Color.FromArgb(192, 235, 166));

            btnTimeIn.Style.HoverBackColor = Color.FromArgb(52, 121, 40);
            btnTimeIn.Style.HoverForeColor = Color.FromArgb(255, 255, 255);
            btnTimeIn.Style.HoverBorder = new Pen(Color.FromArgb(52, 121, 40));

            btnTimeIn.Style.FocusedBackColor = Color.FromArgb(192, 235, 166);
            btnTimeIn.Style.FocusedForeColor = Color.FromArgb(255, 255, 255);
            btnTimeIn.Style.FocusedBorder = new Pen(Color.FromArgb(192, 235, 166));

            btnTimeIn.Style.PressedBackColor = Color.FromArgb(52, 121, 40);
            btnTimeIn.Style.PressedForeColor = Color.FromArgb(255, 255, 255);
            btnTimeIn.Style.PressedBorder = new Pen(Color.FromArgb(52, 121, 40));

            RoundedElements.rounded(btnTimeIn, 10);
            btnTimeIn.Invalidate();

            //Time oUt
            btnTimeOut.Style.BackColor = Color.FromArgb(216, 64, 64);
            btnTimeOut.Style.ForeColor = Color.White;
            btnTimeOut.Style.Border = new Pen(Color.FromArgb(216, 64, 64));

            btnTimeOut.Style.HoverBackColor = Color.FromArgb(163, 29, 29);
            btnTimeOut.Style.HoverForeColor = Color.FromArgb(255, 255, 255);
            btnTimeOut.Style.HoverBorder = new Pen(Color.FromArgb(163, 29, 29));

            btnTimeOut.Style.FocusedBackColor = Color.FromArgb(216, 64, 64);
            btnTimeOut.Style.FocusedForeColor = Color.FromArgb(255, 255, 255);
            btnTimeOut.Style.FocusedBorder = new Pen(Color.FromArgb(216, 64, 64));

            btnTimeOut.Style.PressedBackColor = Color.FromArgb(163, 29, 29);
            btnTimeOut.Style.PressedForeColor = Color.FromArgb(255, 255, 255);
            btnTimeOut.Style.PressedBorder = new Pen(Color.FromArgb(163, 29, 29));
            RoundedElements.rounded(btnTimeOut, 10);
            btnTimeOut.Invalidate();
        }

        private void InitSummaryPanelProperties()
        {
            RoundedElements.rounded(_dashboard.pnlSummaryBase, 15);
            RoundedElements.rounded(_dashboard.pnlSummaryBase2, 12);

            Panel[] mainSummaryCards = { _dashboard.pnlSummaryCard1, _dashboard.pnlSummaryCard2, _dashboard.pnlSummaryCard3, _dashboard.pnlSummaryCard4, _dashboard.pnlSummaryCard5 };
            Panel[] subSummaryCards = { _dashboard.pnlSummarySubCard2, _dashboard.pnlSummarySubCard3, _dashboard.pnlSummarySubCard4, _dashboard.pnlSummarySubCard5 };

            foreach (Panel pnl in mainSummaryCards)
                RoundedElements.rounded(pnl, 15);

            foreach (Panel pnl in subSummaryCards)
                RoundedElements.rounded(pnl, 12);
        }

        private void InitAttendanceLogProperties()
        {
            RoundedElements.rounded(_dashboard.pnlAttendanceLogBase1, 15);
            RoundedElements.rounded(_dashboard.pnlAttendanceLogBase2, 12);
        }

        private void InitDataGrid()
        {
            RoundedElements.rounded(_dashboard.AttendanceDataGrid, 15);
            RoundedElements.rounded(_dashboard.pnlDataGridBase, 15);
        }

        private void InitJobDeskPanelProperties()
        {
            RoundedElements.rounded(_dashboard.pnlJobDeskProfileMain, 15);
            RoundedElements.rounded(_dashboard.pnlJobDeskProfileSub, 12);
            RoundedElements.rounded(_dashboard.pnlJobDeskDashMain, 15);
            RoundedElements.rounded(_dashboard.pnlJobDeskDashSub, 12);
        }

        private void InitViewProfileButtonProperties()
        {
            SfButton btn = _dashboard.btnJobDeskViewProfile;
            btn.Style.BackColor = Color.White;
            btn.Style.ForeColor = Color.FromArgb(51, 51, 51);
            btn.Style.Border = new Pen(Color.White);
        }

        private void InitJobDeskButtons()
        {
            SfButton[] payslipButtons = { _dashboard.btnPrint, _dashboard.btnPreviewDocument, _dashboard.btnAttendanceRequest, _dashboard.btnFileLeave };
            Panel[] panels = { _dashboard.btnPanel1, _dashboard.btnPanel2, _dashboard.btnPanel3, _dashboard.btnPanel4 };

            foreach (SfButton btn in payslipButtons)
            {
                btn.Style.BackColor = Color.White;
                btn.Style.ForeColor = Color.FromArgb(252, 184, 49);
                btn.Style.Border = new Pen(Color.White);

                btn.Style.HoverBackColor = Color.FromArgb(252, 184, 49);
                btn.Style.HoverForeColor = Color.White;
                btn.Style.HoverBorder = new Pen(Color.FromArgb(252, 184, 49));

                btn.Style.PressedBackColor = Color.FromArgb(247, 165, 2);
                btn.Style.PressedForeColor = Color.White;
                btn.Style.PressedBorder = new Pen(Color.FromArgb(247, 165, 2));

                btn.Style.FocusedBackColor = Color.White;
                btn.Style.FocusedForeColor = Color.FromArgb(252,184,49);
                btn.Style.FocusedBorder = new Pen(Color.White);

                RoundedElements.rounded(btn, 7);
            }

            foreach(Panel panel in panels)
            {
                RoundedElements.rounded(panel, 10);
            }
        }

        private void InitLeaveCardProperties()
        {
            RoundedElements.rounded(_dashboard.pnlLeaveCard1Main, 15);
            RoundedElements.rounded(_dashboard.pnlLeaveCard1Sub, 12);
            RoundedElements.rounded(_dashboard.pnlLeaveCard2Main, 15);
            RoundedElements.rounded(_dashboard.pnlLeaveCard2Sub, 12);
            RoundedElements.rounded(_dashboard.pnlLeaveCard3Main, 15);
        }

        private void InitAccountsPanelProperties()
        {
            RoundedElements.rounded(_dashboard.pnlAccountProfileBase, 15);
            RoundedElements.rounded(_dashboard.pnlAccountProfileSub, 12);
            RoundedElements.rounded(_dashboard.pnlPersonalInformationMain,15);
            RoundedElements.rounded(_dashboard.pnlPersonalInformationSub, 12);
            RoundedElements.rounded(_dashboard.pnlContactInformationMain, 15);
            RoundedElements.rounded(_dashboard.pnlContactInformationSub, 12);
            RoundedElements.rounded(_dashboard.pnlEmploymentInformationMain, 15);
            RoundedElements.rounded(_dashboard.pnlEmploymentInformationSub, 12);
            RoundedElements.rounded(_dashboard.pnlFinancialInformationMain, 15);
            RoundedElements.rounded(_dashboard.pnlFinancialInformationSub,12);
        }

        private void InitAccountButtonProperties()
        {
            _dashboard.btnEditAccountInfo.Style.BackColor = Color.White;
            _dashboard.btnEditAccountInfo.Style.ForeColor = Color.White;
            _dashboard.btnEditAccountInfo.Style.Border = new Pen(Color.White);

            _dashboard.btnEditAccountInfo.Style.HoverBackColor = Color.White;
            _dashboard.btnEditAccountInfo.Style.HoverForeColor = Color.White;
            _dashboard.btnEditAccountInfo.Style.HoverBorder = new Pen(Color.White);

            _dashboard.btnEditAccountInfo.Style.FocusedBackColor = Color.White;
            _dashboard.btnEditAccountInfo.Style.FocusedForeColor = Color.White;
            _dashboard.btnEditAccountInfo.Style.FocusedBorder = new Pen(Color.White);

            _dashboard.btnEditAccountInfo.Style.PressedBackColor = Color.White;
            _dashboard.btnEditAccountInfo.Style.PressedForeColor = Color.White;
            _dashboard.btnEditAccountInfo.Style.PressedBorder = new Pen(Color.White);

            _dashboard.btnEditAccountInfo.Style.HoverImage = ImageIconUtils.ChangeIconColor((Bitmap)_dashboard.btnEditAccountInfo.Image, Color.LightGray);
        }
    }
}