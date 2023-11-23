using ClientApp.Models.PriceControl;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp;

/// <summary>
/// Interaction logic for PriceControl.xaml
/// </summary>
public partial class PriceControlPage : Page
{
    public ObservableCollection<Item>? items;

    private readonly string PriceControlAPI = "http://localhost:3004/items";
    public PriceControlPage()
    {
        InitializeComponent();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        LoadPrices();
    }

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
        this.NavigationService.GoBack();
    }

    private async void LoadPrices()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var response = await client.GetStringAsync(PriceControlAPI);
                items = JsonSerializer.Deserialize<ObservableCollection<Item>>(response, options)!;
                foreach (var item in items) { item.PropertyChanged += Item_PropertyChanged; }
                dgItems.ItemsSource = items;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Not connected to the Price Control service");
            }
        }
    }

    private async void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var item = sender as Item;
        using (HttpClient client = new HttpClient())
        {
            string url = PriceControlAPI + "/" + item.Id;
            string json = JsonSerializer.Serialize<Item>(item);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(url, content);
        }
    }
}
