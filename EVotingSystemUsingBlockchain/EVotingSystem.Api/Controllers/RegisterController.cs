using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVotingSystem.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVotingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IDigitalSignatureService digitalSignature;

        public RegisterController(IDigitalSignatureService digitalSignature)
        {
            this.digitalSignature = digitalSignature;
        }

        [HttpPost]
        [Route("GenerateKey")]
        public void GenerateKey()
        {
            digitalSignature.AssignKey();
        }
    }
}