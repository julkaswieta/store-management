using System.Windows.Controls;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for FinancialApproval.xaml
    /// </summary>
    public partial class FinancialApprovalPage : Page
    {
        public FinancialApprovalPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
