using EVotingSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nodes;
using System.Collections.Generic;
using System.Net.Http;

namespace EVotingSystem.UI.Controllers
{
    public class BlockchainController : Controller
    {
        private readonly ILogger<BlockchainController> _logger;

        public BlockchainController(ILogger<BlockchainController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Blocks()
        {
            var response  = Client.Connect("127.0.0.1", "BlockHistory", 9, 13000);
            var final =  JsonConvert.DeserializeObject<List<BlockViewModel>>(response);
            return View(final);
        }

        public IActionResult Transactions()
        {
            return View();
        }

    }
}