using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EVotingSystem.UI.Controllers
{
    public class PartyController : Controller
    {
        private readonly ILogger<PartyController> _logger;

        public PartyController(ILogger<PartyController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}