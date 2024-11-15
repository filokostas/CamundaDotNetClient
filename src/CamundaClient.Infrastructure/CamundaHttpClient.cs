namespace CamundaClient.Infrastructure;

public static class CamundaHttpClient
{
    public static HttpClient Create(string baseUrl)
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        return client;
    }
}