using System.Net.Http;
using System.Net.Http.Headers;

internal class Program
{
    private static async global::System.Threading.Tasks.Task Main(string[] args)
    {
        HttpClient client = new HttpClient();

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.openpix.com.br/api/v1/charge?return_existing=true");

        request.Headers.Add("Authorization", "Q2xpZW50X0lkX2FhNDA4ZTQ5LWUwNjMtNGFmNy1hZmE1LWRmMDhhYjNhOGMzZDpDbGllbnRfU2VjcmV0Xzl5Y2dJdjNrdnhtMlhGVVdQNFArN1VVKzRNMWkvT1F4dktxTWJmejlRcEU9");

        request.Content = new StringContent("{\"correlationID\":\"123\",\"value\":1000,\"comment\":\"my-first-charge\"}");
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        HttpResponseMessage response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
    }
}