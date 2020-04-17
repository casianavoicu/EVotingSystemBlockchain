using EVotingSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace EVotingSystem.UI.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ILogger<CandidateController> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient EVotingApi;

        public CandidateController(ILogger<CandidateController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
            EVotingApi = httpClientFactory.CreateClient("EVotingSystemApi");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate(CreateCandidateViewModel model)
        {
            HttpResponseMessage httpResponse = await EVotingApi.GetAsync("candidate");
            //download wallet!!
            return RedirectToAction("Index", "Candidate");
        }
    }
}