using PresentationLayer.Views.Edit_Information;
using PresentationLayer.Views.Utility_Classes;


namespace PresentationLayer.Views
{
    public partial class EditPersonalInformation_View : Form, IEditPersonalInformation_View
    {
        public EditPersonalInformation_View()
        {
            InitializeComponent();

            MaterialSkinClass.initMaterialSkin();
        }

        public void setFieldEmployeeInfo()
        {
            throw new NotImplementedException();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
