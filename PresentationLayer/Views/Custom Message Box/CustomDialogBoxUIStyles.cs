using PresentationLayer.Views.Utility_Classes;
using Syncfusion.WinForms.Controls;

namespace PresentationLayer.Views.Custom_Message_Box
{
    internal class CustomDialogBoxUIStyles
    {
        DialogBox _dBox;

        public CustomDialogBoxUIStyles(DialogBox dBox)
        {
            _dBox = dBox;
            MaterialSkinClass.initMaterialSkin();

        }

        
    }
}
