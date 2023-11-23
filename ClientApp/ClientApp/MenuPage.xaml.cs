using System.Windows;
using System.Windows.Controls;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void btnPriceControl_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PriceControlPage());
        }

        private void btnInventoryControl_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InventoryControlPage());
        }

        private void btnOffers_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new OffersPage());
        }

        private void btnFinanceApproval_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FinancialApprovalPage());
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ReportsPage());
        }
    }
}
