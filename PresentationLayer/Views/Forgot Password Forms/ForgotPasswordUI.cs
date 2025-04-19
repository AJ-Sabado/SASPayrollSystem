

using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;

namespace PresentationLayer.Views.Forgot_Password_Forms
{
    internal class ForgotPasswordUI
    {

        ForgotPassword_View _forgotPassView;

        public ForgotPasswordUI(ForgotPassword_View forgotPassView)
        {
            _forgotPassView = forgotPassView;
        }

        public void InitializeUIStyle()
        {
            InitPanels();
            InitButtonProperties();
            RoundForms();

        }

        public void InitPanels()
        {
            _forgotPassView.panelForgotPass1.Visible = true;
            _forgotPassView.panelForgotPass3.Visible = false;
            _forgotPassView.panelForgotPass2.Visible = false;
        }

        public void InitButtonProperties()
        {
            SfButton[] btns = { _forgotPassView.btnCancel , _forgotPassView.btnNext, _forgotPassView.btnOk,
                                _forgotPassView.btnOk, _forgotPassView.btnCancel3, _forgotPassView.btnNext2};

            foreach(SfButton btn in btns)
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
        }

        public void RoundForms()
        {
            _forgotPassView.FormBorderStyle = FormBorderStyle.None;
            RoundedElements.rounded(_forgotPassView, 20);
        }
    }
}
