using ClientApp.Models.Alerts;
using ClientApp.Models.InventoryControl;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for InventoryControl.xaml
    /// </summary>
    public partial class InventoryControlPage : Page
    {
        private readonly string InventoryControlApiUrl = "http://localhost:3003/inventory";
        private readonly string HeadquartersApiUrl = "http://localhost:3002/request";
        private readonly HttpClient client;
        private System.Timers.Timer timer;

        public InventoryControlPage()
        {
            InitializeComponent();
            this.DataContext = this;
            client = new HttpClient();
            timer = new System.Timers.Timer(10000);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            timer.Dispose();
            this.NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetItems();
            GetAlerts();
            timer.Elapsed += async (sender, e) =>
            {
                await GetItems();
                await GetAlerts();
            };
            timer.AutoReset = true;
            timer.Start();
        }

        private async Task GetItems()
        {
            try
            {
                string url = InventoryControlApiUrl + "/items";
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string responseBody = await client.GetStringAsync(url);
                var _items = JsonSerializer.Deserialize<ObservableCollection<Item>>(responseBody, options);
                ObservableCollection<string> tempItems = new ObservableCollection<string>();
                foreach (var item in _items)
                {
                    tempItems.Add(item.ToString());
                }
                Dispatcher.InvokeAsync(() => lstStock.ItemsSource = tempItems);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Inventory control service not connected");
            }
        }

        private async Task GetAlerts()
        {
            try
            {
                string url = InventoryControlApiUrl + "/alerts";
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string responseBody = await client.GetStringAsync(url);
                var alerts = JsonSerializer.Deserialize<ObservableCollection<Alert>>(responseBody, options);
                ObservableCollection<AlertListItem> tempAlerts = new ObservableCollection<AlertListItem>();
                foreach (var alert in alerts)
                {
                    var alertItem = new AlertListItem(alert);
                    tempAlerts.Add(alertItem);
                }
                Dispatcher.InvokeAsync(() => lstAlerts.ItemsSource = tempAlerts);
            }
            catch (HttpRequestException ex)
            {
            }
        }

        private async void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var alertItem = button.DataContext as AlertListItem;
            UpdateAlert(alertItem.alert);
            PlaceStockOrder(alertItem.alert.ItemId);
            var alerts = lstAlerts.ItemsSource as ObservableCollection<AlertListItem>;
            alerts.Remove(alertItem);
            lstAlerts.ItemsSource = alerts;
        }

        private async void UpdateAlert(Alert alert)
        {
            string url = InventoryControlApiUrl + "/alerts/" + alert.Id;
            string json = JsonSerializer.Serialize<Alert>(alert);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(url, content);
        }

        private async void PlaceStockOrder(int itemId)
        {
            string url = HeadquartersApiUrl + "/" + itemId;
            string response = await client.GetStringAsync(url);
            MessageBox.Show(response);
        }
    }
}
