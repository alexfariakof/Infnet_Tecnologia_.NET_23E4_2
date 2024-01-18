using Integration_PIX_Adapter.Adapters.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Integration_PIX_Adapter.Adapters.OpenPix;
public class OpenPix : IChargePix
{
    private const string baseUrl = "https://api.openpix.com.br/api/v1/";
    private string Authorization = null;
    public PixCharge CreateCharge(PixCharge charge)
    {
        this.GetAuthorization();
        HttpClient client = new HttpClient();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}charge?return_existing=true");
        request.Headers.Add("Authorization", Authorization);
        request.Content = new StringContent(JsonConvert.SerializeObject(new { value = charge.Value, correlationID = charge.CorrelationID }));
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = client.SendAsync(request).Result;
        response.EnsureSuccessStatusCode();
        string responseBody = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<PixCharge>(responseBody);
    }
    public bool IsChargeApporve(Guid correlationID)
    {
        throw new NotImplementedException();
    }

    private void GetAuthorization()
    {
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

        if (File.Exists(jsonFilePath))
        {
            var jsonContent = File.ReadAllText(jsonFilePath);
            var config = JObject.Parse(jsonContent);
            this.Authorization = config["OpenPIX"]?["Authorization"]?.ToString();            
        }
        else
        {
            throw new ArgumentException("Arquivo com chave de autenticação não encontrado");
        }
    }

}
