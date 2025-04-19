using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Views.Forgot_Password_Forms
{
    public interface IForgotPassword_View
    {

        event EventHandler NextClick;
        event EventHandler NextClick2;
        event EventHandler CancelClick;

        void Show();
        void Close();
        void btnNext_Click();
        void btnNext2_Click();
    }
}
