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
            InitMenuButtonProperties();
            InitDashboardCardProperties();
            InitAttendanceButtonProperties();
            InitSummaryPanelProperties();
            InitAttendanceLogProperties();
            InitDataGrid();
            InitJobDeskPanelProperties();
            InitViewProfileButtonProperties();
            InitJobDeskButtons();
            InitLeaveCardProperties();
            InitAccountsPanelProperties();
            InitAccountButtonProperties();
        }

        public void ApplyMaterialSkin(MaterialSkinManager manager)
        {
            manager.EnforceBackcolorOnAllComponents = false;
            manager.Theme = MaterialSkinManager.Themes.LIGHT;
            manager.ColorScheme = new ColorScheme(
                primary: Color.White,
                darkPrimary: Color.FromArgb(25, 118, 210),
                lightPrimary: Color.FromArgb(230, 230, 230),
                accent: Color.FromArgb(252, 184, 49),
                textShade: TextShade.BLACK
            );
        }

        private void SetButtonStyle(SfButton btn, Color baseColor, Color hoverColor, Color pressedColor, Color textColor)
        {
            btn.Style.BackColor = baseColor;
            btn.Style.ForeColor = textColor;
            btn.Style.Border = new Pen(baseColor);

            btn.Style.HoverBackColor = hoverColor;
            btn.Style.HoverForeColor = textColor;
            btn.Style.HoverBorder = new Pen(hoverColor);

            btn.Style.FocusedBackColor = baseColor;
            btn.Style.FocusedForeColor = textColor;
            btn.Style.FocusedBorder = new Pen(baseColor);

            btn.Style.PressedBackColor = pressedColor;
            btn.Style.PressedForeColor = textColor;
            btn.Style.PressedBorder = new Pen(pressedColor);
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

                var originalIcon = (Bitmap)btn.Image;
                var whiteIcon = ImageIconUtils.InvertImageColors(originalIcon); 

                btn.Style.Image = whiteIcon;                 
                btn.Style.HoverImage = originalIcon;         
                btn.Style.FocusedImage = originalIcon;     
                btn.Style.PressedImage = originalIcon;       

                btn.Invalidate(); 
            }
        }

        private void InitDashboardCardProperties()
        {

            var mainCards = new[] { _dashboard.pnlDBMainCard1, _dashboard.pnlDBMainCard2, _dashboard.pnlDBMainCard3, _dashboard.pnlDBMainCard4 };
            
            foreach (var card in mainCards) { 
                RoundedElements.rounded(card, 15); 
            }

            var subCards = new[] { _dashboard.pnlDBSubCard1, _dashboard.pnlDBSubCard2, _dashboard.pnlDBSubCard4 };
            foreach (var subCard in subCards)
            {
                RoundedElements.rounded(subCard, 12);
            }

            _dashboard.pnlDBMainCard1.BackColor = Color.FromArgb(252, 184, 49);
        }

        private void InitAttendanceButtonProperties()
        {
            SetButtonStyle(_dashboard.btnTimeIn, Color.FromArgb(192, 235, 166), Color.FromArgb(52, 121, 40), Color.FromArgb(52, 121, 40), Color.White);
            RoundedElements.rounded(_dashboard.btnTimeIn, 10);

            SetButtonStyle(_dashboard.btnTimeOut, Color.FromArgb(216, 64, 64), Color.FromArgb(163, 29, 29), Color.FromArgb(163, 29, 29), Color.White);
            RoundedElements.rounded(_dashboard.btnTimeOut, 10);
        }

        private void InitSummaryPanelProperties()
        {
            RoundedElements.rounded(_dashboard.pnlSummaryBase, 15);
            RoundedElements.rounded(_dashboard.pnlSummaryBase2, 12);

            Panel[] mainSummaryCards = { _dashboard.pnlSummaryCard1, _dashboard.pnlSummaryCard2, _dashboard.pnlSummaryCard3, _dashboard.pnlSummaryCard4, _dashboard.pnlSummaryCard5 };
            foreach (var pnl in mainSummaryCards) RoundedElements.rounded(pnl, 15);

            Panel[] subSummaryCards = { _dashboard.pnlSummarySubCard2, _dashboard.pnlSummarySubCard3, _dashboard.pnlSummarySubCard4, _dashboard.pnlSummarySubCard5 };
            foreach (var pnl in subSummaryCards) RoundedElements.rounded(pnl, 12);
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
            SetButtonStyle(_dashboard.btnJobDeskViewProfile, Color.White, Color.White, Color.White, Color.FromArgb(51, 51, 51));
        }

        private void InitJobDeskButtons()
        {
            SfButton[] jobDeskButtons = { _dashboard.btnPrint, _dashboard.btnPreviewDocument, _dashboard.btnAttendanceRequest, _dashboard.btnFileLeave };
            Panel[] panels = { _dashboard.btnPanel1, _dashboard.btnPanel2, _dashboard.btnPanel3, _dashboard.btnPanel4 };

            foreach (var btn in jobDeskButtons)
            {
                SetButtonStyle(btn, Color.White, Color.FromArgb(252, 184, 49), Color.FromArgb(247, 165, 2), Color.FromArgb(252, 184, 49));
                btn.Style.HoverForeColor = Color.White;
                btn.Style.FocusedForeColor = Color.White;
                btn.Style.PressedForeColor = Color.White;
                RoundedElements.rounded(btn, 7);
            }

            foreach (var panel in panels) RoundedElements.rounded(panel, 10);
        }

        private void InitLeaveCardProperties()
        {
            var cards = new (Control panel, int radius)[]
            {
                (_dashboard.pnlLeaveCard1Main, 15),
                (_dashboard.pnlLeaveCard1Sub, 12),
                (_dashboard.pnlLeaveCard2Main, 15),
                (_dashboard.pnlLeaveCard2Sub, 12),
                (_dashboard.pnlLeaveCard3Main, 15)
            };

            foreach (var (panel, radius) in cards)
            {
                RoundedElements.rounded(panel, radius);
            }
        }

        private void InitAccountsPanelProperties()
        {
            var panels = new (Control panel, int radius)[]
            {
                (_dashboard.pnlAccountProfileBase, 15),
                (_dashboard.pnlAccountProfileSub, 12),
                (_dashboard.pnlPersonalInformationMain, 15),
                (_dashboard.pnlPersonalInformationSub, 12),
                (_dashboard.pnlContactInformationMain, 15),
                (_dashboard.pnlContactInformationSub, 12),
                (_dashboard.pnlEmploymentInformationMain, 15),
                (_dashboard.pnlEmploymentInformationSub, 12),
                (_dashboard.pnlFinancialInformationMain, 15),
                (_dashboard.pnlFinancialInformationSub, 12)
            };

            foreach (var (panel, radius) in panels)
            {
                RoundedElements.rounded(panel, radius);
            }
        }

        private void InitAccountButtonProperties()
        {
            SetButtonStyle(_dashboard.btnEditAccountInfo, Color.White, Color.White, Color.White, Color.White);
        }
    }
}
