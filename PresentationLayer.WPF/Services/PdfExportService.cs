using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PresentationLayer.WPF.Services
{
    internal class PdfExportService : IPdfExportService
    {
        public void SaveUIElementAsPdf(FrameworkElement element)
        {
            // Create print dialog without showing it to user
            PrintDialog printDialog = new PrintDialog();

            // Set the Microsoft Print to PDF printer
            printDialog.PrintQueue = new PrintQueue(new PrintServer(), "Microsoft Print to PDF");

            // Get the XPS document writer
            var writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);

            // Setup the paginator
            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            element.Measure(pageSize);
            element.Arrange(new Rect(new Point(0, 0), pageSize));

            // Setup print ticket
            var printTicket = printDialog.PrintTicket;
            printTicket.OutputColor = OutputColor.Color;
            printTicket.PageOrientation = PageOrientation.Portrait;

            // Create document paginator
            var documentPaginator = new VisualPaginator(element, pageSize);

            // Set job description and output file
            printDialog.PrintQueue.CurrentJobSettings.Description = "Payslip PDF";

            // Print to PDF
            writer.Write(documentPaginator, printTicket);
        }

        private class VisualPaginator : DocumentPaginator
        {
            private readonly FrameworkElement _visual;
            private readonly Size _pageSize;

            public VisualPaginator(FrameworkElement visual, Size pageSize)
            {
                _visual = visual;
                _pageSize = pageSize;
            }

            public override DocumentPage GetPage(int pageNumber)
            {
                return new DocumentPage(_visual, _pageSize, new Rect(_pageSize), new Rect(_pageSize));
            }

            public override bool IsPageCountValid => true;
            public override int PageCount => 1;
            public override Size PageSize
            {
                get => _pageSize;
                set { }
            }
            public override IDocumentPaginatorSource Source => null;
        }
    }
}