using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Views
{
    internal class ImageIconUtils
    {
        public static Bitmap InvertImageColors(Bitmap original)
        {
            Bitmap invertedImage = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color originalColor = original.GetPixel(x, y);
                    // Invert the color
                    Color invertedColor = Color.FromArgb(originalColor.A, 255 - originalColor.R, 255 - originalColor.G, 255 - originalColor.B);
                    invertedImage.SetPixel(x, y, invertedColor);
                }
            }

            return invertedImage;
        }

        public static Bitmap ChangeIconColor(Bitmap original, Color newColor)
        {
            Bitmap newIcon = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(newIcon))
            {
                for (int x = 0; x < original.Width; x++)
                {
                    for (int y = 0; y < original.Height; y++)
                    {
                        Color originalColor = original.GetPixel(x, y);
                        // Blend the original color with the new color
                        Color blendedColor = Color.FromArgb(originalColor.A,
                                                            (originalColor.R + newColor.R) / 2,
                                                            (originalColor.G + newColor.G) / 2,
                                                            (originalColor.B + newColor.B) / 2);
                        newIcon.SetPixel(x, y, blendedColor);
                    }
                }
            }
            return newIcon;
        }

    }
}
