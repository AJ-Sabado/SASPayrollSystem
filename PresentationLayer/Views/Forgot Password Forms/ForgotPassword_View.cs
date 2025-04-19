using PresentationLayer.Presenters;
using PresentationLayer.Views.Forgot_Password_Forms;
using ServicesLayer;
using Syncfusion.WinForms.Controls;
using System.Runtime.InteropServices;


namespace PresentationLayer.Views
{
    public partial class ForgotPassword_View : Form, IForgotPassword_View
    {
        private ForgotPasswordUI _UiStyles;
        private ForgotPasswordPresenter _presenter;
        private IUnitOfWork _unitOfWork;

        public event EventHandler NextClick;
        public event EventHandler NextClick2;
        public event EventHandler CancelClick;

        public ForgotPassword_View(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _presenter = new ForgotPasswordPresenter(_unitOfWork, this);
            InitializeComponent();

            _UiStyles = new ForgotPasswordUI(this);
            _UiStyles.InitializeUIStyle();

            //Button Delegations to Event Handler
            ButtonEvents();
        }

        public void ButtonEvents()
        {
            btnNext.Click += delegate
            {
                NextClick?.Invoke(this, EventArgs.Empty);
            };
            btnNext2.Click += delegate
            {
                NextClick2?.Invoke(this, EventArgs.Empty);
            };

            SfButton[] btn = { btnOk, btnCancel3, btnCancel };
            foreach(SfButton b in btn)
            {
                b.Click += delegate
                {
                    CancelClick?.Invoke(this, EventArgs.Empty);
                };
            }
        }

        public void Show()
        {
            this.ShowDialog();
        }

        private void Close()
        {
            this.Close();
            this.Dispose();
        }

        public void btnNext_Click()
        {
            panelForgotPass1.Visible = false;
            panelForgotPass2.Visible = true;
            panelForgotPass3.Visible = false;
        }

        public void btnNext2_Click()
        {
            panelForgotPass1.Visible = false;
            panelForgotPass2.Visible = false;
            panelForgotPass3.Visible = true;
            this.Size = new Size(this.Size.Width, 250);
            RoundedElements.rounded(this, 10);
        }
    }
}
