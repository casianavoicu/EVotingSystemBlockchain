using EVotingSystem.UI.API;
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
        private readonly EVotingApi EVotingApi;

        public CandidateController(ILogger<CandidateController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
            EVotingApi = new EVotingApi(httpClientFactory);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate(CreateCandidateViewModel model)
        {
            var result = await EVotingApi.SendToApi<CreateCandidateViewModel>(model, "Candidate/AddCandidate");
            //download wallet!!
            return RedirectToAction("Index", "Candidate");
        }
    }
}