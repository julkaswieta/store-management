namespace ClientApp.Models.Offers
{
    public class AllOffersItem
    {

        public AllOffersItem(Offer offer)
        {
            this.offer = offer;
            OfferString = offer.ToString();
            LoyalCustomersOnly = offer.LoyalCustomersOnly;
            CheckboxText = LoyalCustomersOnly ? "Loyals only" : "All customers";
        }

        public bool LoyalCustomersOnly { get; set; }
        public string CheckboxText { get; set; }
        public string OfferString { get; set; }

        public Offer offer;

    }
}
