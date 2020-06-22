using EVotingSystem.UI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EVotingSystem.UI.API
{
    public class EVotingApi
    {
        private readonly IHttpClientFactory httpClient;
        private readonly HttpClient Api;

        public EVotingApi(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
            Api = httpClient.CreateClient("EVotingSystemApi");
        }

        public async Task<T> SendToApi<T>(object body, string url)
        {
            string jsonBody = JsonConvert.SerializeObject(body);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await Api.PostAsync(url, content);
            string responseString = await httpResponse.Content.ReadAsStringAsync();
            T response = JsonConvert.DeserializeObject<T>(responseString);

            return response;
        }
        public async Task<T> GetFromApi <T>(string url)
        {
            HttpResponseMessage httpResponse = await Api.GetAsync(url);
            string responseString = await httpResponse.Content.ReadAsStringAsync();
            T response = JsonConvert.DeserializeObject<T>(responseString);

            return response;
        }
    }
}
