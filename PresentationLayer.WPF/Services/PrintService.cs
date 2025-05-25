using ServicesLayer;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PresentationLayer.WPF.Services
{
    public class PrintService : IPrintService
    {
        private PrintTemplateType _currentPrintTemplate = PrintTemplateType.PayslipRegTemplate;

        public void PrintWindow(Window window)
        {
            PrintDialog printDialog = new PrintDialog();

            // Get default printer without showing dialog
            PrintQueue defaultPrinter = GetDefaultPrinter();
            if (defaultPrinter != null)
            {
                printDialog.PrintQueue = defaultPrinter;

                if (window.Content is Visual visual)
                {
                    // Auto-determine orientation based on window content
                    SetOptimalOrientation(printDialog, window.Content as FrameworkElement);
                    printDialog.PrintVisual(visual, "Payslip Print");
                }
                else
                {
                    MessageBox.Show("Nothing to print.");
                }
            }
            else
            {
                MessageBox.Show("No default printer found.");
            }
        }

        public void PrintUserControl(UserControl control)
        {
            PrintDialog printDialog = new PrintDialog();

            // Get default printer without showing dialog
            PrintQueue defaultPrinter = GetDefaultPrinter();
            if (defaultPrinter != null)
            {
                printDialog.PrintQueue = defaultPrinter;

                if (control is Visual visual)
                {
                    // Auto-determine orientation based on control dimensions
                    SetOptimalOrientation(printDialog, control);

                    // Get page size after setting orientation
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
            else
            {
                MessageBox.Show("No default printer found.");
            }
        }

        // Alternative methods with manual printer selection
        public void PrintWindowWithPrinter(Window window, string printerName)
        {
            PrintDialog printDialog = new PrintDialog();

            PrintQueue printer = GetPrinterByName(printerName);
            if (printer != null)
            {
                printDialog.PrintQueue = printer;

                if (window.Content is Visual visual)
                {
                    SetOptimalOrientation(printDialog, window.Content as FrameworkElement);
                    printDialog.PrintVisual(visual, "Payslip Print");
                }
                else
                {
                    MessageBox.Show("Nothing to print.");
                }
            }
            else
            {
                MessageBox.Show($"Printer '{printerName}' not found.");
            }
        }

        public void PrintUserControlWithPrinter(UserControl control, string printerName)
        {
            PrintDialog printDialog = new PrintDialog();

            PrintQueue printer = GetPrinterByName(printerName);
            if (printer != null)
            {
                printDialog.PrintQueue = printer;

                if (control is Visual visual)
                {
                    SetOptimalOrientation(printDialog, control);

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
            else
            {
                MessageBox.Show($"Printer '{printerName}' not found.");
            }
        }

        private PrintQueue GetDefaultPrinter()
        {
            try
            {
                LocalPrintServer printServer = new LocalPrintServer();
                return printServer.DefaultPrintQueue;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting default printer: {ex.Message}");
                return null;
            }
        }

        private PrintQueue GetPrinterByName(string printerName)
        {
            try
            {
                LocalPrintServer printServer = new LocalPrintServer();
                return printServer.GetPrintQueue(printerName);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting printer '{printerName}': {ex.Message}");
                return null;
            }
        }

        private void SetOptimalOrientation(PrintDialog printDialog, FrameworkElement element)
        {
            if (element == null || printDialog.PrintQueue == null)
                return;

            try
            {
                // Get the element's desired size
                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Size elementSize = element.DesiredSize;

                // If element doesn't have a meaningful size, use actual dimensions
                if (elementSize.Width == 0 || elementSize.Height == 0)
                {
                    elementSize = new Size(element.ActualWidth, element.ActualHeight);
                }

                // If still no meaningful size, try getting render size
                if (elementSize.Width == 0 || elementSize.Height == 0)
                {
                    elementSize = element.RenderSize;
                }

                // Determine optimal orientation based on content aspect ratio
                bool contentIsWider = elementSize.Width > elementSize.Height;

                // Get print capabilities
                PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities();

                // Set page orientation based on content
                if (capabilities.PageOrientationCapability != null)
                {
                    PrintTicket printTicket = printDialog.PrintQueue.DefaultPrintTicket.Clone();

                    if (contentIsWider)
                    {
                        // Content is wider - use landscape
                        if (capabilities.PageOrientationCapability.Contains(PageOrientation.Landscape))
                        {
                            printTicket.PageOrientation = PageOrientation.Landscape;
                        }
                        else if (capabilities.PageOrientationCapability.Contains(PageOrientation.ReverseLandscape))
                        {
                            printTicket.PageOrientation = PageOrientation.ReverseLandscape;
                        }
                    }
                    else
                    {
                        // Content is taller - use portrait
                        if (capabilities.PageOrientationCapability.Contains(PageOrientation.Portrait))
                        {
                            printTicket.PageOrientation = PageOrientation.Portrait;
                        }
                        else if (capabilities.PageOrientationCapability.Contains(PageOrientation.ReversePortrait))
                        {
                            printTicket.PageOrientation = PageOrientation.ReversePortrait;
                        }
                    }

                    // Apply the print ticket
                    printDialog.PrintTicket = printTicket;
                }
            }
            catch (System.Exception ex)
            {
                // If auto-orientation fails, continue with default settings
                System.Diagnostics.Debug.WriteLine($"Auto-orientation failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Alternative method that also considers page margins and scaling
        /// </summary>
        private void SetOptimalOrientationAdvanced(PrintDialog printDialog, FrameworkElement element)
        {
            if (element == null || printDialog.PrintQueue == null)
                return;

            try
            {
                PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities();
                PrintTicket printTicket = printDialog.PrintQueue.DefaultPrintTicket.Clone();

                // Get page dimensions for both orientations
                double pageWidth = capabilities.PageImageableArea?.ExtentWidth ?? 816; // ~8.5" at 96 DPI
                double pageHeight = capabilities.PageImageableArea?.ExtentHeight ?? 1056; // ~11" at 96 DPI

                // Measure element
                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Size elementSize = element.DesiredSize;

                if (elementSize.Width == 0 || elementSize.Height == 0)
                {
                    elementSize = new Size(element.ActualWidth, element.ActualHeight);
                }

                // Calculate scaling factors for both orientations
                double portraitScaleX = pageWidth / elementSize.Width;
                double portraitScaleY = pageHeight / elementSize.Height;
                double portraitScale = Math.Min(portraitScaleX, portraitScaleY);

                double landscapeScaleX = pageHeight / elementSize.Width; // Swapped for landscape
                double landscapeScaleY = pageWidth / elementSize.Height;  // Swapped for landscape
                double landscapeScale = Math.Min(landscapeScaleX, landscapeScaleY);

                // Choose orientation that provides better scaling (closer to 1.0 is better)
                bool useLandscape = Math.Abs(1.0 - landscapeScale) < Math.Abs(1.0 - portraitScale);

                if (capabilities.PageOrientationCapability != null)
                {
                    if (useLandscape)
                    {
                        if (capabilities.PageOrientationCapability.Contains(PageOrientation.Landscape))
                        {
                            printTicket.PageOrientation = PageOrientation.Landscape;
                        }
                    }
                    else
                    {
                        if (capabilities.PageOrientationCapability.Contains(PageOrientation.Portrait))
                        {
                            printTicket.PageOrientation = PageOrientation.Portrait;
                        }
                    }

                    printDialog.PrintTicket = printTicket;
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Advanced auto-orientation failed: {ex.Message}");
            }
        }

        public void SetCurrentPrintTemplate(PrintTemplateType templateType)
        {
            _currentPrintTemplate = templateType;
        }

        public PrintTemplateType GetCurrentPrintTemplate()
        {
            return _currentPrintTemplate;
        }
    }
}