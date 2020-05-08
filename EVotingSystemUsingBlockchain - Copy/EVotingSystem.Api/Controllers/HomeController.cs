using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVotingSystem.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("GetResults")]
        [HttpGet]
        public ActionResult<HttpContent> GetResults()
        {
            string[] test = { "10", "12" };
            
            return Ok(test);
        }
    }
}