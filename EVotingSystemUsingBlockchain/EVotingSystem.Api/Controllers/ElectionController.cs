﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EVotingSystem.Api.Controllers
{
    public class ElectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}