using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Media;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardIC
{
    public class ICDashboard_ViewModel
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public ICDashboard_ViewModel()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Daily\nHours",
                    Values = new ChartValues<double> { 4, 6, 5, 2, 4, 1, 0.5 },
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 5,
                    Stroke = Brushes.DodgerBlue,
                    Fill = Brushes.Transparent
                }
            };

            DateTime today = DateTime.Today;

            int daysSinceSunday = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-daysSinceSunday);

            Labels = Enumerable.Range(0, 7)
                .Select(i =>
                {
                    DateTime date = sunday.AddDays(i);
                    return date.ToString("MMM dd"); // e.g. "May 18"
                })
                .ToArray();


            YFormatter = value => $"{value:0.##}";
        }
    }
}
