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
}