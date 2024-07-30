public interface IHttpClientFactory
{
    HttpClient CreateClient();
}

public class MyHttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:44398/api");
        client.Timeout = TimeSpan.FromSeconds(30);
        return client;
    }
}
