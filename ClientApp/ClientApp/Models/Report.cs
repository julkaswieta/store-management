using System.Text.Json.Serialization;

namespace ClientApp.Models.Reports
{
    public class Report
    {
        [JsonPropertyName("Report Code")]
        public string Code { get; set; }

        [JsonPropertyName("Number of Customers")]
        public long Customers { get; set; }

        [JsonPropertyName("Total income")]
        public long Income { get; set; }

        [JsonPropertyName("Total profit")]
        public long Profit { get; set; }

        [JsonPropertyName("Number of purchases")]
        public long Purchases { get; set; }
    }
}
