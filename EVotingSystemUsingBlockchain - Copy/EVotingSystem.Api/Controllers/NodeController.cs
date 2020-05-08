using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVotingSystem.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private static List<string> IPs = new List<string>();

        [HttpPost]
        public List<string> AddIp ([FromBody] string value)
        {
            var IP = HttpContext.Connection.RemoteIpAddress.ToString() + "/" + value;

            if (!IPs.Contains(IP))
            {
                IPs.Add(IP);
            }

            return IPs;
        }
    }
}