using Azure.Core;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Views.Utility_Classes
{
    public class MaterialSkinClass
    //This class is used to apply Material Skin to the forms
    {
        public static void initMaterialSkin()
        {
            MaterialSkinManager manager = MaterialSkinManager.Instance;
            manager.EnforceBackcolorOnAllComponents = false;
            manager.Theme = MaterialSkinManager.Themes.LIGHT;
            manager.ColorScheme = new ColorScheme(
                primary: Color.FromArgb(252, 184, 49),       // Main color
                darkPrimary: Color.FromArgb(220, 160, 40),   // Darker shade of main
                lightPrimary: Color.FromArgb(255, 224, 140), // Lighter shade of main
                accent: Color.White,         // Accent (you can keep or adjust as needed)
                textShade: TextShade.WHITE
            );
        }
    }
}
