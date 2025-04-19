using PresentationLayer.Views.Utility_Classes;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PresentationLayer.Views.Custom_Message_Box
{
    public enum dBoxType
    {
        Information,
        Warning,
        Error,
        Success,
        Question
    }

    public enum dBoxResponse
    {
        OK,
        CANCEL,
        YES,
        NO
    }

    public partial class DialogBox : Form
    {
        Image _iconInformation;
        Image _iconWarning;
        Image _iconError;
        Image _iconSuccess;
        Image _iconQuestion;

        string _title;
        string _message;
        dBoxType _dBoxType;

        public dBoxResponse Response { get; private set; } = dBoxResponse.CANCEL;

        public DialogBox(string? title, string message, dBoxType type)
        {
            InitializeComponent();
            MaterialSkinClass.initMaterialSkin();
            RoundedElements.rounded(this, 10); // Optional custom UI tweak

            _title = title ?? "";
            _message = message;
            _dBoxType = type;

            loadIcons();
            FormDecision();
        }

        private void loadIcons()
        {
            try
            {
                string iconPath = Path.Combine(Application.StartupPath, "Resources", "DialogBoxIcons");

                _iconInformation = Image.FromFile(Path.Combine(iconPath, "Information.png"));
                _iconWarning = Image.FromFile(Path.Combine(iconPath, "Warning.png"));
                _iconError = Image.FromFile(Path.Combine(iconPath, "Error.png"));
                _iconSuccess = Image.FromFile(Path.Combine(iconPath, "Success.png"));
                _iconQuestion = Image.FromFile(Path.Combine(iconPath, "Question.png"));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void FormDecision()
        {
            switch (_dBoxType)
            {
                case dBoxType.Information:
                    ShowInformation();
                    break;
                case dBoxType.Warning:
                    ShowWarning();
                    break;
                case dBoxType.Error:
                    ShowError();
                    break;
                case dBoxType.Success:
                    ShowSuccess();
                    break;
                case dBoxType.Question:
                    ShowQuestion();
                    break;
            }
        }

        private void ShowInformation()
        {
            SetupDialog(_iconInformation, _title == "" ? "Information" : _title, _message, "OK");
            pnlBtn2.Visible = false;
            pnlBtn3.Visible = false;

            btn1.Click += (s, e) =>
            {
                Response = dBoxResponse.OK;
                Close();
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Response = dBoxResponse.OK;
                    Close();
                }
            };
        }

        private void ShowWarning()
        {
            SetupDialog(_iconWarning, _title == "" ? "Warning!" : _title, _message, "OK", "Cancel");

            btn1.Click += (s, e) =>
            {
                Response = dBoxResponse.OK;
                Close();
            };

            btn2.Click += (s, e) =>
            {
                Response = dBoxResponse.CANCEL;
                Close();
            };
        }

        private void ShowError()
        {
            SetupDialog(_iconError, _title == "" ? "Error!" : _title, _message, "OK");
            pnlBtn2.Visible = false;
            pnlBtn3.Visible = false;

            btn1.Click += (s, e) =>
            {
                Response = dBoxResponse.OK;
                Close();
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Response = dBoxResponse.OK;
                    Close();
                }
            };
        }

        private void ShowSuccess()
        {
            SetupDialog(_iconSuccess, _title == "" ? "Success!" : _title, _message, "OK");
            pnlBtn2.Visible = false;
            pnlBtn3.Visible = false;

            btn1.Click += (s, e) =>
            {
                Response = dBoxResponse.OK;
                Close();
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Response = dBoxResponse.OK;
                    Close();
                }
            };
        }

        private void ShowQuestion()
        {
            SetupDialog(_iconQuestion, _title == "" ? "Are you sure?" : _title, _message, "YES", "NO", "CANCEL");

            btn1.Click += (s, e) =>
            {
                Response = dBoxResponse.YES;
                Close();
            };

            btn2.Click += (s, e) =>
            {
                Response = dBoxResponse.NO;
                Close();
            };

            btn3.Click += (s, e) =>
            {
                Response = dBoxResponse.CANCEL;
                Close();
            };
        }

        private void SetupDialog(Image icon, string title, string message, string btn1Text, string? btn2Text = null, string? btn3Text = null)
        {
            pbIcon.Image = icon;
            lblTitle.Text = title;
            lblMessage.Text = message;

            btn1.Text = btn1Text;
            this.AcceptButton = btn1;

            if (btn2Text != null)
            {
                btn2.Text = btn2Text;
                pnlBtn2.Visible = true;
            }
            else
            {
                pnlBtn2.Visible = false;
            }

            if (btn3Text != null)
            {
                btn3.Text = btn3Text;
                pnlBtn3.Visible = true;
            }
            else
            {
                pnlBtn3.Visible = false;
            }
        }

        public static dBoxResponse Show(string message, dBoxType type)
        {
            return Show(null, message, type);
        }

        public static dBoxResponse Show(string? title, string message, dBoxType type)
        {
            using DialogBox box = new DialogBox(title, message, type);
            box.ShowDialog();
            return box.Response;
        }
    }
}
