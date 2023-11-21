
using System.Text.Json;

namespace EnablingApi.Models;

public class Repository : IRepository
{
    private readonly RequestContext requestContext;
    public Repository(RequestContext requestContext)
    {
        this.requestContext = requestContext;
    }

    public IEnumerable<Request> GetRequests()
    {
        LoadRequests();
        return requestContext.Requests.OrderBy(p => p.Id).ToList();
    }

    public void UpdateRequest(Request request)
    {
        LoadRequests();
        requestContext.Remove(requestContext.Requests.Find(request.Id));
        requestContext.Requests.Add(request);
        requestContext.SaveChanges();
        SaveRequests();
    }

    private void LoadRequests()
    {
        using (StreamReader reader = new StreamReader("./Data/requests.json"))
        {
            requestContext.Requests.RemoveRange(requestContext.Requests);
            requestContext.SaveChanges();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string json = reader.ReadToEnd();
            var requests = JsonSerializer.Deserialize<List<Request>>(json, options);
            requestContext.Requests.AddRange(requests);
            requestContext.SaveChanges();
        }
    }

    private void SaveRequests()
    {
        using (StreamWriter writer = new StreamWriter("./Data/requests.json"))
        {
            var requests = JsonSerializer.Serialize<List<Request>>(requestContext.Requests.ToList());
            writer.Write(requests);
        }
    }
}