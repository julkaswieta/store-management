using System.Text.Json;
using PriceControlApi.Models;

namespace PriceControlApi.Database;

public class Repository : IRepository
{
    private readonly ItemContext itemContext;
    private readonly OfferContext offerContext;

    public Repository(ItemContext itemContext, OfferContext offerContext)
    {
        this.itemContext = itemContext;
        this.offerContext = offerContext;
    }

    public IEnumerable<Item> GetItems()
    {
        LoadItems();
        return itemContext.Items.OrderBy(p => p.Id).ToList();
    }

    public IEnumerable<Offer> GetOffers()
    {
        LoadOffers();
        return offerContext.Offers.OrderBy(p => p.Id).ToList();
    }

    public void UpdateItem(Item item)
    {
        LoadItems();
        itemContext.Remove(itemContext.Items.Find(item.Id));
        itemContext.Items.Add(item);
        itemContext.SaveChanges();
        SaveItems();
    }

    private void LoadItems()
    {
        itemContext.Items.RemoveRange(itemContext.Items);
        itemContext.SaveChanges();
        using (StreamReader reader = new StreamReader("./Data/items.json"))
        {
            string jsonString = reader.ReadToEnd();
            var items = JsonSerializer.Deserialize<List<Item>>(jsonString);
            itemContext.Items.AddRange(items);
            itemContext.SaveChanges();
        }
    }

    private void SaveItems()
    {
        using (StreamWriter writer = new StreamWriter("./Data/items.json"))
        {
            var items = JsonSerializer.Serialize<List<Item>>(itemContext.Items.ToList());
            writer.Write(items);
        }
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