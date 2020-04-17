using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EVotingSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EVotingSystem.UI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient EVotingApi;

        public RegisterController(ILogger<RegisterController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
            EVotingApi = httpClientFactory.CreateClient("EVotingSystemApi");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            HttpResponseMessage httpResponse = await EVotingApi.GetAsync("candidate");
            return RedirectToAction("Contact", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(RegisterViewModel model)
        {
            HttpResponseMessage httpResponse = await EVotingApi.GetAsync("candidate");
            return RedirectToAction("Contact", "Home");
        }
    }
}