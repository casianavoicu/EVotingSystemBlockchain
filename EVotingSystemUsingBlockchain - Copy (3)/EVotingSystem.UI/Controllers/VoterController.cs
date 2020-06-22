using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EVotingSystem.UI.Controllers
{
    public class VoterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Vote()
        {
            return View();
        }
    }
}