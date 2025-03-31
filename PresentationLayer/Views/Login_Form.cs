using ServicesLayer;
using ServicesLayer.Exceptions;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace PresentationLayer.Views
{
    public partial class Login_Form : Form
    {
        private IUnitOfWork _unitOfWork;

        private System.Windows.Forms.Timer timer;
        private int targetX;
        private int speed = 15;
        private bool isSignIn = true;

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
        public Login_Form(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();

            //Initializing Transtiion Timer
            timer = new System.Windows.Forms.Timer(); // Use Windows Forms Timer
            timer.Interval = 10; // Controls animation speed
            timer.Tick += Timer_Tick;

            //For Runding Form COrners
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {
            InitCloseBtnProperties(btnCloseForm);
            InitCloseBtnProperties(btnCloseForm2);
            InitButtonProperties(btnForgotPass);
            InitButtonProperties(btnSignIn);
            InitButtonProperties(btnSignUpOption);
            InitButtonProperties(btnSignUp);
            InitButtonProperties(btnSignIn);
            InitForgotPassButton();

            //Sets default values on initial runtime instance
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            int step = Math.Min(speed, Math.Abs(bgPanelMotion.Left - targetX)); // Prevents overshooting

            if (bgPanelMotion.Left < targetX)
            {
                bgPanelMotion.Left += step;
            }
            else if (bgPanelMotion.Left > targetX)
            {
                bgPanelMotion.Left -= step;
            }

            if (bgPanelMotion.Left == targetX)
            {
                timer.Stop();
            }
        }

        public async void InitCloseBtnProperties(Syncfusion.WinForms.Controls.SfButton btn)
        {
            //Init Close Form
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

        public async void InitButtonProperties(Syncfusion.WinForms.Controls.SfButton btn)
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

        public void InitForgotPassButton()
        {
            btnForgotPass.Style.Border = new Pen(Color.FromArgb(242, 242, 242));
            btnForgotPass.Style.BackColor = Color.FromArgb(242, 242, 242);
            btnForgotPass.Style.ForeColor = Color.FromArgb(51, 51, 51);

            btnForgotPass.Style.FocusedBorder = new Pen(Color.FromArgb(242, 242, 242));
            btnForgotPass.Style.FocusedBackColor = Color.FromArgb(242, 242, 242);
            btnForgotPass.Style.FocusedForeColor = Color.FromArgb(51, 51, 51);

            btnForgotPass.Style.HoverBorder = new Pen(Color.FromArgb(242, 242, 242));
            btnForgotPass.Style.HoverBackColor = Color.FromArgb(242, 242, 242);
            btnForgotPass.Style.HoverForeColor = Color.FromArgb(255, 170, 0);

            btnForgotPass.Style.PressedBorder = new Pen(Color.FromArgb(242, 242, 242));
            btnForgotPass.Style.PressedBackColor = Color.FromArgb(242, 242, 242);
            btnForgotPass.Style.PressedForeColor = Color.FromArgb(255, 147, 0);

            btnForgotPass.Invalidate();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Async Login Example
        private async void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                await _unitOfWork.LoginUser(txtBoxUsername.Text, textBoxExt1.Text);
                this.Hide();
                new Dashboard_Employee().Show();
                this.Show();

            }

            catch (UserNotFoundException)
            {
                MessageBox.Show("User does not exist!");
            }
            catch (IncorrectPasswordException)
            {
                MessageBox.Show("Wrong password!");
            }
        }

        private async void btnSignUp_ClickAsync(object sender, EventArgs e)
        {
            int panelWidth = bgPanelMotion.Width;
            int formWidth = this.ClientSize.Width;

            if (isSignIn)
            {
                targetX = formWidth - panelWidth;
                btnSignUpOption.Text = "Sign In";
                lblSignInTitle.Text = "Already have an Account?";
                lblSignInDescription.Text = "Welcome back! We’re glad to see you again. " +
                    "Sign in to your account and continue your journey with us. " +
                    "We can’t wait to see what you’ll do next!";
                isSignIn = false;
            }
            else
            {
                targetX = 0;
                btnSignUpOption.Text = "Sign Up";
                lblSignInTitle.Text = "No Account Yet??";
                lblSignInDescription.Text = "If you don’t have an account yet, " +
                    "sign up today and become a valued member of our ever-growing " +
                    "Strategic Assistant Family! Join us and take the first step " +
                    "toward a smarter, more efficient future.";
                isSignIn = true;
            }
            timer.Start();
        }

        private void btnCloseForm2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnForgotPass_Click(object sender, EventArgs e)
        {
            this.Hide();
            var forgotPasswordForm = new ForgotPassword(_unitOfWork);
            forgotPasswordForm.ShowDialog();
            this.Show();
        }
    }
}
