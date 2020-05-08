using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace EVotingSystem.UI.Controllers
{
    public class BlockchainController : Controller
    {
        private readonly ILogger<BlockchainController> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient EVotingApi;

        public BlockchainController(ILogger<BlockchainController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
            EVotingApi = httpClientFactory.CreateClient("EVotingSystemApi");
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage httpResponse = await EVotingApi.GetAsync("candidate");
            return Ok();
        }

    }
}