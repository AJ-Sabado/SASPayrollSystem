using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Presenters.FileLeave
{
    internal interface IFileLeave_Presenter
    {
        public void showEmployeeData();
        public void fetchLeaveDetails();
        public void fetchDocument();
        public void downloadLeaveForm();

    }
}
