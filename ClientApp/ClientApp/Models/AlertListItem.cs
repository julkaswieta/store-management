namespace ClientApp.Models.Alerts
{
    public class AlertListItem
    {
        public AlertListItem(Alert alert)
        {
            this.alert = alert;
            AlertString = alert.ToString();
            ButtonText = "Order";
        }

        public string ButtonText { get; set; }
        public string AlertString { get; set; }

        public Alert alert;
    }
}
