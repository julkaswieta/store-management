using ClientApp.Models.Offers;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for Offers.xaml
    /// </summary>
    public partial class OffersPage : Page
    {
        private readonly HttpClient client;
        private readonly string OffersApiUrl = "http://localhost:3006/offers";
        private ObservableCollection<Offer> offers;
        public OffersPage()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var response = await client.GetStringAsync(OffersApiUrl);
                offers = JsonSerializer.Deserialize<ObservableCollection<Offer>>(response, options)!;
                var offerItems = new ObservableCollection<AllOffersItem>();
                foreach (var offer in offers)
                {
                    offerItems.Add(new AllOffersItem(offer));
                }
                lstAllOffers.ItemsSource = offerItems;
            }
            catch
            {
                MessageBox.Show("Not connected to the Offers service");
            }
        }

        public void ChangeTargetCustomers(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            var item = checkbox.DataContext as AllOffersItem;
            item.offer.LoyalCustomersOnly = item.LoyalCustomersOnly;
            item.LoyalCustomersOnly = item.LoyalCustomersOnly;
            checkbox.Content = item.LoyalCustomersOnly ? "Loyals only" : "All customers";
            UpdateOffer(item.offer);
        }

        private async void UpdateOffer(Offer offer)
        {
            string url = OffersApiUrl + "/" + offer.Id;
            string json = JsonSerializer.Serialize<Offer>(offer);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(url, content);
        }

        private void btnAddOffer_Click(object sender, RoutedEventArgs e)
        {
            var item = offers.OrderByDescending(p => p.Id).FirstOrDefault();

            if (item != null)
            {
                Offer offer = new Offer
                {
                    Id = item.Id + 1,
                    Code = txtCode.Text,
                    Description = txtDescription.Text,
                    ItemIds = txtItemIds.Text,
                    LoyalCustomersOnly = (bool)cbLoyalsOnly.IsChecked
                };
                var items = lstAllOffers.ItemsSource as ObservableCollection<AllOffersItem>;
                items.Add(new AllOffersItem(offer));
                lstAllOffers.ItemsSource = items;
                AddOffer(offer);
            }
        }

        private async void AddOffer(Offer offer)
        {
            string json = JsonSerializer.Serialize<Offer>(offer);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(OffersApiUrl, content);
        }
    }
}
