using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EVotingSystem.Api.Controllers
{
    public class ElectionController : Controller
    {
        private readonly ILogger<ElectionController> _logger;

        public ElectionController(ILogger<ElectionController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}