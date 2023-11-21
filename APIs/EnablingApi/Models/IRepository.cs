namespace EnablingApi.Models;

public interface IRepository
{
    public IEnumerable<Request> GetRequests();
    public void UpdateRequest(Request request);
}