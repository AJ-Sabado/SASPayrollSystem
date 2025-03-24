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

        public async static void rounded(Control control, int radius, int borderThickness)
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

            control.Paint += (sender, e) =>
            {
                if (borderThickness > 0)
                {
                    using (Pen borderPen = new Pen(control.ForeColor, borderThickness))
                    using (GraphicsPath borderPath = new GraphicsPath())
                    {
                        borderPath.StartFigure();
                        borderPath.AddArc(borderThickness / 2, borderThickness / 2, (radius * 2) - borderThickness, (radius * 2) - borderThickness, 180, 90);
                        borderPath.AddArc(control.Width - ((radius * 2) - borderThickness) - borderThickness / 2, borderThickness / 2, (radius * 2) - borderThickness, (radius * 2) - borderThickness, 270, 90);
                        borderPath.AddArc(control.Width - ((radius * 2) - borderThickness) - borderThickness / 2, control.Height - ((radius * 2) - borderThickness) - borderThickness / 2, (radius * 2) - borderThickness, (radius * 2) - borderThickness, 0, 90);
                        borderPath.AddArc(borderThickness / 2, control.Height - ((radius * 2) - borderThickness) - borderThickness / 2, (radius * 2) - borderThickness, (radius * 2) - borderThickness, 90, 90);
                        borderPath.CloseFigure();

                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        e.Graphics.DrawPath(borderPen, borderPath);
                    }
                }
            };

            control.Invalidate(); // Force repaint
        }
    }

    }

