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

        public BlockchainController(ILogger<BlockchainController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var response = Client.Connect("127.0.0.1", "AccountsHistory", 9, 13000);
            var final = JsonConvert.DeserializeObject<List<AccountsViewModelcs>>(response);
            return View(final);
        }

        public IActionResult Blocks()
        {
            var response  = Client.Connect("127.0.0.1", "BlockHistory", 9, 13000);
            var final =  JsonConvert.DeserializeObject<List<BlockViewModel>>(response);
            return View(final);
        }

        public IActionResult Transactions()
        {
            var response = Client.Connect("127.0.0.1", "TransactionHistory", 9, 13000);
            var final = JsonConvert.DeserializeObject<List<TransactionViewModel>>(response);
            return View(final);
        }

    }
}