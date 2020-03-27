using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVotingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private static List<string> IPs = new List<string>();

        [HttpGet]
        public List<string> GetNodeIPs()
        {
            var IP = HttpContext.Connection.RemoteIpAddress.ToString();

            IPs.Add(IP);

            return IPs;
        }
    }
}