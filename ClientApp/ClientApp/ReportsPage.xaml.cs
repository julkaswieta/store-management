using System.Net.Http;
using System.Windows.Controls;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        private readonly string ReportsApiUrlBase = "http://localhost:3007/reports";
        private string report;
        public ReportsPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btnToday_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GetReport("today");
        }

        private async void GetReport(string option)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ReportsApiUrlBase + "/" + option;
                var response = await client.GetStringAsync(url);
                if (response != null)
                {
                    report = response;
                    report = report.Substring(1, report.Length - 2);

                    txtReport.Text = report;
                }
            }
        }

        private void btnWeek_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GetReport("weekly");
        }

        private void btnMonth_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GetReport("monthly");
        }

        private void btnYear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GetReport("yearly");
        }
    }
}
