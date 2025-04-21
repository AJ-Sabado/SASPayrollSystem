using DomainLayer.Models.User;
using DomainLayer.ViewModels.AttendanceLog;
using DomainLayer.ViewModels.DashboardDetails;
using DomainLayer.ViewModels.JobDeskDetails;
using PresentationLayer.Views;
using PresentationLayer.Views.Custom_Message_Box;
using ServicesLayer;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;

namespace PresentationLayer.Presenters.DashboardEmployee
{
    public class DashboardEmployeePresenter
    {
        private readonly ILogin_Form _loginForm;
        private readonly IDashboard_Employee _dashboardForm;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardEmployeePresenter(IUnitOfWork unitOfWork, IDashboard_Employee dashboardForm, ILogin_Form loginForm)
        {
            _unitOfWork = unitOfWork;
            _dashboardForm = dashboardForm;
            _loginForm = loginForm;

            _dashboardForm.Exit += Exit;
            _dashboardForm.printPayslip += OnPrintPayslip;
            _dashboardForm.downloadPayslip += OnSaveAsPdf;
        }
        //public DashboardDetailsViewModel GetDashboardDetails(IUserModel user)
        //{
        //    return _unitOfWork.GetDashboardDetails(user);
        //}
        //public IEnumerable<AttendanceLogViewModel> GetAttendanceLogs(IUserModel user)
        //{
        //    return _unitOfWork.GetAttendanceLog(user);
        //}
        //public JobDeskDetailsViewModel GetJobDeskDetails(IUserModel user)
        //{
        //    return _unitOfWork.GetJobDeskDetails(user);
        //}

        private void Exit(object? Sender, EventArgs e)
        {
            _loginForm.UsernameField = "";
            _loginForm.PasswordField = "";
            _loginForm.Show();
        }

        void OnPrintPayslip(object? Sender, EventArgs e)
        {
            /*try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string templatePath = Path.Combine(basePath, "Document Templates", "EMPLOYEE PAYSLIP TEMPLATE.xlsx");

                if (File.Exists(templatePath))
                {
                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = null;

                    try
                    {
                        workbook = excelApp.Workbooks.Open(templatePath);
                        excelApp.Visible = false; // Don't show the Excel window

                        // Get the active sheet
                        Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                        // Set paper size to A4
                        worksheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;

                        // Set margins (left, right, top, bottom)
                        worksheet.PageSetup.LeftMargin = 0.25 * 72; // 1 inch = 72 points
                        worksheet.PageSetup.RightMargin = 0.25 * 72;
                        worksheet.PageSetup.TopMargin = 0.5 * 72;
                        worksheet.PageSetup.BottomMargin = 0.5 * 72;

                        // Print the document
                        workbook.PrintOut();

                        DialogBox.Show("Print Successful!", "Your document was printed successfully and is now ready for pickup.", dBoxType.Success);
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Show("Printing Error!", "Error printing the document: " + ex.Message, dBoxType.Error);
                    }
                    finally
                    {
                        // Clean up
                        if (workbook != null)
                        {
                            workbook.Close(false);
                        }

                        excelApp.Quit();
                    }
                }
                else
                {
                    DialogBox.Show("File not found!", "The file could not be found. Please check the file path and try again.", dBoxType.Error);
                }

            }
            catch (Exception ex)
            {
                DialogBox.Show("Print Error", "An error occurred while printing the payslip: " + ex.Message, dBoxType.Error);
            }*/

            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string templatePath = Path.Combine(basePath, "Document Templates", "EMPLOYEE PAYSLIP TEMPLATE.xlsx");

                if (File.Exists(templatePath))
                {
                    using (PrintDialog printDialog = new PrintDialog())
                    {
                        // Show Print Dialog
                        if (printDialog.ShowDialog() == DialogResult.OK)
                        {
                            string selectedPrinter = printDialog.PrinterSettings.PrinterName;

                            Excel.Application excelApp = new Excel.Application();
                            Excel.Workbook workbook = null;

                            try
                            {
                                workbook = excelApp.Workbooks.Open(templatePath);
                                excelApp.Visible = false;
                                excelApp.DisplayAlerts = false;

                                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                                worksheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                                worksheet.PageSetup.LeftMargin = 0.25 * 72;
                                worksheet.PageSetup.RightMargin = 0.25 * 72;
                                worksheet.PageSetup.TopMargin = 0.5 * 72;
                                worksheet.PageSetup.BottomMargin = 0.5 * 72;

                                // Print with selected printer
                                workbook.PrintOut(1, 1, 1, false, selectedPrinter, false, false);

                                DialogBox.Show("Printing Payslip!", "Your payslip is currently being printed. Please wait a moment while we process your request.", dBoxType.Success);
                            }
                            catch (Exception ex)
                            {
                                DialogBox.Show("Printing Error!", "Error printing the document: " + ex.Message, dBoxType.Error);
                            }
                            finally
                            {
                                if (workbook != null)
                                    workbook.Close(false);

                                excelApp.Quit();
                            }
                        }
                        else
                        {
                            DialogBox.Show("Print Cancelled", "The printing operation was cancelled.", dBoxType.Error);
                        }
                    }
                }
                else
                {
                    DialogBox.Show("File not found!", "The file could not be found. Please check the file path and try again.", dBoxType.Error);
                }
            }
            catch (Exception ex)
            {
                DialogBox.Show("Print Error", "An error occurred while printing the payslip: " + ex.Message, dBoxType.Error);
            }
        }

        private void OnSaveAsPdf(object? Sender, EventArgs e)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string templatePath = Path.Combine(basePath, "Document Templates", "EMPLOYEE PAYSLIP TEMPLATE.xlsx");

            if (File.Exists(templatePath))
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = null;

                try
                {
                    // Open the Excel file
                    workbook = excelApp.Workbooks.Open(templatePath);
                    excelApp.Visible = false; // Don't show the Excel window

                    // Get the active sheet
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    // Set paper size to A4
                    worksheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;

                    // Set margins (left, right, top, bottom)
                    worksheet.PageSetup.LeftMargin = 0.25 * 72; // 1 inch = 72 points
                    worksheet.PageSetup.RightMargin = 0.25 * 72;
                    worksheet.PageSetup.TopMargin = 0.5 * 72;
                    worksheet.PageSetup.BottomMargin = 0.5 * 72;

                    // Open SaveFileDialog for user to select the destination path
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "PDF Files (*.pdf)|*.pdf",
                        Title = "Save As PDF"
                    };

                    // Show the SaveFileDialog and check if the user selected a file
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string destinationPath = saveFileDialog.FileName;

                        // Save the document as PDF to the chosen destination
                        workbook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, destinationPath);

                        // Show custom dialog box after saving
                        DialogBox.Show("Saved Successfully!", "Your document has been saved as a PDF successfully.", dBoxType.Success);
                    }
                }
                catch (Exception ex)
                {
                    DialogBox.Show("Saving Error!", "Error saving the document as PDF: " + ex.Message, dBoxType.Error);
                }
                finally
                {
                    // Clean up
                    if (workbook != null)
                    {
                        workbook.Close(false);
                    }

                    excelApp.Quit();
                }
            }
            else
            {
                DialogBox.Show("File Error!", "File not Found!", dBoxType.Error);
            }
        }


    }
}
