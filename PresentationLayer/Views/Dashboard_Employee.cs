using PresentationLayer.Views;
using ServicesLayer;
using Syncfusion.Windows.Forms;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class Dashboard_Employee : Form
    {
        private IUnitOfWork _unitOfWork;
        public Dashboard_Employee(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();

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

            _unitOfWork.InitialSeeding();
        }

        public void Dashboard_Employee_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
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

        public void InitMenuButtonProperties()
        {
            SfButton[] menuButtons = new SfButton[] { btnDashboard, btnJobDesk };

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

                Bitmap InitialBlackIcons = new Bitmap(btn.Image);
                Bitmap InvertedWhiteIcons = ImageIconUtils.InvertImageColors(InitialBlackIcons);

                btn.Style.Image = InvertedWhiteIcons;
                btn.Style.HoverImage = InitialBlackIcons;
                btn.Style.FocusedImage = InitialBlackIcons;
                btn.Style.PressedImage = InitialBlackIcons;

                RoundedElements.rounded(btn, 10);

                btn.Invalidate();
            }
        }

        public void InitDashboardCardProperties()
        {

            RoundedElements.rounded(pnlDBMainCard1, 30);
            RoundedElements.rounded(pnlDBMainCard2, 30);
            RoundedElements.rounded(pnlDBMainCard3, 30);
            RoundedElements.rounded(pnlDBSubCard1, 27);
            RoundedElements.rounded(pnlDBSubCard2, 27);

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
            RoundedElements.rounded(pnlSummaryBase, 30);
            RoundedElements.rounded(pnlSummaryBase2, 27);

            Panel[] mainSummaryCards = new Panel[] { pnlSummaryCard1, pnlSummaryCard2, pnlSummaryCard3, pnlSummaryCard4};
            Panel[] subSummaryCards = new Panel[] { pnlSummarySubCard2, pnlSummarySubCard3, pnlSummarySubCard4 };

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
            RoundedElements.rounded(pnlAttendanceLogBase1, 30);
            RoundedElements.rounded(pnlAttendanceLogBase2, 27);
            
        }


        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void btnJobDesk_Click(object sender, EventArgs e)
        {

        }

    }
}
