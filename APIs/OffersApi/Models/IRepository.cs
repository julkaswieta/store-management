namespace OffersApi.Models;

public interface IRepository
{
    public IEnumerable<Offer> GetOffers();

    public void UpdateOffer(Offer offer);

    public void AddOffer(Offer offer);

    public void DeleteOffer(int id);
}