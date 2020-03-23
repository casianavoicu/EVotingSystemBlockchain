using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EVotingSystem.UI.Controllers
{
    public class BlockchainController : Controller
    {
        private readonly ILogger<BlockchainController> _logger;

        public BlockchainController(ILogger<BlockchainController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}