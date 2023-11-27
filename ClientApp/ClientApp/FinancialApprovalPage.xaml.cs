using ClientApp.Models.Finance;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for FinancialApproval.xaml
    /// </summary>
    public partial class FinancialApprovalPage : Page
    {
        private readonly string FinanceApprovalApiUrl = "http://localhost:3008/requests";
        HttpClient client;
        public FinancialApprovalPage()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private void btnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ObservableCollection<FinancialRequest> requests = new ObservableCollection<FinancialRequest>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var response = await client.GetStringAsync(FinanceApprovalApiUrl);
                    requests = JsonSerializer.Deserialize<ObservableCollection<FinancialRequest>>(response, options)!;
                    ObservableCollection<RequestItem> requestItems = new ObservableCollection<RequestItem>();
                    foreach (var request in requests)
                    {
                        requestItems.Add(new RequestItem(request));
                    }
                    dgRequests.ItemsSource = requestItems;
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show("Not connected to the Financial Control service");
                }
            }
        }

        private async void btnApproveRequest_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext as RequestItem;
            if (button.Content.ToString().Equals("Approve"))
            {
                item.Request.Status = "Approved";
                item.ButtonText = "Reject";
                item.RequestText = item.Request.ToString();
            }
            else
            {
                item.Request.Status = "Rejected";
                item.ButtonText = "Approve";
                item.RequestText = item.Request.ToString();
            }
            var requests = dgRequests.ItemsSource as ObservableCollection<RequestItem>;
            requests.Remove(item);
            requests.Add(item);
            dgRequests.ItemsSource = requests;

            string url = FinanceApprovalApiUrl + "/" + item.Request.Id;
            string json = JsonSerializer.Serialize<FinancialRequest>(item.Request);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(url, content);
        }
    }
}