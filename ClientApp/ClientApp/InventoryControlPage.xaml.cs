using ClientApp.Models.InventoryControl;
using System.Collections.ObjectModel;
using System.Net.Http;
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
        private ObservableCollection<string> items;
        private readonly HttpClient client;
        private System.Timers.Timer timer;
        public InventoryControlPage()
        {
            InitializeComponent();
            this.DataContext = this;
            client = new HttpClient();
            items = new ObservableCollection<string>();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetItems();
            timer = new System.Timers.Timer(10000);
            timer.Elapsed += async (sender, e) => await GetItems();
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
    }
}
