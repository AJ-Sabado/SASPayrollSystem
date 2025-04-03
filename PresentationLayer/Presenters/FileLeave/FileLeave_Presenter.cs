

using PresentationLayer.Views.FileLeaveForm;

namespace PresentationLayer.Presenters.FileLeave
{
    internal class FileLeave_Presenter : IFileLeave_Presenter
    {
        private readonly IFileLeave_Form _view;
        private byte[] fileData;

        public FileLeave_Presenter(IFileLeave_Form view)
        {
            _view = view;
        }
        public void downloadLeaveForm()
        {
            throw new NotImplementedException();
        }

        public void fetchDocument()
        {
            OpenFileDialog fdProof = new OpenFileDialog();

            fdProof.Title = "Attach Proof of Attendance";
            fdProof.Filter = "PDF Files|*.pdf";

            if (fdProof.ShowDialog() == DialogResult.OK)
            {
                _view.AttachmentFileName = Path.GetFileName(fdProof.FileName);
                fileData = File.ReadAllBytes(fdProof.FileName);
            }
        }

        public void fetchLeaveDetails()
        {
            throw new NotImplementedException();
        }

        public void showEmployeeData()
        {
            throw new NotImplementedException();
        }
    }
}
