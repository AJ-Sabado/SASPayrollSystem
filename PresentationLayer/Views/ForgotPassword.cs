using MaterialSkin;
using MaterialSkin.Controls;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Views
{
    public partial class ForgotPassword : Form
    {

        private IUnitOfWork _unitOfWork;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public ForgotPassword(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();

            //Init Buttons
            InitButtonProperties(btnCancel);
            InitButtonProperties(btnNext);
            InitButtonProperties(btnOk);
            InitButtonProperties(btnOk);
            InitButtonProperties(btnCancel3);
            InitButtonProperties(btnNext2);

            //InitPanels
            panelForgotPass1.Visible = true;
            panelForgotPass3.Visible = false;
            panelForgotPass2.Visible = false;

            //For Runding Form COrners
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            _unitOfWork.InitialSeeding();
        }

        public void InitButtonProperties(Syncfusion.WinForms.Controls.SfButton btn)
        {
            btn.Style.Border = new Pen(Color.FromArgb(0, 122, 225));
            btn.Style.BackColor = Color.FromArgb(0, 122, 225);
            btn.Style.ForeColor = Color.White;

            btn.Style.FocusedBackColor = Color.FromArgb(0, 122, 225);
            btn.Style.FocusedBorder = new Pen(Color.FromArgb(0, 122, 225));
            btn.Style.FocusedForeColor = Color.White;

            btn.Style.HoverBorder = new Pen(Color.FromArgb(242, 242, 242));
            btn.Style.HoverBackColor = Color.FromArgb(242, 242, 242);
            btn.Style.HoverForeColor = Color.FromArgb(51, 51, 51);

            btn.Style.PressedBorder = new Pen(Color.FromArgb(242, 242, 242));
            btn.Style.PressedBackColor = Color.FromArgb(242, 242, 242);
            btn.Style.PressedForeColor = Color.FromArgb(33, 33, 33);

            btn.Invalidate();

            RoundedElements.rounded(btn, 10);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            panelForgotPass1.Visible = false;
            panelForgotPass2.Visible = true;
            panelForgotPass3.Visible = false;
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            panelForgotPass1.Visible = false;
            panelForgotPass2.Visible = false;
            panelForgotPass3.Visible = true;
        }
    }
}
