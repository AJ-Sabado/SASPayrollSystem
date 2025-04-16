using PresentationLayer.Views.Login_Page;
using ServicesLayer;
using ServicesLayer.Exceptions;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace PresentationLayer.Views
{
    public partial class Login_Form : Form, ILogin_Form
    {
        //private IUnitOfWork _unitOfWork;

        //UI Styles
        LoginUiStyles formStyles;

        //Event Handler for Sign In event
        public event EventHandler SignIn;

        //Field bindings
        public string UsernameField { get => txtBoxUsername.Text; set => txtBoxUsername.Text = value; }
        public string PasswordField { get => textBoxExt1.Text; set => textBoxExt1.Text = value; }

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
        public Login_Form()
        {
            InitializeComponent();

            //Initializing Transtiion Timer
            timer = new System.Windows.Forms.Timer(); // Use Windows Forms Timer
            timer.Interval = 10; // Controls animation speed
            timer.Tick += Timer_Tick;

            //For Runding Form COrners
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            //Button Delegations to Event Handler
            btnSignIn.Click += delegate
            {
                SignIn?.Invoke(this, EventArgs.Empty);
            };
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {
            formStyles = new LoginUiStyles(this);
            formStyles.InitializeUI(); // Initialize UI styles

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

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //This is moved to Login Presenter
        //Async Login Example
        //private async void btnSignIn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        await _unitOfWork.LoginUser(txtBoxUsername.Text, textBoxExt1.Text);
        //        this.Hide();
        //        new Dashboard_Employee().Show();
        //        this.Show();

        //    }

        //    catch (UserNotFoundException)
        //    {
        //        MessageBox.Show("User does not exist!");
        //    }
        //    catch (IncorrectPasswordException)
        //    {
        //        MessageBox.Show("Wrong password!");
        //    }
        //}

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
            //this.Hide();
            //var forgotPasswordForm = new ForgotPassword(_unitOfWork);
            //forgotPasswordForm.ShowDialog();
            //this.Show();
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
