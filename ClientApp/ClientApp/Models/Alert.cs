namespace ClientApp.Models.Alerts
{
    public class Alert
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public required string DateTriggered { get; set; }

        public override string ToString()
        {
            return this.Id + ", Item: " + this.ItemId + ", alert triggerred: " + this.DateTriggered;
        }
    }
}
