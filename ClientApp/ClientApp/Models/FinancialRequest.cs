using System.Text.Json.Serialization;

namespace ClientApp.Models.Finance
{
    public class FinancialRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public decimal AmountRequested { get; set; }
        public required string Status { get; set; }
        [JsonIgnore]
        public string? ButtonText { get; set; }

        public override string ToString()
        {
            return $"Request {Id}: Customer {CustomerId} requested £{AmountRequested} for item {ItemId} [{Status}]";
        }
    }
}
