using System.Net.Http;

namespace EVotingSystem.UI.API
{
    public class EVotingApi
    {
        private readonly IHttpClientFactory httpClient;

        public EVotingApi(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
          
        }

        public HttpClient Create(EVotingApi eVoting)
        {
            return httpClient.CreateClient("EVotingSystemApi");
        }
    }
}
