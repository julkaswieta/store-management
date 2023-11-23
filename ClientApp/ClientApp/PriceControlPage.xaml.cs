using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp
{
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
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var response = await client.GetStringAsync(PriceControlAPI);
                items = JsonSerializer.Deserialize<ObservableCollection<Item>>(response, options)!;
                foreach (var item in items) { item.PropertyChanged += Item_PropertyChanged; }
                dgItems.ItemsSource = items;
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as Item;
            MessageBox.Show("Price changed to: " + item.Price);
        }


    }

    public partial class Item : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }

        private decimal price;
        public decimal Price
        {
            get => price;
            set
            {
                if (price != value)
                {
                    price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
