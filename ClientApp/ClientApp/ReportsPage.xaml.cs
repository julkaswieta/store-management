using ClientApp.Models.Reports;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
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
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = ReportsApiUrlBase + "/" + option;
                    var response = await client.GetStringAsync(url);
                    if (response != null)
                    {
                        Report report = JsonSerializer.Deserialize<Report>(response)!;
                        txtCode.Text = report.Code;
                        txtCustomers.Text = FormatNumber(report.Customers);
                        txtIncome.Text = "£ " + FormatMoney(report.Income);
                        txtProfit.Text = "£ " + FormatMoney(report.Profit);
                        txtPurchases.Text = FormatNumber(report.Purchases);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Reports service not connected");
            }
            catch { }
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

        private string FormatMoney(long number)
        {
            return $"{number:n}";
        }
        private string FormatNumber(long number)
        {
            return $"{number:n0}";
        }
    }
}
