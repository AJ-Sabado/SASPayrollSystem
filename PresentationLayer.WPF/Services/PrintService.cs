using ServicesLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PresentationLayer.WPF.Services
{
    public class PrintService : IPrintService
    {
        public void PrintWindow(Window window)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // Optional: You can specify which visual element to print. Here we print the entire window content.
                if (window.Content is Visual visual)
                {
                    printDialog.PrintVisual(visual, "Payslip Print");
                }
                else
                {
                    MessageBox.Show("Nothing to print.");
                }
            }
        }

        public void PrintUserControl(UserControl control)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                if (control is Visual visual)
                {
                    // Optional: You can transform layout before printing
                    Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                    control.Measure(pageSize);
                    control.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));

                    printDialog.PrintVisual(visual, "UserControl Print");
                }
                else
                {
                    MessageBox.Show("Invalid control to print.");
                }
            }
        }
    }
}
