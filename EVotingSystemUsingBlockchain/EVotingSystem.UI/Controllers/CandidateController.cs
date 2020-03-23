using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EVotingSystem.UI.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ILogger<CandidateController> _logger;

        public CandidateController(ILogger<CandidateController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}