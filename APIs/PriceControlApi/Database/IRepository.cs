using PriceControlApi.Models;

namespace PriceControlApi.Database;

public interface IRepository
{
    public IEnumerable<Item> GetItems();
    public IEnumerable<Offer> GetOffers();

    public void UpdateItem(Item item);

    public void UpdateOffer(Offer offer);

    public void AddOffer(Offer offer);

    public void DeleteOffer(int id);
}