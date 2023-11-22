using System.Text.Json;

namespace OffersApi.Models;

public class Repository : IRepository
{
    private readonly OfferContext offerContext;

    public Repository(OfferContext offerContext)
    {
        this.offerContext = offerContext;
    }

    public IEnumerable<Offer> GetOffers()
    {
        LoadOffers();
        return offerContext.Offers.OrderBy(p => p.Id).ToList();
    }

    private void LoadOffers()
    {
        offerContext.Offers.RemoveRange(offerContext.Offers);
        offerContext.SaveChanges();
        using (StreamReader reader = new StreamReader("./Data/offers.json"))
        {
            string jsonString = reader.ReadToEnd();
            var offers = JsonSerializer.Deserialize<List<Offer>>(jsonString);
            offerContext.Offers.AddRange(offers);
            offerContext.SaveChanges();
        }
    }

    public void UpdateOffer(Offer offer)
    {
        LoadOffers();
        offerContext.Remove(offerContext.Offers.Find(offer.Id));
        offerContext.Offers.Add(offer);
        offerContext.SaveChanges();
        SaveOffers();
    }

    private void SaveOffers()
    {
        using (StreamWriter writer = new StreamWriter("./Data/offers.json"))
        {
            var offers = JsonSerializer.Serialize<List<Offer>>(offerContext.Offers.ToList());
            writer.Write(offers);
        }
    }

    public void AddOffer(Offer offer)
    {
        LoadOffers();
        offerContext.Offers.Add(offer);
        offerContext.SaveChanges();
        SaveOffers();
    }

    public void DeleteOffer(int id)
    {
        LoadOffers();
        offerContext.Offers.Remove(offerContext.Offers.Find(id));
        offerContext.SaveChanges();
        SaveOffers();
    }
}