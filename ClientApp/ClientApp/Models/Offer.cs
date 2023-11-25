namespace ClientApp.Models.Offers
{
    public class Offer
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public required string Code { get; set; }
        public string ItemIds { get; set; }
        public bool LoyalCustomersOnly { get; set; }

        public override string ToString()
        {
            return "Offer " + this.Code + " for products: " + this.ItemIds;
        }
    }
}
