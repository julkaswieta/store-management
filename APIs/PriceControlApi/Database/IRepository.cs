using PriceControlApi.Models;

namespace PriceControlApi.Database;

public interface IRepository
{
    public IEnumerable<Item> GetItems();
    public IEnumerable<Offer> GetOffers();
}