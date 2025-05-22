using LiveCharts;
using LiveCharts.Wpf;
using System.Globalization;
using System.Windows.Media;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminDashPage_ViewModel:Base_ViewModel
    {
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection PayrollSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public AdminDashPage_ViewModel()
        {
            LoadPieChartData();
            LoadLineChartData();
        }

        private void LoadPieChartData()
        {
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Independent\nContractor",
                    Values = new ChartValues<double> { 45 },
                    Fill = Brushes.SteelBlue,
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y}"
                },
                new PieSeries
                {
                    Title = "Regular",
                    Values = new ChartValues<double> { 55 },
                    Fill = Brushes.DodgerBlue,
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y}"
                }
            };
        }

        private void LoadLineChartData()
        {
            // Example payroll data for the last 5 bi-weekly periods
            var payrollAmounts = new ChartValues<double> { 250000, 265000, 258000, 270000, 268500 };
            var payrollDates = new[]
            {
                DateTime.Today.AddDays(-14 * 4).ToString("MMM dd"),
                DateTime.Today.AddDays(-14 * 3).ToString("MMM dd"),
                DateTime.Today.AddDays(-14 * 2).ToString("MMM dd"),
                DateTime.Today.AddDays(-14 * 1).ToString("MMM dd"),
                DateTime.Today.ToString("MMM dd")
            };

            PayrollSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Payroll",
                    Values = payrollAmounts,
                    PointGeometry = DefaultGeometries.Circle,
                    Stroke = Brushes.SteelBlue,
                    Fill = Brushes.LightSteelBlue,
                    StrokeThickness = 2,
                    LineSmoothness = 0.3
                }
            };

            Labels = payrollDates;
            Formatter = value => $"₱{value:N0}";

        }
    }
}

