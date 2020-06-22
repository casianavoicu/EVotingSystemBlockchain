using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EVotingSystem.UI.Controllers
{
    public class VotesController : Controller
    {
        private readonly ILogger<VotesController> _logger;

        public VotesController(ILogger<VotesController> _logger)
        {
            this._logger = _logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}