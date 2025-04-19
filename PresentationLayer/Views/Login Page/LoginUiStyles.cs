using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Views.Login_Page
{
    internal class LoginUiStyles
    {
        private readonly Login_Form _loginForm;

        public LoginUiStyles(Login_Form loginForm)
        {
            _loginForm = loginForm;
        }

        public void InitializeUI()
        {
            // Initialize Button Properties
            InitCloseBtnProperties(_loginForm.btnCloseForm);
            InitCloseBtnProperties(_loginForm.btnCloseForm2);
            InitButtonProperties(_loginForm.btnForgotPass);
            InitButtonProperties(_loginForm.btnSignIn);
            InitButtonProperties(_loginForm.btnSignUpOption);
            InitButtonProperties(_loginForm.btnSignUp);
            InitForgotPassButton();
        }

        private void InitCloseBtnProperties(SfButton btn)
        {
            btn.Style.Border = new Pen(Color.FromArgb(242, 242, 242));
            btn.Style.BackColor = Color.FromArgb(242, 242, 242);
            btn.Style.ForeColor = Color.FromArgb(51, 51, 51);

            btn.Style.FocusedBorder = new Pen(Color.FromArgb(242, 242, 242));
            btn.Style.FocusedBackColor = Color.FromArgb(242, 242, 242);
            btn.Style.FocusedForeColor = Color.FromArgb(51, 51, 51);

            btn.Style.HoverBorder = new Pen(Color.FromArgb(200, 0, 0));
            btn.Style.HoverBackColor = Color.FromArgb(200, 0, 0);
            btn.Style.HoverForeColor = Color.FromArgb(255, 255, 255);

            btn.Style.PressedBorder = new Pen(Color.FromArgb(180, 0, 0));
            btn.Style.PressedBackColor = Color.FromArgb(180, 0, 0);
            btn.Style.PressedForeColor = Color.FromArgb(255, 255, 255);

            btn.Invalidate();
        }

        private void InitButtonProperties(SfButton btn)
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

        private void InitForgotPassButton()
        {
            var btn = _loginForm.btnForgotPass;
            btn.Style.Border = new Pen(Color.FromArgb(242, 242, 242));
            btn.Style.BackColor = Color.FromArgb(242, 242, 242);
            btn.Style.ForeColor = Color.FromArgb(51, 51, 51);

            btn.Style.FocusedBorder = new Pen(Color.FromArgb(242, 242, 242));
            btn.Style.FocusedBackColor = Color.FromArgb(242, 242, 242);
            btn.Style.FocusedForeColor = Color.FromArgb(51, 51, 51);

            btn.Style.HoverBorder = new Pen(Color.FromArgb(242, 242, 242));
            btn.Style.HoverBackColor = Color.FromArgb(242, 242, 242);
            btn.Style.HoverForeColor = Color.FromArgb(255, 170, 0);

            btn.Style.PressedBorder = new Pen(Color.FromArgb(242, 242, 242));
            btn.Style.PressedBackColor = Color.FromArgb(242, 242, 242);
            btn.Style.PressedForeColor = Color.FromArgb(255, 147, 0);

            btn.Invalidate();
        }
    }
}

