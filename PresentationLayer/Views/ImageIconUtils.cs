using System;
using System.Collections.Generic;
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

    }
}
