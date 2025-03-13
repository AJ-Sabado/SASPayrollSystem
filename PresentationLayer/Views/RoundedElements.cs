using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Views
{
    internal class RoundedElements
    {
        public async static void rounded(Control control, int radius)
        {
            if (control == null || radius < 1 || control.Width < radius * 2 || control.Height < radius * 2)
                return;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                path.AddArc(control.Width - (radius * 2), 0, radius * 2, radius * 2, 270, 90);
                path.AddArc(control.Width - (radius * 2), control.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
                path.AddArc(0, control.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
                path.CloseFigure();

                control.Region = new Region(path);
            }

            control.Invalidate(); // Force repaint
        }
    }

    }

